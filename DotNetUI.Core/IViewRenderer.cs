using System;
using System.Drawing;

namespace DotNetUI
{
	public interface IViewRenderer //: IDisposable
	{
		void SetView (IView view);
		void UpdateValue (string property, object value);
		void Remove (IView view);
		object NativeView { get; }
		SizeF GetIntrinsicSize (SizeF availableSize);
		void SetFrame (RectangleF frame);
	}
}
