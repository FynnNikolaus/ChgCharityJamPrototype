using ChgCharityJamPrototype.DTO;
using ChgCharityJamPrototype.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using SDCS.Engine;

namespace ChgCharityJamPrototype.Controllers;

public class BackendBoardController : Controller
{
	private readonly ILogger<BackendBoardController> _logger;
	private readonly IConfiguration _configuration;
	private readonly IHostEnvironment _fileProvider;
	private readonly IHubContext<CommunicationHub> _hubContext;

	private readonly Game _game;

	public BackendBoardController(ILogger<BackendBoardController> logger,
									IConfiguration configuration,
									IHostEnvironment fileProvider,
									IHubContext<CommunicationHub> hubContext,
									Game game)
	{
		_logger = logger;
		_configuration = configuration;
		_fileProvider = fileProvider;
		_hubContext = hubContext;
		_game = game;
	}

	[HttpGet]
	public IActionResult Index()
	{
		return View(null);
	}

	[HttpGet("BackendBoard/teams")]
	public IActionResult GetTeams()
	{
		var teams = _game.TeamManager.GetAllTeams();

		return Ok(teams);
	}

	[HttpGet("BackendBoard/cards")]
	public IActionResult GetCards()
	{
		var cardResponses = ReadCardsFromJson();

		return Ok(cardResponses);
	}

	[HttpPost("addTeam")]
	public async Task<IActionResult> AddTeam([FromForm] CreateTeamRequest newTeam)
	{
		_game.TeamManager.AddTeam(new SDCS.Teams.TeamData
		{
			Name = newTeam.Name,
			Balance = newTeam.Balance,
			Workspace = newTeam.Workspace
		});

		return Ok();
	}

	[HttpDelete]
	public IActionResult DeleteTeam([FromQuery] string team)
	{
		return Ok();
	}

	private List<CardResponse> ReadCardsFromJson()
	{
		using StreamReader sr = new("MockFiles/cards.json");
		var json = sr.ReadToEnd();
		var cardList = JsonConvert.DeserializeObject<List<CardResponse>>(json);

		return cardList;
	}
}