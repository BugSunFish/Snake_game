using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static System.Console;

namespace Snake_game
{
    internal class Program
    {
        static int x = 8;
        static int y = 8;
        static int xb = x;
        static int yb = y;
        static int xa = 1;
        static int ya = 1;
        static int f = 0;
        static int dis = 1;
        static Random random = new Random();
        static bool end = false;
        static bool apple = false;
        static int[] xt = new int[2];
        static int[] yt = new int[2];
        static Stopwatch sw = new Stopwatch();
        static Thread t = new Thread(Walk);
        static Thread g = new Thread(Grafics);
        static Thread n = new Thread(Nnetwork);

        static void Room()
        {
            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition(15, i);
                Console.Write("█");
            }
            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("█");
            }
            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition(i, 15);
                Console.Write("█");
            }
            for (int i = 0; i < 16; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("█");
            }
        }

        static void Tail()
        {
            Array.Resize(ref xt, xt.Length + 1);
            Array.Resize(ref yt, yt.Length + 1);
        }

        static void Clear()
        {
            Console.Clear();
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine();
            }
        }

        static void Nnetwork()
        {
            while (true)
            {
            }
        }

        static void Grafics()
        {
            while (true)
            {
                Console.SetCursorPosition(xb, yb);
                Console.Write(" ");
                for (int t = f; t < xt.Length; t++)
                {
                    if (xt[t] == xa && yt[t] == ya)
                    {
                        apple = false;
                    }
                }
                if (apple == true)
                {
                    Console.SetCursorPosition(xa, ya);
                    Console.Write("O");
                }
                Console.SetCursorPosition(x, y);
                Console.Write("@");
                Console.SetCursorPosition(0, 0);
                Console.Write("█");
                Thread.Sleep(50);
                if (end)
                {
                    Thread.Sleep(150);
                    int x = 8;
                    int y = 8;
                    int xb = x;
                    int yb = y;
                    apple = false;
                }
            }
        }

        static void Walk()
        {
            while(true)
            {
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.W:
                        if (dis != 2) {
                            dis = 1;
                        }
                        break;
                    case ConsoleKey.S:
                        if (dis != 1)
                        {
                            dis = 2;
                        }
                        break;
                    case ConsoleKey.A:
                        if (dis != 4)
                        {
                            dis = 3;
                        }
                        break;
                    case ConsoleKey.D:
                        if (dis != 3)
                        {
                            dis = 4;
                        }
                        break;
                    case ConsoleKey.Spacebar:
                        Tail();
                        break;
                    default:
                        break;
                }
                Thread.Sleep(150);
            }
        }

        static void Game()
        {
            while (true)
            {
                sw.Start();
                if (x == xa && y == ya)
                {
                    apple = false;
                    Tail();
                }
                if (apple == false)
                {
                    xa = random.Next(14) + 1;
                    ya = random.Next(14) + 1;
                    apple = true;
                }
                for (int k = f; k < xt.Length; k++)
                {
                    if (xt[k] == x && yt[k] == y - 1 && dis == 1)
                    {
                        end = true;
                    }
                    if (xt[k] == x && yt[k] == y + 1 && dis == 2)
                    {
                        end = true;
                    }
                    if (xt[k] == x - 1 && yt[k] == y && dis == 3)
                    {
                        end = true;
                    }
                    if (xt[k] == x + 1 && yt[k] == y && dis == 4)
                    {
                        end = true;
                    }
                }
                xt[xt.Length-1] = x;
                yt[yt.Length-1] = y;
                f++;
                Array.Resize(ref xt, xt.Length + 1);
                Array.Resize(ref yt, yt.Length + 1);
                xb = xt[f];
                yb = yt[f];
                switch (dis)
                {
                    case 1:
                        y--;
                        break;
                    case 2:
                        y++;
                        break;
                    case 3:
                        x--;
                        break;
                    case 4:
                        x++;
                        break;
                }
                switch (x, y)
                {
                    case ( >= 0 and <= 15, 15):
                    case ( >= 0 and <= 15, 0):
                    case ( 15, >= 0 and <= 15):
                    case ( 0, >= 0 and <= 15):
                        end = true;
                        break;
                }
                Thread.Sleep(150);
                if (end)
                {
                    Clear();
                    Room();
                    x = 8;
                    y = 8;
                    xb = x;
                    yb = y;
                    Array.Resize(ref xt, xt.Length - xt.Length + 2 );
                    Array.Resize(ref yt, yt.Length - yt.Length + 2 );
                    f = 0;
                    apple = false;
                    break;
                }
                sw.Stop();
            }
        }

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            SetWindowSize(16, 16);
            Room();
            t.Start();
            g.Start();
            n.Start();
            while (true) 
            {
                end = false;
                int dis = 1;
                Game();
            }
        }
    }
}
