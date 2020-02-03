using System;
using Foundation;
using AppKit;

namespace DotNetUI.Platform
{
	public static class ViewExtensions
	{
		public static void SetText(this NSTextField label, string text)
			=> label.StringValue = text;

		public static void SetText(this NSTextField label, NSAttributedString text)
			=> label.AttributedStringValue = text;

		public static void SetBackgroundColor (this NSView view, NSColor color)
			=> view.Layer.BackgroundColor = color.CGColor;

		public static NSColor GetBackgroundColor (this NSView view) =>
			NSColor.FromCGColor(view.Layer.BackgroundColor);
	}
}
