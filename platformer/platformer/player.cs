using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    internal class Player
    {
        //het character word vanaf de playerpositie in de stage opgebouwd
        public void buildplayer(String[,] stage1, int[] playerposition, int[] attack, int lastdirection)
        {
            int colms = stage1.GetLength(1);
            int rows = stage1.GetLength(0);
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < colms; x++)
                {
                    string buildstage1 = stage1[y, x];
                    Console.SetCursorPosition(x, y);
                    if (y == playerposition[0] && x == playerposition[1])
                    {
                        Console.Write("o");
                    }
                    else if (y - 1 == playerposition[0] && x == playerposition[1])
                    {
                        Console.Write("|");
                    }
                    else if (y - 1 == playerposition[0] && x + 1 == playerposition[1])
                    {
                        Console.Write("/");
                    }
                    else if (y - 1 == playerposition[0] && x - 1 == playerposition[1])
                    {
                        Console.Write("\\");
                    }
                    else if (y - 2 == playerposition[0] && x + 1 == playerposition[1])
                    {
                        Console.Write("/");
                    }
                    else if (y - 2 == playerposition[0] && x - 1 == playerposition[1])
                    {
                        Console.Write("\\");
                    }
                }
            }
        }

        public string buttonpress(String[,] stage1, int[] playerposition, int[] attack, int lastdirection)
        {
            int colms = stage1.GetLength(1);
            int rows = stage1.GetLength(0);
            string old_block = " ";

            //bewaar blok naast character hand
            if (lastdirection == 1)
            {
                old_block = stage1[playerposition[0] + 1, playerposition[1] + 2];
            }
            else if (lastdirection == 0)
            {
                old_block = stage1[playerposition[0] + 1, playerposition[1] - 2];
            }

            //verander het block naast character hand in het actie teken
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < colms; x++)
                {
                    string buildstage1 = stage1[y, x];
                    Console.SetCursorPosition(x, y);
                    if (attack[0] >= 1)
                    {
                        if (lastdirection == 1 && y - 1 == playerposition[0] && x - 2 == playerposition[1])
                        {
                            stage1[playerposition[0] + 1, playerposition[1] + 2] = "/";
                            attack[0]--;
                        }
                        else if (lastdirection == 0 && y - 1 == playerposition[0] && x + 2 == playerposition[1])
                        {
                            stage1[playerposition[0] + 1, playerposition[1] - 2] = "\\";
                            attack[0]--;
                        }
                    }
                }
            }
            return old_block;
        }

        public int Walk(String[,] stage1, int[] playerposition, ConsoleKeyInfo keypress, int lastdirection, int[] attack)
        {
            Player player = new();
            Stage stage = new Stage();
            ConsoleKey key = keypress.Key;
            //het character loopt naar lings of rechts op basis van input. Al loopt het character hiermee in een muur dan wordt deze terug gezet waardoor het lijkt of er nooit bewogen is. nog andere manier van preventie zoeken

            //links, rechts. Kan er niet een kant op worden bewogen dan word het character terug gezet. 
            switch (key)
            {
                case ConsoleKey.D:
                    playerposition[1]++;
                    lastdirection = 1;
                    if (!player.Canwalk(stage1, playerposition[0], playerposition[1]))
                    {
                        playerposition[1]--;
                    }
                    break;

                case ConsoleKey.A:
                    playerposition[1]--;
                    lastdirection = 0;
                    if (!player.Canwalk(stage1, playerposition[0], playerposition[1]))
                    {
                        playerposition[1]++;
                    }
                    break;

                case ConsoleKey.W:
                    //jump
                    //staat het character op stenen dan kan er gesprongen worden. er word gekeken naar welke richting het laatst is gelopen. Naar deze kant word gesprongen. 
                    //staat het character niet op stenen dan valt deze automatisch naar beneden. ook hier wordt naar de laatste righting gekeken.
                    if (Onground(stage1, playerposition[0], playerposition[1]))
                    {
                        if (lastdirection == 0)
                        {
                            playerposition[1]--;
                            playerposition[1]--;
                            if (!player.Canwalk(stage1, playerposition[0], playerposition[1]))
                            {
                                playerposition[1]++;
                                playerposition[1]++;
                            }
                        }
                        else if (lastdirection == 1)
                        {
                            playerposition[1]++;
                            playerposition[1]++;

                            if (!player.Canwalk(stage1, playerposition[0], playerposition[1]))
                            {
                                playerposition[1]--;
                                playerposition[1]--;
                            }
                        }
                        if (!player.Canjump(stage1, playerposition[0], playerposition[1]))
                        {
                            playerposition[0] = playerposition[0] - 3;
                        }
                    }
                    break;
                case ConsoleKey.F:
                    //buttonpress code maakt hier gebuik van om het blok naast de hand van het character te veranderen in het actie teken
                        attack[0]++;
                    break;
            }
            //staat de speler niet op blokken dan zal deze vanzelf naar beneden vallen
            if (!Onground(stage1, playerposition[0], playerposition[1]))
            {
                playerposition[0]++;
                if (lastdirection == 0)
                {
                    playerposition[1]--;
                    if (!player.Canwalk(stage1, playerposition[0], playerposition[1]))
                    {
                        playerposition[1]++;
                    }
                }
                else
                {
                    playerposition[1]++;
                    if (!player.Canwalk(stage1, playerposition[0], playerposition[1]))
                    {
                        playerposition[1]--;
                    }
                }
            }

            return lastdirection;
        }

        //controleer of er boven het character ruimte vrij is
        public Boolean Canjump(string[,] stage1, int y, int x)
        {
            return stage1[y - 1, x] == "=" | stage1[y - 1, x - 1] == "=" | stage1[y - 1, x + 1] == "=";
        }
        //controleer of er naast het character ruimte vrij is, Ook word er gecontroleerd of er op de plaats van de benen ruimte vrij is. dit zodat de code makkelijk overgezet kan worden naar een doolhof
        public Boolean Canwalk(string[,] stage1, int y, int x)
        {
            return stage1[y, x + 1] == " " & stage1[y, x - 1] == " " & stage1[y + 1, x - 1] == " " & stage1[y + 1, x + 1] == " " & stage1[y + 2, x] == " " & stage1[y + 2, x - 1] == " " & stage1[y + 2, x + 1] == " ";
        }
        //controleer of het character het doel heeft bereikt
        public Boolean Checkgoal(string[,] stage1, int y, int x)
        {
            return stage1[y, x + 1] == "x" & stage1[y, x - 1] == "x" & stage1[y + 2, x] == "x" & stage1[y + 2, x - 1] == "x" & stage1[y + 2, x + 1] == "x";
        }
        //controleer of het character boven "stenen" staat
        public Boolean Onground(string[,] stage1, int y, int x)
        {
            return stage1[y + 3, x] == "=" | stage1[y + 3, x - 1] == "=" | stage1[y + 3, x + 1] == "=";
        }

    }
}
