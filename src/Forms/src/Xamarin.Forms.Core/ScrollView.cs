using System;
using System.ComponentModel;
using System.Graphics;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms
{
	[ContentProperty("Content")]
	public class ScrollView : Layout, IScrollViewController, IElementConfiguration<ScrollView>, IFlowDirectionController
	{
		#region IScrollViewController

		[EditorBrowsable(EditorBrowsableState.Never)]
		public RectangleF LayoutAreaOverride
		{
			get => _layoutAreaOverride;
			set
			{
				if (_layoutAreaOverride == value)
					return;
				_layoutAreaOverride = value;
				// Dont invalidate here, we can relayout immediately since this only impacts our innards
				UpdateChildrenLayout();
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public event EventHandler<ScrollToRequestedEventArgs> ScrollToRequested;

		[EditorBrowsable(EditorBrowsableState.Never)]
		public PointF GetScrollPositionForElement(VisualElement item, ScrollToPosition pos)
		{
			ScrollToPosition position = pos;
			float y = GetCoordinate(item, "Y", 0);
			float x = GetCoordinate(item, "X", 0);

			if (position == ScrollToPosition.MakeVisible)
			{
				var scrollBounds = new RectangleF(ScrollX, ScrollY, Width, Height);
				var itemBounds = new RectangleF(x, y, item.Width, item.Height);
				if (scrollBounds.Contains(itemBounds))
					return new PointF(ScrollX, ScrollY);
				switch (Orientation)
				{
					case ScrollOrientation.Vertical:
						position = y > ScrollY ? ScrollToPosition.End : ScrollToPosition.Start;
						break;
					case ScrollOrientation.Horizontal:
						position = x > ScrollX ? ScrollToPosition.End : ScrollToPosition.Start;
						break;
					case ScrollOrientation.Both:
						position = x > ScrollX || y > ScrollY ? ScrollToPosition.End : ScrollToPosition.Start;
						break;
				}
			}
			switch (position)
			{
				case ScrollToPosition.Center:
					y = y - Height / 2 + item.Height / 2;
					x = x - Width / 2 + item.Width / 2;
					break;
				case ScrollToPosition.End:
					y = y - Height + item.Height;
					x = x - Width + item.Width;
					break;
			}
			return new PointF(x, y);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendScrollFinished()
		{
			if (_scrollCompletionSource != null)
				_scrollCompletionSource.TrySetResult(true);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SetScrolledPosition(float x, float y)
		{
			if (ScrollX == x && ScrollY == y)
				return;

			ScrollX = x;
			ScrollY = y;

			Scrolled?.Invoke(this, new ScrolledEventArgs(x, y));
		}

		#endregion IScrollViewController

		public static readonly BindableProperty OrientationProperty = BindableProperty.Create("Orientation", typeof(ScrollOrientation), typeof(ScrollView), ScrollOrientation.Vertical);

		static readonly BindablePropertyKey ScrollXPropertyKey = BindableProperty.CreateReadOnly("ScrollX", typeof(float), typeof(ScrollView), 0d);

		public static readonly BindableProperty ScrollXProperty = ScrollXPropertyKey.BindableProperty;

		static readonly BindablePropertyKey ScrollYPropertyKey = BindableProperty.CreateReadOnly("ScrollY", typeof(float), typeof(ScrollView), 0d);

		public static readonly BindableProperty ScrollYProperty = ScrollYPropertyKey.BindableProperty;

		static readonly BindablePropertyKey ContentSizePropertyKey = BindableProperty.CreateReadOnly("ContentSize", typeof(SizeF), typeof(ScrollView), default(SizeF));

		public static readonly BindableProperty ContentSizeProperty = ContentSizePropertyKey.BindableProperty;

		readonly Lazy<PlatformConfigurationRegistry<ScrollView>> _platformConfigurationRegistry;

		public static readonly BindableProperty HorizontalScrollBarVisibilityProperty = BindableProperty.Create(nameof(HorizontalScrollBarVisibility), typeof(ScrollBarVisibility), typeof(ScrollView), ScrollBarVisibility.Default);

		public static readonly BindableProperty VerticalScrollBarVisibilityProperty = BindableProperty.Create(nameof(VerticalScrollBarVisibility), typeof(ScrollBarVisibility), typeof(ScrollView), ScrollBarVisibility.Default);

		View _content;
		TaskCompletionSource<bool> _scrollCompletionSource;
		RectangleF _layoutAreaOverride;

		public View Content
		{
			get { return _content; }
			set
			{
				if (_content == value)
					return;

				OnPropertyChanging();
				if (_content != null)
					InternalChildren.Remove(_content);
				_content = value;
				if (_content != null)
					InternalChildren.Add(_content);
				OnPropertyChanged();
			}
		}

		public SizeF ContentSize
		{
			get { return (SizeF)GetValue(ContentSizeProperty); }
			private set { SetValue(ContentSizePropertyKey, value); }
		}

		public ScrollOrientation Orientation
		{
			get { return (ScrollOrientation)GetValue(OrientationProperty); }
			set { SetValue(OrientationProperty, value); }
		}

		public float ScrollX
		{
			get { return (float)GetValue(ScrollXProperty); }
			private set { SetValue(ScrollXPropertyKey, value); }
		}

		public float ScrollY
		{
			get { return (float)GetValue(ScrollYProperty); }
			private set { SetValue(ScrollYPropertyKey, value); }
		}

		public ScrollBarVisibility HorizontalScrollBarVisibility
		{
			get { return (ScrollBarVisibility)GetValue(HorizontalScrollBarVisibilityProperty); }
			set { SetValue(HorizontalScrollBarVisibilityProperty, value); }
		}

		public ScrollBarVisibility VerticalScrollBarVisibility
		{
			get { return (ScrollBarVisibility)GetValue(VerticalScrollBarVisibilityProperty); }
			set { SetValue(VerticalScrollBarVisibilityProperty, value); }
		}

		public ScrollView()
		{
			_platformConfigurationRegistry = new Lazy<PlatformConfigurationRegistry<ScrollView>>(() => new PlatformConfigurationRegistry<ScrollView>(this));
		}

		public event EventHandler<ScrolledEventArgs> Scrolled;

		public IPlatformElementConfiguration<T, ScrollView> On<T>() where T : IConfigPlatform
		{
			return _platformConfigurationRegistry.Value.On<T>();
		}

		public Task ScrollToAsync(float x, float y, bool animated)
		{
			if (Orientation == ScrollOrientation.Neither)
				return Task.FromResult(false);

			var args = new ScrollToRequestedEventArgs(x, y, animated);
			OnScrollToRequested(args);
			return _scrollCompletionSource.Task;
		}

		public Task ScrollToAsync(Element element, ScrollToPosition position, bool animated)
		{
			if (Orientation == ScrollOrientation.Neither)
				return Task.FromResult(false);

			if (!Enum.IsDefined(typeof(ScrollToPosition), position))
				throw new ArgumentException("position is not a valid ScrollToPosition", "position");

			if (element == null)
				throw new ArgumentNullException("element");

			if (!CheckElementBelongsToScrollViewer(element))
				throw new ArgumentException("element does not belong to this ScrollView", "element");

			var args = new ScrollToRequestedEventArgs(element, position, animated);
			OnScrollToRequested(args);
			return _scrollCompletionSource.Task;
		}

		bool IFlowDirectionController.ApplyEffectiveFlowDirectionToChildContainer => false;

		protected override void LayoutChildren(float x, float y, float width, float height)
		{
			var over = ((IScrollViewController)this).LayoutAreaOverride;
			if (!over.IsEmpty)
			{
				x = over.X + Padding.Left;
				y = over.Y + Padding.Top;
				width = over.Width - Padding.HorizontalThickness;
				height = over.Height - Padding.VerticalThickness;
			}

			if (_content != null)
			{
				SizeRequest size;
				switch (Orientation)
				{
					case ScrollOrientation.Horizontal:
						size = _content.Measure(float.PositiveInfinity, height, MeasureFlags.IncludeMargins);
						LayoutChildIntoBoundingRegion(_content, new RectangleF(x, y, GetMaxWidth(width, size), height));
						ContentSize = new SizeF(GetMaxWidth(width), height);
						break;
					case ScrollOrientation.Vertical:
						size = _content.Measure(width, float.PositiveInfinity, MeasureFlags.IncludeMargins);
						LayoutChildIntoBoundingRegion(_content, new RectangleF(x, y, width, GetMaxHeight(height, size)));
						ContentSize = new SizeF(width, GetMaxHeight(height));
						break;
					case ScrollOrientation.Both:
						size = _content.Measure(float.PositiveInfinity, float.PositiveInfinity, MeasureFlags.IncludeMargins);
						LayoutChildIntoBoundingRegion(_content, new RectangleF(x, y, GetMaxWidth(width, size), GetMaxHeight(height, size)));
						ContentSize = new SizeF(GetMaxWidth(width), GetMaxHeight(height));
						break;
					case ScrollOrientation.Neither:
						LayoutChildIntoBoundingRegion(_content, new RectangleF(x, y, width, height));
						ContentSize = new SizeF(width, height);
						break;
				}
			}
		}

		[Obsolete("OnSizeRequest is obsolete as of version 2.2.0. Please use OnMeasure instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override SizeRequest OnSizeRequest(float widthConstraint, float heightConstraint)
		{
			if (Content == null)
				return new SizeRequest();

			switch (Orientation)
			{
				case ScrollOrientation.Horizontal:
					widthConstraint = float.PositiveInfinity;
					break;
				case ScrollOrientation.Vertical:
					heightConstraint = float.PositiveInfinity;
					break;
				case ScrollOrientation.Both:
					widthConstraint = float.PositiveInfinity;
					heightConstraint = float.PositiveInfinity;
					break;
				case ScrollOrientation.Neither:
					widthConstraint = Width;
					heightConstraint = Height;
					break;
			}

			SizeRequest contentRequest = Content.Measure(widthConstraint, heightConstraint, MeasureFlags.IncludeMargins);
			contentRequest.Minimum = new SizeF(Math.Min(40, contentRequest.Minimum.Width), Math.Min(40, contentRequest.Minimum.Height));
			return contentRequest;
		}

		internal override void ComputeConstraintForView(View view)
		{
			switch (Orientation)
			{
				case ScrollOrientation.Horizontal:
					LayoutOptions vOptions = view.VerticalOptions;
					if (vOptions.Alignment == LayoutAlignment.Fill && (Constraint & LayoutConstraint.VerticallyFixed) != 0)
					{
						view.ComputedConstraint = LayoutConstraint.VerticallyFixed;
					}
					break;
				case ScrollOrientation.Vertical:
					LayoutOptions hOptions = view.HorizontalOptions;
					if (hOptions.Alignment == LayoutAlignment.Fill && (Constraint & LayoutConstraint.HorizontallyFixed) != 0)
					{
						view.ComputedConstraint = LayoutConstraint.HorizontallyFixed;
					}
					break;
				case ScrollOrientation.Both:
					view.ComputedConstraint = LayoutConstraint.None;
					break;
			}
		}

		bool CheckElementBelongsToScrollViewer(Element element)
		{
			return Equals(element, this) || element.RealParent != null && CheckElementBelongsToScrollViewer(element.RealParent);
		}

		void CheckTaskCompletionSource()
		{
			if (_scrollCompletionSource != null && _scrollCompletionSource.Task.Status == TaskStatus.Running)
			{
				_scrollCompletionSource.TrySetCanceled();
			}
			_scrollCompletionSource = new TaskCompletionSource<bool>();
		}

		float GetCoordinate(Element item, string coordinateName, float coordinate)
		{
			if (item == this)
				return coordinate;
			coordinate += (float)typeof(VisualElement).GetProperty(coordinateName).GetValue(item, null);
			var visualParentElement = item.RealParent as VisualElement;
			return visualParentElement != null ? GetCoordinate(visualParentElement, coordinateName, coordinate) : coordinate;
		}

		float GetMaxHeight(float height)
		{
			return Math.Max(height, _content.Bounds.Top + Padding.Top + _content.Bounds.Bottom + Padding.Bottom);
		}

		static float GetMaxHeight(float height, SizeRequest size)
		{
			return Math.Max(size.Request.Height, height);
		}

		float GetMaxWidth(float width)
		{
			return Math.Max(width, _content.Bounds.Left + Padding.Left + _content.Bounds.Right + Padding.Right);
		}

		static float GetMaxWidth(float width, SizeRequest size)
		{
			return Math.Max(size.Request.Width, width);
		}

		void OnScrollToRequested(ScrollToRequestedEventArgs e)
		{
			CheckTaskCompletionSource();
			ScrollToRequested?.Invoke(this, e);
		}
	}
}
