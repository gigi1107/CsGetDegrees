using WDown.Models;

namespace WDown.ViewModels
{
    // VM for Monster Detail Page
    public class MonsterDetailViewModel : BaseViewModel
    {
        public Monster Data { get; set; }
        public MonsterDetailViewModel(Monster data = null)
        {
            Title = data?.Name;
            Data = data;
        }
    }
}
