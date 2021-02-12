using System;
using UIKit;
using Xamarin.Platform.HotReload;

namespace Xamarin.Platform.Platform.iOS
{
	public class ContainerViewController : UIViewController, IReloadHandler
	{
		IView? _view;
		public IView? CurrentView
		{
			get => _view;
			set => SetView(value);
		}

		UIView? currentNativeView;
		void SetView(IView? view, bool forceRefresh = false)
		{
			if (view == _view && !forceRefresh)
				return;
			_view = view;
			if (_view is IHotReloadableView ihr)
				ihr.ReloadHandler = this;
			currentNativeView?.RemoveFromSuperview();
			currentNativeView = null;
			if(IsViewLoaded && _view != null)
			{
				View!.AddSubview(currentNativeView = _view.ToNative());
			}
		}

		public override void LoadView()
		{
			base.LoadView();
			if(_view  != null)
				View!.AddSubview(currentNativeView = _view.ToNative());
		}
		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();
			if (currentNativeView == null)
				return;
			currentNativeView.Frame = View!.Bounds;
		}

		public void Reload() => SetView(CurrentView, true);
	}
}
