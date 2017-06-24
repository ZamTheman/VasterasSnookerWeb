using System.Collections.Generic;
using VästeråsSnooker.Models.DataModels;

namespace VästeråsSnooker.BL
{
    public interface IGamesRepository
    {
        List<Game> GetAllGames();
        int CreateGame(IGame game);
    }
}