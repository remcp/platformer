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
            bool stage1cleared = false;
            bool repeat = true;
            while (repeat == true)
            {
                string[,] currentstage = stage.Stage1();
                game.Run1(dead, stage1cleared, currentstage);
            }
        }
    }
}