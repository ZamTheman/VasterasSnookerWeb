using System.Collections.Generic;
using VästeråsSnooker.Models.DataModels;

namespace VästeråsSnooker.Models.ViewModels
{
    public class ViewPlayersViewModel : IViewPlayersViewModel
    {
        public List<IPlayer> AllPlayers { get; set; }
        public IPlayer ActivePlayer { get; set; }
    }
}