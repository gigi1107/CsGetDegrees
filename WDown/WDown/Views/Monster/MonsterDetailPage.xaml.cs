using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WDown.ViewModels;
using WDown.Models;
using WDown.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
// This populates the Monster Index view
namespace WDown.Views.Monster
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MonsterDetailPage : ContentPage
    {
        private MonsterDetailViewModel _viewModel;

        public MonsterDetailPage(MonsterDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
           
        }

        public MonsterDetailPage()
        {
            InitializeComponent();

            var data = new WDown.Models.Monster
            {
                // Default values
                Name = "Monster Unknown",
                Description = "This is a monster description.",
                Level = 1
            };

            _viewModel = new MonsterDetailViewModel(data);
            BindingContext = _viewModel;
        }

        // Handles when user choose Edit button
        public async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MonsterEditPage(_viewModel));
        }
        // Handles when user choose Delete button
        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WDown.Views.Monster.MonsterDeletePage(_viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}