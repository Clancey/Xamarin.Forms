using System;
using System.Globalization;
using System.Graphics;

namespace Xamarin.Forms
{
	[Xaml.ProvideCompiled("Xamarin.Forms.Core.XamlC.BoundsTypeConverter")]
	[Xaml.TypeConversion(typeof(RectangleF))]
	public sealed class BoundsTypeConverter : TypeConverter
	{
		public override object ConvertFromInvariantString(string value)
		{
			if (value != null)
			{
				float x = -1, y = -1, w = -1, h = -1;
				string[] xywh = value.Split(',');
				bool hasX, hasY, hasW, hasH;

				hasX = (xywh.Length == 2 || xywh.Length == 4) && float.TryParse(xywh[0], NumberStyles.Number, CultureInfo.InvariantCulture, out x);
				hasY = (xywh.Length == 2 || xywh.Length == 4) && float.TryParse(xywh[1], NumberStyles.Number, CultureInfo.InvariantCulture, out y);
				hasW = xywh.Length == 4 && float.TryParse(xywh[2], NumberStyles.Number, CultureInfo.InvariantCulture, out w);
				hasH = xywh.Length == 4 && float.TryParse(xywh[3], NumberStyles.Number, CultureInfo.InvariantCulture, out h);

				if (!hasW && xywh.Length == 4 && string.Compare("AutoSize", xywh[2].Trim(), StringComparison.OrdinalIgnoreCase) == 0)
				{
					hasW = true;
					w = AbsoluteLayout.AutoSize;
				}

				if (!hasH && xywh.Length == 4 && string.Compare("AutoSize", xywh[3].Trim(), StringComparison.OrdinalIgnoreCase) == 0)
				{
					hasH = true;
					h = AbsoluteLayout.AutoSize;
				}

				if (hasX && hasY && xywh.Length == 2)
					return new RectangleF(x, y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize);
				if (hasX && hasY && hasW && hasH && xywh.Length == 4)
					return new RectangleF(x, y, w, h);
			}

			throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(RectangleF)}");
		}

		public override string ConvertToInvariantString(object value)
		{
			if (!(value is RectangleF rect))
				throw new NotSupportedException();
			return $"{rect.X.ToString(CultureInfo.InvariantCulture)}, {rect.Y.ToString(CultureInfo.InvariantCulture)}, {(rect.Width == AbsoluteLayout.AutoSize ? nameof(AbsoluteLayout.AutoSize) : rect.Width.ToString(CultureInfo.InvariantCulture))}, {(rect.Height == AbsoluteLayout.AutoSize ? nameof(AbsoluteLayout.AutoSize) : rect.Height.ToString(CultureInfo.InvariantCulture))}";
		}
	}
}