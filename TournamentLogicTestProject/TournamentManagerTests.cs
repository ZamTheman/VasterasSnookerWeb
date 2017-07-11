using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VästeråsSnooker.BL.Tournament;
using System.Collections.Generic;
using Moq;
using VästeråsSnooker.Models.DataModels;

namespace TournamentLogicTestProject
{
    [TestClass]
    public class TournamentManagerTests
    {
        List<int> playerIds = new List<int> { 1, 3, 5, 7, 9, 12 };
        Mock<ITournamentRepository> _repo = new Mock<ITournamentRepository>();

        [TestMethod]
        public void CreateTournamentTest_shouldReturn_true()
        {
            var outString = "Created in Mock";
            _repo.Setup(create => create.AddGroupStage(It.IsAny<List<TournamentGame>>(), out outString)).Returns(true);
            var tournamentManager = new TournamentManager(_repo.Object);
            var structure = TournamentStructure.GroupstageOnly;
            string error = "";
            var created = tournamentManager.CreateTournament(playerIds, structure, out error);

            Assert.IsTrue(created);
            Assert.IsTrue(error == outString);
            _repo.Verify(l => l.AddGroupStage(It.Is<List<TournamentGame>>(li => li.Count == calculateListCount(playerIds.Count)), out outString));
            _repo.VerifyAll();
        }

        private int calculateListCount(int nrGames)
        {
            return (nrGames*nrGames-nrGames)/2;
        }
    }
}
