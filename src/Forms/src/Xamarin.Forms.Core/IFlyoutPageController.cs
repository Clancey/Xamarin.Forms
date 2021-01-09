using System;
using System.Graphics;

namespace Xamarin.Forms
{
	public interface IFlyoutPageController
	{
		bool CanChangeIsPresented { get; set; }

		RectangleF DetailBounds { get; set; }

		RectangleF FlyoutBounds { get; set; }

		bool ShouldShowSplitMode { get; }

		void UpdateFlyoutLayoutBehavior();

		event EventHandler<BackButtonPressedEventArgs> BackButtonPressed;
	}

	[Obsolete("IMasterDetailPageController is obsolete as of version 5.0.0. Please use IFlyoutPageController instead.")]
	public interface IMasterDetailPageController
	{
		bool CanChangeIsPresented { get; set; }

		RectangleF DetailBounds { get; set; }

		RectangleF MasterBounds { get; set; }

		bool ShouldShowSplitMode { get; }

		void UpdateMasterBehavior();

		event EventHandler<BackButtonPressedEventArgs> BackButtonPressed;
	}
}
