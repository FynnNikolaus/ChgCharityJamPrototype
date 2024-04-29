using ChgCharityJamPrototype.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ChgCharityJamPrototype.Controllers
{
    public class BackendBoardController : Controller
    {
        private readonly ILogger<BackendBoardController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHostEnvironment _fileProvider;

        private readonly BackendModel _backendModel;

        public BackendBoardController(ILogger<BackendBoardController> logger,
                                        IConfiguration configuration,
                                        IHostEnvironment fileProvider,
                                        BackendModel backendModel)
        {
            _logger = logger;
            _configuration = configuration;
            _fileProvider = fileProvider;
            _backendModel = backendModel;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // this initialization process should be done in either 
            // the BackendModel itself or in an appropriate Logic / Builder Class
            // Initialize cards
            if(!_backendModel.Cards._cardList.Any())
            {
                InitializeCards();
            }
            // initialize teams
            if(!_backendModel.Teams._teamList.Any())
            {
                InitializeTeams();
            }

            return View(_backendModel);
        }
        
        [HttpPost]
        public IActionResult AddTeam([FromQuery] string team)
        {
            if(team is null)
            {
                return BadRequest("No name given");
            }

            _backendModel.Teams._teamList.Add(new Team(team, new System.Drawing.Color(), 0));

            return Ok(team);
        }

        [HttpDelete]
        public IActionResult DeleteTeam([FromQuery] string team)
        {
            var teamToDelete = _backendModel.Teams._teamList.Find(x => x.Name is not null && x.Name.Equals(team, StringComparison.OrdinalIgnoreCase));
            if(teamToDelete is null)
            {
                return NotFound();
            }

            _backendModel.Teams._teamList.Remove(teamToDelete);

            return Ok();
        }

        private void InitializeTeams()
        {
            var testTeamMember = new Team.Builder().WithName("TestTeam").WithBalance(420).WithHexColor("#3498db").Build();
            _backendModel.Teams.AddTeamMembers([testTeamMember]);
        }

        private void InitializeCards()
        {
            string subPath = Path.Combine(_configuration["AssetDefinition:AssetLocation"] ?? "", _configuration["AssetDefinition:LanguageSettings"] ?? "", _configuration["AssetDefinition:CardFile"] ?? "");

            var filInfo = _fileProvider.ContentRootFileProvider.GetFileInfo(subPath);

            if (!filInfo.Exists)
                throw new NullReferenceException($"The Assets file could not be found in directory {filInfo.PhysicalPath}");

            using (StreamReader reader = new StreamReader(filInfo.CreateReadStream(), Encoding.UTF8))
            {
                var jsonObject = JsonConvert.DeserializeObject<List<Card>>(reader.ReadToEnd());

                if (jsonObject != null)
                    _backendModel.Cards.AddCards(jsonObject);
            }
        }
    }
}
