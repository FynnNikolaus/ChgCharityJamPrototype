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

        private BackendModel _backendModel = new BackendModel();

        public BackendBoardController(ILogger<BackendBoardController> logger,
                                        IConfiguration configuration,
                                        IHostEnvironment fileProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _fileProvider = fileProvider;
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

            //using (StreamReader sr = new StreamReader(filePath))
            //{
            //    var jsonObject = JsonConvert.DeserializeObject<List<Card>>(sr.ReadToEnd());

            //    if (jsonObject != null)
            //        _backendModel.Cards.AddCards(jsonObject);
            //}
        }
    }
}
