using System;
using System.Globalization;
using System.Graphics;

namespace Xamarin.Forms
{
	[Xaml.TypeConversion(typeof(SizeF))]
	public class SizeTypeConverter : TypeConverter
	{
		public override object ConvertFromInvariantString(string value)
		{
			if (value != null)
			{
				string[] wh = value.Split(',');
				if (wh.Length == 2
					&& float.TryParse(wh[0], NumberStyles.Number, CultureInfo.InvariantCulture, out float w)
					&& float.TryParse(wh[1], NumberStyles.Number, CultureInfo.InvariantCulture, out float h))
					return new SizeF(w, h);
			}

			throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", value, typeof(SizeF)));
		}

		public override string ConvertToInvariantString(object value)
		{
			if (!(value is SizeF size))
				throw new NotSupportedException();
			return $"{size.Width.ToString(CultureInfo.InvariantCulture)}, {size.Height.ToString(CultureInfo.InvariantCulture)}";
		}
	}
}