using System.Collections.Generic;
using VästeråsSnooker.Models.DataModels;

namespace VästeråsSnooker.DB
{
    public interface IPlayerDA
    {
        int AddPlayerToDb(IPlayer player);
        List<IPlayer> GetAllPlayers();
        IPlayer GetPlayerById (int id);
    }
}