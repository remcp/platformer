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
            int[] playerposition = { 8, 3};
            int lastdirection = 0;
            int[] attack = { 0, 0, 0 };
            string old_block = " ";
            bool button1 = false;

            long time = 0;

            while (stage1game == false && stage1cleared == false)
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
                        lastdirection = player.Walk(stage1, playerposition, keypress, lastdirection, attack);
                        old_block = player.buttonpress(stage1, playerposition, attack, lastdirection);
                        stage.secretblock1(stage1, playerposition);
                        button1 = stage.Button1(stage1, playerposition, button1);
                        stage.Buildstage1(stage1, old_block, lastdirection, playerposition);
                        player.buildplayer(stage1, playerposition, attack, lastdirection);
                        Console.WriteLine(playerposition[0] + 1);
                        Console.WriteLine(playerposition[1] - 2);
                        Console.WriteLine(playerposition[1] + 2);

                        if (button1 == true)
                        {
                            stage1game = true;
                        }
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
