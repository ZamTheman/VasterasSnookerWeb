using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VästeråsSnooker.Models.ViewModels
{
    public interface IGameViewModel
    {
        [Required]
        int AntalFrames { get; set; }
        [DataType(DataType.Date)]
        DateTime Datum { get; set; }
        [Required]
        List<int> FrameResultat { get; set; }
        int HogstaSerieSpelare1 { get; set; }
        int HogstaSerieSpelare2 { get; set; }
        int Id { get; set; }
        string MatchReferat { get; set; }
        [Required]
        string Spelare1 { get; set; }
        [Required]
        string Spelare2 { get; set; }
        IEnumerable<SelectListItem> SpelareAttVäljaMellan { get; set; }
        string Vinnare { get; set; }
    }
}