using System.ComponentModel;

namespace Xamarin.Forms.Internals
{
	[EditorBrowsable(EditorBrowsableState.Never)]
	interface ILineHeightElement
	{
		float LineHeight { get; }

		void OnLineHeightChanged(float oldValue, float newValue);
	}
}