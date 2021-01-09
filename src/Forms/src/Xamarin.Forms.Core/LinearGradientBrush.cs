using System.Graphics;

namespace Xamarin.Forms
{
	public class LinearGradientBrush : GradientBrush
	{
		public LinearGradientBrush()
		{

		}

		public LinearGradientBrush(GradientStopCollection gradientStops)
		{
			GradientStops = gradientStops;
		}

		public LinearGradientBrush(GradientStopCollection gradientStops, PointF startPoint, PointF endPoint)
		{
			GradientStops = gradientStops;
			StartPoint = startPoint;
			EndPoint = endPoint;
		}

		public override bool IsEmpty
		{
			get
			{
				var linearGradientBrush = this;
				return linearGradientBrush == null || linearGradientBrush.GradientStops.Count == 0;
			}
		}

		public static readonly BindableProperty StartPointProperty = BindableProperty.Create(
			nameof(StartPoint), typeof(PointF), typeof(LinearGradientBrush), new PointF(0, 0));

		public PointF StartPoint
		{
			get => (PointF)GetValue(StartPointProperty);
			set => SetValue(StartPointProperty, value);
		}

		public static readonly BindableProperty EndPointProperty = BindableProperty.Create(
			nameof(EndPoint), typeof(PointF), typeof(LinearGradientBrush), new PointF(1, 1));

		public PointF EndPoint
		{
			get => (PointF)GetValue(EndPointProperty);
			set => SetValue(EndPointProperty, value);
		}
	}
}