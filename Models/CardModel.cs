namespace ChgCharityJamPrototype.Models
{
    public class CardModel
    {
        private List<Card> _cardList;

        public CardModel()
        {
            _cardList = new List<Card>();
        }

        public void AddCards(IEnumerable<Card> cards)
        {
            _cardList.AddRange(cards);
        }

        public void DeleteCards(List<string> idsToDelete)
        {
            _cardList.RemoveAll(card => idsToDelete.Contains(card.Id));
        }
    }
}
