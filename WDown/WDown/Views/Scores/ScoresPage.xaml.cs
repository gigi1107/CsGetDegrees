using System;
using System.Collections.Generic;
using WDown.ViewModels;
using WDown.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace WDown.Views.Scores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ScoresPage : ContentPage
    {
        private ScoresViewModel _viewModel;
        public ScoresPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = ScoresViewModel.Instance;

        }

        private async void OnScoreSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var data = args.SelectedItem as Score;
            if (data == null)
                return;

            await Navigation.PushAsync(new ScoreDetailPage(new ScoreDetailViewModel(data)));

        }

        private async void AddScore_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoresNewPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BindingContext = null;

            if (ToolbarItems.Count > 0)
            {
                ToolbarItems.RemoveAt(0);
            }

            InitializeComponent();

            if (_viewModel.Dataset.Count == 0)
            {

                _viewModel.LoadDataCommand.Execute(null);
            }
            else if (_viewModel.NeedsRefresh())
            {
                _viewModel.LoadDataCommand.Execute(null);
            }

            BindingContext = _viewModel;
        }
    }
}

