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
using Xamarin.Forms;

#if __MOBILE__
using NativeLabel = UIKit.UILabel;
#else
using NativeLabel = AppKit.NSTextField;
#endif


namespace DotNetUI.Renderers
{
	public partial class LabelRenderer : AbstractViewRenderer<ILabel, NativeLabel>
	{
		SizeRequest _perfectSize;
		bool _perfectSizeValid;

		public LabelRenderer() : base(Mapper)
		{

		}
		protected override NativeLabel CreateView() => new NativeLabel();


		public override SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			if (!_perfectSizeValid)
			{
				_perfectSize = base.GetDesiredSize(double.PositiveInfinity, double.PositiveInfinity);
				_perfectSize.Minimum = new Size(Math.Min(10, _perfectSize.Request.Width), _perfectSize.Request.Height);
				_perfectSizeValid = true;
			}

			var widthFits = widthConstraint >= _perfectSize.Request.Width;
			var heightFits = heightConstraint >= _perfectSize.Request.Height;

			if (widthFits && heightFits)
				return _perfectSize;

			var result = base.GetDesiredSize(widthConstraint, heightConstraint);
			var tinyWidth = Math.Min(10, result.Request.Width);
			result.Minimum = new Size(tinyWidth, result.Request.Height);

			//if (widthFits || Element.LineBreakMode == LineBreakMode.NoWrap)
			//	return result;

			bool containerIsNotInfinitelyWide = !double.IsInfinity(widthConstraint);

			if (containerIsNotInfinitelyWide)
			{
				bool textCouldHaveWrapped = false;// Element.LineBreakMode == LineBreakMode.WordWrap || Element.LineBreakMode == LineBreakMode.CharacterWrap;
				bool textExceedsContainer = result.Request.Width > widthConstraint;

				if (textExceedsContainer || textCouldHaveWrapped)
				{
					var expandedWidth = Math.Max(tinyWidth, widthConstraint);
					result.Request = new Size(expandedWidth, result.Request.Height);
				}
			}

			return result;
		}


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
			//label.TextColor = view.Color.ToNativeColor ();
		}



	}
}
