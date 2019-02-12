using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.ViewModels;
using WDown.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Monster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterDeletePage : ContentPage
    {
        private MonsterDetailViewModel _viewModel;

        public WDown.Models.Monster Data { get; set; }

        public MonsterDeletePage(MonsterDetailViewModel viewModel)
        {
            // Save the item
            Data = viewModel.Data;
            viewModel.Title = "Delete " + viewModel.Data.Name;

            InitializeComponent();

            // Set the data binding for the page
            BindingContext = _viewModel = viewModel;
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "DeleteData", Data);

            // Remove Monster Details Page manualy
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);

            await Navigation.PopAsync();
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

    }
}

