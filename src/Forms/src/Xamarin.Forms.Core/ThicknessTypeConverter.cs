using System;
using System.Globalization;

namespace Xamarin.Forms
{
	[Xaml.ProvideCompiled("Xamarin.Forms.Core.XamlC.ThicknessTypeConverter")]
	[Xaml.TypeConversion(typeof(Thickness))]
	public class ThicknessTypeConverter : TypeConverter
	{
		public override object ConvertFromInvariantString(string value)
		{
			if (value != null)
			{
				value = value.Trim();
				if (value.Contains(","))
				{ //Xaml
					var thickness = value.Split(',');
					switch (thickness.Length)
					{
						case 2:
							if (float.TryParse(thickness[0], NumberStyles.Number, CultureInfo.InvariantCulture, out float h)
								&& float.TryParse(thickness[1], NumberStyles.Number, CultureInfo.InvariantCulture, out float v))
								return new Thickness(h, v);
							break;
						case 4:
							if (float.TryParse(thickness[0], NumberStyles.Number, CultureInfo.InvariantCulture, out float l)
								&& float.TryParse(thickness[1], NumberStyles.Number, CultureInfo.InvariantCulture, out float t)
								&& float.TryParse(thickness[2], NumberStyles.Number, CultureInfo.InvariantCulture, out float r)
								&& float.TryParse(thickness[3], NumberStyles.Number, CultureInfo.InvariantCulture, out float b))
								return new Thickness(l, t, r, b);
							break;
					}
				}
				else if (value.Contains(" "))
				{ //CSS
					var thickness = value.Split(' ');
					switch (thickness.Length)
					{
						case 2:
							if (float.TryParse(thickness[0], NumberStyles.Number, CultureInfo.InvariantCulture, out float v)
								&& float.TryParse(thickness[1], NumberStyles.Number, CultureInfo.InvariantCulture, out float h))
								return new Thickness(h, v);
							break;
						case 3:
							if (float.TryParse(thickness[0], NumberStyles.Number, CultureInfo.InvariantCulture, out float t)
								&& float.TryParse(thickness[1], NumberStyles.Number, CultureInfo.InvariantCulture, out h)
								&& float.TryParse(thickness[2], NumberStyles.Number, CultureInfo.InvariantCulture, out float b))
								return new Thickness(h, t, h, b);
							break;
						case 4:
							if (float.TryParse(thickness[0], NumberStyles.Number, CultureInfo.InvariantCulture, out t)
								&& float.TryParse(thickness[1], NumberStyles.Number, CultureInfo.InvariantCulture, out float r)
								&& float.TryParse(thickness[2], NumberStyles.Number, CultureInfo.InvariantCulture, out b)
								&& float.TryParse(thickness[3], NumberStyles.Number, CultureInfo.InvariantCulture, out float l))
								return new Thickness(l, t, r, b);
							break;
					}
				}
				else
				{ //single uniform thickness
					if (float.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out float l))
						return new Thickness(l);
				}
			}

			throw new InvalidOperationException($"Cannot convert \"{value}\" into {typeof(Thickness)}");
		}

		public override string ConvertToInvariantString(object value)
		{
			if (!(value is Thickness t))
				throw new NotSupportedException();
			return $"{t.Left.ToString(CultureInfo.InvariantCulture)}, {t.Top.ToString(CultureInfo.InvariantCulture)}, {t.Right.ToString(CultureInfo.InvariantCulture)}, {t.Bottom.ToString(CultureInfo.InvariantCulture)}";
		}
	}
}