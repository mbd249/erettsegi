using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2015, 10, "Fej vagy írás", 2)]
    class Y2015M10
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4_Fej_vagy_iras\\kiserlet.txt");
        static string Ki = System.IO.Path.Combine(Program.BasePath, "megoldas\\dobasok.txt");

        // véletlen szám generátor
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3456();
            Feladat7();
        }

        static char FejVagyIras()
        {
            // véletlen szám generálása (szám mod 2 = 0|1, ha 0, akkor fej, ha 1 akkor írás)
            return rnd.Next() % 2 == 0 ? 'F' : 'I';
        }

        static void Feladat1()
        {
            Kiir(1);
            Console.WriteLine($"A pénzfeldobás eredménye: {FejVagyIras()}");
        }

        static void Feladat2()
        {
            Kiir(2);
            Console.Write("Tippeljen! (F/I)= ");
            // bekérünk egy tippel
            var tipp = Console.ReadLine()[0];
            var eredmeny = FejVagyIras();
            // kiírjuk az eredményt
            Console.WriteLine($"A tipp {tipp}, a dobás eredménye {eredmeny} volt.");
            Console.WriteLine(tipp == eredmeny ? "Ön eltalálta." : "Ön nem találta el.");
        }

        static void Feladat3456()
        {
            // mind a 4 feladathoz ugyanúgy kellene végigmenni az összes értéken, ezért ezeket egyszerre oldjuk meg

            // a pénzfeldobások száma, az egymás utáni két fej száma, a leghosszabb fejl sorozat hossza
            int dobasokSzama = 0, duplaFejek = 0, elsoFej = 0, leghosszabbFejek = 0;
            // az éppen vizsgált fej dobások közül az elsö, az éppen vizsgált fejek száma
            int aktualisElsoFej = 0, aktualisFejekSzama = 0;
            // az összes fej száma
            float fejek = 0f;

            using (var reader = System.IO.File.OpenText(Be))
            {
                while (!reader.EndOfStream)
                {
                    // soronként megnöveljük a dobások számát
                    dobasokSzama++;
                    var dobas = reader.ReadLine();
                    // ha az érték fej
                    if (dobas == "F")
                    {
                        // ha ez az elsö fej (a legelsö dobás fej, vagy ez elött írás volt)
                        // akkor az aktualisFej változót a dobások számára állítjuk (dobások száma 1-el kezdödik!)
                        if (aktualisElsoFej == 0)
                            aktualisElsoFej = dobasokSzama;
                        // az épp vizsgált fejek száma
                        aktualisFejekSzama++;
                        // az összes fejek száma
                        fejek++;
                    }
                    // különben, ha a dobás írás
                    else
                    {
                        // ha két fej volt egymás után, megnöveljük a dupla fejek számát
                        if (aktualisFejekSzama == 2)
                            duplaFejek++;
                        // ha több fej volt egymás után, mint az eddigi legtöbb
                        if (aktualisFejekSzama > leghosszabbFejek)
                        {
                            // eltároljuk az elsö fej helyét
                            elsoFej = aktualisElsoFej;
                            // és az egymás utáni fejek számát
                            leghosszabbFejek = aktualisFejekSzama;
                        }
                        // az aktuális elsö fej és azok sz´mát visszaállítjuk 0-ra
                        aktualisElsoFej = aktualisFejekSzama = 0;
                    }
                }
            }

            // 3
            Kiir(3);
            // kiírjuk a dobások számát
            Console.WriteLine($"A kísérlet {dobasokSzama} dbásból állt.");

            // 4
            Kiir(4);
            // kiírjuk a fejek arányát (0.00 formázás a kerekítéshez)
            Console.WriteLine($"A kísérlet során a fej relatív gyakorisága {fejek / dobasokSzama * 100f:0.00}% volt.");

            // 5
            Kiir(5);
            // kiírjuk hányszor volt pontosan két fej egymás után
            Console.WriteLine($"A kísérlet során {duplaFejek} alkalommal dobtak pontosan két fejet egymás után.");

            // 6
            Kiir(6);
            // kiírjuk hány fejböl állt a leghosszabb fej sorozat és az hol kezdödött
            Console.WriteLine($"A leghosszabb tisztafej sorozat {leghosszabbFejek} tagból áll, kezdete a(z) {elsoFej}. dobás.");
        }

        static void Feladat7()
        {
            // tömb az 1000*4 dobás tárolásához
            char[] dobasok = new char[4000];
            // hány olyan FFF* sorozat volt, amelyben az utolsó érték fej és hány olyan, ahol írás
            int fej = 0, iras = 0;
            // változó annak tárolásához, hogy az elözö 3 dobás értéke mind fej volt-e
            bool mindFej = true;
            for (int i = 0; i < dobasok.Length; i++)
            {
                // "feldobjuk" az érmét
                dobasok[i] = FejVagyIras();
                // minden 4. érték esetén
                if (i % 4 == 3)
                {
                    // ha az elözö 3 dobás fej volt
                    // akkor megvizsgáljuk, hogy az eddigi 3 dobás fej volt-e
                    if (mindFej)
                    {
                        // ha a 4. dobás fej, megnöveljük a fejek számát 1-el
                        if (dobasok[i] == 'F')
                            fej++;
                        // különben az írások számát
                        else
                            iras++;
                    }
                    // visszaállítjuk a mindFej változót igazra
                    mindFej = true;
                }
                // különben az elsö 3 érték esetén
                else
                {
                    // a mindFej változó értékét állítjuk be
                    // a változó kezdö értéke igaz
                    // ha a mindFej változó igaz és a dobás fej, akkor
                    //      mindFej = igaz && igaz -> igaz
                    // különben ha a változó hamis, vagy a dobás írás
                    //      mindFej = hamis && igaz|hamis -> hamis
                    mindFej &= dobasok[i] == 'F';
                }
            }

            using (var writer = System.IO.File.CreateText(Ki))
            {
                // elöször az eredményt írjuk ki a fájba
                writer.WriteLine($"FFFF: {fej}, FFFI: {iras}");
                // utána végigmegyünk az összes dobáson
                for (int i = 0; i < dobasok.Length; i++)
                {
                    // ha ez nem a legelsö dobás, de a dobás indexe modulo 4 == 0 (4, 8, 16, ...),
                    // akkor kiírunk egy szóközt
                    if (i > 0 && i % 4 == 0)
                        writer.Write(" ");
                    // kiírjuk a dobás-t a fájba
                    writer.Write(dobasok[i]);
                }
            }
        }

        static void Kiir(int feladat)
        {
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
