using System;

namespace Xamarin.Forms
{
	public class ValueChangedEventArgs : EventArgs
	{
		public ValueChangedEventArgs(float oldValue, float newValue)
		{
			OldValue = oldValue;
			NewValue = newValue;
		}

		public float NewValue { get; private set; }

		public float OldValue { get; private set; }
	}
}