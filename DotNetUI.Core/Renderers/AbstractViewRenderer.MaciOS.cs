using System;
using System.Drawing;
using DotNetUI.Platform;

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

		public void SetFrame (RectangleF rect) => View.Frame = rect.ToCGRect ();

		protected virtual void OnNativeViewCreated ()
		{

		}
	}
}
