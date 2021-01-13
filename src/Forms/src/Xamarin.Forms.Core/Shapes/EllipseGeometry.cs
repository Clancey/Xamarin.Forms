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
			BindableProperty.Create(nameof(RadiusX), typeof(float), typeof(EllipseGeometry), 0f);

		public static readonly BindableProperty RadiusYProperty =
			BindableProperty.Create(nameof(RadiusY), typeof(float), typeof(EllipseGeometry), 0f);

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