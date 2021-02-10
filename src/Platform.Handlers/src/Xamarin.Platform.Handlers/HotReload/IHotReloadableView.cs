using System;
using Xamarin.Platform.Core;
namespace Xamarin.Platform.HotReload
{
	public interface IHotReloadableView : IReplaceableView, IView
	{
		void TransferState(IView newView);
		void Reload();
	}
}
