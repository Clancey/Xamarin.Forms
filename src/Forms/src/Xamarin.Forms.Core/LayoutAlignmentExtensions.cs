using System;

namespace Xamarin.Forms
{
	internal static class LayoutAlignmentExtensions
	{
		public static float ToFloat(this LayoutAlignment align)
		{
			switch (align)
			{
				case LayoutAlignment.Start:
					return 0;
				case LayoutAlignment.Center:
					return 0.5f;
				case LayoutAlignment.End:
					return 1;
			}
			throw new ArgumentOutOfRangeException("align");
		}
	}
}