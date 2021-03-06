﻿using WDown.Models;
using WDown.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Items
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ItemDeletePage : ContentPage
	{
	    // ReSharper disable once NotAccessedField.Local
	    private ItemDetailViewModel _viewModel;

        public Item Data { get; set; }

        public ItemDeletePage (ItemDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Delete " + viewModel.Title;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }
        // Handles when user clicks delete button
	    private async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteData", Data);

            // Remove Item Details Page manualy
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }

        // Handles when user clicks cancel button
	    private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}