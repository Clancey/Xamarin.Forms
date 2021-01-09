using System;
using System.Graphics;

namespace Xamarin.Forms
{
	public interface IScrollViewController : ILayoutController
	{
		PointF GetScrollPositionForElement(VisualElement item, ScrollToPosition position);

		event EventHandler<ScrollToRequestedEventArgs> ScrollToRequested;

		void SendScrollFinished();

		void SetScrolledPosition(float x, float y);

		RectangleF LayoutAreaOverride { get; set; }
	}
}