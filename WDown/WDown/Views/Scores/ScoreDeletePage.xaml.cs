using WDown.Models;
using WDown.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Scores

{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreDeletePage : ContentPage
    {
        //this is the code behind for the delete page for score
        private ScoreDetailViewModel _viewModel;

        public Score Data { get; set; }

        public ScoreDeletePage(ScoreDetailViewModel viewModel)
        {
            // Save off the item
            Data = viewModel.Data;
            viewModel.Title = "Delete " + viewModel.Title;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        //DELETES CURRENT SCORE AND TAKES YOU BACK TWO PAGES
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteData", Data);

            // Remove Item Details Page manualy
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }

        //CANCELS OPERATION, HANDELS CANCEL CLICKED
        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
