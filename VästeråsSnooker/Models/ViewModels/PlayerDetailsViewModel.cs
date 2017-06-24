
using System.Web.UI.WebControls;

namespace VästeråsSnooker.Models.ViewModels
{
    public class PlayerDetailsViewModel : IPlayerDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
    }
}