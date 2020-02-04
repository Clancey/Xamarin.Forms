using System;
using Xamarin.Forms;

namespace DotNetUI
{
	public interface IViewRenderer //: IDisposable
	{
		void SetView (IView view);
		void UpdateValue (string property, object value);
		void Remove (IView view);
		object NativeView { get; }
		Size GetIntrinsicSize (Size availableSize);
		void SetFrame (Rectangle frame);
	}
}
