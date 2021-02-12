using System;
using Xamarin.Platform.Core;
namespace Xamarin.Platform.HotReload
{
	public interface IHotReloadableView : IReplaceableView, IView
	{
		IReloadHandler ReloadHandler { get; set; }
		void TransferState(IView newView);
		void Reload();
	}
}
