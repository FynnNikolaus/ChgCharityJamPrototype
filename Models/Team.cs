using System.Drawing;

namespace ChgCharityJamPrototype.Models
{
    public class Team
    {
        public Team(string name, Color color, int balance)
        {
            Name = name;
            Color = color;
            Balance = balance;
        }

        public string Name { get; set; }
        public Color Color { get; set; }
        public int Balance { get; set; }

        public class Builder
        {
            private string _name;
            private Color _color;
            private int _balance;

            public Builder WithName(string name)
            {
                _name = name;
                return this;
            }

            public Builder WithHexColor(string hexColorCode)
            {
                _color = ColorTranslator.FromHtml(hexColorCode);
                return this;
            }

            public Builder WithBalance(int balance)
            {
                _balance = balance;
                return this;
            }

            public Team Build()
            {
                return new Team(_name, _color, _balance);
            }
        }
    }
}
