using System;

namespace Xamarin.Forms.Shapes
{
	public class RotateTransform : Transform
	{
		public RotateTransform()
		{

		}

		public RotateTransform(float angle)
		{
			Angle = angle;
		}

		public RotateTransform(float angle, float centerX, float centerY)
		{
			Angle = angle;
			CenterX = centerX;
			CenterY = centerY;
		}

		public static readonly BindableProperty AngleProperty =
			BindableProperty.Create(nameof(Angle), typeof(float), typeof(RotateTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty CenterXProperty =
			BindableProperty.Create(nameof(CenterX), typeof(float), typeof(RotateTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty CenterYProperty =
			BindableProperty.Create(nameof(CenterY), typeof(float), typeof(RotateTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public float Angle
		{
			set { SetValue(AngleProperty, value); }
			get { return (float)GetValue(AngleProperty); }
		}

		public float CenterX
		{
			set { SetValue(CenterXProperty, value); }
			get { return (float)GetValue(CenterXProperty); }
		}

		public float CenterY
		{
			set { SetValue(CenterYProperty, value); }
			get { return (float)GetValue(CenterYProperty); }
		}

		static void OnTransformPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			(bindable as RotateTransform).OnTransformPropertyChanged();
		}

		void OnTransformPropertyChanged()
		{
			float radians = (float)Math.PI * Angle / 180;
			float sin = (float)Math.Sin(radians);
			float cos = (float)Math.Cos(radians);

			Value = new Matrix(cos, sin, -sin, cos, CenterX * (1 - cos) + CenterY * sin, CenterY * (1 - cos) - CenterX * sin);
		}
	}
}