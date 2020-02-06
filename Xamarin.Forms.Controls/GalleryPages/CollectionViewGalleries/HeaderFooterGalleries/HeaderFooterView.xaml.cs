using Xamarin.Forms.Xaml;

namespace Xamarin.Forms.Controls.GalleryPages.CollectionViewGalleries.HeaderFooterGalleries
{
#if XAMLC
	[XamlCompilation(XamlCompilationOptions.Compile)]
#endif
	public partial class HeaderFooterView : ContentPage
	{
		readonly DemoFilteredItemSource _demoFilteredItemSource = new DemoFilteredItemSource(3);

		public HeaderFooterView()
		{
			InitializeComponent();

			CollectionView.ItemTemplate = ExampleTemplates.PhotoTemplate();
			CollectionView.ItemsSource = _demoFilteredItemSource.Items;
		}
	}
}