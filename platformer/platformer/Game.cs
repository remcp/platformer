using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    internal class Game
    {
        public void Run1(bool stage1game, bool stage1cleared, string[,] stage1)
        {
            Stage stage = new Stage();
            Player player = new Player();
            Console.CursorVisible = false;
            ConsoleKeyInfo keypress = new ConsoleKeyInfo();
            int[] playerposition = { 8, 2};
            int lastdirection = 0;
            int[] attack = { 0, 0, 0 };

            long time = 0;

            while (stage1game == true || stage1cleared == false)
            {
                long milliseconds = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                if (Console.KeyAvailable)
                {
                    keypress = Console.ReadKey();
                }
                try
                {
                    if (milliseconds - time >= 150)
                    {
                        lastdirection = player.Walk(stage1, playerposition, keypress, lastdirection);
                        player.Attack(attack, keypress);
                        stage.secretblock1(stage1, playerposition);
                        stage.Buildstage1(stage1);
                        player.buildplayer(stage1, playerposition, attack, lastdirection);

                        time = milliseconds;
                    }
                }
                catch
                {
                    Console.Clear();
                    stage1game = false;
                }
            }
        }
    }
}
