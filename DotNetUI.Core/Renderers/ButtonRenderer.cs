using System;
using System.Drawing;
using DotNetUI.Views;

namespace DotNetUI.Renderers {
	public partial class ButtonRenderer {

		public static PropertyMapper<IButton> ButtonMapper = new PropertyMapper<IButton>(ViewRenderer.ViewMapper) {
			[nameof(IButton.Text)] = MapPropertyButtonText
		};
	}
}
