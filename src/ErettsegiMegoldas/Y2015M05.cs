using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2015, 5, "Expedíció", 3)]
    class Y2015M05
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4_Expedicio\\veetel.txt");
        static string Ki = System.IO.Path.Combine(Program.BasePath, "megoldas\\adaas.txt");

        // egy üzenet adatait tároló osztály
        class Uzenet
        {
            // az üzenet napja
            public byte Nap { get; }
            // az üzenetet fogó rádiós
            public byte Radios { get; }
            // az üzenet szöveg
            public string Szoveg { get; }

            public Uzenet(byte nap, byte radios, string szoveg)
            {
                Nap = nap;
                Radios = radios;
                Szoveg = szoveg;
            }
        }

        // az üzeneteket tároló lista
        static List<Uzenet> uzenetek = new List<Uzenet>();

        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat7();
        }

        static void Feladat1()
        {
            // beolvassuk a sorokat
            var sorok = System.IO.File.ReadAllLines(Be);
            // végigmegyünk a sorokon páronként
            for (int i = 0; i < sorok.Length; i += 2)
            {
                var sor = sorok[i].Split(' '); // az elsö sor szóközzel tagolva
                var uzenet = new Uzenet(
                    byte.Parse(sor[0]), // nap
                    byte.Parse(sor[1]), // rádiós
                    sorok[i + 1]        // üzenet
               );
                uzenetek.Add(uzenet);
            }
        }

        static void Feladat2()
        {
            Kiir(2);
            // az elsö üzenet
            Console.WriteLine($"Az elsö üzenet rögzítöje: {uzenetek[0].Radios}");
            // az utolsó üzenet
            Console.WriteLine($"Az utolsó üzenet rögzítöje: {uzenetek[uzenetek.Count - 1].Radios}");
        }

        static void Feladat3()
        {
            Kiir(3);
            // végigmegyünk az üzeneteken
            for (int i = 0; i < uzenetek.Count; i++)
            {
                // ha az üzenet szövege tartalmazza a "farkas" szót
                // akkor kiírjuk az üzenet adatait
                if (uzenetek[i].Szoveg.IndexOf("farkas") > -1)
                    Console.WriteLine($"{uzenetek[i].Nap}. nap {uzenetek[i].Radios}. radióamatör");
            }
        }

        static void Feladat4()
        {
            Kiir(4);
            // az egyes napokon fogadott üzenetek
            int[] napok = new int[11];
            for (int i = 0; i < uzenetek.Count; i++)
            {
                napok[uzenetek[i].Nap - 1]++;
            }
            for (int i = 0; i < napok.Length; i++)
            {
                // kiírjuk az adott napi feljegyzések számát
                Console.WriteLine($"{i + 1}. nap: {napok[i]} rádióamatör");
            }
        }

        static void Feladat5()
        {
            // az egyek üzeneteket tároló tömb
            char[][] helyreallitottUzenetek = new char[11][];
            // végigmegyünk az összes üzeneten
            for (int i = 1; i < uzenetek.Count; i++)
            {
                // ha ez az elsö üzenet, ami az adott naphoz tartozik,
                // akkor az üzenetet eltároljuk a tömbben
                if (helyreallitottUzenetek[uzenetek[i].Nap - 1] == null)
                    helyreallitottUzenetek[uzenetek[i].Nap - 1] = uzenetek[i].Szoveg.ToCharArray();
                else
                {
                    // különben végigmegyünk az üzenet hosszán
                    for (int j = 0; j < helyreallitottUzenetek[i].Length; j++)
                    {
                        // ha az adott karakter == #, akkor a helyreállított üzenet j. karakterét
                        // a vizsgált i. üzenet j. karakterére változtatjuk
                        if (helyreallitottUzenetek[i][j] == '#')
                            helyreallitottUzenetek[i][j] = uzenetek[i].Szoveg[j];

                    }
                }

            }

            using (var writer = System.IO.File.CreateText(Ki))
            {
                // végigmegyünk a helyreállított üzeneteken
                for (int i = 0; i < helyreallitottUzenetek.Length; i++)
                {
                    // a fájlba írjuk az egyes üzeneteket.
                    Console.WriteLine(helyreallitottUzenetek[i]);
                }
            }
        }

        // 6. Feladat 
        // ez a feladat csak a kód bemásolása
        static bool szame(string szo)
        {
            bool valasz = true;
            for (int i = 0; i < szo.Length; i++)
            {
                if (szo[i] < '0' || szo[i] > '9')
                    valasz = false;
            }
            return valasz;
        }

        static void Feladat7()
        {
            Kiir(7);
            // beolvassuk a nap és rádiós számát
            Console.Write("Adja meg a nap sorszámát! ");
            var nap = byte.Parse(Console.ReadLine());
            Console.Write("Adja meg a rádióamatör sorszámát! ");
            var radios = byte.Parse(Console.ReadLine());

            // kiválasztjuk azt az üzenetet, amit a megadott napon a megadott rádiós fogadott,
            // majd annak a szövegét
            string szoveg = uzenetek.Where(u => u.Nap == nap && u.Radios == radios).Select(u => u.Szoveg).FirstOrDefault();
            
            // ha nincs ilyen üzenet, kiírjuk azt
            if (szoveg == null)
                Console.WriteLine("Nincs ilyen feljegyzés");
            else
            {
                // különben eltároljuk a farkasok számát és az aktuális karakter indexét
                int index = 0;
                int farkasok = 0;
                int szam = 0;
                byte nulla = (byte)'0';
                // végigmegyünk a szöveg karakterein egyesével
                do
                {
                    // ha az adott karakter szám
                    if (char.IsDigit(szoveg[index]))
                    {
                        // hozzáadjuk a szam változó értékéhez
                        szam = szam * 10 + (byte)szoveg[index] - nulla;
                    }
                    else
                    {
                        // különben ha a karakter #, akkor biztosan nem tudjuk megállapítani a farkasok számát
                        if (szoveg[index] == '#')
                        {
                            farkasok = 0;
                            // ezért kilépünk a ciklusból
                            break;
                        }
                        else
                        {
                            // a karakter nem szám, de nem is #, tehát pl. szóköz vagy /
                            // az eddig megállapított számot hozzáadjuk a farkasok számához
                            farkasok += szam;
                            // a számot 0-ra állítjuk
                            szam = 0;
                            // ha a karakter nem /, akkor valószínüleg szóköz, tehát végeztünk
                            // kilépünk a ciklusból
                            if (szoveg[index] != '/')
                                break;
                        }
                    }
                    // minden karakter után az indexet 1-el növeljük
                    index++;
                } while (index < szoveg.Length);

                // ha a farkasok száma 0, akkor nincs információ
                if (farkasok == 0)
                    Console.WriteLine("Nincs információ");
                // különben kiírjuk a farkasok számát
                else
                    Console.WriteLine($"A megfigyelt egyedek száma: {farkasok}");
            }
        }

        static void Kiir(int feladat)
        {
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
