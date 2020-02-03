using DotNetUI.Views;
using DotNetUI.Platform;
using System;
using System.ComponentModel;
using RectangleF = CoreGraphics.CGRect;
using SizeF = CoreGraphics.CGSize;
using Foundation;
using System.Collections.Generic;
using CoreGraphics;
using System.Diagnostics;

#if __MOBILE__
using NativeLabel = UIKit.UILabel;
#else
using NativeLabel = AppKit.NSTextField;
#endif


namespace DotNetUI.Renderers
{
	public partial class LabelRenderer : AbstractViewRenderer<ILabel, NativeLabel>
	{
		public LabelRenderer() : base(Mapper)
		{

		}
		protected override NativeLabel CreateView() => new NativeLabel();


		public static void MapPropertyText(IViewRenderer renderer, Views.ILabel view)
		{
			var label = renderer.NativeView as NativeLabel;
			if (view.TextType == Xamarin.Forms.TextType.Html)
			{

				label.SetText(view.Text.ToNSAttributedString());
			}
			else
			{
				label.SetText(view.Text);
			}
		}
		public static void MapPropertyColor (IViewRenderer renderer, Views.ILabel view) {
			var label = renderer.NativeView as NativeLabel;
			if (label == null)
				return;
			label.TextColor = view.Color.ToNativeColor ();
		}

	}
}
