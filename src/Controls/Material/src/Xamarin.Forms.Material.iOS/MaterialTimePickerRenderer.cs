using Xamarin.Forms.Platform.iOS;

namespace Xamarin.Forms.Material.iOS
{
	public class MaterialTimePickerRenderer : TimePickerRendererBase<MaterialTextField>, IMaterialEntryRenderer
	{
		internal void UpdatePlaceholder() => Control?.UpdatePlaceholder(this);

		protected internal override void UpdateFont()
		{
			base.UpdateFont();
			Control?.ApplyTypographyScheme(Element);
		}

		protected override MaterialTextField CreateNativeControl() => new NoCaretMaterialTextField(this, Element);
		protected override void SetBackgroundColor(Color color) => ApplyTheme();
		protected override void SetBackground(Brush brush) => ApplyTheme();
		protected internal override void UpdateTextColor() => Control?.UpdateTextColor(this);
		protected virtual void ApplyTheme() => Control?.ApplyTheme(this);
		protected virtual void ApplyThemeIfNeeded() => Control?.ApplyThemeIfNeeded(this);

		protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
		{
			base.OnElementChanged(e);

			if(e.NewElement != null)
				UpdatePlaceholder();
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			ApplyThemeIfNeeded();
		}

		string IMaterialEntryRenderer.Placeholder => string.Empty;
		Color IMaterialEntryRenderer.PlaceholderColor => null;
		Color IMaterialEntryRenderer.TextColor => Element?.TextColor ?? null;
		Color IMaterialEntryRenderer.BackgroundColor => Element?.BackgroundColor ?? null;
		Brush IMaterialEntryRenderer.Background => Element?.Background ?? Brush.Default;
	}
}