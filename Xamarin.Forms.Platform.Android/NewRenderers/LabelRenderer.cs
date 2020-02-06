using System;
using Android.Content;

namespace Xamarin.Forms.Platform.Android.NewRenderers
{
	public class LabelRenderer :  RendererWrapper<DotNetUI.Renderers.LabelRenderer>
	{
		public LabelRenderer(Context context) : base(context)
		{
		}
	}
}
