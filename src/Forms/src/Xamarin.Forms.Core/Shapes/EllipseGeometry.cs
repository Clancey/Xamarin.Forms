using System.Graphics;

namespace Xamarin.Forms.Shapes
{
	public class EllipseGeometry : Geometry
	{
		public EllipseGeometry()
		{

		}

		public EllipseGeometry(PointF center, float radiusX, float radiusY)
		{
			Center = center;
			RadiusX = radiusX;
			RadiusY = radiusY;
		}

		public static readonly BindableProperty CenterProperty =
			BindableProperty.Create(nameof(Center), typeof(PointF), typeof(EllipseGeometry), new PointF());

		public static readonly BindableProperty RadiusXProperty =
			BindableProperty.Create(nameof(RadiusX), typeof(double), typeof(EllipseGeometry), 0.0);

		public static readonly BindableProperty RadiusYProperty =
			BindableProperty.Create(nameof(RadiusY), typeof(double), typeof(EllipseGeometry), 0.0);

		public PointF Center
		{
			set { SetValue(CenterProperty, value); }
			get { return (PointF)GetValue(CenterProperty); }
		}

		public float RadiusX
		{
			set { SetValue(RadiusXProperty, value); }
			get { return (float)GetValue(RadiusXProperty); }
		}

		public float RadiusY
		{
			set { SetValue(RadiusYProperty, value); }
			get { return (float)GetValue(RadiusYProperty); }
		}
	}
}