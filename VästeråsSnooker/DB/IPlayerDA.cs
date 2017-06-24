using System.Collections.Generic;
using V�ster�sSnooker.Models.DataModels;

namespace V�ster�sSnooker.DB
{
    public interface IPlayerDA
    {
        int AddPlayerToDb(IPlayer player);
        List<IPlayer> GetAllPlayers();
        IPlayer GetPlayerById (int id);
    }
}