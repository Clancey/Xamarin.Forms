using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Xamarin.Forms.Platform.iOS.NewRenderers
{
	class RendererWrapper<T> : IVisualElementRenderer where T : DotNetUI.IViewRenderer , new ()
	{
		T Renderer = new T();

		VisualElement Element;
		VisualElement IVisualElementRenderer.Element => Element;

		UIView IVisualElementRenderer.NativeView => Renderer.NativeView as UIView;

		UIViewController IVisualElementRenderer.ViewController => null;

		public event EventHandler<VisualElementChangedEventArgs> ElementChanged;

		void IDisposable.Dispose()
		{
			//Renderer.Dispose();
		}

		SizeRequest IVisualElementRenderer.GetDesiredSize(double widthConstraint, double heightConstraint)
			=> new SizeRequest(Renderer.GetIntrinsicSize(new Size(widthConstraint, heightConstraint)));

		void IVisualElementRenderer.SetElement(VisualElement element)
		{
			ElementChanged?.Invoke(this, new VisualElementChangedEventArgs(Element, element));
			Element = element;
			Renderer.SetView((DotNetUI.IView)element);
		}

		void IVisualElementRenderer.SetElementSize(Size size) => Renderer.SetFrame(new Rectangle(Point.Zero, size));
	}
}