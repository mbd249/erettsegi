using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2005, 5, "Lottó", 1)]
    public class Y2005M05
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4lotto\\lottosz.dat");
        static string Ki = System.IO.Path.Combine(Program.BasePath, "megoldas\\lotto52.ki");

        // a lottószámok tárolására használt lista
        static List<byte[]> lottoszamok = new List<byte[]>();

        static void Main(string[] args)
        {
            Beolvas();
            var het52 = Feladat1();
            var het52Rendezett = Feladat2(het52);
            lottoszamok.Add(het52Rendezett);
            var het = Feladat3();
            Feladat4(het);
            Feladat5();
            Feladat6();
            Feladat7();
            Feladat8();
        }

        static void Beolvas()
        {
            // a fájl sorainak olvasása
            var sorok = System.IO.File.ReadAllLines(Be);
            foreach (var item in sorok)
            {
                // minden sort szóközönként elválasztjuk, majd az egyes elemeket byte típusúvá alakítjuk
                lottoszamok.Add(item.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(i => byte.Parse(i)).ToArray());
            }
        }

        static byte[] Feladat1()
        {
            // a feladat számának kiírása
            Kiir(1);
            // az 5 lottószám tárolására használt tömb.
            byte[] szamok = new byte[5];
            Console.WriteLine("Adja meg az 52. hét lottószámait.");
            // 5 számot kérünt be
            for (int i = 0; i < 5; i++)
            {
                Console.Write($"A(z) {i + 1}. szám: ");
                var s = Console.ReadLine();
                if (byte.TryParse(s, out var szam) && szam > 0 && szam <= 90)
                {
                    szamok[i] = szam;
                }
                else
                {
                    Console.WriteLine("Érvénytelen szám.");
                    i--;
                }
            }
            Console.WriteLine();
            return szamok;
        }

        static byte[] Feladat2(byte[] szamok)
        {
            Kiir(2);
            // a számok sorba rendezése
            szamok = szamok.OrderBy(i => i).ToArray();
            Console.WriteLine("Az 52. hét számai emelkedö sorrendben: " + string.Join(" ", szamok));
            return szamok;
        }

        static int Feladat3()
        {
            Kiir(3);
            Console.Write("Adja meg a hét számát (1-51): ");
            var s = Console.ReadLine();
            return int.Parse(s);
        }

        /// <summary>
        /// Kiirja a megadott het lottoszamait.
        /// </summary>
        /// <param name="index">A het szama (1-51)</param>
        static void Feladat4(int index)
        {
            Kiir(4);
            // -1 mert a szamok listaja 0-val kezdödik
            Console.WriteLine($"A(z) {index}. hét nyeröszámai: {string.Join(" ", lottoszamok[index - 1])}");
        }

        static void Feladat5()
        {
            Kiir(5);

            // a 90 szám húzásait ebben a tömbben tároljuk
            bool[] huzasok = new bool[90];
            // 51-ig, mert az 52. hetet nem vesszük figyelembe
            for (int i = 0; i < 51; i++)
            {
                for (int j = 0; j < lottoszamok[i].Length; j++)
                {
                    // a számnak megfelelö element "igazra" állítjuk
                    huzasok[lottoszamok[i][j] - 1] = true;
                }
            }

            // ha a tömbben van olyan elem, amelyik nem igaz, akkor van olyan szám, amit nem húztak ki
            var vanNemHuzottSzam = huzasok.Any(h => !h);

            Console.WriteLine((vanNemHuzottSzam ? "Van" : "Nincs") + " olyan szám amit nem húztak ki.");
        }

        static void Feladat6()
        {
            Kiir(6);
            int paratlanSzamok = 0;
            // 51-ig, mert az 52. hetet nem vesszük figyelembe
            for (int i = 0; i < 51; i++)
            {
                for (int j = 0; j < lottoszamok[i].Length; j++)
                {
                    // ha a szám páros, akkor a szám mod 2 = 0
                    // különben szám mod 2 = 1
                    // ezért elegendö ezt a páratlan számok számához hozzáadni
                    paratlanSzamok += lottoszamok[i][j] % 2;
                }
            }
            Console.WriteLine($"{paratlanSzamok} páratlan számot húztak ki");
        }

        static void Feladat7()
        {
            // a szám-tömböket egyesével szöveggé alakítjuk, az egyek elemeket szóközzel választjuk el
            System.IO.File.WriteAllLines(Ki, lottoszamok.Select(l => string.Join(" ", l)));
        }

        static void Feladat8()
        {
            // nem használunk WriteLinet, mert a kiírásnál új sorba fognak kerülni az adatok
            Console.Write("8. feladat");
            // az egyes számok kihúzásainak száma
            int[] huzasokSzama = new int[90];
            for (int i = 0; i < 51; i++)
            {
                for (int j = 0; j < lottoszamok[i].Length; j++)
                {
                    huzasokSzama[lottoszamok[i][j] - 1]++;
                }
            }

            for (int i = 0; i < huzasokSzama.Length; i++)
            {
                // ha az i értéke osztható 15-el (i mod 15 == 0), akkor új sort kezdünk (0 mod 15 = 0)
                if (i % 15 == 0)
                    Console.WriteLine();
                // kiírjuk az adott szám húzásainak a számát
                Console.Write(huzasokSzama[i].ToString() + " ");
            }
        }

        static void Kiir(int feladat)
        {
            // a feladat számának kiírása
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
