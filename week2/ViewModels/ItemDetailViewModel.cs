using System;

using week2.Models;

namespace week2.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {

            Item = item;
        }
    }
}
