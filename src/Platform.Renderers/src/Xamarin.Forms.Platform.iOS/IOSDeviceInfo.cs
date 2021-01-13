using System;
using System.Graphics;
using Foundation;
using UIKit;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Platform.iOS
{
	internal class IOSDeviceInfo : DeviceInfo
	{
		SizeF _pixelScreenSize;
		SizeF _scaledScreenSize;
		float _scalingFactor;
		bool _disposed;
		readonly NSObject _notification;

		public IOSDeviceInfo()
		{
			_notification = UIDevice.Notifications.ObserveOrientationDidChange(OrientationChanged);

			UpdateScreenSize();
		}

		public override SizeF PixelScreenSize => _pixelScreenSize;
		public override SizeF ScaledScreenSize => _scaledScreenSize;

		public override float ScalingFactor => _scalingFactor;

		void UpdateScreenSize()
		{
			_scalingFactor = (float)UIScreen.MainScreen.Scale;

			var boundsWidth = UIScreen.MainScreen.Bounds.Width;
			var boundsHeight = UIScreen.MainScreen.Bounds.Height;

			// We can't rely directly on the MainScreen bounds because they may not have been updated yet
			// But CurrentOrientation is up-to-date, so we can use it to work out the dimensions
			var width = CurrentOrientation.IsLandscape()
				? Math.Max(boundsHeight, boundsWidth)
				: Math.Min(boundsHeight, boundsWidth);

			var height = CurrentOrientation.IsPortrait()
				? Math.Max(boundsHeight, boundsWidth)
				: Math.Min(boundsHeight, boundsWidth);

			_scaledScreenSize = new SizeF((float)width, (float)height);
			_pixelScreenSize = new SizeF(_scaledScreenSize.Width * _scalingFactor, _scaledScreenSize.Height * _scalingFactor);
		}

		void OrientationChanged(object sender, NSNotificationEventArgs args)
		{
			CurrentOrientation = UIDevice.CurrentDevice.Orientation.ToDeviceOrientation();
			UpdateScreenSize();
		}

		protected override void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}

			_disposed = true;

			if (disposing)
			{
				_notification.Dispose();
			}

			base.Dispose(disposing);
		}
	}
}
