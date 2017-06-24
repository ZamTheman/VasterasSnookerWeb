using System;

namespace VästeråsSnooker.Models.DataModels
{
    public interface IGame
    {
        int Id { get; set; }
        int AntalFrames { get; set; }
        DateTime Datum { get; set; }
        string FrameResultat { get; set; }
        int HogstaSerieSpelare1 { get; set; }
        int HogstaSerieSpelare2 { get; set; }
        string MatchReferat { get; set; }
        int Spelare1 { get; set; }
        int Spelare2 { get; set; }
        int Vinnare { get; set; }
    }
}