using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            Stage stage = new Stage();
            bool dead = false;
            bool mazecleared = false;
            string[,] currentstage = stage.Stage1();
            game.Run1(dead, mazecleared, currentstage);
        }
    }
}