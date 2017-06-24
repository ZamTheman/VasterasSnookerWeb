using System.Collections.Generic;
using VästeråsSnooker.Models.DataModels;

namespace VästeråsSnooker.BL
{
    public interface IPlayerRepository
    {
        int AddPlayerToDb(IPlayer player);
        List<IPlayer> GetAllPlayers();
        IPlayer GetPlayerById(int id);
    }
}
