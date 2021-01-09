﻿using System;
using System.ComponentModel;
using System.Graphics;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms
{
	public sealed class PinchGestureRecognizer : GestureRecognizer, IPinchGestureController
	{
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsPinching { get; set; }

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendPinch(Element sender, double delta, PointF currentScalePoint)
		{
			EventHandler<PinchGestureUpdatedEventArgs> handler = PinchUpdated;
			if (handler != null)
			{
				handler(sender, new PinchGestureUpdatedEventArgs(GestureStatus.Running, delta, currentScalePoint));
			}
			IsPinching = true;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendPinchCanceled(Element sender)
		{
			EventHandler<PinchGestureUpdatedEventArgs> handler = PinchUpdated;
			if (handler != null)
			{
				handler(sender, new PinchGestureUpdatedEventArgs(GestureStatus.Canceled));
			}
			IsPinching = false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendPinchEnded(Element sender)
		{
			EventHandler<PinchGestureUpdatedEventArgs> handler = PinchUpdated;
			if (handler != null)
			{
				handler(sender, new PinchGestureUpdatedEventArgs(GestureStatus.Completed));
			}
			IsPinching = false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public void SendPinchStarted(Element sender, PointF initialScalePoint)
		{
			EventHandler<PinchGestureUpdatedEventArgs> handler = PinchUpdated;
			if (handler != null)
			{
				handler(sender, new PinchGestureUpdatedEventArgs(GestureStatus.Started, 1, initialScalePoint));
			}
			IsPinching = true;
		}

		public event EventHandler<PinchGestureUpdatedEventArgs> PinchUpdated;
	}
}