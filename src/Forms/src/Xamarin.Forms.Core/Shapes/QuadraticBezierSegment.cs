using System.Graphics;

namespace Xamarin.Forms.Shapes
{
	public class QuadraticBezierSegment : PathSegment
	{
		public QuadraticBezierSegment()
		{

		}

		public QuadraticBezierSegment(PointF point1, PointF point2)
		{
			Point1 = point1;
			Point2 = point2;
		}

		public static readonly BindableProperty Point1Property =
			BindableProperty.Create(nameof(Point1), typeof(PointF), typeof(QuadraticBezierSegment), new PointF(0, 0));

		public static readonly BindableProperty Point2Property =
			BindableProperty.Create(nameof(Point2), typeof(PointF), typeof(QuadraticBezierSegment), new PointF(0, 0));

		public PointF Point1
		{
			set { SetValue(Point1Property, value); }
			get { return (PointF)GetValue(Point1Property); }
		}

		public PointF Point2
		{
			set { SetValue(Point2Property, value); }
			get { return (PointF)GetValue(Point2Property); }
		}
	}
}