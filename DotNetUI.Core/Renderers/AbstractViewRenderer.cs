using System;
using Xamarin.Forms;
#if __IOS__
using NativeView = UIKit.UIView;
#elif __MACOS__
using NativeView = AppKit.NSView;
#elif MONOANDROID
using NativeView = Android.Views.View;
#elif NETSTANDARD
using NativeView = System.Object;
#endif

namespace DotNetUI.Renderers
{
	public abstract partial class AbstractViewRenderer<TVirtualView, TNativeView> : IViewRenderer
		where TVirtualView : class, IView
#if !NETSTANDARD
		where TNativeView : NativeView
#endif
	{

		protected readonly PropertyMapper<TVirtualView> mapper;

		private TVirtualView _virtualView;
		private TNativeView _nativeView;

		protected abstract TNativeView CreateView();

		public NativeView View => _nativeView;
		public TNativeView TypedNativeView => _nativeView;
		protected TVirtualView VirtualView => _virtualView;
		public object NativeView => _nativeView;
		public virtual void SetView(IView view)
		{
			_virtualView = view as TVirtualView;
			_nativeView = CreateView();
			mapper?.UpdateProperties(this, _virtualView);
		}

		public virtual void Remove(IView view)
		{
			_virtualView = null;
		}

		protected virtual void DisposeView(TNativeView nativeView)
		{

		}

		public virtual void UpdateValue(string property)
			=> mapper?.UpdateProperty(this, _virtualView, property);

		
	}
}
