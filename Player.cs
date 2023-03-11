using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    internal class Player
    {
        public int Positioin = 0;
        public Color Color;
        public string Name;
        public int Money;
        public bool Activity;
        public int PassCount = 0;
        public Player(Color color)
        {
            this.Color = color;
        }
    }
}
