using System.Graphics;
using UIKit;
using Xamarin.Forms;

namespace Xamarin.Platform
{
	public static class SliderExtensions
	{ 
		public static void UpdateMinimum(this UISlider uiSlider, ISlider slider)
		{
			uiSlider.MaxValue = (float)slider.Maximum;
		}

		public static void UpdateMaximum(this UISlider uiSlider, ISlider slider)
		{
			uiSlider.MinValue = (float)slider.Minimum;
		}

		public static void UpdateValue(this UISlider uiSlider, ISlider slider)
		{
			if ((float)slider.Value != uiSlider.Value)
				uiSlider.Value = (float)slider.Value;
		}

		public static void UpdateMinimumTrackColor(this UISlider uiSlider, ISlider slider)
		{
			UpdateMinimumTrackColor(uiSlider, slider, null);
		}

		public static void UpdateMinimumTrackColor(this UISlider uiSlider, ISlider slider, UIColor? defaultMinTrackColor)
		{
			uiSlider.MinimumTrackTintColor = slider.MinimumTrackColor?.ToNative() ?? defaultMinTrackColor;
		}

		public static void UpdateMaximumTrackColor(this UISlider uiSlider, ISlider slider)
		{
			UpdateMaximumTrackColor(uiSlider, slider, null);
		}

		public static void UpdateMaximumTrackColor(this UISlider uiSlider, ISlider slider, UIColor? defaultMaxTrackColor) =>
			uiSlider.MaximumTrackTintColor = slider.MaximumTrackColor?.ToNative() ?? defaultMaxTrackColor;

		public static void UpdateThumbColor(this UISlider uiSlider, ISlider slider)
		{
			UpdateThumbColor(uiSlider, slider, null);
		}

		public static void UpdateThumbColor(this UISlider uiSlider, ISlider slider, UIColor? defaultThumbColor) =>
			uiSlider.ThumbTintColor = slider.ThumbColor?.ToNative() ?? defaultThumbColor;
	}
}