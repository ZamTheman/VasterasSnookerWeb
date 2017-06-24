using System.Web.Mvc;
using VästeråsSnooker.Models;
using VästeråsSnooker.Models.DataModels;
using VästeråsSnooker.Models.ViewModels;
using VästeråsSnooker.BL;
using VästeråsSnooker.DB;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using VästeråsSnooker.Helpers;
using System.Linq;
using System;

namespace VästeråsSnooker.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IViewPlayersViewModel _viewPlayersViewModel;
        private readonly IPlayerDetailsViewModel _playerDetailsViewModel;

        public PlayerController(IPlayerRepository playerRepository, IViewPlayersViewModel viewPlayersViewModel, IPlayerDetailsViewModel playerDetailsViewModel)
        {
            _playerRepository = playerRepository;
            _viewPlayersViewModel = viewPlayersViewModel;
            _playerDetailsViewModel = playerDetailsViewModel;
        }
        
        public ActionResult ViewPlayers()
        {
            _viewPlayersViewModel.AllPlayers = _playerRepository.GetAllPlayers();
            _viewPlayersViewModel.ActivePlayer = _viewPlayersViewModel.AllPlayers.Count != 0 ? _viewPlayersViewModel.AllPlayers[0] : null;

            return View(_viewPlayersViewModel);
        }
        
        [HttpPost]
        public ActionResult ActivePlayerChanged(ActivePlayerChanged playerChanged)
        {
            var player = _playerRepository.GetPlayerById(playerChanged.Id);
            return Json(player);
        }
       
        [HttpPost]
        public async Task<ActionResult> PlayerDetails(PlayerDetails player)
        {
            var newPlayer = _playerRepository.GetPlayerById(player.Id);
            _playerDetailsViewModel.Id = newPlayer.Id;
            _playerDetailsViewModel.Name = newPlayer.Name;
            _playerDetailsViewModel.ImageUrl = await FileStorageDA.GetImageByPlayerId(player.Id);

            return PartialView(_playerDetailsViewModel);
        }

        [HttpPost]
        public ActionResult CreatePlayerPartial()
        {
            var player = new CreatePlayerViewModel();
            return PartialView(player);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePlayer(CreatePlayerViewModel player)
        {
            int newPlayerId = -1;
            if (string.IsNullOrEmpty(player.Name))
                return CreateError("Spelare kunde inte skapas", "Spelaren måste ha ett namn.");

            try
            {
                Player playerToStore = new Player()
                {
                    Name = player.Name
                };
                newPlayerId = _playerRepository.AddPlayerToDb(playerToStore);
                if (newPlayerId < 0)
                    return CreateError("Spelaren kunde inte lagras i databasen", "");
            }
            catch
            {
                return CreateError("Spelaren kunde inte lagras i databasen", "");
            }

            if (player.ImageUpload != null)
            {
                try
                {
                    Image bitmap = Image.FromStream(player.ImageUpload.InputStream);
                    ImageFormat imageFormat = null;

                    string imageFormatAsString = player.ImageUpload.ContentType.Split('/').Last();

                    switch (imageFormatAsString)
                    {
                        case "png":
                            imageFormat = ImageFormat.Png;
                            break;
                        case "jpg":
                        case "jpeg":
                            imageFormat = ImageFormat.Jpeg;
                            break;
                        case "gif":
                            imageFormat = ImageFormat.Gif;
                            break;
                        case "bmp":
                            imageFormat = ImageFormat.Bmp;
                            break;
                        default:
                            break;
                    }
                    double ratio = (double)bitmap.Width / (double)bitmap.Height;
                    bitmap = ratio >= 0 ? ImageProcessing.ResizeImage(bitmap, 150, (int)(150 / ratio)) : ImageProcessing.ResizeImage(bitmap, (int)(150 / ratio), 150);


                    await FileStorageDA.UploadImage(bitmap, newPlayerId + "_profileImage." + imageFormatAsString);
                }
                catch (Exception e)
                {
                    return CreateError("Bilden kunde inte sparas", "Spelare är sparad med bilden kunde inte lagras inte sparas. Vänligen kontrollera att bilden var i ett vanligt bildformat(jpg, png, gif eller bmp.");
                }
            }
            return View("PlayerCreated", player);
        }

        private ActionResult CreateError(string title, string errorDetail)
        {
            var error = new ErrorMessage
            {
                Title = title,
                Message = errorDetail + "\r\n Om problemet uppstår igen vänligen informera Samuel."
            };
            return View("Error", error);
        }
    }
}