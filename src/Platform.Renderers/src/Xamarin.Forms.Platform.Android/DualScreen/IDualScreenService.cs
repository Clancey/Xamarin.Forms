using System;
using System.Graphics;
using System.Threading.Tasks;

namespace Xamarin.Forms.Platform.Android.DualScreen
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
		bool IsDualScreenDevice { get; }
	}
}
