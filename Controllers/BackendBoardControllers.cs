using ChgCharityJamPrototype.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ChgCharityJamPrototype.Controllers
{
    public class BackendBoardController : Controller
    {
        private readonly ILogger<BackendBoardController> _logger;

        public BackendBoardController(ILogger<BackendBoardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var cardModel = new CardModel();

            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "bin", "debug", "net8.0", "wwwroot", "Assets", "DE", "Cards.json");

            using (StreamReader sr = new StreamReader(filePath))
            {
                var jsonObject = JsonConvert.DeserializeObject<List<Card>>(sr.ReadToEnd());

                if (jsonObject != null)
                    cardModel.AddCards((IEnumerable<Card>)jsonObject);
            }

            return View(cardModel);
        }
    }
}
