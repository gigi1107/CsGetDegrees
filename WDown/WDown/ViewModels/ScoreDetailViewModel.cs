
using WDown.Models;

// VM for Score Detail Page
namespace WDown.ViewModels
{
    public class ScoreDetailViewModel : BaseViewModel
    {
        public Score Data { get; set; }
        public ScoreDetailViewModel(Score data = null)
        {
            Title = data?.Name;
            Data = data;
        }
    }
}