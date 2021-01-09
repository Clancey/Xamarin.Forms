using System.Graphics;

namespace Xamarin.Forms.Shapes
{
	public class LineSegment : PathSegment
	{
		public LineSegment()
		{

		}

		public LineSegment(PointF point)
		{
			Point = point;
		}

		public static readonly BindableProperty PointProperty =
			BindableProperty.Create(nameof(PointF), typeof(PointF), typeof(LineSegment), new PointF(0, 0));

		public PointF Point
		{
			set { SetValue(PointProperty, value); }
			get { return (PointF)GetValue(PointProperty); }
		}
	}
}