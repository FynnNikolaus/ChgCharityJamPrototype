namespace ChgCharityJamPrototype.Models
{
    public class BackendModel
    {
        private static readonly Lazy<BackendModel> _instance = new Lazy<BackendModel>(() => new BackendModel());

        public static BackendModel Instance => _instance.Value;

        public BackendModel() 
        {
            Teams = new TeamModel();
            Cards = new CardModel();
        }
        public TeamModel Teams { get; set; }
        public CardModel Cards { get; set; }
    }
}
