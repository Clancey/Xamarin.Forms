using System;
namespace Xamarin.Platform.HotReload
{
	public static class HotReloadExtensions
	{
		public static void CheckHandlers(this IView view)
		{
			if (view?.Handler == null)
				return;

			var renderType = Xamarin.Platform.Registrar.Handlers.GetRendererType(view.GetType());
			if (renderType != view.Handler.GetType())
				view.Handler = null;
			if(view is ILayout layout)
			{
				foreach (var v in layout.Children)
					CheckHandlers(v);
			}
		}

	}
}
