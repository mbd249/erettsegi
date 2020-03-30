using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2018, 10, "Kerítés", 1)]
    class Y2018M10
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4_Kerites\\kerites.txt");
        static string Ki = System.IO.Path.Combine(Program.BasePath, "megoldas\\utcakep.txt");

        // egy telek adatait tartalmazó osztály
        class Telek
        {
            // a telekhez tartozó házszám
            public int Hazszam { get; }
            // a telek mérete
            public int Meret { get; }
            // a kerítés színe
            public char Kerites { get; }

            public Telek(int hazszam, int meret, char kerites)
            {
                Hazszam = hazszam;
                Meret = meret;
                Kerites = kerites;
            }
        }

        // a páratlan és páros telkeket tároló lista
        static List<Telek> paratlan = new List<Telek>(),
            paros = new List<Telek>();
        // az utolsó eladott telek
        static Telek utolsoTelek;

        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat6();
        }

        static void Feladat1()
        {
            // a páros és páratlan telkek házszáma
            int paratlanHazszam = 1, parossHazszam = 2;
            using (var reader = System.IO.File.OpenText(Be))
            {
                // amíg a fájl végéres érünk
                while (!reader.EndOfStream)
                {
                    // egy sor szóközzel tagolva
                    var sor = reader.ReadLine().Split(' ');
                    // a telket tároló változó
                    Telek telek;
                    // ha a telek a páros oldalon van
                    if (sor[0] == "0")
                    {
                        // példányosítjuk a telket a páros házszámmal
                        telek = new Telek(
                            parossHazszam,      // a házszám
                            int.Parse(sor[1]),  // a telek mérete
                            sor[2][0]           // a kerítés színe (karakterként
                        );
                        // hozzáadjuk a telket a páros oldal listájához
                        paros.Add(telek);
                        // a páros házszámot megnöveljük 2-vel (2,4,6,...)
                        parossHazszam += 2;
                    }
                    // különben
                    else
                    {
                        // példányosítjuk a telket a páratlan házszámmal
                        telek = new Telek(
                            paratlanHazszam,    // a házszám
                            int.Parse(sor[1]),  // a telek mérete
                            sor[2][0]           // a kerítés színe
                        );
                        // hozzáadjuk a telket a páratlan oldal listájához
                        paratlan.Add(telek);
                        // a páratlan házszámot megnöveljük 2-vel (1,3,5,...)
                        paratlanHazszam += 2;
                    }
                    // eltároljuk az aktuális telket, mint utolsót
                    utolsoTelek = telek;
                }
            }
        }

        static void Feladat2()
        {
            Kiir(2);
            // kiírjuk az eladott telkek számát (amit páros és páratlan telkek számának összege)
            Console.WriteLine($"Az eladott telkek száma: {paros.Count + paratlan.Count}");
            Console.WriteLine();
        }

        static void Feladat3()
        {
            Kiir(3);
            // az utolsó telek oldalát megkapjuk, ha a házszámot 2-vel osztjuk és megvizsgáljuk a maradékot
            // páros: házszám mod 2 = 0, páratlan: házszám mod 2 = 1
            // kiírjuk az oldalt és a telek házszámát
            Console.WriteLine($"A pár{(utolsoTelek.Hazszam % 2 == 0 ? "os" : "atlan")} oldalon adták el az utolsó telket.");
            Console.WriteLine($"Az utolsó telek házszáma: {utolsoTelek.Hazszam}");
            Console.WriteLine();
        }

        static void Feladat4()
        {
            Kiir(4);
            // végigmegyünk a telkeken az utolsó elötti telekig (Count-1)
            for (int i = 0; i < paratlan.Count-1; i++)
            {
                // ha az i. telek kerítésének színe megegyezik az következö (i+1.) telek színével
                // és a kerítés be van festve (a kerítés színe egy betü)
                if (paratlan[i].Kerites == paratlan[i + 1].Kerites && char.IsLetter(paratlan[i].Kerites))
                {
                    // kiírjuk a telek házszámát
                    Console.WriteLine($"A szomszédossal egyezik a kérítés színe: {paratlan[i].Hazszam}");
                    // és kilépünk a ciklusból
                    break;
                }
            }
            Console.WriteLine();
        }

        static void Feladat5()
        {
            Kiir(5);
            Console.Write("Adjon meg egy házszámot! ");
            // bekérünk egy házszámot
            int hazszam = int.Parse(Console.ReadLine());
            // a telek indexének meghatározása pl:
            //      páros 2: 2-1/2=0, 4: 4-1/2=1, stb.
            //      páratlan 1: 1-1/2=0, 3-1/2=1, stb.
            var index = (hazszam - 1) / 2;
            // kiírjuk a kerítés színét
            Console.WriteLine($"A kerítés színe / állapota: {paratlan[index].Kerites}");
            // végigmegyünk a lehetséges színeken
            for (char i = 'A'; i <= 'Z'; i++)
            {
                // eltároljuk, hogy van-e olyan szomszéd akinek a kerítése azonos színü
                bool vanAzonos = false;
                // a ciklus az elözö telektöl (vagy 0-tól, ha a kiválasztott telek az elsö)
                // a következö telekig (vagy az utolsóig, ha a kiválasztott telek az utolsó)
                // megvizsgálja a telkek színét
                // elözö telek: Max(0, index-1), ha index 0, akkor index-1=-1, tehát 0 nagyobb, különben nem
                // utolsó telek: Min(Count-1, index+1), ha index==Count-1 (az utolsó telek indexe), akkor Count-1 < Count, különben nem
                for (int j = Math.Max(0, index - 1); j <= Math.Min(paratlan.Count - 1, index + 1); j++)
                {
                    // ha a telek kerítésének színe megegyezik a vizsgált színnel
                    if (paratlan[j].Kerites == i)
                    {
                        // akkor van azonos színü kerítés
                        vanAzonos = true;
                        // kilépünk a ciklusból
                        break;
                    }
                }
                // ha nem volt azonos színü kerítés
                if (!vanAzonos)
                {
                    // kiírjuk a színt
                    Console.WriteLine($"Egy lehetséges festési szín: {i}");
                    // és kilépünk a ciklusból
                    break;
                }
            }
        }

        static void Feladat6()
        {
            using (var writer = System.IO.File.CreateText(Ki))
            {
                // végigmegyünk a páratlan telkeken kétszer
                for (int i = 0; i < paratlan.Count; i++)
                {
                    // elöször kiírjuk a kerítés színét
                    // a string ezen konstruktora egy karaktert n-szer ismételve hozza létre a szöveget
                    writer.Write(new string(paratlan[i].Kerites, paratlan[i].Meret));
                }
                writer.WriteLine();
                for (int i = 0; i < paratlan.Count; i++)
                {
                    // majd a házszámot
                    // string.PadRight(hossz, karakter) addig füzi a karaktert a szöveghez, 
                    // amíg annak hossze nem egyezik a megadott hosszal
                    writer.Write(paratlan[i].Hazszam.ToString().PadRight(paratlan[i].Meret, ' '));
                }
            }
        }

        static void Kiir(int feladat)
        {
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
