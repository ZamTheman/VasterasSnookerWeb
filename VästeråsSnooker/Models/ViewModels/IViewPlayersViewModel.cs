using System.Collections.Generic;
using VästeråsSnooker.Models.DataModels;

namespace VästeråsSnooker.Models.ViewModels
{
    public interface IViewPlayersViewModel
    {
        List<IPlayer> AllPlayers { get; set; }
        IPlayer ActivePlayer { get; set; }
    }
}