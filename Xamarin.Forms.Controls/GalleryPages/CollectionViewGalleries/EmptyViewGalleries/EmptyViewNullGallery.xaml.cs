using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Controls.GalleryPages.CollectionViewGalleries.EmptyViewGalleries
{
#if XAMLC
	[XamlCompilation(XamlCompilationOptions.Compile)]
#endif
	public partial class EmptyViewNullGallery : ContentPage
	{
		public EmptyViewNullGallery()
		{
			InitializeComponent();
		}
	}
}