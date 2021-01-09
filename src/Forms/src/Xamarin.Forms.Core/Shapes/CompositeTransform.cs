namespace Xamarin.Forms.Shapes
{
	public sealed class CompositeTransform : Transform
	{
		public static readonly BindableProperty CenterXProperty =
			BindableProperty.Create(nameof(CenterX), typeof(float), typeof(CompositeTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty CenterYProperty =
			BindableProperty.Create(nameof(CenterY), typeof(float), typeof(CompositeTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty ScaleXProperty =
			BindableProperty.Create(nameof(ScaleX), typeof(float), typeof(CompositeTransform), 1.0, propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty ScaleYProperty =
			BindableProperty.Create(nameof(ScaleY), typeof(float), typeof(CompositeTransform), 1.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty SkewXProperty =
			BindableProperty.Create(nameof(SkewX), typeof(float), typeof(CompositeTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty SkewYProperty =
			BindableProperty.Create(nameof(SkewY), typeof(float), typeof(CompositeTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty RotationProperty =
			BindableProperty.Create(nameof(Rotation), typeof(float), typeof(CompositeTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty TranslateXProperty =
			BindableProperty.Create(nameof(TranslateX), typeof(float), typeof(CompositeTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

		public static readonly BindableProperty TranslateYProperty =
			BindableProperty.Create(nameof(TranslateY), typeof(float), typeof(CompositeTransform), 0.0,
				propertyChanged: OnTransformPropertyChanged);

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

		public float SkewX
		{
			set { SetValue(SkewXProperty, value); }
			get { return (float)GetValue(SkewXProperty); }
		}

		public float SkewY
		{
			set { SetValue(SkewYProperty, value); }
			get { return (float)GetValue(SkewYProperty); }
		}

		public float Rotation
		{
			set { SetValue(RotationProperty, value); }
			get { return (float)GetValue(RotationProperty); }
		}

		public float TranslateX
		{
			set { SetValue(TranslateXProperty, value); }
			get { return (float)GetValue(TranslateXProperty); }
		}

		public float TranslateY
		{
			set { SetValue(TranslateYProperty, value); }
			get { return (float)GetValue(TranslateYProperty); }
		}

		static void OnTransformPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			(bindable as CompositeTransform).OnTransformPropertyChanged();
		}

		void OnTransformPropertyChanged()
		{
			TransformGroup xformGroup = new TransformGroup
			{
				Children =
				{
					new TranslateTransform
					{
						X = -CenterX,
						Y = -CenterY
					},
					new ScaleTransform
					{
						ScaleX = ScaleX,
						ScaleY = ScaleY
					},
					new SkewTransform
					{
						AngleX = SkewX,
						AngleY = SkewY
					},
					new RotateTransform
					{
						Angle = Rotation
					},
					new TranslateTransform
					{
						X = CenterX,
						Y = CenterY
					},
					new TranslateTransform
					{
						X = TranslateX,
						Y = TranslateY
					}
				}
			};

			Value = xformGroup.Value;
		}
	}
}