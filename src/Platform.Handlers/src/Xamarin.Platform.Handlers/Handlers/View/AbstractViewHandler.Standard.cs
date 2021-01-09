using System.Graphics;
using Xamarin.Forms;

namespace Xamarin.Platform.Handlers
{
	public abstract partial class AbstractViewHandler<TVirtualView, TNativeView>
	{
		public void SetFrame(RectangleF rect)
		{

		}

		public virtual SizeF GetDesiredSize(float widthConstraint, float heightConstraint)
			=> SizeF.Zero;

		void SetupContainer()
		{

		}

		void RemoveContainer()
		{

		}
	}
}