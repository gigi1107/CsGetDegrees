using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using WDown.Views;
using WDown.Models;
using WDown.ViewModels;
using WDown.Views.Battle;

namespace WDown.Views.Scores
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreDetailPage : ContentPage
    {
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private ScoreDetailViewModel _viewModel;

        List<string> characterAtDeath = new List<string>();
        List<string> monstersDead = new List<string>();
        List<string> itemsDropped = new List<string>();

    

        public ScoreDetailPage(ScoreDetailViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
            foreach(var name in _viewModel.Data.CharacterAtDeathList.Split(','))
            {
                characterAtDeath.Add(name);
            }

            foreach (var name in _viewModel.Data.MonstersKilledList.Split(','))
            {
                monstersDead.Add(name);
            }

            foreach (var name in _viewModel.Data.ItemsDroppedList.Split(','))
            {
                itemsDropped.Add(name);
            }

        }

        public ScoreDetailPage()
        {
            InitializeComponent();

            var data = new Score
            {
                Name = "Score name",
                ScoreTotal = 0
            };

            _viewModel = new ScoreDetailViewModel(data);
            BindingContext = _viewModel;
            setBindingsForLists();
        }

        public void setBindingsForLists()
        {
            CharactersAtDeath.ItemsSource = characterAtDeath;
            MonstersKilled.ItemsSource = monstersDead;
            ItemsDropped.ItemsSource = itemsDropped;
        }

        private async void Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoreEditPage(_viewModel));
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ScoreDeletePage(_viewModel));
        }

        private async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void Return_To_Main_Battle_Screen(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BattleOpeningPage());
        }
    }
}