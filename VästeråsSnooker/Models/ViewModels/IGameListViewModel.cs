using System.Collections.Generic;

namespace VästeråsSnooker.Models.ViewModels
{
    public interface IGameListViewModel
    {
        List<GameViewModel> AllGames { get; set; }
        Dictionary<int, string> AllPlayers { get; set; }
    }
}