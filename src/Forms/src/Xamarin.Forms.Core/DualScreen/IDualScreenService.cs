﻿using System;
using System.ComponentModel;
using System.Graphics;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.DualScreen
{
	internal interface IDualScreenService
	{
		event EventHandler OnScreenChanged;
		bool IsSpanned { get; }
		bool IsLandscape { get; }
		RectangleF GetHinge();
		SizeF ScaledScreenSize { get; }
		PointF? GetLocationOnScreen(VisualElement visualElement);
		object WatchForChangesOnLayout(VisualElement visualElement, Action action);
		void StopWatchingForChangesOnLayout(VisualElement visualElement, object handle);
		Task<int> GetHingeAngleAsync();
	}
}
