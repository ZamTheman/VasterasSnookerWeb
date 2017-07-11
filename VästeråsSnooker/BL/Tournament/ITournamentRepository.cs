using System.Collections.Generic;
using VästeråsSnooker.Models.DataModels;

namespace VästeråsSnooker.BL.Tournament
{
    public interface ITournamentRepository
    {
        bool AddNewTournament();
        bool AddGroupStage(List<TournamentGame> games, out string error);
    }
}