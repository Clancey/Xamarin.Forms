using System;
using Xamarin.Forms;
using Xamarin.Platform;
using Xamarin.Platform.Core;
using Xamarin.Platform.HotReload;

namespace Sample
{
	public abstract class View : FrameworkElement, IView, IHotReloadableView
	{
		public View()
		{
			HotReloadHelper.AddActiveView(this);
		}

		public IReloadHandler ReloadHandler { get; set; }
		IView IReplaceableView.ReplacedView => HotReloadHelper.GetReplacedView(this) ?? this;

		void IHotReloadableView.TransferState(IView newView)
		{
			//TODO: Let you hot reload the the ViewModel
			//if (newView is View v)
			//	v.BindingContext = BindingContext;
		}

		void IHotReloadableView.Reload()
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				this.CheckHandlers();
				//Handler = null;
				if (ReloadHandler != null)
				{
					ReloadHandler.Reload();
					return;
				}
//				var handler = Handler;
//				Handler = null;
//				var replacedView = HotReloadHelper.GetReplacedView(this) ?? this;
//#if __IOS__
//				var native = replacedView.ToNative();
//#endif
			});
		}
	}
}
