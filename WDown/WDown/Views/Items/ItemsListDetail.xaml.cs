using System;
using System.Collections.Generic;

using Xamarin.Forms;
using WDown.Models;
using WDown.ViewModels;

namespace WDown.Views.Items
{
    public partial class ItemsListDetail : ContentPage
    {
        private ItemDetailViewModel _viewModel;

        public ItemsListDetail(ItemDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        public ItemsListDetail()
        {
            InitializeComponent();

            var data = new Item();

            _viewModel = new ItemDetailViewModel(data);
            BindingContext = _viewModel;
        }


        
    }
}
