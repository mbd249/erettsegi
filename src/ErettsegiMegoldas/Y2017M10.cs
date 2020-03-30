using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2017, 10, "Hiányzások", 1)]
    class Y2017M10
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4_Hianyzasok\\naplo.txt");

        // egy tanuló hiányzásait tartalmazó osztály
        class Hianyzas
        {
            // a hónap száma
            public int Honap { get; }
            // a hónap napja
            public int Nap { get; }
            // a tanuló neve
            public string Tanulo { get; }
            // az egyes órák 
            public string Orak { get; }
            // az igazolt hiányzások száma
            public int IgazoltHianyzasok { get; }
            // az igazolatlan hiányzások száma
            public int IgazolatlanHianyzasok { get; }

            public Hianyzas(int honap, int nap, string tanulo, string orak)
            {
                Honap = honap;
                Nap = nap;
                Tanulo = tanulo;
                Orak = orak;

                // végigmegyünk az egyes órákon és megállapítjuk az egyes hiányzások számát
                for (int i = 0; i < orak.Length; i++)
                {
                    // ha az adott óra karaktere X, akkor igazolt hiányzás
                    if (orak[i] == 'X')
                        IgazoltHianyzasok++;
                    // különben ha I, akkor igazolatlan
                    else if (orak[i] == 'I')
                        IgazolatlanHianyzasok++;
                }
            }
        }

        // a hiányzásokat tároló lista
        static List<Hianyzas> hianyzasok = new List<Hianyzas>();

        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat5();
            Feladat6();
            Feladat7();
        }

        static void Feladat1()
        {
            using (var reader = System.IO.File.OpenText(Be))
            {
                // a hónapot és napot tároló változók
                int honap = 1, nap = 1;
                while (!reader.EndOfStream)
                {
                    // egy sor szóközökkel tagolva
                    var sor = reader.ReadLine().Split(' '); 
                    // ha a sor #-el kezdödik
                    if (sor[0] == "#")
                    {
                        // akkor a hónapot és napot eltároljuk
                        honap = int.Parse(sor[1]);
                        nap = int.Parse(sor[2]);
                    }
                    else
                    {
                        // különben a sor elemeiböl egy hiányzást példányosítunk
                        hianyzasok.Add(
                            new Hianyzas(
                                honap,                  // hónap
                                nap,                    // nap
                                sor[0] + " " + sor[1],  // név (a Split(' ') miatt a név ketté lesz választva, egy szóközzel úja összefüzzük)
                                sor[2]                  // az órák
                            )
                        );
                    }
                }
            }
        }

        static void Feladat2()
        {
            Kiir(2);
            // a hiányzásos sorok száma a létrehozott hiányzások száma
            Console.WriteLine($"A naplóban {hianyzasok.Count} bejegyzés van.");
        }

        static void Feladat3()
        {
            Kiir(3);
            // az összes igazolt és igazolatlan hiányzás száma
            int igazolt = 0, igazolatlan = 0;
            for (int i = 0; i < hianyzasok.Count; i++)
            {
                // hozzáadjuk a megfelölelö változóhoz a hiányzások számát
                igazolt += hianyzasok[i].IgazoltHianyzasok;
                igazolatlan += hianyzasok[i].IgazolatlanHianyzasok;
            }
            // kiírjuk az eredményt
            Console.WriteLine($"Az igazolt hiányzások száma {igazolt}, az igazolatlanoké {igazolatlan} óra.");
        }

        static void Feladat5()
        {
            Kiir(5);
            // bekérjük egy hónap és nap számát
            Console.Write("A hónap sorszáma=");
            int honap = int.Parse(Console.ReadLine());
            Console.Write("A nap sorszáma=");
            int nap = int.Parse(Console.ReadLine());
            // kiírjuk az eredményt
            Console.WriteLine($"Azon a napon {hetnapja(honap, nap)} volt.");
        }

        static void Feladat6()
        {
            Kiir(6);
            // bekérjük egy nap nevét és az óra sorszámát
            Console.Write("A nap neve=");
            string nap = Console.ReadLine();
            Console.Write("Az óra sorszáma=");
            // -1 az indexelés miatt
            int ora = int.Parse(Console.ReadLine()) - 1;

            // az keresett óra hiányzásai
            int oraiHianyzasok = 0;
            // végigmegyünk a hiányzásokon
            for (int i = 0; i < hianyzasok.Count; i++)
            {
                // ha a hiányzás napja a megadott nap
                if (hetnapja(hianyzasok[i].Honap, hianyzasok[i].Nap) == nap)
                {
                    // és az adott óra karaktere X vagy I, akkor a tanuló hiányzott
                    // megnöveljük a hiányzások számát
                    switch (hianyzasok[i].Orak[ora])
                    {
                        case 'X':
                        case 'I':
                            oraiHianyzasok++;
                            break;
                    }
                }
            }
            // kiírjuk az eredményt
            Console.WriteLine($"Ekkor összesen {oraiHianyzasok} óra hiányzás történt.");
        }

        static void Feladat7()
        {
            // az egyes tanulók és azok hiányzásai
            Dictionary<string, int> tanulok = new Dictionary<string, int>();
            // a legtöbb hiányzás
            int maxHianyzas = 0;
            // végigmegyünk az összes hiányzások
            for (int i = 0; i < hianyzasok.Count; i++)
            {
                // ha a tanulónak már vannak hiányzásai, akkor hozzáadjuk az órákat
                if (tanulok.ContainsKey(hianyzasok[i].Tanulo))
                    tanulok[hianyzasok[i].Tanulo] += hianyzasok[i].IgazolatlanHianyzasok + hianyzasok[i].IgazoltHianyzasok;
                // különben hozzáadjuk a tanulót és hiányzásainak számát
                else
                    tanulok[hianyzasok[i].Tanulo] = hianyzasok[i].IgazolatlanHianyzasok + hianyzasok[i].IgazoltHianyzasok;
                // ha a tanuló többet hiányzott, mint az eddigi legtöbb, akkor eltároljuk a hiányzások számát
                if (tanulok[hianyzasok[i].Tanulo] > maxHianyzas)
                    maxHianyzas = tanulok[hianyzasok[i].Tanulo];
            }
            Console.Write($"A legtöbbet hiányzó tanulók:");
            // megkeressük azokat a tanulókat, akik maxHianyzas alkalommal hiányoztak, kiválasztjuk a nevüket
            // összefüzzük egy szóközzel és kiírjuk
            Console.WriteLine(string.Join(" ", tanulok.Where(i => i.Value == maxHianyzas).Select(i => i.Key)));
            Console.WriteLine();
        }

        // ez a függvény 1:1 a megadott kód (leszámítva a hibát a feladatban)
        static string hetnapja(int honap, int nap)
        {
            string[] napnev = { "vasarnap", "hetfo", "kedd", "szerda", "csutortok", "pentek", "szombat" };
            // A pseudokód hibás!
            // Decemberben 334 napot kell a naphoz adni, nem 335 napot!
            int[] napszam = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334 };
            var napsorszam = (napszam[honap - 1] + nap) % 7;
            return napnev[napsorszam];
        }

        static void Kiir(int feladat)
        {
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
