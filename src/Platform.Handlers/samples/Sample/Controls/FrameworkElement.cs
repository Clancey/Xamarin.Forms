using System.Graphics;
using Xamarin.Forms;
using Xamarin.Platform;
using Xamarin.Platform.Layouts;

namespace Sample
{
	public abstract class FrameworkElement : IFrameworkElement
	{
		public bool IsEnabled => true;

		public Color BackgroundColor { get; set; } = Colors.Transparent;

		public RectangleF Frame
		{
			get;
			protected set;
		}

		public IViewHandler Handler { get; set; }

		public IFrameworkElement Parent { get; set; }

		public SizeF DesiredSize { get; protected set; }

		public virtual bool IsMeasureValid { get; protected set; }

		public bool IsArrangeValid { get; protected set; }

		public float Width { get; set; } = -1;
		public float Height { get; set; } = -1;

		public Thickness Margin { get; set; } = Thickness.Zero;

		public virtual void Arrange(RectangleF bounds)
		{
			Frame = this.ComputeFrame(bounds);
		}

		public void InvalidateMeasure()
		{
			IsMeasureValid = false;
			IsArrangeValid = false;
		}

		public void InvalidateArrange()
		{
			IsArrangeValid = false;
		}

		public virtual SizeF Measure(float widthConstraint, float heightConstraint)
		{
			if (!IsMeasureValid)
			{
				DesiredSize = this.ComputeDesiredSize(widthConstraint, heightConstraint);
			}

			IsMeasureValid = true;
			return DesiredSize;
		}
	}
}
