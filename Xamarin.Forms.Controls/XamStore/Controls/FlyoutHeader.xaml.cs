using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Controls.XamStore
{
#if XAMLC
	[XamlCompilation(XamlCompilationOptions.Compile)]
#endif
	public partial class FlyoutHeader : ContentView
	{
		public FlyoutHeader ()
		{
			InitializeComponent ();
		}
	}
}