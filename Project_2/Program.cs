using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static System.Console;

namespace Project_2
{
    internal class Program
    {
        static int x = 8;
        static int y = 8;
        static int xb = x;
        static int yb = y;
        static int f = 1;
        static int dis = 1;
        static bool end = false;
        static Stopwatch sw = new Stopwatch();
        static Thread t = new Thread(Walk);
        static Thread g = new Thread(Grafics);

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

        static void Grafics()
        {
            while (true)
            {
                Console.SetCursorPosition(xb, yb);
                Console.Write(" ");
                Console.SetCursorPosition(x, y);
                Console.Write("@");
                if (end)
                {
                    Thread.Sleep(50);
                    int x = 8;
                    int y = 8;
                    int xb = x;
                    int yb = y;
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
                        dis = 1;
                        break;
                    case ConsoleKey.S:
                        dis = 2;
                        break;
                    case ConsoleKey.A:
                        dis = 3;
                        break;
                    case ConsoleKey.D:
                        dis = 4;
                        break;
                    default:
                        break;
                }
                
            }
        }

        static void Game()
        {
            while (true)
            {
                sw.Start();
                Thread.Sleep(300);
                xb = x;
                yb = y;
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
                if (end)
                {
                    Clear();
                    Room();
                    x = 8;
                    y = 8;
                    xb = x;
                    yb = y;
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
            while (true) 
            {
                end = false;
                int dis = 1;
                Game();
            }
        }
    }
}
