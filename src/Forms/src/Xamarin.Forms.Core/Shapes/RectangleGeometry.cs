using System.Graphics;

namespace Xamarin.Forms.Shapes
{
	public class RectangleGeometry : Geometry
	{
		public RectangleGeometry()
		{

		}

		public RectangleGeometry(RectangleF rect)
		{
			Rect = rect;
		}

		public static readonly BindableProperty RectProperty =
			BindableProperty.Create(nameof(Rect), typeof(RectangleF), typeof(RectangleGeometry), new RectangleF());

		public RectangleF Rect
		{
			set { SetValue(RectProperty, value); }
			get { return (RectangleF)GetValue(RectProperty); }
		}
	}
}