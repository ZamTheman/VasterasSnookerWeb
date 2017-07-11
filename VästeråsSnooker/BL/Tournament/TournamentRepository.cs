using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VästeråsSnooker.Models.DataModels;
using Dapper;

namespace VästeråsSnooker.BL.Tournament
{
    public class TournamentRepository : ITournamentRepository
    {
        public bool AddGroupStage(List<TournamentGame> games, out string error)
        {
            throw new NotImplementedException();
        }

        public bool AddNewTournament()
        {
            throw new NotImplementedException();
        }
    }
}