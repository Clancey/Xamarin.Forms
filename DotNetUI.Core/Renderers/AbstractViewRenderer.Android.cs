using System;
using Android.Content;
using Android.Views;
using Xamarin.Forms;

namespace DotNetUI.Renderers
{
	public partial class AbstractViewRenderer<TVirtualView, TNativeView>
	{
		public AbstractViewRenderer(Context context, PropertyMapper<TVirtualView> mapper)
		{
			Context = context;
		}

		public Context Context { get; }

		public void SetFrame(Rectangle frame)
		{
			var nativeView = TypedNativeView;
			if (nativeView == null)
				return;

			var scale = Context.Resources.DisplayMetrics.Density;

			var left = frame.Left * scale;
			var top = frame.Top * scale;
			var bottom = frame.Bottom * scale;
			var right = frame.Right * scale;

			if (nativeView.LayoutParameters == null)
			{
				nativeView.LayoutParameters = new ViewGroup.LayoutParams(
					(int)(frame.Width * scale),
					(int)(frame.Height * scale));
			}
			else
			{
				nativeView.LayoutParameters.Width = (int)(frame.Width * scale);
				nativeView.LayoutParameters.Height = (int)(frame.Height * scale);
			}

			nativeView.Layout((int)left, (int)top, (int)right, (int)bottom);
		}

		public virtual SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint)
			=> new SizeRequest();
	}
}
