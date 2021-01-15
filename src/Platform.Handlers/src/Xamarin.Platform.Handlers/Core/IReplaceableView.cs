using System;
namespace Xamarin.Platform
{
	public interface IReplaceableView
	{
		IView ReplacedView { get; }
	}
}
