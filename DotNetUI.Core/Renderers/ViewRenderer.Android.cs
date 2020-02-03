using System;
namespace DotNetUI.Renderers {
	public partial class ViewRenderer {
		public static void MapPropertyIsEnabled (IViewRenderer renderer, IView view)
		{
			var nativeView = renderer.NativeView as Android.Views.View;
			if (nativeView != null)
				nativeView.Enabled = view.IsEnabled;
		}
		public static void MapBackgroundColor (IViewRenderer renderer, IView view)
		{
		}
	}
}
