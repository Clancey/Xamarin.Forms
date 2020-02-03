using System;
using Android.Content;
using DotNetUI.Views;
using System.ComponentModel;
using Android.Content.Res;
using Android.Graphics;
#if __ANDROID_29__
using AndroidX.Core.View;
#else
using Android.Support.V4.View;
#endif
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using AView = Android.Views.View;
using Color = Xamarin.Forms.Color;
using Xamarin.Forms.Platform.Android;

namespace DotNetUI.Renderers {
	public partial class LabelRenderer : AbstractViewRenderer<ILabel, TextView> {
		public LabelRenderer (Context context) : base (context, Mapper)
		{

		}
		ColorStateList _labelTextColorDefault;
		Color _lastUpdateColor = Color.Default;
		protected override TextView CreateView ()
		{
			var text = new TextView (Context);
			_labelTextColorDefault = text.TextColors;
			return text;
		}

		public static void MapPropertyText (IViewRenderer renderer, Views.ILabel view)
		{
			var textView = renderer.NativeView as TextView;
			if (textView == null)
				return;
			textView.Text = view.Text;
		}
		public static void MapPropertyColor (IViewRenderer renderer, Views.ILabel view)
		{
			var labelRenderer = renderer as LabelRenderer;
			if(labelRenderer == null)
				return;
			var c = view.Color;
			if (c == labelRenderer._lastUpdateColor)
				return;


			if (c.IsDefault)
				labelRenderer.TypedNativeView.SetTextColor (labelRenderer._labelTextColorDefault);
			else
				labelRenderer.TypedNativeView.SetTextColor (c.ToAndroid ());
		}
	}
}
