using System;
using DotNetUI.Platform;
using Xamarin.Forms;

#if __MOBILE__
using NativeColor = UIKit.UIColor;
#else
using NativeColor = AppKit.NSColor;
#endif

namespace DotNetUI.Renderers {
	public partial class AbstractViewRenderer<TVirtualView, TNativeView> {

		protected AbstractViewRenderer (PropertyMapper<TVirtualView> mapper)
		{
			this.mapper = mapper;
		}

		public void SetFrame (Rectangle rect) => View.Frame = rect.ToCGRect ();

		protected virtual void OnNativeViewCreated ()
		{

		}
		public virtual SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
		{
			CoreGraphics.CGSize s;
#if __MOBILE__
			s = TypedNativeView.SizeThatFits(new CoreGraphics.CGSize((float)widthConstraint, (float)heightConstraint));
#else
			var control = TypedNativeView as AppKit.NSControl;
			if (control != null)
				s = control.SizeThatFits(new CoreGraphics.CGSize(widthConstraint, heightConstraint));
			else
				s = TypedNativeView.FittingSize;
#endif
			var request = new Size(s.Width == float.PositiveInfinity ? double.PositiveInfinity : s.Width,
				s.Height == float.PositiveInfinity ? double.PositiveInfinity : s.Height);
			return new SizeRequest(request, request);
		}
	}
}
