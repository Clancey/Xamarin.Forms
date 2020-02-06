using System;
using DotNetUI.Renderers;
using Xamarin.Forms;

namespace DotNetUI
{
	public interface IView {
		bool IsEnabled { get; }
		Color BackgroundColor { get; }
		Rectangle Frame { get; }
	}
}
