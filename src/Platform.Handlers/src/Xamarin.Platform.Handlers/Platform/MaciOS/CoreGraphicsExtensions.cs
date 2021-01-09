using System.Graphics;
using CoreGraphics;
using Xamarin.Forms;

namespace Xamarin.Platform
{
	public static class CoreGraphicsExtensions
	{
		public static PointF ToPoint(this CGPoint size)
		{
			return new PointF((float)size.X, (float)size.Y);
		}

		public static SizeF ToSize(this CGSize size)
		{
			return new SizeF((float)size.Width, (float)size.Height);
		}

		public static CGSize ToCGSize(this SizeF size)
		{
			return new CGSize(size.Width, size.Height);
		}

		public static RectangleF ToRectangle(this CGRect rect)
		{
			return new RectangleF((float)rect.X, (float)rect.Y, (float)rect.Width, (float)rect.Height);
		}

		public static CGRect ToCGRect(this RectangleF rect)
		{
			return new CGRect(rect.X, rect.Y, rect.Width, rect.Height);
		}
	}
}
