using System;
using Xamarin.Platform.Shapes;

namespace Xamarin.Platform
{
	public interface IClipShapeView
	{
		IShape ClipShape { get; }
	}
}