using Xamarin.Forms.Internals;

namespace Xamarin.Forms
{
	static class LineHeightElement
	{
		public static readonly BindableProperty LineHeightProperty =
			BindableProperty.Create(nameof(ILineHeightElement.LineHeight), typeof(float), typeof(ILineHeightElement), -1f,
									propertyChanged: OnLineHeightChanged);

		static void OnLineHeightChanged(BindableObject bindable, object oldValue, object newValue)
		{
			((ILineHeightElement)bindable).OnLineHeightChanged((float)oldValue, (float)newValue);
		}

	}
}