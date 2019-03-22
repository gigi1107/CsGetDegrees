using WDown.Models;

namespace WDown.ViewModels
{
    public class CharacterDetailViewModel : BaseViewModel
    {
        // VM for Character Detail Page
        public Character Data { get; set; }
        public CharacterDetailViewModel(Character data = null)
        {
            Title = data?.Name;
            Data = data;
        }
    }
}
