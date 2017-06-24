using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VästeråsSnooker.Models.ViewModels
{
    public class GameListViewModel : IGameListViewModel
    {
        public List<GameViewModel> AllGames { get; set; }
        public Dictionary<int, string> AllPlayers { get; set; }
    }
}