using System.ComponentModel.DataAnnotations;
using System.Web;

namespace VästeråsSnooker.Models.ViewModels
{
    public interface ICreatePlayerViewModel
    {
        [Required]
        string Name { get; set; }
        HttpPostedFileBase ImageUpload { get; set; }
    }
}