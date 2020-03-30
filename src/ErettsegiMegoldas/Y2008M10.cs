using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2008, 10, "Robot", 2)]
    public class Y2008M10
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4_Robot\\program.txt");
        static string Ki = System.IO.Path.Combine(Program.BasePath, "megoldas\\ujprog.txt");

        static string[] utasitasok;

        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
        }

        static void Feladat1()
        {
            // beolvassuk a fájl sorait
            var sorok = System.IO.File.ReadAllLines(Be);
            // az elsö sor értéke alapján példányosítjuk a tömböt
            utasitasok = new string[int.Parse(sorok[0])];
            for (int i = 0; i < utasitasok.Length; i++)
            {
                // a tömbbe írjuk a többi sor értékét
                utasitasok[i] = sorok[i + 1];
            }
        }

        static void Feladat2()
        {
            Kiir(2);
            Console.Write("Adja meg az utasítás sorszámát: ");
            int index = int.Parse(Console.ReadLine());

            // a kiválasztott utasítás (index-1, mert a tömb 0-tól van indexelve)
            var utasitas = utasitasok[index - 1];

            // 2.a
            // ha az utasításban szerepelnek a megadott karaktersorok (pl: ED), akkor egyszerüsíthetö
            // ha nem szerepel egy karaktersorozat, akkor az IndexOf() értéke -1
            if (utasitas.IndexOf("ED") > -1 || utasitas.IndexOf("DE") > -1 || utasitas.IndexOf("NK") > -1 || utasitas.IndexOf("KN") > -1)
                Console.WriteLine("Az utasítás egyszerüsíthetö.");
            else
                Console.WriteLine("Az utasítás nem egyszerüsíthetö.");

            // 2.b
            // a megtett út KN (x) és ED (y) irányban
            // mint egy koordináta-rendszerben:
            // E: fel (y+1)
            // D: le (y-1)
            // K: jobbra (x+1)
            // N: balra (x-1)
            int x = 0, y = 0;
            for (int i = 0; i < utasitas.Length; i++)
            {
                // minden egyes irányhoz eltároljuk a változásokat
                switch (utasitas[i])
                {
                    case 'E': y++; break;
                    case 'D': y--; break;
                    case 'K': x++; break;
                    case 'N': x--; break;
                }
            }
            // az x és y abszolút értékének megfelelö lépést kell tenni
            // pl: D -3 -> 3 lépést északra, E +3 -> 3 lépést délre
            Console.WriteLine($"{Math.Abs(y)} lépést kell tenni az ED, {Math.Abs(x)} lépést a KN tengely mentén.");

            // 2.c
            // eltároljuk a maximális távolságot
            double tav = 0d;
            // a maximális távolság indexét
            int maxIndex = 0;
            // ugyanaz mint az elözö feladatban
            x = y = 0;
            for (int i = 0; i < utasitas.Length; i++)
            {
                // ugyanugy számoljuk a lépésenkéti távolságot ED és KN irányban
                switch (utasitas[i])
                {
                    case 'E': y++; break;
                    case 'D': y--; break;
                    case 'K': x++; break;
                    case 'N': x--; break;
                }

                // a Pitagorasz-tétel segítségével megállapítjuk a kiindulóponttól (0;0) lévö távolságot
                // c = négyzetgyök(a2 + b2)
                // a távolságot 3 tizedesjegyre kerekítjük
                double t = Math.Round(Math.Sqrt(x * x + y * y), 3, MidpointRounding.AwayFromZero);
                // ha az adott ponton nagyobb a távolság, mint eddig, eltároljuk a távolságot és az indexet
                if (t > tav)
                {
                    tav = t;
                    maxIndex = i;
                }
            }

            // kiírjuk a távolságot és az indexet (+1!)
            Console.WriteLine($"A robot a {maxIndex + 1}. lépést követöen volt a legtávolabb.");
            Console.WriteLine($"A maxiális távolság {tav:0.000} cm.");
        }

        static void Feladat3()
        {
            Kiir(3);
            Console.WriteLine("Végrehajtható utasítások:");
            for (int i = 0; i < utasitasok.Length; i++)
            {
                // az elhasznált energia mennyisége
                int energia = 0;
                // az elözö irány (egy olyan karakter ami nem fordul elö az utasításokban)
                char elozoIrany = ' ';
                // az utasítás karakterei
                for (int j = 0; j < utasitasok[i].Length; j++)
                {
                    // ha az elözö irány nem egyezik meg a j. iránnyal,
                    // akkor vagy irányt váltottunk vagy elindultunk (elözö irány ' ')
                    if (elozoIrany != utasitasok[i][j])
                        energia += 2;
                    // a mozgáshoz is kell 1 energia
                    energia++;
                    // ha túlléptük a 100 egységet, akkor befejezhetjük a ciklust
                    if (energia > 100)
                        break;
                }
                // ha az energia kevesebb vagy egyenlö 100, akkor kiírjuk az utasítás sorszámát és a szükséges energia mennyiségét
                if (energia <= 100)
                    Console.WriteLine($"{i + 1} {energia}");
            }
        }

        static void Feladat4()
        {
            Kiir(4);
            using (var writer = System.IO.File.CreateText(Ki))
            {
                // végigmegyünk az összes utasításon
                for (int i = 0; i < utasitasok.Length; i++)
                {
                    // ha nem az elsö utasítást írjuk, akkor beszúrunk egy sortörést
                    if (i > 0)
                        writer.WriteLine();

                    // tároljuk az elozo utasítást és hány ilyen utasítás volt eddit
                    // az elso utasítással kezdünk, amiböl 1 volt
                    char elozoUtasitas = utasitasok[i][0];
                    int azonosUtasitasok = 1;

                    for (int j = 1; j < utasitasok[i].Length; j++)
                    {
                        // ha az aktualis karakter megegyezik az elozo utasitassal, akkor megnöveljük az azonos utasítások számát
                        if (elozoUtasitas == utasitasok[i][j])
                            azonosUtasitasok++;
                        else
                        {
                            // ha több azonos utasítás volt
                            if (azonosUtasitasok > 1)
                            {
                                // akkor kiírjuk az azonos utasítások számát, majd az utasítást
                                writer.Write($"{azonosUtasitasok}{elozoUtasitas}");
                            }
                            else
                            {
                                // különben csak az utasítást
                                writer.Write(elozoUtasitas);
                            }
                            // az elözö utasítást az aktuálisra állítjuk és ennek a számát 1-re
                            elozoUtasitas = utasitasok[i][j];
                            azonosUtasitasok = 1;
                        }
                    }
                    // a fájlba írjuk az utolsó utasítást
                    if (azonosUtasitasok > 1)
                        writer.Write($"{azonosUtasitasok}{elozoUtasitas}");
                    else
                        writer.Write(elozoUtasitas);
                }
            }
        }

        static void Feladat5()
        {
            Kiir(5);
            Console.Write("Adjon meg egy új formátumú utasítássort: ");
            var utasitas = Console.ReadLine();
            var sb = new StringBuilder();
            // az utasítások száma
            int szam = 0;
            // végigmegyünk az utasítás karakterein
            for (int i = 0; i < utasitas.Length; i++)
            {
                // ha a karakter szám
                if (char.IsDigit(utasitas[i]))
                {
                    // a számot hozzáadjuk az aktualis számhoz
                    // pl: a karaktersor 12D, szam=0
                    // az elsö karakter '1', tehát szam= 0*10 + 1 -> 1
                    // a következö '2' lesz, tehát szam=1*10+2 -> 12
                    szam += szam * 10 + utasitas[i] - '0';
                }
                else
                {
                    // ha a karakter nem szám, akkor az eredményhez adjuk
                    // ha elötte nem szám áll, akkor szam=0
                    // ha elötte szám áll, akkor szam>0
                    // ha szam==0, akkor egyszer akarjuk kiírni a karaktert, különben szam-szor
                    // ezért a szam és közül a nagyobbat választjuk
                    // ha a string ezen konstruktorát használjuk, akkor egy olyan szöveget kapunkt, 
                    // amiben a megadott karakter N-szer szerepel
                    sb.Append(new string(utasitas[i], Math.Max(1, szam)));
                    // a számot visszaállítjuk 0-ra
                    szam = 0;
                }
            }
            // kiírjuk az utasítást
            Console.WriteLine("Az utásítás a régi formátumban:");
            Console.WriteLine(sb.ToString());
        }

        static void Kiir(int feladat)
        {
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
