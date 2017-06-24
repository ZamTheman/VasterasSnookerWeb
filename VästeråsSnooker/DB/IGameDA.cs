using System.Collections.Generic;
using VästeråsSnooker.Models.DataModels;

namespace VästeråsSnooker.DB
{
    public interface IGameDA
    {
        List<IGame> GetAllGames();
    }
}