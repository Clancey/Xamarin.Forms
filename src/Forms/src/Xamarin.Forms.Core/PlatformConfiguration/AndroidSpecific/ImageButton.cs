namespace Xamarin.Forms.PlatformConfiguration.AndroidSpecific
{
	using System.Graphics;
	using FormsImageButton = Forms.ImageButton;

	public static class ImageButton
	{
		#region Shadow
		public static readonly BindableProperty IsShadowEnabledProperty = BindableProperty.Create("IsShadowEnabled", typeof(bool), typeof(Forms.ImageButton), false);

		public static bool GetIsShadowEnabled(BindableObject element)
		{
			return (bool)element.GetValue(IsShadowEnabledProperty);
		}

		public static void SetIsShadowEnabled(BindableObject element, bool value)
		{
			element.SetValue(IsShadowEnabledProperty, value);
		}

		public static bool GetIsShadowEnabled(this IPlatformElementConfiguration<Android, FormsImageButton> config)
		{
			return GetIsShadowEnabled(config.Element);
		}

		public static IPlatformElementConfiguration<Android, FormsImageButton> SetIsShadowEnabled(this IPlatformElementConfiguration<Android, FormsImageButton> config, bool value)
		{
			SetIsShadowEnabled(config.Element, value);
			return config;
		}

		public static readonly BindableProperty ShadowColorProperty = BindableProperty.Create("ShadowColor", typeof(Color), typeof(ImageButton), null);

		public static Color GetShadowColor(BindableObject element)
		{
			return (Color)element.GetValue(ShadowColorProperty);
		}

		public static void SetShadowColor(BindableObject element, Color value)
		{
			element.SetValue(ShadowColorProperty, value);
		}

		public static Color GetShadowColor(this IPlatformElementConfiguration<Android, FormsImageButton> config)
		{
			return GetShadowColor(config.Element);
		}

		public static IPlatformElementConfiguration<Android, FormsImageButton> SetShadowColor(this IPlatformElementConfiguration<Android, FormsImageButton> config, Color value)
		{
			SetShadowColor(config.Element, value);
			return config;
		}

		public static readonly BindableProperty ShadowRadiusProperty = BindableProperty.Create("ShadowRadius", typeof(double), typeof(ImageButton), 10.0);

		public static double GetShadowRadius(BindableObject element)
		{
			return (double)element.GetValue(ShadowRadiusProperty);
		}

		public static void SetShadowRadius(BindableObject element, double value)
		{
			element.SetValue(ShadowRadiusProperty, value);
		}

		public static double GetShadowRadius(this IPlatformElementConfiguration<Android, FormsImageButton> config)
		{
			return GetShadowRadius(config.Element);
		}

		public static IPlatformElementConfiguration<Android, FormsImageButton> SetShadowRadius(this IPlatformElementConfiguration<Android, FormsImageButton> config, double value)
		{
			SetShadowRadius(config.Element, value);
			return config;
		}

		public static readonly BindableProperty ShadowOffsetProperty = BindableProperty.Create("ShadowOffset", typeof(SizeF), typeof(VisualElement), SizeF.Zero);

		public static SizeF GetShadowOffset(BindableObject element)
		{
			return (SizeF)element.GetValue(ShadowOffsetProperty);
		}

		public static void SetShadowOffset(BindableObject element, SizeF value)
		{
			element.SetValue(ShadowOffsetProperty, value);
		}

		public static SizeF GetShadowOffset(this IPlatformElementConfiguration<Android, FormsImageButton> config)
		{
			return GetShadowOffset(config.Element);
		}

		public static IPlatformElementConfiguration<Android, FormsImageButton> SetShadowOffset(this IPlatformElementConfiguration<Android, FormsImageButton> config, SizeF value)
		{
			SetShadowOffset(config.Element, value);
			return config;
		}
		#endregion
	}
}
