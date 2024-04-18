using ChgCharityJamPrototype.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChgCharityJamPrototype.Controllers
{
    public class BackendBoardController : Controller
    {
        private readonly ILogger<BackendBoardController> _logger;
        private readonly IConfiguration _configuration;

        private BackendModel _backendModel = new BackendModel();

        public BackendBoardController(ILogger<BackendBoardController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            InitializeCards();
            InitializeTeams();

            return View(_backendModel);
        }

        private void InitializeTeams()
        {
            var testTeamMember = new Team.Builder().WithName("TestTeam").WithBalance(420).WithHexColor("#3498db").Build();
            _backendModel.Teams.AddTeamMembers([testTeamMember]);
        }

        private void InitializeCards()
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), _configuration["AssetDefinition:AssetLocation"] ?? "", _configuration["AssetDefinition:LanguageSettings"] ?? "", _configuration["AssetDefinition:CardFile"] ?? "");

            using (StreamReader sr = new StreamReader(filePath))
            {
                var jsonObject = JsonConvert.DeserializeObject<List<Card>>(sr.ReadToEnd());

                if (jsonObject != null)
                    _backendModel.Cards.AddCards(jsonObject);
            }
        }
    }
}
