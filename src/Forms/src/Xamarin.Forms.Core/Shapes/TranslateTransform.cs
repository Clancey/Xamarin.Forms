namespace Xamarin.Forms.Shapes
{
	public class TranslateTransform : Transform
	{
		public TranslateTransform()
		{

		}

		public TranslateTransform(float x, float y)
		{
			X = x;
			Y = y;
		}

		public static readonly BindableProperty XProperty =
			BindableProperty.Create(nameof(X), typeof(float), typeof(TranslateTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty YProperty =
			BindableProperty.Create(nameof(Y), typeof(float), typeof(TranslateTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public float X
		{
			set { SetValue(XProperty, value); }
			get { return (float)GetValue(XProperty); }
		}

		public float Y
		{
			set { SetValue(YProperty, value); }
			get { return (float)GetValue(YProperty); }
		}

		static void OnTransformPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			(bindable as TranslateTransform).OnTransformPropertyChanged();
		}

		void OnTransformPropertyChanged()
		{
			Value = new Matrix(1, 0, 0, 1, X, Y);
		}
	}
}