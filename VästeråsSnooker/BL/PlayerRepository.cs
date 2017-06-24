using System.Collections.Generic;
using VästeråsSnooker.DB;
using VästeråsSnooker.Models.DataModels;

namespace VästeråsSnooker.BL
{
    public class PlayerRepository : IPlayerRepository
    {
        private IPlayerDA _playerDa;
        public PlayerRepository(IPlayerDA playerDA)
        {
            _playerDa = playerDA;
        }

        public int AddPlayerToDb(IPlayer player)
        {
            return _playerDa.AddPlayerToDb(player);
        }

        public List<IPlayer> GetAllPlayers()
        {
            return _playerDa.GetAllPlayers();
        }

        public IPlayer GetPlayerById(int id)
        {
            return _playerDa.GetPlayerById(id);
        }
    }
}