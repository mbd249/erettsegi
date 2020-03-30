using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2019, 10, "eUtazás", 3)]
    class Y2019M10
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4_eUtazas\\utasadat.txt");
        static string Ki = System.IO.Path.Combine(Program.BasePath, "megoldas\\figyelmeztetes.txt");

        // egy felszállás adatait tartalmazó osztály
        class Felszallas
        {
            // a megálló száma
            public int Megallo { get; }
            // a felszállás dátuma és ideje
            public DateTime Datum { get; }
            // a jegy/bérlet azonosítója
            public string Azonosito { get; }
            // a jegy/bérlet típusa
            public string Tipus { get; }
            // a fennmaradó jegyek száma
            public int JegyekSzama { get; }
            // a bérlet lejárati dátuma
            public DateTime LejaratiDatum { get; }

            public Felszallas(int megallo, DateTime datum, string azonosito, string tipus, int jegyekSzama, DateTime lejaratiDatum)
            {
                Megallo = megallo;
                Datum = datum;
                Azonosito = azonosito;
                Tipus = tipus;
                JegyekSzama = jegyekSzama;
                LejaratiDatum = lejaratiDatum;
            }
        }

        // a felszállásokat tároló lista
        static List<Felszallas> felszallasok = new List<Felszallas>();

        static void Main(string[] args)
        {
            DateTime d1 = DateTime.Today;

            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat7();
        }

        static void Feladat1()
        {
            using (var reader = System.IO.File.OpenText(Be))
            {
                while (!reader.EndOfStream)
                {
                    // egy sor adatai szóközzel tagolva
                    var sor = reader.ReadLine().Split(' ');
                    // a jegyek száma (ha nem jegy, akkor -1)
                    var jegyekSzama = sor[4].Length < 3 ? int.Parse(sor[4]) : -1;
                    // a bérlet lejárati dátuma (ha nem bérlet, akkor DateTime.MinValue (0001.01.01. 00:00:00)
                    var lejaratiDatum = sor[4].Length < 3 ? DateTime.MinValue : DateTime.ParseExact(sor[4], "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
                    felszallasok.Add(new Felszallas(
                        int.Parse(sor[0]),  // megálló
                        DateTime.ParseExact(sor[1], "yyyyMMdd-HHmm", null), // dátum
                        sor[2],             // azonosító
                        sor[3],             // típus
                        jegyekSzama,        // a jegyek száma
                        lejaratiDatum       // bérlet lejárati ideje
                        ));
                }
            }
        }

        static void Feladat2()
        {
            Kiir(2);
            // kiírjuk a felszállások számát
            Console.WriteLine($"A buszra {felszallasok.Count} utas akart felszállni.");
        }

        static void Feladat3()
        {
            Kiir(3);
            // leszállított utasok száma
            int utasok = 0;
            // végigmegyünk a felszállásokon
            for (int i = 0; i < felszallasok.Count; i++)
            {
                // ha a jegy/bérlet nem érvényes, akkor megnöveljük az utasok számát
                if (!Ervenyes(felszallasok[i]))
                    utasok++;
            }
            // kiírjuk az eredményt
            Console.WriteLine($"A buszra {utasok} utas nem szállhatott fel.");
        }

        static void Feladat4()
        {
            Kiir(4);
            // az egyes megállóban felszálló utasok száma
            int[] utasok = new int[30];
            // a megálló indexe
            // a megállók 0-29. vannak számozva!
            int megallo = 0;
            // végigmegyünk a felszállásokon
            for (int i = 0; i < felszallasok.Count; i++)
            {
                // megnöveljük a megállóban felszálló utasok számát
                utasok[felszallasok[i].Megallo]++;
                // ha ez nagyobb, mint az eddigi megállóban felszálló utasok száma
                if (utasok[felszallasok[i].Megallo] > utasok[megallo])
                {
                    // eltároljuk a megálló számát
                    megallo = felszallasok[i].Megallo;
                }
            }
            // kiírjuk az eredményt
            Console.WriteLine($"A legtöbb utas ({utasok[megallo]} fő) a {megallo}. megállóban próbált felszállni.");
        }

        static void Feladat5()
        {
            Kiir(5);
            // az ingyenesen és kedvezményesen utazók száma
            int ingyenes = 0, kedvezmenyes = 0;
            // végigmegyünk a felszállásokon
            for (int i = 0; i < felszallasok.Count; i++)
            {
                // ha a bérlet érvényes
                if (Ervenyes(felszallasok[i]))
                {
                    // akkor megvizsgáljuk a típusát
                    // és megnöveljük a kedvezményes/ingyenes bérlettel utazók számát
                    switch (felszallasok[i].Tipus)
                    {
                        case "TAB":
                        case "NYB":
                            kedvezmenyes++;
                            break;
                        case "NYP":
                        case "RVS":
                        case "GYK":
                            ingyenes++;
                            break;
                    }
                }
            }
            // kiírjuk az eredményt
            Console.WriteLine($"Ingyenesen utazók száma: {ingyenes} fő");
            Console.WriteLine($"A kedvezményesen utazók száma: {kedvezmenyes} fő");
        }

        // ez a függvény 1:1 a feladatban szereplö kód
        // DIV: /
        // MOD: %
        static int napokszama(int e1, int h1, int n1, int e2, int h2, int n2)
        {
            h1 = (h1 + 9) % 12;
            e1 = e1 - h1 / 10;
            var d1 = 365 * e1 + e1 / 4 - e1 / 100 + e1 / 400 + (h1 * 306 + 5) / 10 + n1 - 1;
            h2 = (h2 + 9) % 12;
            e2 = e2 - h2 / 10;
            var d2 = 365 * e2 + e2 / 4 - e2 / 100 + e2 / 400 + (h2 * 306 + 5) / 10 + n2 - 1;
            return d2 - d1;
        }

        static void Feladat7()
        {
            using (var writer = System.IO.File.CreateText(Ki))
            {
                // végigmegyünk a felszállásokon
                for (int i = 0; i < felszallasok.Count; i++)
                {
                    // kiszámítjuk a fennmaradó napok számát
                    // a lejárat dátumából kivonjuk a felszállás dátumát (idöpont nélkül)
                    // ha ez kisebb mint 0, akkor a lejárat a felszállás elött volt
                    // ha 0, akkor a felszállás napján jár le (és még érvényes)
                    // ha nagyobb mint 3, akkor a lejárat több mint 3 nap múlva lesz
                    var napok = (felszallasok[i].LejaratiDatum - felszallasok[i].Datum.Date).Days;
                    // ha 3 napon belül jár le a bérlet
                    if (napok >= 0 && napok < 4)
                    {
                        // kiírjuk a bérlet azonosítóját és a lejárati dátumot (yyyy-MM-dd formázással)
                        writer.WriteLine($"{felszallasok[i].Azonosito} {felszallasok[i].LejaratiDatum:yyyy-MM-dd}");
                    }
                }
            }
        }

        static bool Ervenyes(Felszallas felszallas)
        {
            // a jegy érvényes, ha több mint 0 felszállásra van lehetöség
            // a bérlet érvényes, ha a lejárat dátuma a felszállás dátumánál késöbbi
            return felszallas.JegyekSzama > 0 || felszallas.LejaratiDatum >= felszallas.Datum.Date;
        }

        static void Kiir(int feladat)
        {
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
