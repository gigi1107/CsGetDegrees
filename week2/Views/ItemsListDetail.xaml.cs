using System;
using System.Collections.Generic;

using Xamarin.Forms;
using week2.Models;
using week2.ViewModels;

namespace week2.Views
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
