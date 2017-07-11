namespace VästeråsSnooker.Models.DataModels
{
    public class TournamentGame : Game
    {
        public int TournamentId { get; set; }
        public bool IsPlayed { get; set; }
    }
}