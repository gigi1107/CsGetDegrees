using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

using System.Linq;
using System.Text;
using System.Threading.Tasks;


using WDown.GameEngine;
using WDown.ViewModels;
using WDown.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WDown.Views.Battle
{
    // This page populates the Monster Battle List View before entering the BattleMainPage
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BattleMonsterListPage : ContentPage
        {
            public List<WDown.Models.Monster> Datalist = new List<WDown.Models.Monster>();

        private BattleViewModel _viewModel;


        // Initialize page and populate buttons
        public BattleMonsterListPage()
            {
                InitializeComponent();
              

            MessagingCenter.Send(this, "StartBattle");
            Debug.WriteLine("Battle Start" + " Characters :" + BattleViewModel.Instance.BattleEngine.CharacterList.Count);

            // Load the Characters into the Battle Engine
            MessagingCenter.Send(this, "LoadCharacters");

            // Start the Round
            MessagingCenter.Send(this, "StartRound");
            Debug.WriteLine("Round Start" + " Monsters:" + BattleViewModel.Instance.BattleEngine.MonsterList.Count);

            foreach (Models.Monster Monster in BattleViewModel.Instance.BattleEngine.MonsterList)
            {
                Debug.WriteLine("Round Start" + " Monster List:" + Monster.Name);
            }
            Debug.WriteLine("round start observable collection monster list: ");
            foreach (Models.Monster Monster in BattleViewModel.Instance.FightingMonsters)
            {
                Debug.WriteLine("Round Start" + " Monster List:" + Monster.Name);
            }

            _viewModel = BattleViewModel.Instance;

            Datalist = BattleViewModel.Instance.BattleEngine.MonsterList;
            BindingContext = Datalist;

            foreach (var data in Datalist)
                {
                    var myHP = new Label()
                    {
                        Text = "HP : " + data.GetHealthCurrent(),
                        TextColor = Color.Black,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    };

                    var myLevel = new Label()
                    {
                        Text = "Level : " + data.Level,
                        TextColor = Color.Black,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    };

                    var myName = new Label()
                    {
                        Text = data.Name,
                        TextColor = Color.Black,
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    };

                    var myURI = "Troll2.png";

                    if (!string.IsNullOrEmpty(data.ImageURI))
                    {
                        myURI = data.ImageURI;
                    }

                    var myImage = new Image()
                    {
                        //Source = FileImageSource.FromUri(new Uri(myURI)),
                        Source = myURI,
                        WidthRequest = 50.0,
                        HeightRequest = 50.0
                    };

                    #region Relative
                    //RelativeLayout myInnerBox = new RelativeLayout();

                    //myInnerBox.Children.Add(myImage,
                    //    Constraint.Constant(0),
                    //    Constraint.Constant(0),
                    //    Constraint.RelativeToParent((parent) => { return parent.Width; }),
                    //    Constraint.RelativeToParent((parent) => { return parent.Height; }));

                    //myInnerBox.Children.Add(myLevel,
                    //    Constraint.Constant(0),
                    //    Constraint.Constant(0),
                    //    Constraint.RelativeToParent((parent) => { return parent.Width; }),
                    //    Constraint.RelativeToParent((parent) => { return parent.Height; }));

                    //myInnerBox.Children.Add(myHP,
                    //    Constraint.Constant(0),
                    //    Constraint.Constant(0),
                    //    Constraint.RelativeToParent((parent) => { return parent.Width; }),
                    //    Constraint.RelativeToParent((parent) => { return parent.Height; }));
                    #endregion Relative

                    StackLayout OuterFrame = new StackLayout
                    {
                        MinimumWidthRequest = 400,
                        WidthRequest = 400,
                        Padding = 10
                    };

                    OuterFrame.Children.Add(myImage);
                    OuterFrame.Children.Add(myName);
                    OuterFrame.Children.Add(myLevel);
                    OuterFrame.Children.Add(myHP);

                    MonsterListFrame.Children.Add(OuterFrame);
                }
            }
        // This starts the Character Select Page    
        private async void OnNextClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WDown.Views.Battle.BattleMainPage(_viewModel));
        }

    }
    }