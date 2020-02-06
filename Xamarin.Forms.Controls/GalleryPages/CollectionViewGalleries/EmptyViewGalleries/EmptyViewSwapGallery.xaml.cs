﻿using System;
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
	public partial class EmptyViewSwapGallery : ContentPage
	{
		readonly DemoFilteredItemSource _demoFilteredItemSource = new DemoFilteredItemSource();

		public EmptyViewSwapGallery()
		{
			InitializeComponent();

			CollectionView.ItemTemplate = ExampleTemplates.PhotoTemplate();

			CollectionView.ItemsSource = _demoFilteredItemSource.Items;

			SearchBar.TextChanged += SearchBarTextChanged;

			EmptyViewSwitch.Toggled += EmptyViewSwitchToggled;

			SwitchEmptyView();
		}

		private void SearchBarTextChanged(object sender, TextChangedEventArgs e)
		{
			_demoFilteredItemSource.FilterItems(SearchBar.Text);
		}

		private void EmptyViewSwitchToggled(object sender, ToggledEventArgs e)
		{
			SwitchEmptyView();
		}

		void SwitchEmptyView()
		{
			CollectionView.EmptyView = EmptyViewSwitch.IsToggled
			   ? Resources["EmptyView1"]
			   : Resources["EmptyView2"];
		}
	}
}