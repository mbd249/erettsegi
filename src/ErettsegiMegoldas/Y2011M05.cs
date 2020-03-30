using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2011, 5, "Szójáték", 3)]
    class Y2011M05
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4_Szojatek\\szoveg.txt");
        static string Ki = System.IO.Path.Combine(Program.BasePath, "megoldas\\letra.txt");

        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3();
            var szavak = Feladat4();
            Feladat5(szavak);
        }

        static void Feladat1()
        {
            Kiir(1);
            Console.Write("Adjon meg egy szót: ");
            // beolvasunk egy szót
            var szo = Console.ReadLine();
            for (int i = 0; i < szo.Length; i++)
            {
                // ha az i. karakter magánhangzó, kiírjuk, hogy van benne magánhangzó és visszatérünk
                switch (szo[i])
                {
                    case 'a':
                    case 'e':
                    case 'i':
                    case 'o':
                    case 'u':
                        Console.WriteLine("Van benne magánhangzó.");
                        return;
                }
            }
            // kiírjuk, hogy nincs benne magánhangzó
            Console.WriteLine("Nincs benne megánhangzó.");
        }

        static void Feladat2()
        {
            Kiir(2);
            string leghosszabbSzo = "";
            using (var reader = System.IO.File.OpenText(Be))
            {
                // beolvassuk a szavakat
                while (!reader.EndOfStream)
                {
                    var szo = reader.ReadLine();
                    // ha a szó hossza nagyobb, mint az eddigi leghosszabb szóé,
                    // akkor eltároljuk az aktuális szót
                    if (szo.Length > leghosszabbSzo.Length)
                        leghosszabbSzo = szo;
                }
            }
            // kiírjuk a szót és a hosszát
            Console.WriteLine($"A leghosszabb a(z) {leghosszabbSzo}, ami {leghosszabbSzo.Length} karakterböl áll.");
        }

        static void Feladat3()
        {
            Kiir(3);
            // az összes szó száma és azoké, amelyekben több a magánhangzó
            float szavakSzama = 0, tobbMaganhazosSzavak = 0;
            using (var reader = System.IO.File.OpenText(Be))
            {
                while (!reader.EndOfStream)
                {
                    var szo = reader.ReadLine();
                    int maganhagzok = 0, massalhangzok = 0;
                    // megszámoljuk a magán- és mássalhangzókat
                    for (int i = 0; i < szo.Length; i++)
                    {
                        switch (szo[i])
                        {
                            case 'a':
                            case 'e':
                            case 'i':
                            case 'o':
                            case 'u':
                                maganhagzok++;
                                break;
                            default:
                                massalhangzok++;
                                break;
                        }
                    }
                    // ha több a magánhangzó
                    if (maganhagzok > massalhangzok)
                        tobbMaganhazosSzavak++;
                    // egy szóval több
                    szavakSzama++;
                }
            }
            // kiírjuk az eredményt
            // a százalékot a 0.00 formázással 2 tizedesjegyre kerekítjük
            Console.WriteLine($"{tobbMaganhazosSzavak}/{szavakSzama} : {(tobbMaganhazosSzavak / szavakSzama) * 100f:0.00}%");
        }

        static List<string> Feladat4()
        {
            Kiir(4);
            List<string> eredmeny = new List<string>();
            // beolvassuk az ötkarakteres szavakat
            using (var reader = System.IO.File.OpenText(Be))
            {
                while (!reader.EndOfStream)
                {
                    var szo = reader.ReadLine();
                    if (szo.Length == 5)
                        eredmeny.Add(szo);
                }
            }

            Console.Write("Adjon meg egy szórészletet: ");
            // bekérjük a szórészletet
            var szoreszlet = Console.ReadLine();
            Console.WriteLine("A szórészlethez tartozó létra szavai:");
            // hogy volt-e találat
            bool talalat = false;
            for (int i = 0; i < eredmeny.Count; i++)
            {
                // ha a szórészlet az i. szó közepén van (*szoreszlet* -> IndexOf(szoreszlet)==1)
                if (eredmeny[i].IndexOf(szoreszlet) == 1)
                {
                    // ha már volt találat, kiírunk egy szóközt
                    if (talalat)
                        Console.Write(" ");
                    // kiírjuk a szót
                    Console.Write(eredmeny[i]);
                    // eltároljuk, hogy volt találat
                    talalat = true;
                }
            }
            // ha nem volt találat, kiírjuk, hogy nincs találat
            if (!talalat)
                Console.Write("A szórészlethez nem található szó.");
            Console.WriteLine();
            return eredmeny;
        }

        static void Feladat5(List<string> szavak)
        {
            Kiir(5);
            bool elso = true;
            using (var writer = System.IO.File.CreateText(Ki))
            {
                // a szavakat azok közepe (1. karaktertöl 3 karakter) szerint csoportosítjuk és rendezzük
                foreach (var csoport in szavak.GroupBy(s => s.Substring(1, 3)).OrderBy(g => g.Key))
                {
                    // ha a csoportban nincs legalább 2 szó, akkor a következöre ugrunk
                    if (csoport.Count() < 2)
                        continue;

                    // ha ez nem az elsö csoport
                    if (!elso)
                    {
                        // kiírunk egy sortörést (az üres sorokhoz)
                        writer.WriteLine();
                    }

                    // a csoport szavait egy-egy sortöréssel összekapcsolva szöveggé alakítjuk és kiírjuk
                    writer.WriteLine(string.Join(Environment.NewLine, csoport));
                    // az összes többi csoport esetében az már nem lehet az elsö csoport
                    elso = false;
                }
            }
        }

        static void Kiir(int feladat)
        {
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
