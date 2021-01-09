using System;
using System.Collections.Generic;
using System.Graphics;
using Xamarin.Forms;

namespace Xamarin.Platform.Layouts
{
	public class HorizontalStackLayoutManager : StackLayoutManager
	{
		public HorizontalStackLayoutManager(IStackLayout layout) : base(layout)
		{
		}

		public override SizeF Measure(float widthConstraint, float heightConstraint)
		{
			var heightMeasureConstraint = ResolveConstraints(heightConstraint, Stack.Height);

			var measure = Measure(heightMeasureConstraint, Stack.Spacing, Stack.Children);

			var finalWidth = ResolveConstraints(widthConstraint, Stack.Width, measure.Width);

			return new SizeF(finalWidth, measure.Height);
		}

		public override void Arrange(RectangleF bounds) => Arrange(Stack.Spacing, Stack.Children);

		static SizeF Measure(float heightConstraint, int spacing, IReadOnlyList<IView> views)
		{
			float totalRequestedWidth = 0;
			float requestedHeight = 0;

			foreach (var child in views)
			{
				var measure = child.IsMeasureValid ? child.DesiredSize : child.Measure(float.PositiveInfinity, heightConstraint);
				totalRequestedWidth += measure.Width;
				requestedHeight = Math.Max(requestedHeight, measure.Height);
			}

			var accountForSpacing = MeasureSpacing(spacing, views.Count);
			totalRequestedWidth += accountForSpacing;

			return new SizeF(totalRequestedWidth, requestedHeight);
		}

		static void Arrange(int spacing, IEnumerable<IView> views)
		{
			float stackWidth = 0;

			foreach (var child in views)
			{
				var destination = new RectangleF(stackWidth, 0, child.DesiredSize.Width, child.DesiredSize.Height);
				child.Arrange(destination);

				stackWidth += destination.Width + spacing;
			}
		}
	}
}
