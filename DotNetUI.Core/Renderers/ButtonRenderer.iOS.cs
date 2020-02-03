using System;
using DotNetUI.Views;
using UIKit;

namespace DotNetUI.Renderers {
	public partial class ButtonRenderer : AbstractViewRenderer<IButton,UIButton> {
		public static void MapPropertyButtonText(IViewRenderer renderer, IButton view)
		{
			var button = renderer.NativeView as UIButton;
			button.SetTitle(view.Text, UIControlState.Normal);
		}
		public ButtonRenderer() : base(ButtonMapper)
		{

		}

		protected override UIButton CreateView()
		{
			return new UIButton();
		}
	}
}
