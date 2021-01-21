using System;
using CoreGraphics;
using CGPoint = CoreGraphics.CGPoint;
using CGRect = CoreGraphics.CGRect;
using CGSize = CoreGraphics.CGSize;
using System.Graphics;
#if __MOBILE__
using UIKit;
namespace Xamarin.Forms.Platform.iOS
#else
using AppKit;
using UIColor = AppKit.NSColor;

namespace Xamarin.Forms.Platform.MacOS
#endif
{
	public static class ColorExtensions
	{
#if __MOBILE__
		internal static readonly UIColor Black = UIColor.Black;
		internal static readonly UIColor SeventyPercentGrey = new UIColor(0.7f, 0.7f, 0.7f, 1);

		internal static UIColor LabelColor
		{
			get
			{
				if (Forms.IsiOS13OrNewer)
					return UIColor.LabelColor;

				return UIColor.Black;
			}
		}

		internal static UIColor PlaceholderColor
		{
			get
			{
				if (Forms.IsiOS13OrNewer)
					return UIColor.PlaceholderTextColor;

				return SeventyPercentGrey;
			}
		}

		internal static UIColor SecondaryLabelColor
		{
			get
			{
				if (Forms.IsiOS13OrNewer)
					return UIColor.SecondaryLabelColor;

				return new Color(.32f, .4f, .57f).ToUIColor();
			}
		}

		internal static UIColor BackgroundColor
		{
			get
			{
				if (Forms.IsiOS13OrNewer)
					return UIColor.SystemBackgroundColor;

				return UIColor.White;
			}
		}

		internal static UIColor SeparatorColor
		{
			get
			{
				if (Forms.IsiOS13OrNewer)
					return UIColor.SeparatorColor;

				return UIColor.Gray;
			}
		}

		internal static UIColor OpaqueSeparatorColor
		{
			get
			{
				if (Forms.IsiOS13OrNewer)
					return UIColor.OpaqueSeparatorColor;

				return UIColor.Black;
			}
		}

		internal static UIColor GroupedBackground
		{
			get
			{
				if (Forms.IsiOS13OrNewer)
					return UIColor.SystemGroupedBackgroundColor;

				return new UIColor(247f / 255f, 247f / 255f, 247f / 255f, 1);
			}
		}

		internal static UIColor AccentColor
		{
			get
			{
				if (Forms.IsiOS13OrNewer)
					return UIColor.SystemBlueColor;

				return Color.FromRgba(50, 79, 133, 255).ToUIColor();
			}
		}

		internal static UIColor Red
		{
			get
			{
				if (Forms.IsiOS13OrNewer)
					return UIColor.SystemRedColor;

				return UIColor.FromRGBA(255, 0, 0, 255);
			}
		}

		internal static UIColor Gray
		{
			get
			{
				if (Forms.IsiOS13OrNewer)
					return UIColor.SystemGrayColor;

				return UIColor.Gray;
			}
		}

		internal static UIColor LightGray
		{
			get
			{
				if (Forms.IsiOS13OrNewer)
					return UIColor.SystemGray2Color;
				return UIColor.LightGray;

			}
		}

#else
		internal static readonly NSColor Black = NSColor.Black;
		internal static readonly NSColor SeventyPercentGrey = NSColor.FromRgba(0.7f, 0.7f, 0.7f, 1);
		internal static readonly NSColor AccentColor = Colors.FromRgba(50, 79, 133, 255).ToNSColor();

		internal static NSColor LabelColor
		{
			get
			{
				if (Forms.IsMojaveOrNewer)
					return NSColor.LabelColor;

				return NSColor.Black.UsingColorSpace("NSCalibratedRGBColorSpace");
			}
		}

		internal static NSColor TextColor
		{
			get
			{
				if (Forms.IsMojaveOrNewer)
					return NSColor.Text;

				return NSColor.Black;
			}
		}

		internal static NSColor ControlBackgroundColor
		{
			get
			{
				if (Forms.IsMojaveOrNewer)
					return NSColor.ControlBackground;

				return NSColor.Clear;
			}
		}

		internal static NSColor WindowBackgroundColor
		{
			get
			{
				if (Forms.IsMojaveOrNewer)
					return NSColor.WindowBackground;

				return NSColor.White;
			}
		}

		internal static NSColor PlaceholderColor
		{
			get
			{
				if (Forms.IsMojaveOrNewer)
					return NSColor.PlaceholderTextColor;

				return SeventyPercentGrey;
			}
		}

		internal static NSColor SecondaryLabelColor
		{
			get
			{
				if (Forms.IsMojaveOrNewer)
					return NSColor.SecondaryLabelColor;

				return new Color(.32, .4, .57).ToNSColor();
			}
		}

		internal static NSColor GroupedBackground
		{
			get
			{
				if (Forms.IsMojaveOrNewer)
					return NSColor.SystemGrayColor;

				return Colors.LightGray.ToNSColor();
			}
		}
#endif

		public static CGColor ToCGColor(this Color color)
		{
#if __MOBILE__
			return color.ToUIColor().CGColor;
#else
            return color.ToNSColor().CGColor;
#endif
		}

#if __MOBILE__
		public static UIColor FromPatternImageFromBundle(string bgImage)
		{
			var image = UIImage.FromBundle(bgImage);
			if (image == null)
				return UIColor.White;

			return UIColor.FromPatternImage(image);
		}
#endif

		public static Color ToColor(this UIColor color)
		{
			nfloat red;
			nfloat green;
			nfloat blue;
			nfloat alpha;
#if __MOBILE__
			color.GetRGBA(out red, out green, out blue, out alpha);
#else
			if (color.Type == NSColorType.Catalog)
				throw new InvalidOperationException("Cannot convert a NSColorType.Catalog color without specifying the color space, use the overload to specify an NSColorSpace");

			color.GetRgba(out red, out green, out blue, out alpha);
#endif
			return new Color((float)red, (float)green, (float)blue, (float)alpha);
		}

#if __MACOS__
		public static Color ToColor(this UIColor color, NSColorSpace colorSpace)
		{
			var convertedColor = color.UsingColorSpace(colorSpace);

			return convertedColor.ToColor();
		}
#endif

#if __MOBILE__
		public static UIColor ToUIColor(this Color color)
		{
			return new UIColor((float)color.Red, (float)color.Green, (float)color.Blue, (float)color.Alpha);
		}

		public static UIColor ToUIColor(this Color color, Color defaultColor)
			=> color?.ToUIColor() ?? defaultColor.ToUIColor();

		public static UIColor ToUIColor(this Color color, UIColor defaultColor)
			=> color?.ToUIColor() ?? defaultColor;
#else
		public static NSColor ToNSColor(this Color color)
		{
			return NSColor.FromRgba((float)color.R, (float)color.G, (float)color.B, (float)color.A);
		}

		public static NSColor ToNSColor(this Color color, Color defaultColor)
		{
			if (color.IsDefault)
				return defaultColor.ToNSColor();

			return color.ToNSColor();
		}

		public static NSColor ToNSColor(this Color color, NSColor defaultColor)
		{
			if (color.IsDefault)
				return defaultColor;

			return color.ToNSColor();
		}
#endif
	}

	public static class PointExtensions
	{
		public static PointF ToPoint(this CGPoint point)
		{
			return new PointF((float)point.X, (float)point.Y);
		}

		public static CGPoint ToNative(this PointF point)
		{
			return new CGPoint(point.X, point.Y);
		}
	}

	public static class SizeExtensions
	{
		public static CGSize ToNative(this SizeF size)
		{
			return new CGSize((float)size.Width, (float)size.Height);
		}
	}

	public static class RectangleExtensions
	{
		public static RectangleF ToRectangle(this CGRect rect)
		{
			return new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
		}

		public static CGRect ToNative(this RectangleF rect)
		{
			return new CGRect(rect.X, rect.Y, rect.Width, rect.Height);
		}
	}
}