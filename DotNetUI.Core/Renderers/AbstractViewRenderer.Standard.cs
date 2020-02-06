using System;
using Xamarin.Forms;

namespace DotNetUI.Renderers
{
	public abstract partial class AbstractViewRenderer<TVirtualView, TNativeView>
	{
		public void SetFrame(Rectangle rect)
		{

		}
		public virtual SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
			=> new SizeRequest();
	}
}
