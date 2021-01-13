using Android.Content;
using Android.Content.Res;
using Xamarin.Forms;
using AColor = Android.Graphics.Color;
using AndroidX.Core.Content;
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

		public static Color ToColor(this uint argb) => Color.FromBytes((byte)((argb & 0x00ff0000) >> 0x10), (byte)((argb & 0x0000ff00) >> 0x8), (byte)(argb & 0x000000ff), (byte)((argb & 0xff000000) >> 0x18));

		public static AColor ToNative(this Color self, Color defaultColor) =>
			self?.ToNative() ?? defaultColor.ToNative();

		public static ColorStateList ToAndroidPreserveDisabled(this Color color, ColorStateList defaults)
		{
			int disabled = defaults.GetColorForState(States[1], color.ToNative());
			return new ColorStateList(States, new[] { color.ToNative().ToArgb(), disabled });
		}

		public static Color ToColor(this AColor color) =>
			new Color(color.R, color.G, color.B, color.A);
	}
}