using System.Collections.Generic;
using System.Graphics;
using NSubstitute;
using Xamarin.Forms;

namespace Xamarin.Platform.Handlers.UnitTests.Layouts
{
	public abstract class StackLayoutManagerTests
	{
		protected IStackLayout CreateTestLayout()
		{
			var stack = Substitute.For<IStackLayout>();
			stack.Height.Returns(-1);
			stack.Width.Returns(-1);
			stack.Spacing.Returns(0);

			return stack;
		}

		protected IView CreateTestView() 
		{
			var view = Substitute.For<IView>();

			view.Height.Returns(-1);
			view.Width.Returns(-1);

			return view;
		}

		protected IView CreateTestView(SizeF viewSize)
		{
			var view = CreateTestView();

			view.Measure(Arg.Any<float>(), Arg.Any<float>()).Returns(viewSize);
			view.DesiredSize.Returns(viewSize);

			return view;
		}

		protected IStackLayout BuildStack(int viewCount, float viewWidth, float viewHeight)
		{
			var stack = CreateTestLayout();

			var children = new List<IView>();

			for (int n = 0; n < viewCount; n++)
			{
				var view = CreateTestView(new SizeF(viewWidth, viewHeight));
				children.Add(view);
			}

			stack.Children.Returns(children.AsReadOnly());

			return stack;
		}
	}
}
