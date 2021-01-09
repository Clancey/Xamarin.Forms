using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Graphics;

namespace Xamarin.Forms
{
	public struct Region
	{
		// While Regions are currently rectangular, they could in the future be transformed into any shape.
		// As such the internals of how it keeps shapes is hidden, so that future internal changes can occur to support shapes
		// such as circles if required, without affecting anything else.

		IReadOnlyList<RectangleF> Regions { get; }
		readonly Thickness _inflation;

		Region(IList<RectangleF> positions) : this()
		{
			Regions = new ReadOnlyCollection<RectangleF>(positions);
		}

		Region(IList<RectangleF> positions, Thickness inflation) : this(positions)
		{
			_inflation = inflation;
		}

		public static Region FromLines(float[] lineHeights, float maxWidth, float startX, float endX, float startY)
		{
			var positions = new List<RectangleF>();
			var endLine = lineHeights.Length - 1;
			var lineHeightTotal = startY;

			for (var i = 0; i <= endLine; i++)
				if (endLine != 0) // MultiLine
				{
					if (i == 0) // First Line
						positions.Add(new RectangleF(startX, lineHeightTotal, maxWidth - startX, lineHeights[i]));

					else if (i != endLine) // Middle Line
						positions.Add(new RectangleF(0, lineHeightTotal, maxWidth, lineHeights[i]));

					else // End Line
						positions.Add(new RectangleF(0, lineHeightTotal, endX, lineHeights[i]));

					lineHeightTotal += lineHeights[i];
				}
				else // SingleLine
					positions.Add(new RectangleF(startX, lineHeightTotal, endX - startX, lineHeights[i]));

			return new Region(positions);
		}

		public bool Contains(PointF pt)
		{
			return Contains(pt.X, pt.Y);
		}

		public bool Contains(float x, float y)
		{
			if (Regions == null)
				return false;

			for (int i = 0; i < Regions.Count; i++)
				if (Regions[i].Contains(x, y))
					return true;

			return false;
		}

		public Region Deflate()
		{
			if (_inflation == null)
				return this;

			return Inflate(_inflation.Left * -1, _inflation.Top * -1, _inflation.Right * -1, _inflation.Bottom * -1);
		}

		public Region Inflate(float size)
		{
			return Inflate(size, size, size, size);
		}

		public Region Inflate(float left, float top, float right, float bottom)
		{
			if (Regions == null)
				return this;

			RectangleF[] rectangles = new RectangleF[Regions.Count];
			for (int i = 0; i < Regions.Count; i++)
			{
				var region = Regions[i];

				if (i == 0) // this is the first line
					region.Top -= top;

				region.Left -= left;
				region.Width += right + left;

				if (i == Regions.Count - 1) // This is the last line
					region.Height += bottom + top;

				rectangles[i] = region;
			}

			var inflation = new Thickness(_inflation == null ? left : left + _inflation.Left,
									   _inflation == null ? top : top + _inflation.Top,
									   _inflation == null ? right : right + _inflation.Right,
									   _inflation == null ? bottom : bottom + _inflation.Bottom);

			return new Region(rectangles, inflation);
		}
	}
}