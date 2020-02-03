using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
#if __ANDROID_29__
using AndroidX.Core.View;
using AndroidX.AppCompat.Widget;
#else
using Android.Support.V4.View;
using Android.Support.V7.Widget;
#endif
using Android.Util;
using Android.Views;
using DotNetUI.Views;

namespace DotNetUI.Renderers {
	public partial class ButtonRenderer : AbstractViewRenderer<IButton, AppCompatButton> {
		public ButtonRenderer(Context context) : base(context, ButtonMapper)
		{

		}

		public static void MapPropertyButtonText(IViewRenderer renderer, Views.IButton view)
		{

		}

		protected override AppCompatButton CreateView()
		{
			throw new NotImplementedException();
		}
	}
}
