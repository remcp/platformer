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

                //deze code zorgt ervoor dat de onderstaande code niet op een input hoeft te wachten maar door de loop blijft gaan
                if (Console.KeyAvailable)
                {
                    keypress = Console.ReadKey();
                }
                try
                {
                    if (milliseconds - time >= 150)
                    {
                        //eerst word aan de hand van een key input bepaald wat de positie van de player moet worden
                        lastdirection = player.Walk(stage1, playerposition, keypress, lastdirection, attack);
                        //word f ingedrukt zal het blok naast de hand van het character veranderen in het actie teken
                        old_block = player.buttonpress(stage1, playerposition, attack, lastdirection);
                        //er word gecontroleerd of de speler een bepaald blok heeft aangeraakt, hierna wordt een actie uitgevoerd.
                        stage.secretblock1(stage1, playerposition);
                        //er word gecontroleerd of de juiste blok word "ingedrukt"
                        button1 = stage.Button1(stage1, playerposition, button1);
                        //de stage word opgebouwd
                        stage.Buildstage1(stage1, old_block, lastdirection, playerposition);
                        //de player word in de stage vanuit de playerpositie opgebouwd
                        player.buildplayer(stage1, playerposition, attack, lastdirection);
                        Console.WriteLine(playerposition[0] + 1);
                        Console.WriteLine(playerposition[1] - 2);
                        Console.WriteLine(playerposition[1] + 2);

                        //level behaald
                        if (button1 == true)
                        {
                            stage1game = true;
                        }
                        time = milliseconds;
                    }
                }
                //gaat de player out of bounds dan sluit het spel af. Hiervoor een end game screen maken
                catch
                {
                    Console.Clear();
                    stage1game = false;
                }
            }
        }
    }
}
