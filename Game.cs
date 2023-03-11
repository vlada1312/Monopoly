using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    internal class Game
    {
        ArrayList fields = new ArrayList();
        


        public List<Player> players= new List<Player>();
        public Game()
        {
            this.fields.Add(1);
            this.players = initialization();
        }
        List<Player> initialization()
        {
            Player player1 = new Player(Color.Red); Player player2 = new Player(Color.Purple);
            Player player3 = new Player(Color.Aqua); Player player4 = new Player(Color.Green);
            List<Player> players = new List<Player>() { player1, player2, player3, player4 };
            return players;
        }

        public void step()
        {
            Random step = new Random();
            for (int i = 0; i < 10; i++)
            {
                players[0].Positioin += step.Next();
            }
             
        }
    }
}
