using System.Graphics;
using Android.Content;

namespace Xamarin.Forms.Platform.Android
{
	internal class ShellPageContainer : PageContainer
	{
		public ShellPageContainer(Context context, IVisualElementRenderer child, bool inFragment = false) : base(context, child, inFragment)
		{
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			var width =(float) Context.FromPixels(r - l);
			var height = (float)Context.FromPixels(b - t);
			Child.Element.Layout(new RectangleF(0, 0, width, height));
			base.OnLayout(changed, l, t, r, b);
		}
	}
}