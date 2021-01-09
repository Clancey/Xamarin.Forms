using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using RectangleF = System.Graphics.RectangleF;
using SizeF = System.Graphics.SizeF;

namespace Xamarin.Platform.Handlers
{
	public class LayoutViewGroup : ViewGroup
	{
		public LayoutViewGroup(Context context) : base(context)
		{
		}

		public LayoutViewGroup(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public LayoutViewGroup(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}

		public LayoutViewGroup(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
		}

		public LayoutViewGroup(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
		{
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			if (Context == null)
			{
				return;
			}

			if (CrossPlatformMeasure == null)
			{
				base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
				return;
			}

			var deviceIndependentWidth = widthMeasureSpec.ToDouble(Context);
			var deviceIndependentHeight = heightMeasureSpec.ToDouble(Context);

			var size = CrossPlatformMeasure((float)deviceIndependentWidth, (float)deviceIndependentHeight);

			var nativeWidth = Context.ToPixels(size.Width);
			var nativeHeight = Context.ToPixels(size.Height);

			SetMeasuredDimension((int)nativeWidth, (int)nativeHeight);
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			if (CrossPlatformArrange == null || Context == null)
			{
				return;
			}

			var deviceIndependentLeft = (float)Context.FromPixels(l);
			var deviceIndependentTop = (float)Context.FromPixels(t);
			var deviceIndependentRight = (float)Context.FromPixels(r);
			var deviceIndependentBottom = (float)Context.FromPixels(b);

			var destination = RectangleF.FromLTRB(deviceIndependentLeft, deviceIndependentTop,
				deviceIndependentRight, deviceIndependentBottom);

			CrossPlatformArrange(destination);
		}

		internal Func<float, float, SizeF>? CrossPlatformMeasure { get; set; }
		internal Action<RectangleF>? CrossPlatformArrange { get; set; }
	}
}
