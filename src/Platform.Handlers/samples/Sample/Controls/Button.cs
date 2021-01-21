﻿using System;
using System.Graphics;
using Xamarin.Forms;
using Xamarin.Platform;

namespace Sample
{
	public class Button : Xamarin.Forms.View, IButton
	{
		public const int DefaultCornerRadius = -1;

		public string Text { get; set; }

		public Color Color { get; set; }

		public int CornerRadius { get; set; } = DefaultCornerRadius;

		public Color BorderColor { get; set; }

		public float BorderWidth { get; set; }

		public Font Font { get; set; }

		public string FontFamily { get; set; }

		public float FontSize { get; set; }

		public FontAttributes FontAttributes { get; set; }

		public TextTransform TextTransform { get; set; }

		public TextAlignment HorizontalTextAlignment { get; set; }

		public TextAlignment VerticalTextAlignment { get; set; }

		public float CharacterSpacing { get; set; }

		public Action Pressed { get; set; }
		public Action Released { get; set; }
		public Action Clicked { get; set; }

		void IButton.Pressed() => Pressed?.Invoke();
		void IButton.Released() => Released?.Invoke();
		void IButton.Clicked() => Clicked?.Invoke();

		public new float Width
		{
			get { return WidthRequest; }
			set { WidthRequest = value; }
		}

		public new float Height
		{
			get { return HeightRequest; }
			set { HeightRequest = value; }
		}
	}
}