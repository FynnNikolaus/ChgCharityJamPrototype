namespace ChgCharityJamPrototype.Models
{
    public class BackendModel
    {
        public BackendModel() 
        {
            Teams = new TeamModel();
            Cards = new CardModel();
        }
        public TeamModel Teams { get; set; }
        public CardModel Cards { get; set; }
    }
}
