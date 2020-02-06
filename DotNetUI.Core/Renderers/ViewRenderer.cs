using System;
using DotNetUI.Views;

namespace DotNetUI.Renderers {
	public partial class ViewRenderer {
		public static PropertyMapper<IView> ViewMapper = new PropertyMapper<IView> {
			[nameof(IView.IsEnabled)] = MapPropertyIsEnabled,
			[nameof(IView.BackgroundColor)] = MapBackgroundColor,
			[nameof(IView.Frame)] = MapPropertyFrame,
		};

		public static void MapPropertyFrame(IViewRenderer renderer, IView view)
			=> renderer?.SetFrame(view.Frame);
	}
}
