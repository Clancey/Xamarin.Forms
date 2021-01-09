using System;
using System.Globalization;
using System.Graphics;

namespace Xamarin.Forms
{
	[Xaml.ProvideCompiled("Xamarin.Forms.Core.XamlC.RectangleTypeConverter")]
	[Xaml.TypeConversion(typeof(RectangleF))]
	public class RectangleTypeConverter : TypeConverter
	{
		public override object ConvertFromInvariantString(string value)
		{
			if (value != null)
			{
				string[] xywh = value.Split(',');
				if (xywh.Length == 4
					&& float.TryParse(xywh[0], NumberStyles.Number, CultureInfo.InvariantCulture, out float x)
					&& float.TryParse(xywh[1], NumberStyles.Number, CultureInfo.InvariantCulture, out float y)
					&& float.TryParse(xywh[2], NumberStyles.Number, CultureInfo.InvariantCulture, out float w)
					&& float.TryParse(xywh[3], NumberStyles.Number, CultureInfo.InvariantCulture, out float h))
					return new RectangleF(x, y, w, h);
			}

			throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", value, typeof(RectangleF)));
		}

		public override string ConvertToInvariantString(object value)
		{
			if (!(value is RectangleF r))
				throw new NotSupportedException();
			return $"{r.X.ToString(CultureInfo.InvariantCulture)}, {r.Y.ToString(CultureInfo.InvariantCulture)}, {r.Width.ToString(CultureInfo.InvariantCulture)}, {r.Height.ToString(CultureInfo.InvariantCulture)}";
		}
	}
}