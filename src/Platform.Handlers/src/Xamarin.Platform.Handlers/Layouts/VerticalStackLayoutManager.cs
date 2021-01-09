using System;
using System.Collections.Generic;
using System.Graphics;
using Xamarin.Forms;

namespace Xamarin.Platform.Layouts
{
	public class VerticalStackLayoutManager : StackLayoutManager
	{
		public VerticalStackLayoutManager(IStackLayout stackLayout) : base(stackLayout)
		{
		}

		public override SizeF Measure(float widthConstraint, float heightConstraint)
		{
			var widthMeasureConstraint = ResolveConstraints(widthConstraint, Stack.Width);

			var measure = Measure(widthMeasureConstraint, Stack.Spacing, Stack.Children);

			var finalHeight = ResolveConstraints(heightConstraint, Stack.Height, measure.Height);

			return new SizeF(measure.Width, finalHeight);
		}

		public override void Arrange(RectangleF bounds) => Arrange(Stack.Spacing, Stack.Children);

		static SizeF Measure(float widthConstraint, int spacing, IReadOnlyList<IView> views)
		{
			float totalRequestedHeight = 0;
			float requestedWidth = 0;

			foreach (var child in views)
			{
				var measure = child.IsMeasureValid ? child.DesiredSize : child.Measure(widthConstraint, float.PositiveInfinity);
				totalRequestedHeight += measure.Height;
				requestedWidth = Math.Max(requestedWidth, measure.Width);
			}

			var accountForSpacing = MeasureSpacing(spacing, views.Count);
			totalRequestedHeight += accountForSpacing;

			return new SizeF(requestedWidth, totalRequestedHeight);
		}

		static void Arrange(int spacing, IEnumerable<IView> views)
		{
			float stackHeight = 0;

			foreach (var child in views)
			{
				var destination = new RectangleF(0, stackHeight, child.DesiredSize.Width, child.DesiredSize.Height);
				child.Arrange(destination);
				stackHeight += destination.Height + spacing;
			}
		}

	}
}
