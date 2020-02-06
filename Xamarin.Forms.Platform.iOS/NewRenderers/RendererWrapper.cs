using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

namespace Xamarin.Forms.Platform.iOS.NewRenderers
{
	public class RendererWrapper<T> : IVisualElementRenderer where T : DotNetUI.IViewRenderer , new ()
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
			if (Element != null)
				Element.PropertyChanged -= Element_PropertyChanged;
		}

		SizeRequest IVisualElementRenderer.GetDesiredSize(double widthConstraint, double heightConstraint)
			=> Renderer.GetDesiredSize(widthConstraint, heightConstraint);

		void IVisualElementRenderer.SetElement(VisualElement element)
		{
			if(Element != null)
				Element.PropertyChanged -= Element_PropertyChanged;
			ElementChanged?.Invoke(this, new VisualElementChangedEventArgs(Element, element));
			Element = element;
			if (Element != null)
				Element.PropertyChanged += Element_PropertyChanged;
			Renderer.SetView((DotNetUI.IView)element);
		}

		private void Element_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
				=> Renderer.UpdateValue(e.PropertyName);

		void IVisualElementRenderer.SetElementSize(Size size) => Renderer.SetFrame(new Rectangle(Point.Zero, size));

	}
}