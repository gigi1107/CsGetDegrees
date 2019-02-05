using System;
using week2.Models;

using Xamarin.Forms;

namespace week2.ViewModels
{
    public class CharacterDetailViewModel : BaseViewModel
    {
            public Character Data { get; set; }
            public CharacterDetailViewModel(Character data = null)
            {
                Title = data?.Id;
                Data = data;
            }

    }
}

