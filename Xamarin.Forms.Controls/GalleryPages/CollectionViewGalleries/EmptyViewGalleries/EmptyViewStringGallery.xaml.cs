using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Controls.GalleryPages.CollectionViewGalleries.EmptyViewGalleries
{
#if XAMLC
	[XamlCompilation(XamlCompilationOptions.Compile)]
#endif
	public partial class EmptyViewStringGallery : ContentPage
	{
		readonly DemoFilteredItemSource _demoFilteredItemSource = new DemoFilteredItemSource();

		public EmptyViewStringGallery()
		{
			InitializeComponent();

			CollectionView.ItemTemplate = ExampleTemplates.PhotoTemplate();

			CollectionView.ItemsSource = _demoFilteredItemSource.Items;

			SearchBar.SearchCommand = new Command(() => _demoFilteredItemSource.FilterItems(SearchBar.Text));
		}
	}
}