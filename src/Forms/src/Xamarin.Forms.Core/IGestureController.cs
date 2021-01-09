using System.Collections.Generic;
using System.Graphics;

namespace Xamarin.Forms.Internals
{
	public interface IGestureController
	{
		IList<GestureElement> GetChildElements(PointF point);

		IList<IGestureRecognizer> CompositeGestureRecognizers { get; }
	}
}