using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    internal class Game
    {
        public void Run1(bool mazegame, bool mazecleared, string[,] maze)
        {
            Stage stage = new Stage();
            Player player = new Player();
            Console.CursorVisible = false;

            int[] playerposition = { 8, 2};
            int lastdirection = 0;
            int[] attack = { 0, 0, 0 };

            while (mazegame == true || mazecleared == false)
            {
                ConsoleKeyInfo keypress = Console.ReadKey();
                try
                {
                    lastdirection = player.Walk(maze, playerposition, keypress, lastdirection);
                    player.Attack(attack, keypress);
                    stage.secretblock1(maze, playerposition);
                    stage.Buildmaze(maze);
                    player.buildplayer(maze, playerposition, attack, lastdirection);
                    Console.WriteLine();
                }
                catch
                {
                    Console.Clear();
                    mazegame = false;
                }
            }
        }
    }
}
