using Microsoft.AspNetCore.Mvc;
using SDCS.Engine;

namespace ChgCharityJamPrototype.Controllers
{
	public class BoardController : Controller
	{
		private readonly Game _game;

		public BoardController(Game game)
		{
			_game = game ?? throw new ArgumentNullException(nameof(game));
		}

		public IActionResult Index()
		{
			var allTeams = _game.TeamManager.GetAllTeams();
			return View(allTeams);
		}
	}
}
