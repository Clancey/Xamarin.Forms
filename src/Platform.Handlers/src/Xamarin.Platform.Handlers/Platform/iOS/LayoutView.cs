using System;
using System.Graphics;
using CoreGraphics;
using UIKit;
using Xamarin.Forms;

namespace Xamarin.Platform
{
	public class LayoutView : UIView
	{
		public override CGSize SizeThatFits(CGSize size)
		{
			if (CrossPlatformMeasure == null)
			{
				return base.SizeThatFits(size);
			}

			var width = size.Width;
			var height = size.Height;

			var crossPlatformSize = CrossPlatformMeasure((float)width, (float)height);

			return base.SizeThatFits(crossPlatformSize.ToCGSize());
		}

		public override void LayoutSubviews()
		{
			base.LayoutSubviews();

			var width = Frame.Width;
			var height = Frame.Height;

			CrossPlatformMeasure?.Invoke((float)width, (float)height);
			CrossPlatformArrange?.Invoke(Frame.ToRectangle());
		}

		internal Func<float, float, SizeF>? CrossPlatformMeasure { get; set; }
		internal Action<RectangleF>? CrossPlatformArrange { get; set; }
	}
}
