using System.Collections.Concurrent;
using System.Graphics;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Android.Util;
using APaint = Android.Graphics.Paint;

namespace Xamarin.Forms.Platform.Android
{
	public sealed class FontImageSourceHandler : IImageSourceHandler
	{
		public Task<Bitmap> LoadImageAsync(
			ImageSource imagesource,
			Context context,
			CancellationToken cancelationToken = default(CancellationToken))
		{
			Bitmap image = null;
			var fontsource = imagesource as FontImageSource;
			if (fontsource != null)
			{
				using var paint = new APaint
				{
					TextSize = TypedValue.ApplyDimension(ComplexUnitType.Dip, (float)fontsource.Size, context.Resources.DisplayMetrics),
					Color = (fontsource.Color ?? Colors.White).ToAndroid(),
					TextAlign = APaint.Align.Left,
					AntiAlias = true,
				};

				paint.SetTypeface(fontsource.FontFamily.ToTypeFace());

				var width = (int)(paint.MeasureText(fontsource.Glyph) + .5f);
				var baseline = (int)(-paint.Ascent() + .5f);
				var height = (int)(baseline + paint.Descent() + .5f);
				image = Bitmap.CreateBitmap(width, height, Bitmap.Config.Argb8888);
				using var canvas = new Canvas(image);
				canvas.DrawText(fontsource.Glyph, 0, baseline, paint);
			}

			return Task.FromResult(image);
		}
	}
}