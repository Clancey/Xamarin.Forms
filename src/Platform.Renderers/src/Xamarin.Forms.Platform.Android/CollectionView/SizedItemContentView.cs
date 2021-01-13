using System;
using Android.Content;

namespace Xamarin.Forms.Platform.Android
{
	internal class SizedItemContentView : ItemContentView
	{
		readonly Func<int> _width;
		readonly Func<int> _height;

		public SizedItemContentView(Context context, Func<int> width, Func<int> height)
			: base(context)
		{
			_width = width;
			_height = height;
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			if (Content == null)
			{
				SetMeasuredDimension(0, 0);
				return;
			}

			var targetWidth = _width();
			var targetHeight = _height();

			Content.Element.Measure((float)Context.FromPixels(targetWidth), (float)Context.FromPixels(targetHeight),
				MeasureFlags.IncludeMargins);

			SetMeasuredDimension(targetWidth, targetHeight);
		}
	}
}
