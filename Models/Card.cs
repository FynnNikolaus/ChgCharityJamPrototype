namespace ChgCharityJamPrototype.Models
{
    public class Card
    {
        public Card(ActivationMode activationMode, string id, string name, string text, int? effectDurationInMinutes)
        {
            ActivationMode = activationMode;
            Id = id;
            Name = name;
            Text = text;
            EffectDurationInMinutes = effectDurationInMinutes;
        }

        public ActivationMode ActivationMode { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public int? EffectDurationInMinutes { get; set; }
    }

    public enum ActivationMode
    {
        Public,
        Anonymous,
        Secret
    }
}
