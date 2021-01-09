using System.Graphics;

namespace Xamarin.Forms.Shapes
{
	public class LineGeometry : Geometry
	{
		public LineGeometry()
		{

		}

		public LineGeometry(PointF startPoint, PointF endPoint)
		{
			StartPoint = startPoint;
			EndPoint = endPoint;
		}

		public static readonly BindableProperty StartPointProperty =
			BindableProperty.Create(nameof(StartPoint), typeof(PointF), typeof(LineGeometry), new PointF());

		public static readonly BindableProperty EndPointProperty =
			BindableProperty.Create(nameof(EndPoint), typeof(PointF), typeof(LineGeometry), new PointF());

		public PointF StartPoint
		{
			set { SetValue(StartPointProperty, value); }
			get { return (PointF)GetValue(StartPointProperty); }
		}

		public PointF EndPoint
		{
			set { SetValue(EndPointProperty, value); }
			get { return (PointF)GetValue(EndPointProperty); }
		}
	}
}