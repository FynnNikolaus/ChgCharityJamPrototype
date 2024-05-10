using ChgCharityJamPrototype.HostedService;
using Microsoft.AspNetCore.Mvc;

namespace ChgCharityJamPrototype.Controllers
{
	public class BoardController : Controller
	{
		private readonly IGameStatusProvider _gameStatusProvider;

		public BoardController(IGameStatusProvider gameStatusProvider)
		{
			_gameStatusProvider = gameStatusProvider ?? throw new ArgumentNullException(nameof(gameStatusProvider));
		}

		public IActionResult Index()
		{
			var gameStatus = _gameStatusProvider.GetLatestGameStatus();
			return View(gameStatus);
		}
	}
}
