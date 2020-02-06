using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Controls.GalleryPages.CollectionViewGalleries.GroupingGalleries
{
#if XAMLC
	[XamlCompilation(XamlCompilationOptions.Compile)]
#endif
	public partial class GridGrouping : ContentPage
	{
		public GridGrouping()
		{
			InitializeComponent();
			CollectionView.ItemsSource = new SuperTeams();
		}
	}
}