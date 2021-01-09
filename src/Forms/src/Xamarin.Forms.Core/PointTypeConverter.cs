using System;
using System.Globalization;
using System.Graphics;

namespace Xamarin.Forms
{
	[Xaml.TypeConversion(typeof(PointF))]
	public class PointTypeConverter : TypeConverter
	{
		public override object ConvertFromInvariantString(string value)
		{
			if (value != null)
			{
				string[] xy = value.Split(',');
				if (xy.Length == 2 && float.TryParse(xy[0], NumberStyles.Number, CultureInfo.InvariantCulture, out var x) && float.TryParse(xy[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var y))
					return new PointF(x, y);
			}

			throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", value, typeof(PointF)));
		}

		public override string ConvertToInvariantString(object value)
		{
			if (!(value is PointF p))
				throw new NotSupportedException();
			return $"{p.X.ToString(CultureInfo.InvariantCulture)}, {p.Y.ToString(CultureInfo.InvariantCulture)}";
		}
	}
}