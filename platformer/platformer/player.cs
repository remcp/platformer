using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platformer
{
    internal class Player
    {
        //het character word vanaf de playerpositie in de stage opgebouwd
        public void buildplayer(String[,] maze, int[] playerposition, int[] attack, int lastdirection)
        {
            int colms = maze.GetLength(1);
            int rows = maze.GetLength(0);
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < colms; x++)
                {
                    string buildmaze = maze[y, x];
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
                    if (attack[0] >= 1)
                    {
                        if (lastdirection == 1 && y - 1 == playerposition[0] && x - 2 == playerposition[1])
                        {
                            Console.Write("/");
                            attack[0]--;
                        }
                        else if (lastdirection == 0 && y - 1 == playerposition[0] && x + 2 == playerposition[1])
                        {
                            Console.Write("\\");
                            attack[0]--;
                        }
                    }
                }
            }
        }

        public int Walk(String[,] maze, int[] playerposition, ConsoleKeyInfo keypress, int lastdirection)
        {
            Player player = new();
            Stage stage = new Stage();

            //het character loopt naar lings of rechts op basis van input. Al loopt het character hiermee in een muur dan wordt deze terug gezet waardoor het lijkt of er nooit bewogen is. nog andere manier van preventie zoeken

            //links, rechts
            if (keypress.KeyChar == 'd')
            {
                playerposition[1]++;
                lastdirection = 1;
                if (!player.Canwalk(maze, playerposition[0], playerposition[1]))
                {
                    playerposition[1]--;
                }
            }
            if (keypress.KeyChar == 'a')
            {
                playerposition[1]--;
                lastdirection = 0;
                if (!player.Canwalk(maze, playerposition[0], playerposition[1]))
                {
                    playerposition[1]++;
                }
            }

            //jump
            //staat het character op stenen dan kan er gesprongen worden. er word gekeken naar welke richting het laatst is gelopen. Naar deze kant word gesprongen. 
            if (Onground(maze, playerposition[0], playerposition[1]))
            {
                if (keypress.KeyChar == 'w')
                {
                    if (lastdirection == 0)
                    {
                        playerposition[1]--;
                        if (!player.Canwalk(maze, playerposition[0], playerposition[1]))
                        {
                            playerposition[1]++;
                        }
                    }
                    else
                    {
                        playerposition[1]++;
                        if (!player.Canwalk(maze, playerposition[0], playerposition[1]))
                        {
                            playerposition[1]--;
                        }
                    }
                    if (!player.Canjump(maze, playerposition[0], playerposition[1]))
                    {
                        playerposition[0] = playerposition[0] - 2;
                    }
                }
            }
            //staat het character niet op stenen dan valt deze automatisch naar beneden. ook hier wordt naar de laatste righting gekeken.
            else if (!Onground(maze, playerposition[0], playerposition[1]))
            {
                playerposition[0]++;
                if (lastdirection == 0)
                {
                    playerposition[1]--;
                    if (!player.Canwalk(maze, playerposition[0], playerposition[1]))
                    {
                        playerposition[1]++;
                    }
                }
                else
                {
                    playerposition[1]++;
                    if (!player.Canwalk(maze, playerposition[0], playerposition[1]))
                    {
                        playerposition[1]--;
                    }
                }
            }
            return lastdirection;
        }

        //controleer of er boven het character ruimte vrij is
        public Boolean Canjump(string[,] maze, int y, int x)
        {
            return maze[y - 1, x] == "=" | maze[y - 1, x - 1] == "=" | maze[y - 1, x + 1] == "=";
        }
        //controleer of er naast het character ruimte vrij is, Ook word er gecontroleerd of er op de plaats van de benen ruimte vrij is. dit zodat de code makkelijk overgezet kan worden naar een doolhof
        public Boolean Canwalk(string[,] maze, int y, int x)
        {
            return maze[y, x + 1] == " " & maze[y, x - 1] == " " & maze[y + 1, x - 1] == " " & maze[y + 1, x + 1] == " " & maze[y + 2, x] == " " & maze[y + 2, x - 1] == " " & maze[y + 2, x + 1] == " ";
        }
        //controleer of het character het doel heeft bereikt
        public Boolean Checkgoal(string[,] maze, int y, int x)
        {
            return maze[y, x + 1] == "x" & maze[y, x - 1] == "x" & maze[y + 2, x] == "x" & maze[y + 2, x - 1] == "x" & maze[y + 2, x + 1] == "x";
        }
        //controleer of het character boven "stenen" staat
        public Boolean Onground(string[,] maze, int y, int x)
        {
            return maze[y + 3, x] == "=" | maze[y + 3, x - 1] == "=" | maze[y + 3, x + 1] == "=";
        }

        public void Attack(int[] attack, ConsoleKeyInfo keypress)
        {
            Player player = new();
            Stage stage = new Stage();

            if (keypress.KeyChar == 'f')
            {
                attack[0]++;
            }
        }
    }
}
