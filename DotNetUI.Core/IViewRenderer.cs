using System;
using Xamarin.Forms;

namespace DotNetUI
{
	public interface IViewRenderer //: IDisposable
	{
		void SetView (IView view);
		void UpdateValue (string property);
		void Remove (IView view);
		object NativeView { get; }
		SizeRequest GetDesiredSize(double widthConstraint, double heightConstraint);
		void SetFrame (Rectangle frame);
	}
}
