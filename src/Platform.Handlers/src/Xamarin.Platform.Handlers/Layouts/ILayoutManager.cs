using System.Graphics;
using Xamarin.Forms;

namespace Xamarin.Platform.Layouts
{
	public interface ILayoutManager
	{
		SizeF Measure(float widthConstraint, float heightConstraint);
		void Arrange(RectangleF bounds);
	}
}
