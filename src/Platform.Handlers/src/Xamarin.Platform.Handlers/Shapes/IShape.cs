using System;
using System.Graphics;

namespace Xamarin.Platform.Shapes
{
	public interface IShape
	{
		PathF PathForBounds(RectangleF rect);
	}
}
