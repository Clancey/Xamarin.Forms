namespace Xamarin.Forms.Shapes
{
	public class ScaleTransform : Transform
	{
		public ScaleTransform()
		{

		}

		public ScaleTransform(float scaleX, float scaleY)
		{
			ScaleX = scaleX;
			ScaleY = scaleY;
		}

		public ScaleTransform(float scaleX, float scaleY, float centerX, float centerY)
		{
			ScaleX = scaleX;
			ScaleY = scaleY;
			CenterX = centerX;
			CenterY = centerY;
		}

		public static readonly BindableProperty ScaleXProperty =
			BindableProperty.Create(nameof(ScaleX), typeof(float), typeof(ScaleTransform), 1.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty ScaleYProperty =
			BindableProperty.Create(nameof(ScaleY), typeof(float), typeof(ScaleTransform), 1.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty CenterXProperty =
			BindableProperty.Create(nameof(CenterX), typeof(float), typeof(ScaleTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty CenterYProperty =
			BindableProperty.Create(nameof(CenterY), typeof(float), typeof(ScaleTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public float ScaleX
		{
			set { SetValue(ScaleXProperty, value); }
			get { return (float)GetValue(ScaleXProperty); }
		}

		public float ScaleY
		{
			set { SetValue(ScaleYProperty, value); }
			get { return (float)GetValue(ScaleYProperty); }
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
			(bindable as ScaleTransform).OnTransformPropertyChanged();
		}

		void OnTransformPropertyChanged()
		{
			Value = new Matrix(ScaleX, 0, 0, ScaleY, CenterX * (1 - ScaleX), CenterY * (1 - ScaleY));
		}
	}
}