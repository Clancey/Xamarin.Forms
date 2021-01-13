using System.Graphics;

namespace Xamarin.Forms
{
	static class TextElement
	{
		public static readonly BindableProperty TextColorProperty =
			BindableProperty.Create(nameof(ITextElement.TextColor), typeof(Color), typeof(ITextElement), null,
									propertyChanged: OnTextColorPropertyChanged);

		public static readonly BindableProperty CharacterSpacingProperty =
			BindableProperty.Create(nameof(ITextElement.CharacterSpacing), typeof(float), typeof(ITextElement), 0.0f,
				propertyChanged: OnCharacterSpacingPropertyChanged);

		static void OnTextColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((ITextElement)bindable).OnTextColorPropertyChanged((Color)oldValue, (Color)newValue);
		}

		static void OnCharacterSpacingPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((ITextElement)bindable).OnCharacterSpacingPropertyChanged((float)oldValue, (float)newValue);
		}

		public static readonly BindableProperty TextTransformProperty =
			BindableProperty.Create(nameof(ITextElement.TextTransform), typeof(TextTransform), typeof(ITextElement), TextTransform.Default,
							propertyChanged: OnTextTransformPropertyChanged);

		static void OnTextTransformPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((ITextElement)bindable).OnTextTransformChanged((TextTransform)oldValue, (TextTransform)newValue);
		}
	}
}