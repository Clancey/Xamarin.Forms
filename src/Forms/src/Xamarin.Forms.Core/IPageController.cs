using System.Collections.ObjectModel;
using System.Graphics;

namespace Xamarin.Forms
{
	public interface IPageController : IVisualElementController
	{
		RectangleF ContainerArea { get; set; }

		bool IgnoresContainerArea { get; set; }

		ObservableCollection<Element> InternalChildren { get; }

		void SendAppearing();

		void SendDisappearing();
	}
}