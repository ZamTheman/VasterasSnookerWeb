using System;

namespace VästeråsSnooker.Models.DataModels
{
    public class Break
    {
        public int Id { get; set; }
        public int Spelare { get; set; }
        public DateTime Datum { get; set; }
        public int Serie { get; set; }
    }
}