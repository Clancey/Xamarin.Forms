using System.Graphics;
using Xamarin.Forms;

namespace Xamarin.Platform
{
	public interface ISlider : IView
	{
		float Minimum { get; }
		float Maximum { get; }
		float Value { get; set; }

		Color MinimumTrackColor { get; }
		Color MaximumTrackColor { get; }
		Color ThumbColor { get; }

		void DragStarted();
		void DragCompleted();
	}
}