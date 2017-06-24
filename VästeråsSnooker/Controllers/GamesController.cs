using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using VästeråsSnooker.BL;
using VästeråsSnooker.Models.DataModels;
using VästeråsSnooker.Models.ViewModels;

namespace VästeråsSnooker.Controllers
{
    public class GamesController : Controller
    {
        private IPlayerRepository _playerRepository;
        private IGamesRepository _gameRepository;
        private IGameViewModel _gameViewModel;
        private IGame _game;
        private IGameListViewModel _gameListViewModel;

        public GamesController(IPlayerRepository playerRepository, IGamesRepository gamesRepository, IGameViewModel gameViewModel, IGame game, IGameListViewModel gameListViewModel)
        {
            _playerRepository = playerRepository;
            _gameRepository = gamesRepository;
            _gameViewModel = gameViewModel;
            _game = game;
            _gameListViewModel = gameListViewModel;
        }

        public ActionResult Index()
        {   
            return View();
        }

        [HttpPost]
        public ActionResult GetAllGames()
        {
            return Json(_gameRepository.GetAllGames());
        }

        [HttpPost]
        public ActionResult GetAllPlayers()
        {
            return Json(_playerRepository.GetAllPlayers());
        }

        public ActionResult CreateGame()
        {
            var allaSpelare = _playerRepository.GetAllPlayers();
            _gameViewModel.SpelareAttVäljaMellan = allaSpelare.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name });
            _gameViewModel.Datum = DateTime.Now;

            return PartialView(_gameViewModel);
        }

        [HttpPost]
        public ActionResult SaveGame(GameViewModel game)
        {
            _game.AntalFrames = game.AntalFrames;
            _game.Datum = game.Datum;
            _game.FrameResultat = ConvertListToString(game.FrameResultat);
            _game.HogstaSerieSpelare1 = game.HogstaSerieSpelare1;
            _game.HogstaSerieSpelare2 = game.HogstaSerieSpelare2;
            _game.MatchReferat = game.MatchReferat;
            _game.Spelare1 = int.Parse(game.Spelare1);
            _game.Spelare2 = int.Parse(game.Spelare2);
            _game.Vinnare = CalculateWinner(game.FrameResultat) == 1 ? int.Parse(game.Spelare1) : int.Parse(game.Spelare2);
            
            int gameId = _gameRepository.CreateGame(_game);
            return View();
        }

        /// <summary>
        /// Takes a semicolon separeted string and converts it to a list of strings
        /// </summary>
        /// <param name="frameResultat"></param>
        /// <returns>List of int</returns>
        private List<int> ConvertStringToListOfInt(string frameResultat)
        {
            var frameResultsAsArrayOfString = frameResultat.Split(';');
            List<int> frameResultsAsListOfInt = new List<int>();
            foreach(var result in frameResultsAsArrayOfString)
            {
                frameResultsAsListOfInt.Add(int.Parse(result));
            }

            return frameResultsAsListOfInt;
        }
        
        /// <summary>
        /// Takes a list of int and convert it to a semicolon separeted string
        /// </summary>
        /// <param name="frameResultat"></param>
        /// <returns>Semicolon separeted string</returns>
        private string ConvertListToString(List<int> frameResultat)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var result in frameResultat)
            {
                sb.Append(result + ";");
            }
            sb.Length = sb.Length - 1;
            return sb.ToString();
        }

        /// <summary>
        /// Takes the list of frame results and calculates if player 1 or player 2 have most won frames. 
        /// </summary>
        /// <param name="frameResultat"></param>
        /// <returns>1 or 2 depending on who won most frames</returns>
        private int CalculateWinner(List<int> frameResultat)
        {
            var nrFrames = frameResultat.Count / 2;
            int frameWinsPlayer1 = 0;
            int frameWinsPlayer2 = 0;
            for (int i = 0; i < frameResultat.Count; i += 2)
            {
                if (frameResultat[i] > frameResultat[i + 1])
                    frameWinsPlayer1++;
                else
                    frameWinsPlayer2++;
            }

            return frameWinsPlayer1 > frameWinsPlayer2 ? 1 : 2;
        }
    }
}