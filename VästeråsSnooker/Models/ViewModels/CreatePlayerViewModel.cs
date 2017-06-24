using System.ComponentModel.DataAnnotations;
using System.Web;

namespace VästeråsSnooker.Models.ViewModels
{
    public class CreatePlayerViewModel : ICreatePlayerViewModel
    {
        [Required]
        public string Name { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}