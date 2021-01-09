using System.Graphics;
using Xamarin.Forms;

#if __IOS__
using NativeColor = UIKit.UIColor;
#else
using NativeColor = AppKit.NSColor;
#endif

namespace Xamarin.Platform.Handlers
{
	public partial class AbstractViewHandler<TVirtualView, TNativeView> : INativeViewHandler
	{
		public void SetFrame(RectangleF rect)
		{
			if (View != null)
				View.Frame = rect.ToCGRect();
		}

		public virtual SizeF GetDesiredSize(float widthConstraint, float heightConstraint)
		{
			var sizeThatFits = TypedNativeView?.SizeThatFits(new CoreGraphics.CGSize(widthConstraint,heightConstraint));

			if (sizeThatFits.HasValue)
			{
				return new SizeF((float)sizeThatFits.Value.Width,(float)sizeThatFits.Value.Height);
			}

			return new SizeF(widthConstraint, heightConstraint);
		}

		void SetupContainer()
		{

		}

		void RemoveContainer()
		{


		}
	}
}