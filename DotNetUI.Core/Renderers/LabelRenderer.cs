using System;
using DotNetUI.Views;

namespace DotNetUI.Renderers {
	public partial class LabelRenderer {
		public static PropertyMapper<ILabel> Mapper = new PropertyMapper<ILabel>(ViewRenderer.ViewMapper)
		{
			[nameof(ILabel.Text)] = MapPropertyText,
			[nameof(ILabel.Color)] = MapPropertyColor,
			[nameof(ILabel.LineHeight)] = MapPropertyLineHeight,
		};
	}
}
