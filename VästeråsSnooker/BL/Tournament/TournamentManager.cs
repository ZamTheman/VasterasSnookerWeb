using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VästeråsSnooker.Models.DataModels;

namespace VästeråsSnooker.BL.Tournament
{
    public enum TournamentStructure
    {
        GroupstageOnly,
        SingleEliminationOnly,
        GroupStageAndElimination
    }

    public class TournamentManager
    {
        ITournamentRepository _tournamentRepository;

        public TournamentManager(ITournamentRepository tournamentRepository)
        {
            _tournamentRepository = tournamentRepository;
        }

        public bool CreateTournament(List<int> playerIds, TournamentStructure tournamentStructure, out string error) 
        {
            var created = false;
            error = "";
            switch (tournamentStructure)
            {
                case TournamentStructure.GroupStageAndElimination:
                    break;
                case TournamentStructure.GroupstageOnly:
                    created = CreateGroupStage(out error, playerIds);
                    break;
                case TournamentStructure.SingleEliminationOnly:
                    break;
                default:
                    break;
            }

            return created;
        }

        private bool CreateGroupStage(out string error, List<int> playersIds)
        {
            List<TournamentGame> games = new List<TournamentGame>();
            for (int i = 0; i < playersIds.Count - 1; i++)
            {
                for (int j = i + 1; j < playersIds.Count; j++)
                {
                    games.Add(new TournamentGame
                    {
                        Spelare1 = playersIds[i],
                        Spelare2 = playersIds[j],
                        IsPlayed = false
                    });
                }
            }
            
            var wasAdded = _tournamentRepository.AddGroupStage(games, out error);

            return wasAdded;
        }
    }
}