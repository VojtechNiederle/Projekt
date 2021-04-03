using System;
using System.Linq;
using System.Threading;


namespace NIM
{
    public class Engine : Program
    {
        public int[,] M = new int[,] { { 1, 0, 0, 0, 0, 0, 0 },
                                       { 1, 1, 1, 0, 0, 0, 0 },
                                       { 1, 1, 1, 1, 1, 0, 0 },
                                       { 1, 1, 1, 1, 1, 1, 1 } };

        public string[] PNames = new string[2]; // jména hráčů
        public int player = 0; // status o tom který hráč hraje 0 nebo 1
        public int num = 0; // číslo zadávající hráč když chce odebrat sirku
        public int line = 99; // číslo řádku z kterýho hráč odebral sirku je to tu proto aby hrač nemohl brat z více řádků
         
        public void Print() // funkce na tisk matice M
        {
            Console.WriteLine("-----------------");
            Console.WriteLine($"   {M[0, 0]}\n  {M[1, 0]}{M[1, 1]}{M[1, 2]}\n {M[2, 0]}{M[2, 1]}{M[2, 2]}{M[2, 3]}{M[2, 4]}\n{M[3, 0]}{M[3, 1]}{M[3, 2]}{M[3, 3]}{M[3, 4]}{M[3, 5]}{M[3, 6]}");
            Console.WriteLine("-----------------");
        }
        public int Suma(int x) // součet hodnot v řádku (x) 
        {
            int ret = 0;
            for (int i = 0; i < 7; ++i)
            {
                ret = ret + M[x, i];
            }
           
            return ret;

        }

        public int[] NimSuma(int x) // vrací nim sumu což je hodnota Sumy řádku ve formátu např.: 7 = new int[] {4,2,1}  , nebo: 5 = new int[] {4,0,1}  
        {
            int[] ret = new int[3];
            int r = Suma(x);

            if (r-4 >=0)
            {
                r = r - 4;
                ret[0] = 4;
            }
            if (r - 2 >= 0)
            {
                r = r - 2;
                ret[1] = 2;
            }
            if (r - 1 >= 0)
            {
                r = r - 1;
                ret[2] = 1;
            }
            return ret;
        }

        public bool NimResult() // Vrací True pokud je matice připravená pro soupeře. Pokud je False hráč musí odebírat sirky z řátku s nevětším počtem sirek dokud nebude výstup True, potom hráč může ukončit tah.
        {
            int[,] Mx = new int[,] { {NimSuma(0)[0], NimSuma(0)[1], NimSuma(0)[2] }, 
                                     { NimSuma(1)[0], NimSuma(1)[1], NimSuma(1)[2] }, 
                                     { NimSuma(2)[0], NimSuma(2)[1], NimSuma(2)[2] }, 
                                     { NimSuma(3)[0], NimSuma(3)[1], NimSuma(3)[2] } };
            bool ret = true;
            if ((Mx[2, 0] + Mx[3, 0]) % 8 != 0)
            {
                ret = false;
            }
            if ((Mx[1, 1] + Mx[2, 1] + Mx[3, 1]) % 4 != 0)
            {
                ret = false;
            }
            if ((Mx[0, 2] + Mx[1, 2] + Mx[2, 2] + Mx[3, 2]) % 2 != 0)
            {
                ret = false;
            }
            return ret;

        }

        public int Bot() // akorát zjišťuje který řádek má největší sumu
        {
            int Sum = 0;
            int ret = 0;
            int[] A = {Suma(0),Suma(1),Suma(2),Suma(3)};
            for (int i = 0; i<4; ++i)
            {
                if (Suma(i) > Sum)
                {
                    Sum = Suma(i);
                    ret = i;
                }
            }
            return ret*10;
        }

        public void Start()
        {
            Console.WriteLine("Enter player1 name:");
            string p1 = Console.ReadLine();
            Console.WriteLine("Enter player2 name:");
            string p2 = Console.ReadLine();
            PNames[0] = p1;
            PNames[1] = p2;

            int x = Suma(0) + Suma(1) + Suma(2) + Suma(3);
            while (Suma(0) + Suma(1) + Suma(2) + Suma(3) != 0)
            {
                Console.Clear();
                Console.WriteLine("-------------------");
                Console.WriteLine(NimResult());
                Console.WriteLine("-------------------");
                Console.WriteLine($"Player {player} on turn:");
                Print();
                Console.WriteLine($"Player {PNames[player]} on turn:");
                if (PNames[player] != "BOT") // pokud hráč není bot tak hraje
                {
                    num = Convert.ToInt32(Console.ReadLine());
                }
                else if(PNames[player] == "BOT") // pokud hráč je bot tak jde na řadu tento process
                {
                    Thread.Sleep(1000);
                    if (line != 99 && Suma(line) == 0) // pokud už na lince není sirka bot ukončí tah.
                    {
                        num = 99;
                    }
                    else if (NimResult() == false || (NimResult() == true && line == 99)) // pokud NimResult je false nebo pokud je true ale line je 99 což se děje v prvním kole bot hraje
                    {
                        if (line == 99) // při prvním tahu což je line = 99 bot určí num
                        {
                            num = Bot();
                        }
                        else // jinak pokračuje postupným odebíráním sirek
                        {
                            num = num + 1;
                        }
                    }
                    else // jinak bot končí tah 
                    {
                        num = 99;
                    }
                }
                if (num == 99 && (Suma(0) + Suma(1) + Suma(2) + Suma(3) != x)) // pokud hráč zkončil tah tzn num = 99 a tah provedl
                {
                    x = Suma(0) + Suma(1) + Suma(2) + Suma(3);
                    line = 99;
                    if (player == 1)
                    {
                        num = 0;
                        player = 0;
                    }
                    else
                    {
                        num = 0;
                        player = 1;
                    }
                }
                else if((line == 99 || line == num / 10) && num != 99)
                {
                    int b = num / 10;
                    int c = num % 10;
                    line = b;
                    M[b, c] = 0;
                }
            }
            Console.Clear();
            Print();
            Console.WriteLine($"Player {PNames[player]} won!");
        }
    }
}
