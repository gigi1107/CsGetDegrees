using WDown.Models;

namespace WDown.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public Item Data { get; set; }
        public ItemDetailViewModel(Item data = null)
        {
            Title = data?.Name;
            Data = data;
        }
    }
}
