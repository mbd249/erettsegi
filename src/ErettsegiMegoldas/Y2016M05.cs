using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2016, 5, "Ötszáz", 2)]
    class Y2016M05
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4_Otszaz\\penztar.txt");
        static string Ki = System.IO.Path.Combine(Program.BasePath, "megoldas\\osszeg.txt");

        // egy kosár adatait tartalmazó osztály
        class Kosar
        {
            public Dictionary<string, int> Termekek { get; } = new Dictionary<string, int>();
            public int OsszesTermek { get; set; }
        }

        // a kosarakat tároló lista
        static List<Kosar> vasarlasok = new List<Kosar>();
        // változó a beolvasott vásárlás sorszámához, a dabszámhoz
        static int vasarlasSzama = -1, darabszam = 0;
        // és a termék nevéhez
        static string termekNeve;

        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat6();
            Feladat7();
            Feladat8();
        }

        static void Feladat1()
        {
            // egy kosarat tároló változó
            Kosar kosar = new Kosar();
            using (var reader = System.IO.File.OpenText(Be))
            {
                // addig olvassuk a fájlt, míg a végére érünk
                while (!reader.EndOfStream)
                {
                    // az aktuális termék neve
                    var termek = reader.ReadLine();
                    // ha a termék == F
                    if (termek == "F")
                    {
                        // akkor a vásárlásokhoz adjuk a kosarat
                        vasarlasok.Add(kosar);
                        // és új kosarat példányosítunk
                        kosar = new Kosar();
                    }
                    // különben a terméket a kosárhoz adjuk
                    else
                    {
                        // ha a kosárban már van ilyen termék,
                        // akkor a mennyiségét megnöveljük 1-el
                        // ez az ellenörzés szükséges, mert ha a termék még nem szerepel a dictionary-ben, 
                        // akkor egy KeyNotFoundException kivételt fogunk kapni
                        if (kosar.Termekek.ContainsKey(termek))
                            kosar.Termekek[termek]++;
                        // különben a termék mennyiségét 1-re állítjuk
                        else
                            kosar.Termekek[termek] = 1;
                        // a kosárban lévö termékek számát 1-el növeljük
                        kosar.OsszesTermek++;
                    }
                }
            }
        }

        static void Feladat2()
        {
            Kiir(2);
            // kiírjuk a vásárlások számát
            Console.WriteLine($"A fizetések száma: {vasarlasok.Count}");
        }

        static void Feladat3()
        {
            Kiir(3);
            // kiírjuk az elsö vásárlás termékeinek a számát
            Console.WriteLine($"Az elsö vásárló {vasarlasok[0].OsszesTermek} darab árucikket vásárolt.");
        }

        static void Feladat4()
        {
            Kiir(4);
            // beolvassuk egy vásárlás sorszámát (-1!), egy termék nevét és a darabszámot
            Console.Write("Adja meg egy vásárlás sorszámát! ");
            vasarlasSzama = int.Parse(Console.ReadLine()) - 1;
            Console.Write("Adja meg egy árucikk nevét! ");
            termekNeve = Console.ReadLine();
            Console.Write("Adja meg a vásárolt darabszámot! ");
            darabszam = int.Parse(Console.ReadLine());
        }

        static void Feladat5()
        {
            Kiir(5);
            // az elsö és utosló vásárlás, valamint az osszes vásárlás számát tároló változók
            int elsoVasarlas = -1, utolsoVasarlas = 0, osszesVasarlas = 0;
            for (int i = 0; i < vasarlasok.Count; i++)
            {
                // ha a vásárlás során vettek a termékböl
                if (vasarlasok[i].Termekek.ContainsKey(termekNeve))
                {
                    // ha az elsö vásárlás üres (=-1), akkor beállítjuk az aktuálisat elsönek
                    if (elsoVasarlas == -1)
                        elsoVasarlas = i + 1;
                    // az utolsó vásárlás mindig az aktuális vásárlás
                    utolsoVasarlas = i + 1;
                    // megnöveljük azon vásárlások számát, ahol vettek a termékböl
                    osszesVasarlas++;
                }
            }

            // kiírjuk az eredményt
            Console.WriteLine($"Az első vásárlás sorszáma: {elsoVasarlas}");
            Console.WriteLine($"Az utolsó vásárlás sorszáma: {utolsoVasarlas}");
            Console.WriteLine($"{osszesVasarlas} vásárlás során vettek belőle.");
        }

        static void Feladat6()
        {
            Kiir(6);
            // kiírjuk a darabszám vásárláskor fizentendö összeget
            Console.WriteLine($"{darabszam} darab vételekor fizetendő: {ertek(darabszam)}");
        }

        static void Feladat7()
        {
            Kiir(7);
            // végigmegyünk a vásárlás termékein
            foreach (var item in vasarlasok[vasarlasSzama].Termekek)
            {
                // kiírjuk a vásárlolt darabszámot (Value) és a termék nevét (Key)
                Console.WriteLine($"{item.Value} {item.Key}");
            }
        }

        static void Feladat8()
        {
            using (var writer = System.IO.File.CreateText(Ki))
            {
                // végigmegyünk a vásárlásokon
                for (int i = 0; i < vasarlasok.Count; i++)
                {
                    // kiválasztjuk a vásárolt termékek feizetendö árát, majd ezek összegét
                    // kiírjuk a vásálás sorszámát (+1) és a végösszeget
                    writer.WriteLine($"{i + 1}: {vasarlasok[i].Termekek.Values.Select(v => ertek(v)).Sum()}");
                }
            }
        }

        static int ertek(int mennyiseg)
        {
            // ha egy terméket vettünk
            if (mennyiseg == 1)
                return 500;
            // ha két terméket vettünk
            else if (mennyiseg == 2)
                return 950;
            // különben az elsö két termék ára + (mennyiseg - 2) * 400
            else
                return 950 + (mennyiseg - 2) * 400;
        }

        static void Kiir(int feladat)
        {
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
