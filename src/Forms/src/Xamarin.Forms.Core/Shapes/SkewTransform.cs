using System;

namespace Xamarin.Forms.Shapes
{
	public class SkewTransform : Transform
	{
		public SkewTransform()
		{

		}

		public SkewTransform(float angleX, float angleY)
		{
			AngleX = angleX;
			AngleY = angleY;
		}

		public SkewTransform(float angleX, float angleY, float centerX, float centerY)
		{
			AngleX = angleX;
			AngleY = angleY;
			CenterX = centerX;
			CenterY = centerY;
		}

		public static readonly BindableProperty AngleXProperty =
			BindableProperty.Create(nameof(AngleX), typeof(float), typeof(SkewTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty AngleYProperty =
			BindableProperty.Create(nameof(AngleY), typeof(float), typeof(SkewTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty CenterXProperty =
			BindableProperty.Create(nameof(CenterX), typeof(float), typeof(SkewTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty CenterYProperty =
			BindableProperty.Create(nameof(CenterY), typeof(float), typeof(SkewTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public float AngleX
		{
			set { SetValue(AngleXProperty, value); }
			get { return (float)GetValue(AngleXProperty); }
		}

		public float AngleY
		{
			set { SetValue(AngleYProperty, value); }
			get { return (float)GetValue(AngleYProperty); }
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
			(bindable as SkewTransform).OnTransformPropertyChanged();
		}

		void OnTransformPropertyChanged()
		{
			float radiansX = (float)Math.PI * AngleX / 180;
			float radiansY = (float)Math.PI * AngleY / 180;
			float tanX = (float)Math.Tan(radiansX);
			float tanY = (float)Math.Tan(radiansY);

			Value = new Matrix(1, tanY, tanX, 1, -CenterY * tanX, -CenterX * tanY);
		}
	}
}