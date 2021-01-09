using System;
using System.Graphics;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms
{
	interface ITextElement
	{
		//note to implementor: implement this property publicly
		Color TextColor { get; }

		//note to implementor: but implement this method explicitly
		void OnTextColorPropertyChanged(Color oldValue, Color newValue);

		float CharacterSpacing { get; }

		//note to implementor: but implement these methods explicitly
		void OnCharacterSpacingPropertyChanged(float oldValue, float newValue);

		TextTransform TextTransform { get; set; }

		void OnTextTransformChanged(TextTransform oldValue, TextTransform newValue);

		string UpdateFormsText(string original, TextTransform transform);
	}
}