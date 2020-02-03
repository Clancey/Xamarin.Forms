using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace DotNetUI
{
	public static class RectExtensions
	{
		public static PointF Center(this RectangleF rectangle) => new PointF(rectangle.X + (rectangle.Width / 2), rectangle.Y + (rectangle.Height / 2));
		public static void Center(this ref RectangleF rectangle, PointF center)
		{
			var halfWidth = rectangle.Width / 2;
			var halfHeight = rectangle.Height / 2;
			rectangle.X = center.X - halfWidth;
			rectangle.Y = center.Y - halfHeight;
		}
		public static bool BoundsContains(this RectangleF rect, PointF point) =>
			point.X >= 0 && point.X <= rect.Width &&
			point.Y >= 0 && point.Y <= rect.Height;

		public static bool Contains(this RectangleF rect, PointF[] points)
			=> points.Any(x => rect.Contains(x));

		//public static RectangleF ApplyPadding(this RectangleF rect, Thickness thickness)
		//{
		//	if (thickness == null)
		//		return rect;
		//	rect.X += thickness.Left;
		//	rect.Y += thickness.Top;
		//	rect.Width -= thickness.HorizontalThickness;
		//	rect.Height -= thickness.VerticalThickness;

		//	return rect;
		//}
	}
}
