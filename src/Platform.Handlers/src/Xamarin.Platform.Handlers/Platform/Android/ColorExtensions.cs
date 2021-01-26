using Android.Content;
using Android.Content.Res;
using AndroidX.Core.Content;
using Xamarin.Forms;
using AColor = Android.Graphics.Color;
using System.Graphics;

namespace Xamarin.Platform
{
	public static class ColorExtensions
	{
		public static readonly int[][] States = { new[] { global::Android.Resource.Attribute.StateEnabled }, new[] { -global::Android.Resource.Attribute.StateEnabled } };

		public static AColor ToNative(this Color self)
		{
			return new AColor((byte)(byte.MaxValue * self.Red), (byte)(byte.MaxValue * self.Green), (byte)(byte.MaxValue * self.Blue), (byte)(byte.MaxValue * self.Alpha));
		}

		public static AColor ToNative(this Color self, int defaultColorResourceId, Context context)
		{
			if (self == null)
			{
				return new AColor(ContextCompat.GetColor(context, defaultColorResourceId));
			}

			return ToNative(self);
		}

		public static AColor ToNative(this Color self, Color defaultColor)
		{
			if (self == null)
				return defaultColor.ToNative();

			return ToNative(self);
		}

		public static ColorStateList ToAndroidPreserveDisabled(this Color color, ColorStateList defaults)
		{
			int disabled = defaults.GetColorForState(States[1], color.ToNative());
			return new ColorStateList(States, new[] { color.ToNative().ToArgb(), disabled });
		}

		public static Color ToColor(this AColor color)
		{
			return Color.FromUint((uint)color.ToArgb());
		}
	}
}