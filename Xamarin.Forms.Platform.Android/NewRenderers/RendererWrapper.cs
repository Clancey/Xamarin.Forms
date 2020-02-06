using System;
using System.ComponentModel;
using Android.Content;
using Android.Views;

namespace Xamarin.Forms.Platform.Android.NewRenderers
{
	public class RendererWrapper<T> : IVisualElementRenderer where T : DotNetUI.IViewRenderer
	{
		T Renderer;
		public RendererWrapper(Context context)
		{
			Renderer = (T)Activator.CreateInstance(typeof(T), context);

		}

		VisualElementTracker _visualElementTracker;
		VisualElement Element;
		VisualElement IVisualElementRenderer.Element => Element;

		public VisualElementTracker Tracker => _visualElementTracker ?? ( _visualElementTracker = new VisualElementTracker(this));

		public ViewGroup ViewGroup => null;

		public global::Android.Views.View View => Renderer.NativeView as global::Android.Views.View;

		public event EventHandler<VisualElementChangedEventArgs> ElementChanged;
		public event EventHandler<PropertyChangedEventArgs> ElementPropertyChanged;

		public void Dispose()
		{
			if (Element != null)
				Element.PropertyChanged -= Element_PropertyChanged;
		}

		public SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
			=> Renderer.GetDesiredSize(widthConstraint, heightConstraint);

		public void SetElement(VisualElement element)
		{
			if (Element != null)
				Element.PropertyChanged -= Element_PropertyChanged;
			ElementChanged?.Invoke(this, new VisualElementChangedEventArgs(Element, element));
			Element = element;
			if (Element != null)
				Element.PropertyChanged += Element_PropertyChanged;
			Renderer.SetView((DotNetUI.IView)element);
		}

		private void Element_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{ 
			Renderer.UpdateValue(e.PropertyName);
			ElementPropertyChanged?.Invoke(this, e);
		}

		public void SetLabelFor(int? id)
		{
			throw new NotImplementedException();
		}

		public void UpdateLayout()
			=> Renderer.SetFrame(Element.Bounds);

	}
}
