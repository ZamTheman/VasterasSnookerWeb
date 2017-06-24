using System.Web.UI.WebControls;

namespace VästeråsSnooker.Models.ViewModels
{
    public interface IPlayerDetailsViewModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string ImageUrl { get; set; }
    }
}