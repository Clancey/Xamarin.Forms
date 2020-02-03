using System;
using DotNetUI.Views;
using AppKit;

namespace DotNetUI.Renderers
{
	public partial class ButtonRenderer : AbstractViewRenderer<IButton, NSButton>
	{
		public static void MapPropertyButtonText(IViewRenderer renderer, IButton view)
		{

		}
		public ButtonRenderer() : base(ButtonMapper)
		{

		}

		protected override NSButton CreateView()
		{
			return new NSButton();
		}
	}
}
