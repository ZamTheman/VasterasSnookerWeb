using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VästeråsSnooker.DB;
using VästeråsSnooker.Models.DataModels;
using System.Linq;
using Dapper;
using VästeråsSnooker.Helpers;
using System.Configuration;
using System;

namespace VästeråsSnooker.BL
{
    public class GamesRepository : IGamesRepository
    {
        private IDbConnection _db;
        private IConfigurationReader _cr;
        private IGameDA _gameDA;

        public GamesRepository(IGameDA gameDA, IConfigurationReader cr)
        {
            _cr = cr;
            _db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString());
            _gameDA = gameDA;
        }

        public List<Game> GetAllGames()
        {
            var result = _db.Query<Game>("SELECT * FROM Games").ToList();
            return result;
        }

        public int CreateGame(IGame game)
        {
            if(game.HogstaSerieSpelare1 > 0)
                _db.Execute("INSERT INTO HighestBreaks (Spelare, Datum, Serie) VALUES (@Spelare1, @Datum, @HogstaSerieSpelare1)", game);

            if(game.HogstaSerieSpelare2 > 0)
                _db.Execute("INSERT INTO HighestBreaks (Spelare, Datum, Serie) VALUES (@Spelare2, @Datum, @HogstaSerieSpelare2)", game);

            return _db.ExecuteScalar<int>("INSERT INTO Games (Spelare1, Spelare2, Datum, AntalFrames, FrameResultat, " +
                "HogstaSerieSpelare1, HogstaSerieSpelare2, Vinnare, MatchReferat) VALUES (@Spelare1, @Spelare2, @Datum, @AntalFrames, @FrameResultat," +
                "@HogstaSerieSpelare1, @HogstaSerieSpelare2, @Vinnare, @MatchReferat)", game);
        }

        public List<Game> GetGamesByPlayerId(int playerId)
        {
            var result = _db.Query<Game>("SELECT * FROM Games WHERE Spelare1 = @PlayerId OR Spelare2 = @PlayerId", new { PlayerId = playerId}).ToList();
            return result;
        }

        public List<Break> GetBreaksByPlayerId(int playerId)
        {
            var result = _db.Query<Break>("SELECT Datum, Serie FROM HighestBreaks WHERE Spelare = @PlayerId", new { PlayerId = playerId }).ToList();
            return result;
        }
    }
}