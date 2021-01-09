using System.Graphics;

namespace Xamarin.Forms.Shapes
{
	public class ArcSegment : PathSegment
	{
		public ArcSegment()
		{

		}

		public ArcSegment(PointF point, SizeF size, float rotationAngle, SweepDirection sweepDirection, bool isLargeArc)
		{
			Point = point;
			Size = size;
			RotationAngle = rotationAngle;
			SweepDirection = sweepDirection;
			IsLargeArc = isLargeArc;
		}

		public static readonly BindableProperty PointProperty =
			BindableProperty.Create(nameof(PointF), typeof(PointF), typeof(ArcSegment), new PointF(0, 0));

		public static readonly BindableProperty SizeProperty =
			BindableProperty.Create(nameof(Size), typeof(SizeF), typeof(ArcSegment), new SizeF(0, 0));

		public static readonly BindableProperty RotationAngleProperty =
			BindableProperty.Create(nameof(RotationAngle), typeof(float), typeof(ArcSegment), 0.0);

		public static readonly BindableProperty SweepDirectionProperty =
			BindableProperty.Create(nameof(SweepDirection), typeof(SweepDirection), typeof(ArcSegment), SweepDirection.CounterClockwise);

		public static readonly BindableProperty IsLargeArcProperty =
			BindableProperty.Create(nameof(IsLargeArc), typeof(bool), typeof(ArcSegment), false);

		public PointF Point
		{
			set { SetValue(PointProperty, value); }
			get { return (PointF)GetValue(PointProperty); }
		}

		[TypeConverter(typeof(SizeTypeConverter))]
		public SizeF Size
		{
			set { SetValue(SizeProperty, value); }
			get { return (SizeF)GetValue(SizeProperty); }
		}

		public float RotationAngle
		{
			set { SetValue(RotationAngleProperty, value); }
			get { return (float)GetValue(RotationAngleProperty); }
		}

		public SweepDirection SweepDirection
		{
			set { SetValue(SweepDirectionProperty, value); }
			get { return (SweepDirection)GetValue(SweepDirectionProperty); }
		}

		public bool IsLargeArc
		{
			set { SetValue(IsLargeArcProperty, value); }
			get { return (bool)GetValue(IsLargeArcProperty); }
		}
	}
}
