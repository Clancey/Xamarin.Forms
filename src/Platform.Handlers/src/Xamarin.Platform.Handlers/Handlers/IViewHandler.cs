using System.Graphics;
using Xamarin.Forms;

namespace Xamarin.Platform
{
	public interface IViewHandler
	{
		void SetVirtualView(IView view);
		void UpdateValue(string property);
		void DisconnectHandler();
		object? NativeView { get; }
		bool HasContainer { get; set; }
		ContainerView ContainerView { get; }
		SizeF GetDesiredSize(float widthConstraint, float heightConstraint);
		void SetFrame(RectangleF frame);
	}
}