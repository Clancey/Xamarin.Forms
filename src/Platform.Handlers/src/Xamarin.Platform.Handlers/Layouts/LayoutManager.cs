using System;
using System.Graphics;
using Xamarin.Forms;

namespace Xamarin.Platform.Layouts
{
	public abstract class LayoutManager : ILayoutManager
	{
		public LayoutManager(ILayout layout)
		{
			Layout = layout;
		}

		public ILayout Layout { get; }

		public abstract SizeF Measure(float widthConstraint, float heightConstraint);
		public abstract void Arrange(RectangleF bounds);

		public static float ResolveConstraints(float externalConstraint, float desiredLength)
		{
			if (desiredLength == -1)
			{
				return externalConstraint;
			}

			return Math.Min(externalConstraint, desiredLength);
		}

		public static float ResolveConstraints(float externalConstraint, float desiredLength, float measuredLength)
		{
			if (desiredLength == -1)
			{
				// No user-specified length, so the measured value will be limited by the external constraint
				return Math.Min(measuredLength, externalConstraint);
			}

			// User-specified length wins, subject to external constraints
			return Math.Min(desiredLength, externalConstraint);
		}
	}
}
