using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VästeråsSnooker.Models.ViewModels
{
    public class GameViewModel : IGameViewModel
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }
        public string Spelare1 { get; set; }
        public string Spelare2 { get; set; }
        public string Vinnare { get; set; }
        public IEnumerable<SelectListItem> SpelareAttVäljaMellan { get; set; }
        public int AntalFrames { get; set; }
        public List<int> FrameResultat { get; set; }
        public int HogstaSerieSpelare1 { get; set; }
        public int HogstaSerieSpelare2 { get; set; }
        public string MatchReferat { get; set; }
    }
}