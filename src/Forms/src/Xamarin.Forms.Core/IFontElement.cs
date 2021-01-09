using System.ComponentModel;

namespace Xamarin.Forms.Internals
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	public interface IFontElement
	{
		//note to implementor: implement these properties publicly
		FontAttributes FontAttributes { get; }
		string FontFamily { get; }

		[TypeConverter(typeof(FontSizeConverter))]
		float FontSize { get; }

		//note to implementor: but implement these methods explicitly
		void OnFontFamilyChanged(string oldValue, string newValue);
		void OnFontSizeChanged(float oldValue, float newValue);
		float FontSizeDefaultValueCreator();
		void OnFontAttributesChanged(FontAttributes oldValue, FontAttributes newValue);
		void OnFontChanged(Font oldValue, Font newValue);
	}
}