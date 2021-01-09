using System.Graphics;
using Xamarin.Forms;

namespace Xamarin.Platform
{
	public interface IFrameworkElement
	{
		bool IsEnabled { get; }
		Color BackgroundColor { get; }
		RectangleF Frame { get; }
		IViewHandler? Handler { get; set; }
		IFrameworkElement? Parent { get; }

		void Arrange(RectangleF bounds);
		SizeF Measure(float widthConstraint, float heightConstraint);

		SizeF DesiredSize { get; }
		bool IsMeasureValid { get; }
		bool IsArrangeValid { get; }

		void InvalidateMeasure();
		void InvalidateArrange();

		float Width { get; }
		float Height { get; }

		Thickness Margin { get; }
	}
}