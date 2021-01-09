using Xamarin.Forms;

namespace Xamarin.Platform
{
	public interface IFont : IView
	{
		FontAttributes FontAttributes { get; }
		string FontFamily { get; }
		float FontSize { get; }
	}
}