using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2013, 5, "Választások", 2)]
    class Y2013M05
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4_Valasztasok\\szavazatok.txt");
        static string Ki = System.IO.Path.Combine(Program.BasePath, "megoldas\\kepviselok.txt");

        // egy jelöltre leadott szavazatokat tartalmazó osztály
        class Jelolt
        {
            // a kerület száma
            public int Kerulet { get; }
            // a szavazatok száma
            public int SzavazatokSzama { get; }
            // a jelölt vezetékneve
            public string Vezeteknev { get; }
            // a jelölt utóneve
            public string Utonev { get; }
            // a jelölt pártja
            public string Part { get; }

            public Jelolt(int kerulet, int szavazatokSzama, string vezeteknev, string utonev, string part)
            {
                Kerulet = kerulet;
                SzavazatokSzama = szavazatokSzama;
                Vezeteknev = vezeteknev;
                Utonev = utonev;
                // ha a párt -, akkor a független szót tároljuk el
                Part = part == "-" ? "független" : part;
            }
        }

        // a jelölteket tároló tömb
        static Jelolt[] jeloltek;
        // a válsztópolgárok száma
        static int valasztopolgarok = 12345;
        // a leadott szavazatok száma
        static float osszesSzavazat = 0f;

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
            Kiir(1);
            Console.WriteLine("A fájl beolvasása.");
            var sorok = System.IO.File.ReadAllLines(Be);
            // a sorok száma alpján példányosítjuk a tömböt
            jeloltek = new Jelolt[sorok.Length];
            for (int i = 0; i < sorok.Length; i++)
            {
                // egy sor szóközzel tagolva
                var sor = sorok[i].Split(' ');
                jeloltek[i] = new Jelolt(
                    int.Parse(sor[0]),  // kerület
                    int.Parse(sor[1]),  // kapott szavazatok
                    sor[2],             // vezetéknév
                    sor[3],             // utónév
                    sor[4]              // párt
                    );
            }
        }

        static void Feladat2()
        {
            Kiir(2);
            // a jelöltek száma a tömb mérete
            Console.WriteLine($"A helyhatósági választáson {jeloltek.Length} képviselö indult.");
        }

        static void Feladat3()
        {
            Kiir(3);
            // beolvassuk a vezeték- és utónevet
            Console.Write("Adja meg a képviselö vezetéknevét: ");
            var vezeteknev = Console.ReadLine();
            Console.Write("Adja meg a képviselö utónevét: ");
            var utonev = Console.ReadLine();
            // végigmegyünk a jelölteken
            for (int i = 0; i < jeloltek.Length; i++)
            {
                // ha a i. jelölt neve megyegyezik a megadottal
                if (jeloltek[i].Vezeteknev.Equals(vezeteknev, StringComparison.OrdinalIgnoreCase) && jeloltek[i].Utonev.Equals(utonev, StringComparison.OrdinalIgnoreCase))
                {
                    // kiírjuk, hogy hány szavazatot kapott
                    Console.WriteLine($"{jeloltek[i].Vezeteknev} {jeloltek[i].Utonev} a helyhatósági választáson {jeloltek[i].SzavazatokSzama} szavazatot kapott.");
                    // és visszatérünk
                    return;
                }
            }
            // különben kiírjuk, hogy nincs ilyen jelölt
            Console.WriteLine("Ilyen nevü képviselöjelölt nem szereptel a nyilvántartásban!");
        }

        static void Feladat4()
        {
            Kiir(4);
            // megszámoljuk a leadott szavazatokat
            float szavazatok = 0;
            for (int i = 0; i < jeloltek.Length; i++)
            {
                szavazatok += jeloltek[i].SzavazatokSzama;
            }
            // a szavazati arány az leadott szavazat / összes választópolgár
            // a 0.00 formázással két tizedesjegyre kerekítjük
            Console.WriteLine($"A választáson {szavazatok} állampolgár, a jogosultak {szavazatok / valasztopolgarok * 100:0.00}%-a vett részt.");
            // eltároljuk az összes szavazat számát
            osszesSzavazat = szavazatok;
        }

        static void Feladat5()
        {
            Kiir(5);
            // az egyes pártokra leadott szavazatok számát tároló változók
            float gyep = 0f, hep = 0f, tisz = 0f, zep = 0f, fuggetlen = 0f;
            for (int i = 0; i < jeloltek.Length; i++)
            {
                // a jelöltek pártja alapján hozzáadjuk a szavazatokat, a pártra leadott szavazatok számához
                // független: a Jelolt osztály konstruktora a - értéket függetlenre alakítja!
                switch (jeloltek[i].Part)
                {
                    case "GYEP": gyep += jeloltek[i].SzavazatokSzama; break;
                    case "HEP": hep += jeloltek[i].SzavazatokSzama; break;
                    case "TISZ": tisz += jeloltek[i].SzavazatokSzama; break;
                    case "ZEP": zep += jeloltek[i].SzavazatokSzama; break;
                    case "független": fuggetlen += jeloltek[i].SzavazatokSzama; break;
                }
            }
            // kiírjuk az egyes pártok szereplését (pártra leadott szavazatok / összes leadott szavat)
            // 0.00 formázás a két tizedesjegyre való kerekítéshez
            Console.WriteLine("A pártokra leadott szavaztok aránya az összes szavat számához viszonyítva:");
            Console.WriteLine($"Gyümölcsevök Pártja= {gyep / osszesSzavazat * 100:0.00}%");
            Console.WriteLine($"Húsevök Pártja= {hep / osszesSzavazat * 100:0.00}%");
            Console.WriteLine($"Tejivók Szövetsége= {tisz / osszesSzavazat * 100:0.00}%");
            Console.WriteLine($"Zöldségevök Pártja= {zep / osszesSzavazat * 100:0.00}%");
            Console.WriteLine($"Független jelöltek= {fuggetlen / osszesSzavazat * 100:0.00}%");
        }

        static void Feladat6()
        {
            Kiir(6);
            // a legtöbb szavazat számát tároló változó
            int legtobbSzavazat = jeloltek[0].SzavazatokSzama;
            // végigmegyünk a jelölteken
            for (int i = 1; i < jeloltek.Length; i++)
            {
                // ha a jelölt több szavazatot kapott, mint az eddigi maximum
                if (jeloltek[i].SzavazatokSzama > legtobbSzavazat)
                {
                    // eltároljuk a szavazatok számát
                    legtobbSzavazat = jeloltek[i].SzavazatokSzama;
                }
            }

            Console.WriteLine($"A választáson legtöbb szavazatot kapott jelölt(ek):");
            // mégegyszer végigmegyünk a jelölteken
            for (int i = 0; i < jeloltek.Length; i++)
            {
                // ha a jelölt annyi szavazatot kapott, mint a legtöbb szavazat
                if (jeloltek[i].SzavazatokSzama == legtobbSzavazat)
                {
                    var jelolt = jeloltek[i];
                    // kiírjuk a jelölt nevét és pártját
                    Console.WriteLine($"{jelolt.Vezeteknev} {jelolt.Utonev} ({jelolt.Part})");
                }
            }
        }

        static void Feladat7()
        {
            Kiir(7);
            // a nyertes jelölteket tároló tömb (kerületenként)
            Jelolt[] nyertesek = new Jelolt[8];
            for (int i = 0; i < jeloltek.Length; i++)
            {
                // ha a kerületben nincs még nyertes (nyertesek[index]==null)
                // vagy az i. jelölt több szavazatot kapott, mint az éppen eltárolt jelölt
                if (nyertesek[jeloltek[i].Kerulet - 1] == null ||
                    nyertesek[jeloltek[i].Kerulet - 1].SzavazatokSzama < jeloltek[i].SzavazatokSzama)
                {
                    // akkor eltároljuk a jelöltet a kerületben
                    nyertesek[jeloltek[i].Kerulet - 1] = jeloltek[i];
                }
            }
            using (var writer = System.IO.File.CreateText(Ki))
            {
                // végigmegyünk a tömbön
                for (int i = 0; i < nyertesek.Length; i++)
                {
                    // fájlba írjuk a nyertes jelölt adatait (került = i+1)
                    writer.WriteLine($"{i + 1} {nyertesek[i].Vezeteknev} {nyertesek[i].Utonev} {nyertesek[i].Part}");
                }
            }
        }

        static void Kiir(int feladat)
        {
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
