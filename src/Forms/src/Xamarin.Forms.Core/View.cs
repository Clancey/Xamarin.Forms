using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Internals;
using Xamarin.Platform;
using Xamarin.Platform.Layouts;
using System.Graphics;

namespace Xamarin.Forms
{
	public class View : VisualElement, IView, IViewController, IGestureController, IGestureRecognizers, IPropertyMapperView
	{
		protected internal IGestureController GestureController => this;

		public static readonly BindableProperty VerticalOptionsProperty =
			BindableProperty.Create(nameof(VerticalOptions), typeof(LayoutOptions), typeof(View), LayoutOptions.Fill,
									propertyChanged: (bindable, oldvalue, newvalue) =>
									((View)bindable).InvalidateMeasureInternal(InvalidationTrigger.VerticalOptionsChanged));

		public static readonly BindableProperty HorizontalOptionsProperty =
			BindableProperty.Create(nameof(HorizontalOptions), typeof(LayoutOptions), typeof(View), LayoutOptions.Fill,
									propertyChanged: (bindable, oldvalue, newvalue) =>
									((View)bindable).InvalidateMeasureInternal(InvalidationTrigger.HorizontalOptionsChanged));

		public static readonly BindableProperty MarginProperty =
			BindableProperty.Create(nameof(Margin), typeof(Thickness), typeof(View), default(Thickness),
									propertyChanged: MarginPropertyChanged);

		internal static readonly BindableProperty MarginLeftProperty =
			BindableProperty.Create("MarginLeft", typeof(float), typeof(View), default(float),
									propertyChanged: OnMarginLeftPropertyChanged);

		static void OnMarginLeftPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var margin = (Thickness)bindable.GetValue(MarginProperty);
			margin.Left = (float)newValue;
			bindable.SetValue(MarginProperty, margin);
		}

		internal static readonly BindableProperty MarginTopProperty =
			BindableProperty.Create("MarginTop", typeof(float), typeof(View), default(float),
									propertyChanged: OnMarginTopPropertyChanged);

