namespace Xamarin.Forms
{
	public interface IShellContentInsetObserver
	{
		void OnInsetChanged(Thickness inset, float tabThickness);
	}
}