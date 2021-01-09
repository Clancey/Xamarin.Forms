using System.Linq;
using System.Graphics;

namespace Xamarin.Platform
{
	internal static class RectExtensions
	{
		public static bool Contains(this RectangleF rect, PointF point) =>
			point.X >= 0 && point.X <= rect.Width &&
			point.Y >= 0 && point.Y <= rect.Height;

		public static bool ContainsAny(this RectangleF rect, PointF[] points)
			=> points.Any(x => rect.Contains(x));
	}
}
