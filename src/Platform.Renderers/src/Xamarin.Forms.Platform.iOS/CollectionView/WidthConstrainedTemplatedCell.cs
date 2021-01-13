﻿using System.Graphics;
using CoreGraphics;
using Foundation;

namespace Xamarin.Forms.Platform.iOS
{
	internal abstract class WidthConstrainedTemplatedCell : TemplatedCell
	{
		[Export("initWithFrame:")]
		[Internals.Preserve(Conditional = true)]
		public WidthConstrainedTemplatedCell(CGRect frame) : base(frame)
		{
		}

		public override void ConstrainTo(CGSize constraint)
		{
			ClearConstraints();
			ConstrainedDimension = constraint.Width;
		}

		protected override (bool, SizeF) NeedsContentSizeUpdate(SizeF currentSize)
		{
			var size = SizeF.Zero;

			if (VisualElementRenderer?.Element == null)
			{
				return (false, size);
			}

			var bounds = VisualElementRenderer.Element.Bounds;

			if (bounds.Width <= 0 || bounds.Height <= 0)
			{
				return (false, size);
			}

			var desiredBounds = VisualElementRenderer.Element.Measure(bounds.Width, float.PositiveInfinity, 
				MeasureFlags.IncludeMargins);

			if (desiredBounds.Request.Height == currentSize.Height)
			{
				// Nothing in the cell needs more room, so leave it as it is
				return (false, size);
			}

			return (true, desiredBounds.Request);
		}
	}
}