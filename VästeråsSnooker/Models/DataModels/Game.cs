using System;

namespace VästeråsSnooker.Models.DataModels
{
    public class Game : IGame
    {
        public int Id { get; set; }
        public int AntalFrames { get; set; }
        public DateTime Datum { get; set; }
        public string FrameResultat { get; set; }
        public int HogstaSerieSpelare1 { get; set; }
        public int HogstaSerieSpelare2 { get; set; }
        public string MatchReferat { get; set; }
        public int Spelare1 { get; set; }
        public int Spelare2 { get; set; }
        public int Vinnare { get; set; }
    }
}