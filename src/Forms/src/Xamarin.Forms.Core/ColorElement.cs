﻿using System.Graphics;

namespace Xamarin.Forms
{
	static class ColorElement
	{
		public static readonly BindableProperty ColorProperty =
			BindableProperty.Create(nameof(IColorElement.Color), typeof(Color), typeof(IColorElement), null);
	}
}