		static void OnMarginTopPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var margin = (Thickness)bindable.GetValue(MarginProperty);
			margin.Top = (float)newValue;
			bindable.SetValue(MarginProperty, margin);
		}

		internal static readonly BindableProperty MarginRightProperty =
			BindableProperty.Create("MarginRight", typeof(float), typeof(View), default(float),
									propertyChanged: OnMarginRightPropertyChanged);

		static void OnMarginRightPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var margin = (Thickness)bindable.GetValue(MarginProperty);
			margin.Right = (float)newValue;
			bindable.SetValue(MarginProperty, margin);
		}

		internal static readonly BindableProperty MarginBottomProperty =
			BindableProperty.Create("MarginBottom", typeof(float), typeof(View), default(float),
									propertyChanged: OnMarginBottomPropertyChanged);


		static void OnMarginBottomPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var margin = (Thickness)bindable.GetValue(MarginProperty);
			margin.Bottom = (float)newValue;
			bindable.SetValue(MarginProperty, margin);
		}

		readonly ObservableCollection<IGestureRecognizer> _gestureRecognizers = new ObservableCollection<IGestureRecognizer>();

		protected internal View()
		{
			_gestureRecognizers.CollectionChanged += (sender, args) =>
			{
				void AddItems()
				{
					foreach (IElement item in args.NewItems.OfType<IElement>())
					{
						ValidateGesture(item as IGestureRecognizer);
						item.Parent = this;
						GestureController.CompositeGestureRecognizers.Add(item as IGestureRecognizer);
					}
				}

				void RemoveItems()
				{
					foreach (IElement item in args.OldItems.OfType<IElement>())
					{
						item.Parent = null;
						GestureController.CompositeGestureRecognizers.Remove(item as IGestureRecognizer);
					}
				}

				switch (args.Action)
				{
					case NotifyCollectionChangedAction.Add:
						AddItems();
						break;
					case NotifyCollectionChangedAction.Remove:
						RemoveItems();
						break;
					case NotifyCollectionChangedAction.Replace:
						AddItems();
						RemoveItems();
						break;
					case NotifyCollectionChangedAction.Reset:
						foreach (IElement item in _gestureRecognizers.OfType<IElement>())
							item.Parent = this;
						foreach (IElement item in GestureController.CompositeGestureRecognizers.OfType<IElement>())
							item.Parent = this;
						break;
				}
			};
		}

		public IList<IGestureRecognizer> GestureRecognizers
		{
			get { return _gestureRecognizers; }
		}

		ObservableCollection<IGestureRecognizer> _compositeGestureRecognizers;

		IList<IGestureRecognizer> IGestureController.CompositeGestureRecognizers
		{
			get { return _compositeGestureRecognizers ?? (_compositeGestureRecognizers = new ObservableCollection<IGestureRecognizer>()); }
		}

		public virtual IList<GestureElement> GetChildElements(PointF point)
		{
			return null;
		}

		public LayoutOptions HorizontalOptions
		{
			get { return (LayoutOptions)GetValue(HorizontalOptionsProperty); }
			set { SetValue(HorizontalOptionsProperty, value); }
		}

		public Thickness Margin
		{
			get { return (Thickness)GetValue(MarginProperty); }
			set { SetValue(MarginProperty, value); }
		}

		public LayoutOptions VerticalOptions
		{
			get { return (LayoutOptions)GetValue(VerticalOptionsProperty); }
			set { SetValue(VerticalOptionsProperty, value); }
		}

		protected override void OnBindingContextChanged()
		{
			this.PropagateBindingContext(GestureRecognizers);
			base.OnBindingContextChanged();
		}

		static void MarginPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((View)bindable).InvalidateMeasureInternal(InvalidationTrigger.MarginChanged);
		}

		void ValidateGesture(IGestureRecognizer gesture)
		{
			if (gesture == null)
				return;
			if (gesture is PinchGestureRecognizer && _gestureRecognizers.GetGesturesFor<PinchGestureRecognizer>().Count() > 1)
				throw new InvalidOperationException($"Only one {nameof(PinchGestureRecognizer)} per view is allowed");
		}

		#region IView

		RectangleF IFrameworkElement.Frame => Bounds;

		public IViewHandler Handler { get; set; }

		protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			base.OnPropertyChanged(propertyName);
			(Handler)?.UpdateValue(propertyName);
		}

		IFrameworkElement IFrameworkElement.Parent => Parent as IView;

		public SizeF DesiredSize { get; protected set; }

		public bool IsMeasureValid { get; protected set; }

		public bool IsArrangeValid { get; protected set; }

		public void Arrange(RectangleF bounds)
		{
			Layout(bounds);
		}

		void IFrameworkElement.Arrange(RectangleF bounds) 
		{
			if (IsArrangeValid)
				return;
			IsArrangeValid = true;
			Layout(this.ComputeFrame(bounds));
		}

		protected override void OnSizeAllocated(float width, float height)
		{
			base.OnSizeAllocated(width, height);
			Handler?.SetFrame(Bounds);
		}

		SizeF IFrameworkElement.Measure(float widthConstraint, float heightConstraint)
		{
			if (!IsMeasureValid)
			{
				DesiredSize = this.ComputeDesiredSize(widthConstraint, heightConstraint);
			}

			IsMeasureValid = true;
			return DesiredSize;
		}

		void IFrameworkElement.InvalidateMeasure()
		{
			IsMeasureValid = false;
			IsArrangeValid = false;
			InvalidateMeasure();
		}

		void IFrameworkElement.InvalidateArrange()
		{
			IsArrangeValid = false;
		}

		protected PropertyMapper propertyMapper;

		protected PropertyMapper<T> GetRendererOverides<T>() where T : IView => (PropertyMapper<T>)(propertyMapper as PropertyMapper<T> ?? (propertyMapper = new PropertyMapper<T>()));
		PropertyMapper IPropertyMapperView.GetPropertyMapperOverrides() => propertyMapper;

		float IFrameworkElement.Width { get => WidthRequest; }
		float IFrameworkElement.Height { get => HeightRequest; }

		#endregion
	}
}