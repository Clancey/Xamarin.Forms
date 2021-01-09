using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms.Controls.TabIndexTest
{
	public static class Colors
	{
		public static Color Cerulean = Colors.FromRgb(0, 115, 209);
		public static Color Gray = Colors.FromRgb(216, 221, 230);
		public static Color White = Colors.White;
		public static Color Black = Colors.Black;
	}
	[Flags]
	public enum DaysOfWeek
	{
		None = 0,
		Sunday = 1 << 0,
		Monday = 1 << 1,
		Tuesday = 1 << 2,
		Wednesday = 1 << 3,
		Thursday = 1 << 4,
		Friday = 1 << 5,
		Saturday = 1 << 6,
	}
}
