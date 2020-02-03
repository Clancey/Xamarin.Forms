using System;
using Xamarin.Forms;

namespace DotNetUI.Views {
	public interface IText : IView{
		string Text { get; }

		TextType TextType { get; }
		//TODO: Add fonts and Colors
		Color Color { get; set; }
	}
}
