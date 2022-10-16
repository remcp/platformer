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
            bool stage1game = false;
            bool stage1cleared = false;
            string[,] currentstage = stage.Stage1();
            game.Run1(stage1game, stage1cleared, currentstage);
            Console.WriteLine("level 1 cleared");
            Console.ReadLine();
        }
    }
}

//gebruikte bron https://www.youtube.com/watch?v=T0MpWTbwseg&t=1423s