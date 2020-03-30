using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2019, 5, "Céges autók", 3)]
    class Y2019M05
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4_Ceges_autok\\autok.txt");

        // egy autó adatait tartalmazó osztály
        class Auto
        {
            // a nap száma
            public int Nap { get; }
            // az idöpont
            public TimeSpan Ido { get; }
            // az autó rendszáma
            public string Rendszam { get; }
            // a személy azonosítója
            public string Szemely { get; }
            // a km számláló állása
            public int Km { get; }
            // ki vagy be? igaz, ha ki
            public bool Ki { get; }

            public Auto(int nap, TimeSpan ido, string rendszam, string szemely, int km, bool ki)
            {
                Nap = nap;
                Ido = ido;
                Rendszam = rendszam;
                Szemely = szemely;
                Km = km;
                Ki = ki;
            }
        }

        // az autókat tartalmazó lista
        static List<Auto> autok = new List<Auto>();

        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat6();
            Feladat7();
        }

        static void Feladat1()
        {
            using (var reader = System.IO.File.OpenText(Be))
            {
                while (!reader.EndOfStream)
                {
                    // egy sor szóközzel tagolva
                    var sor = reader.ReadLine().Split(' ');
                    autok.Add(new Auto(
                        int.Parse(sor[0]),      // nap
                        TimeSpan.Parse(sor[1]), // idöpont
                        sor[2],                 // rendszám
                        sor[3],                 // személy
                        int.Parse(sor[4]),      // km
                        sor[5] == "0"           // ki?
                        ));
                }
            }
        }

        static void Feladat2()
        {
            Kiir(2);
            // végigmegyünk az autókon visszafelé
            for (int i = autok.Count - 1; i >= 0; i--)
            {
                // ha az autót kivitték
                if (autok[i].Ki)
                {
                    // kiírjuk a nap számát és a rendszámot
                    Console.WriteLine($"{autok[i].Nap}. nap rendszám: {autok[i].Rendszam}");
                    // kilépünk a ciklusból
                    break;
                }
            }
        }

        static void Feladat3()
        {
            Kiir(3);
            // bekérjük egy nap számát
            Console.Write("Nap: ");
            int nap = int.Parse(Console.ReadLine());
            // végigmegyünk a listán
            for (int i = 0; i < autok.Count; i++)
            {
                // ha az esemény az adott napon van
                if (autok[i].Nap == nap)
                {
                    // kiírjuk az idöt, a rendszámot, a személyt és hogy ki vagy be
                    Console.WriteLine($"{autok[i].Ido:hh\\:mm} {autok[i].Rendszam} {autok[i].Szemely} {(autok[i].Ki ? "ki" : "be")}");
                }
            }
        }

        static void Feladat4()
        {
            Kiir(4);
            // a kint lévö autók száma
            int kintlevoAutok = 0;
            // végigmegyünk a listán
            for (int i = 0; i < autok.Count; i++)
            {
                // ha az autót kivitték, akkor +1
                if (autok[i].Ki)
                    kintlevoAutok++;
                // különben -1
                else
                    kintlevoAutok--;
            }
            // kiírjuk az eredményt
            Console.WriteLine($"A hónap végén {kintlevoAutok} autót nem hoztak vissza.");
        }

        static void Feladat5()
        {
            Kiir(5);
            // a kezdö km állások autónként
            int[] kezdoKm = new int[10];
            // az utolsó km állás autónként
            int[] vegKm = new int[10];
            for (int i = 0; i < autok.Count; i++)
            {
                // az autó indexe a rendszám utolsó karaktere alapján
                var index = (int)autok[i].Rendszam[5] - (int)'0';
                // ha a kezdö km 0, akkor eltároljuk a km-t
                if (kezdoKm[index] == 0)
                    kezdoKm[index] = autok[i].Km;
                // az utolsó km állás mindig az adott km
                vegKm[index] = autok[i].Km;
            }
            // végigmegyünk az autókon
            for (int i = 0; i < kezdoKm.Length; i++)
            {
                // kiírjuk a rendszámot (az utolsó karakter az index; 0-9)
                // és a megtett távot (utolsó km - start km)
                Console.WriteLine($"CEG30{i} {vegKm[i] - kezdoKm[i]} km");
            }
        }

        static void Feladat6()
        {
            Kiir(6);
            // az elözö km állások autónként
            int[] kezdoKm = new int[10];
            // a leghosszabb megtett táv
            int leghosszabbTav = 0;
            // a személy azonosítója
            string szemely = "";
            // végigmegyünk az autókon
            for (int i = 0; i < autok.Count; i++)
            {
                // megállapitjuk az idexet a rendszám alapján
                var index = (int)autok[i].Rendszam[5] - (int)'0';
                // ha kivitték az autót, akkor eltároljuk a km állást
                if (autok[i].Ki)
                {
                    kezdoKm[index] = autok[i].Km;
                }
                // különben ha visszahozták
                else
                {
                    // megállapítjuk a megtett km-t
                    var km = autok[i].Km - kezdoKm[index];
                    // ha ez nagyobb, mint az eddigi leghosszabb távolság
                    if (km > leghosszabbTav)
                    {
                        // eltároljuk a személy azonosítóját
                        szemely = autok[i].Szemely;
                        // és a megtett km-t
                        leghosszabbTav = km;
                    }
                }
            }
            // kiírjuk az eredményt
            Console.WriteLine($"Leghosszabb út: {leghosszabbTav} km, személy: {szemely}");
        }

        static void Feladat7()
        {
            Kiir(7);
            // bekérünk egy rendszámot
            Console.Write("Rendszám: ");
            string rendszam = Console.ReadLine();

            using (var writer = System.IO.File.CreateText(System.IO.Path.Combine(Program.BasePath, $"megoldas\\{rendszam}_menetlevel.txt")))
            {
                // végigmegyünk az autókon
                for (int i = 0; i < autok.Count; i++)
                {
                    // ha a keresett rendszám megegyezik az autó rendszámával
                    if (autok[i].Rendszam == rendszam)
                    {
                        // ha kivitték az autót,
                        // kiírjuk a személyt, a napot és az idöt és a km-t (sortörés nélkül)
                        if (autok[i].Ki)
                            writer.Write($"{autok[i].Szemely}\t{autok[i].Nap}. {autok[i].Ido:hh\\:mm}\t{autok[i].Km} km");
                        // különben a napot és az idöt és a km-t (sortöréssel)
                        else
                            writer.WriteLine($"\t{autok[i].Nap}. {autok[i].Ido:hh\\:mm}\t{autok[i].Km} km");
                    }
                }
            }
            Console.WriteLine("Menetlevél kész.");
        }

        static void Kiir(int feladat)
        {
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
