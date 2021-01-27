using System;
namespace Xamarin.Platform.Core
{
	public interface IReplaceableView
	{
		IView ReplacedView { get; }
	}
}
