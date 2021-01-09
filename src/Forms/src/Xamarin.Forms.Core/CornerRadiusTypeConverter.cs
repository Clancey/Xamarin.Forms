using System;
using System.Globalization;

namespace Xamarin.Forms
{
	[Xaml.TypeConversion(typeof(CornerRadius))]
	public class CornerRadiusTypeConverter : TypeConverter
	{
		public override object ConvertFromInvariantString(string value)
		{
			if (value != null)
			{
				value = value.Trim();
				if (value.Contains(","))
				{ //Xaml
					var cornerRadius = value.Split(',');
					if (cornerRadius.Length == 4
						&& float.TryParse(cornerRadius[0], NumberStyles.Number, CultureInfo.InvariantCulture, out float tl)
						&& float.TryParse(cornerRadius[1], NumberStyles.Number, CultureInfo.InvariantCulture, out float tr)
						&& float.TryParse(cornerRadius[2], NumberStyles.Number, CultureInfo.InvariantCulture, out float bl)
						&& float.TryParse(cornerRadius[3], NumberStyles.Number, CultureInfo.InvariantCulture, out float br))
						return new CornerRadius(tl, tr, bl, br);
					if (cornerRadius.Length > 1
						&& cornerRadius.Length < 4
						&& float.TryParse(cornerRadius[0], NumberStyles.Number, CultureInfo.InvariantCulture, out float l))
						return new CornerRadius(l);
				}
				else if (value.Trim().Contains(" "))
				{ //CSS
					var cornerRadius = value.Split(' ');
					if (cornerRadius.Length == 2
						&& float.TryParse(cornerRadius[0], NumberStyles.Number, CultureInfo.InvariantCulture, out float t)
						&& float.TryParse(cornerRadius[1], NumberStyles.Number, CultureInfo.InvariantCulture, out float b))
						return new CornerRadius(t, b, b, t);
					if (cornerRadius.Length == 3
						&& float.TryParse(cornerRadius[0], NumberStyles.Number, CultureInfo.InvariantCulture, out float tl)
						&& float.TryParse(cornerRadius[1], NumberStyles.Number, CultureInfo.InvariantCulture, out float trbl)
						&& float.TryParse(cornerRadius[2], NumberStyles.Number, CultureInfo.InvariantCulture, out float br))
						return new CornerRadius(tl, trbl, trbl, br);
					if (cornerRadius.Length == 4
						&& float.TryParse(cornerRadius[0], NumberStyles.Number, CultureInfo.InvariantCulture, out tl)
						&& float.TryParse(cornerRadius[1], NumberStyles.Number, CultureInfo.InvariantCulture, out float tr)
						&& float.TryParse(cornerRadius[2], NumberStyles.Number, CultureInfo.InvariantCulture, out float bl)
						&& float.TryParse(cornerRadius[3], NumberStyles.Number, CultureInfo.InvariantCulture, out br))
						return new CornerRadius(tl, tr, bl, br);
				}
				else
				{ //single uniform CornerRadius
					if (float.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out float l))
						return new CornerRadius(l);
				}
			}

			throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(CornerRadius)}");
		}

		public override string ConvertToInvariantString(object value)
		{
			if (!(value is CornerRadius cr))
				throw new NotSupportedException();
			return $"{cr.TopLeft.ToString(CultureInfo.InvariantCulture)}, {cr.TopRight.ToString(CultureInfo.InvariantCulture)}, {cr.BottomLeft.ToString(CultureInfo.InvariantCulture)}, {cr.BottomRight.ToString(CultureInfo.InvariantCulture)}";

		}
	}
}