using week2.Models;
using System;

using Xamarin.Forms;

namespace week2.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Data { get; set; }
        public ItemDetailViewModel(Item data = null)
        {
            Title = data?.Id;
            Data = data;
        }
    }
}

