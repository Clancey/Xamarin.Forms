﻿using System.Graphics;
using Xamarin.Forms;
using Xamarin.Platform;
using Xamarin.Platform.Core;

namespace Sample
{
	public class MyApp : IApp
	{
		public MyApp()
		{
			Platform.Init();
		}

		public IView CreateView()
		{
			var verticalStack = new VerticalStackLayout() { Spacing = 5, BackgroundColor = Colors.AntiqueWhite };
			var horizontalStack = new HorizontalStackLayout() { Spacing = 2, BackgroundColor = Colors.CornflowerBlue };

			var label = new Label { Text = "This should have a margin", BackgroundColor = Colors.Fuchsia };
			label.Margin = new Thickness(15, 10, 20, 15);

			verticalStack.Add(label);

			var button = new Button() { Text = "A Button", Width = 200 };
			var button2 = new Button()
			{
				Color = Colors.Green,
				Text = "Hello I'm a button",
				BackgroundColor = Colors.Purple,
				Margin = new Thickness(12)
			};

			horizontalStack.Add(button);
			horizontalStack.Add(button2);
			horizontalStack.Add(new Label { Text = "And these buttons are in a HorizontalStackLayout" });

			verticalStack.Add(horizontalStack);
			verticalStack.Add(new Slider());

			return verticalStack;
		}
	}
}