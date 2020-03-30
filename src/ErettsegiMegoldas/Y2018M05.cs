using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2018, 5, "Társalgó", 2)]
    class Y2018M05
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4_Tarsalgo\\ajto.txt");
        static string Ki = System.IO.Path.Combine(Program.BasePath, "megoldas\\athaladas.txt");

        // egy áthaladás adatait tartalmazó osztály
        class Athaladas
        {
            // az áthaladás ideje
            public TimeSpan Ido { get; }
            // a személy azonosítója
            public int Szemely { get; }
            // az áthaladás iránya
            public string Irany { get; }

            public Athaladas(TimeSpan ido, int szemely, string irany)
            {
                Ido = ido;
                Szemely = szemely;
                Irany = irany;
            }
        }

        // az áthaladásokat tároló lista
        static List<Athaladas> athaladasok = new List<Athaladas>();
        // a személyek száma
        static int szemelyekSzama = 0;

        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            var azonosito = Feladat6();
            Feladat7(azonosito);
            Feladat8(azonosito);
        }

        static void Feladat1()
        {
            using (var reader = System.IO.File.OpenText(Be))
            {
                while (!reader.EndOfStream)
                {
                    // egy sor szóközzel tagolva
                    var sor = reader.ReadLine().Split(' ');
                    var athaladas = new Athaladas(
                        new TimeSpan(int.Parse(sor[0]), int.Parse(sor[1]), 0),  // az áthaladás ideje
                        int.Parse(sor[2]),                                      // személy azonosítója
                        sor[3]                                                  // irány
                        );
                    athaladasok.Add(athaladas);
                    // ha a személy azonosítója nagyobb, mint a személyek száma, akkor eltároljuk az azonosítót
                    if (athaladas.Szemely > szemelyekSzama)
                        szemelyekSzama = athaladas.Szemely;
                }
            }
        }

        static void Feladat2()
        {
            Kiir(2);
            // kiírjuk az elsö belépöt
            Console.WriteLine($"Az elsö belépö: {athaladasok[0].Szemely}");
            // végigmegyünk az áthaladásokon visszafelé
            for (int i = athaladasok.Count - 1; i >= 0; i--)
            {
                // ha az áthaladás iránya "ki"
                if (athaladasok[i].Irany == "ki")
                {
                    // akkor kiírjuk az utolsó kilépö adatait
                    Console.WriteLine($"Az utolsó kilépö: {athaladasok[i].Szemely}");
                    // és kilépünk a ciklusból
                    break;
                }
            }
            Console.WriteLine();
        }

        static void Feladat3()
        {
            // a személyek áthaladásainak számát tároló tömb
            int[] szemelyek = new int[szemelyekSzama];
            for (int i = 0; i < athaladasok.Count; i++)
            {
                // minden áthaladásnál a megfelelö személy áthaladásait növeljük 1-el
                szemelyek[athaladasok[i].Szemely - 1]++;
            }
            using (var writer = System.IO.File.CreateText(Ki))
            {
                // végigmegyünk a tömbön
                for (int i = 0; i < szemelyek.Length; i++)
                {
                    // kiírjuk a személy azonosítóját (+1!) és az áthaladásainak a számát
                    writer.WriteLine($"{i + 1} {szemelyek[i]}");
                }
            }
        }

        static void Feladat4()
        {
            Kiir(4);
            // tömb, melyben azt tároljuk, hogy a személy a társalgóban van-e
            bool[] szemelyek = new bool[szemelyekSzama];
            // végigmegyün kaz áthaladásokon
            for (int i = 0; i < athaladasok.Count; i++)
            {
                // a tárolt érték igaz, ha a személy épp bemegy és hamis ha kijön
                szemelyek[athaladasok[i].Szemely - 1] = athaladasok[i].Irany == "be";
            }
            Console.Write($"A végén a társalgóban voltak:");
            // vßegigmegyünk a tömbön
            for (int i = 0; i < szemelyek.Length; i++)
            {
                // ha a tömb értéke igaz, a személy bent van
                // kiírjuk az azonosítóját (+1!)
                if (szemelyek[i])
                    Console.Write($" {i + 1}");
            }
            Console.WriteLine();
        }

        static void Feladat5()
        {
            Kiir(5);
            // az épp a társalgóban lévö személyek és a maximum tárolására szolgáló változók
            int maxTartozkodasok = 0, bentlevok = 0;
            // a bent tartózkodás idöpontja
            TimeSpan ido = TimeSpan.Zero;
            // végigmegyünk az áthaladásokon
            for (int i = 0; i < athaladasok.Count; i++)
            {
                // ha valaki bemegy
                if (athaladasok[i].Irany == "be")
                {
                    // akkor megnöveljül a bentlévök számát
                    bentlevok++;
                    // ha ez több, mint az eddigi maximum
                    if (bentlevok > maxTartozkodasok)
                    {
                        // akkor eltároljuk a bent lévök számát mint maximum
                        maxTartozkodasok = bentlevok;
                        // és az idöpontot
                        ido = athaladasok[i].Ido;
                    }
                }
                else
                {
                    // különben csökkentjük a bent lévök számát
                    bentlevok--;
                }
            }
            // kiírjuk, hogy mikor voltak bent a legtöbben
            Console.WriteLine($"Például {ido:hh\\:mm}-kor voltak a legtöbben a társalgóban.");
            Console.WriteLine();
        }

        static int Feladat6()
        {
            Kiir(6);
            // bekérjük egy személy azonosítóját
            Console.Write("Adja meg a személy azonosítóját! ");
            int azonosito = int.Parse(Console.ReadLine());
            Console.WriteLine();
            // majd visszaadjuk azt a többi feladat megoldásához
            return azonosito;
        }

        static void Feladat7(int azonosito)
        {
            Kiir(7);
            // végigmegyünk az áthaladásokon
            for (int i = 0; i < athaladasok.Count; i++)
            {
                // ha a keresett személy haladt át
                if (athaladasok[i].Szemely == azonosito)
                {
                    // és bement,
                    // akkor kiírjuk az idöt és egy kötöjelet (sortörés nélkül: Wirte()!)
                    if (athaladasok[i].Irany == "be")
                        Console.Write($"{athaladasok[i].Ido:hh\\:mm}-");
                    // különben az áthaladás idejét (sortöréssel: WriteLine()!)
                    else
                        Console.WriteLine(athaladasok[i].Ido.ToString("hh\\:mm"));
                }
            }
            Console.WriteLine();
        }

        static void Feladat8(int azonosito)
        {
            Kiir(8);
            // változó, melyben azt tároljuk, hogy a személy épp a társalgóban van-e
            bool bentVan = false;
            // a tartózkodás hossza és az utolsó belépés ideje
            TimeSpan tartozkodas = TimeSpan.Zero,
                utolsoBe = TimeSpan.Zero;
            // végigmegyünk az áthaladásokon
            for (int i = 0; i < athaladasok.Count; i++)
            {
                // ha a keresett személy
                if (athaladasok[i].Szemely == azonosito)
                {
                    // bement
                    if (athaladasok[i].Irany == "be")
                    {
                        // eltároljuk, hogy a társalgóban van
                        bentVan = true;
                        // és a belépés idejét
                        utolsoBe = athaladasok[i].Ido;
                    }
                    // különben ha kiment
                    else
                    {
                        // eltároljuk, hogy már nincs bent
                        bentVan = false;
                        // és a bent tartózkodás idejéhez hozzáadjuk a mostani bentlét idejét
                        // kilépés ideje (i. áthaladás) - utolsó belépés ideje
                        tartozkodas += (athaladasok[i].Ido - utolsoBe);
                    }
                }
            }
            // ha a vizsgált idöszak végén bent van
            if (bentVan)
            {
                // akkor az utolsó bentlét ideje a megfigyelés vég (15 óra) - belépés ideje
                // ezt is a bent tartózkodás idejéhez adjuk
                tartozkodas += (TimeSpan.FromHours(15) - utolsoBe);
            }
            // kiírjuk a bentlét idejét
            Console.Write($"A(z) {azonosito}. személy összesen {tartozkodas.TotalMinutes} percet volt bent, a megfigyelés végén ");
            // és ha bent volt, akkor azt
            if (bentVan)
                Console.WriteLine("a társalgóban volt.");
            // különben azt, hogy nem volt bent
            else
                Console.WriteLine("nem volt a társalgóban.");
        }

        static void Kiir(int feladat)
        {
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
