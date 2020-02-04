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
	}
}
