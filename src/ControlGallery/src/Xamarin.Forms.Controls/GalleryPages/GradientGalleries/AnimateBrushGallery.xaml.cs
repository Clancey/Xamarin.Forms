﻿using System;

namespace Xamarin.Forms.Controls.GalleryPages.GradientGalleries
{
	public partial class AnimateBrushGallery : ContentPage
	{
		readonly Random _random;

		public AnimateBrushGallery()
		{
			InitializeComponent();

			_random = new Random();

			UpdateBrush();

			Device.StartTimer(TimeSpan.FromSeconds(1), () =>
			{
				UpdateBrush();
				return true;
			});
		}

		void UpdateBrush()
		{
			var linearGradientBrush = new LinearGradientBrush
			{
				StartPoint = new PointF(GetRandomInt(), GetRandomInt()),
				EndPoint = new PointF(GetRandomInt(), GetRandomInt()),
				GradientStops = new GradientStopCollection
					{
						new GradientStop { Color = GetRandomColor() },
						new GradientStop { Color = GetRandomColor() }
					}
			};

			GradientView.Background = linearGradientBrush;
		}

		double GetRandomInt()
		{
			return _random.NextDouble() * 1;
		}

		Color GetRandomColor()
		{
			return Colors.FromRgb(_random.Next(256), _random.Next(256), _random.Next(256));
		}
	}
}