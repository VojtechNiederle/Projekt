using System;
using System.Linq;

namespace NIM
{
    public class Engine : GUI
    {
        public int[,] M = new int[,] { { 1, 0, 0, 0, 0, 0, 0 },
                                       { 1, 1, 1, 0, 0, 0, 0 },
                                       { 1, 1, 1, 1, 1, 0, 0 },
                                       { 1, 1, 1, 1, 1, 1, 1 } };

        public int player = 1;
        public int num;
        public int line = 99;

        public void Print()
        {
            Console.WriteLine("-----------------");
            Console.WriteLine($"   {M[0, 0]}\n  {M[1, 0]}{M[1, 1]}{M[1, 2]}\n {M[2, 0]}{M[2, 1]}{M[2, 2]}{M[2, 3]}{M[2, 4]}\n{M[3, 0]}{M[3, 1]}{M[3, 2]}{M[3, 3]}{M[3, 4]}{M[3, 5]}{M[3, 6]}");
            Console.WriteLine("-----------------");
        }
        public int Suma(int x)
        {
            int ret = 0;
            for (int i = 0; i < 7; ++i)
            {
                ret = ret + M[x, i];
            }
            
            return ret;
        }

        public void Start()
        {
            while (Suma(0) + Suma(1) + Suma(2) + Suma(3) != 1)
            {
                Console.WriteLine("-------------------");
                Console.WriteLine(Suma(0));
                Console.WriteLine(Suma(1));
                Console.WriteLine(Suma(2));
                Console.WriteLine(Suma(3));
                Print();
                Console.WriteLine($"Player{player} on turn:");
                num = Convert.ToInt32(Console.ReadLine());
                if (num == 99)
                {
                    line = 99;
                    if (player == 1)
                    {
                        player = 2;
                    }
                    else
                    {
                        player = 1;
                    }
                }
                else if(line == 99 || line == num / 10)
                {
                    int b = num / 10;
                    int c = num % 10;
                    line = b;
                    M[b, c] = 0;
                }
                
            }
            Console.WriteLine($"Player{player} won!");
        }
    }
}
