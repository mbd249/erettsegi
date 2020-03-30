using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSGradSolutions
{
    [Erettsegi(2012, 5, "Futár", 2)]
    class Y2012M05
    {
        static string Be = System.IO.Path.Combine(Program.BasePath, "Forrasok\\4_Futar\\tavok.txt");
        static string Ki = System.IO.Path.Combine(Program.BasePath, "megoldas\\dijazas.txt");

        // egy fuvar adatait tároló osztály
        class Fuvar
        {
            // a nap száma (1-7)
            public byte Nap { get; }
            // a napon belüli fuvar száma (1-40)
            public byte Sorszam { get; }
            // a megtett távolsád
            public byte Tavolsag { get; }

            public Fuvar(byte nap, byte sorszam, byte tavolsag)
            {
                Nap = nap;
                Sorszam = sorszam;
                Tavolsag = tavolsag;
            }
        }

        static Fuvar[] fuvarok;

        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat6();
            Feladat7();
            var hetiDijazas = Feladat8();
            Feladat9(hetiDijazas);
        }

        static void Feladat1()
        {
            // a sorokat beolvassuk
            var sorok = System.IO.File.ReadAllLines(Be);
            List<Fuvar> lista = new List<Fuvar>();
            for (int i = 0; i < sorok.Length; i++)
            {
                // az aktuális sort szóközönként tagoljuk
                var sor = sorok[i].Split(' ');
                // a sor adatai alapján a fuvar osztályt példányosítjuk ßes a listához adjuk
                lista.Add(
                    new Fuvar(
                        byte.Parse(sor[0]), // nap
                        byte.Parse(sor[1]), // sorszám
                        byte.Parse(sor[2])  // távolság
                    )
                );
            }
            // a fuvarokat nap és sorszám szerint rendezzük és a tömbben tároljuk
            fuvarok = lista.OrderBy(f => f.Nap).ThenBy(f => f.Sorszam).ToArray();
        }

        static void Feladat2()
        {
            Kiir(2);
            // az elsö fuvar távolságának a kiírása
            Console.WriteLine($"A hét elsö útja {fuvarok[0].Tavolsag} km hosszú volt.");
        }

        static void Feladat3()
        {
            Kiir(3);
            // az utolsó út hosszának a kiírása
            Console.WriteLine($"A hét utolsó útja {fuvarok[fuvarok.Length - 1]} km hosszú volt.");
        }

        static void Feladat4()
        {
            Kiir(4);
            // a hßet napjait tároló tömb (igaz: aznap dolgozott, hamis: nem dolgozott)
            bool[] napok = new bool[7];
            for (int i = 0; i < fuvarok.Length; i++)
            {
                // van fuvar, tehát a naphoz tartozó érték a tömbben igaz
                napok[fuvarok[i].Nap - 1] = true;
            }
            Console.Write($"A futár a következö napokon nem dolgozott: ");

            bool elso = true;
            for (int i = 0; i < napok.Length; i++)
            {
                // ha az adott naphoz tartozó érték hamis
                if (!napok[i])
                {
                    // akkor aznap nem dolgozott
                    // ha ez nem az elsö ilyen nap, akkor kiírunk egy vesszöt
                    if (!elso)
                        Console.Write(", ");
                    // kiírjuk a nap számát (napok: 1-7, tömb elemei: 0-6)
                    Console.Write(i + 1);
                    elso = false;
                }
            }
            Console.WriteLine();
        }

        static void Feladat5()
        {
            Kiir(5);
            // megszámoljuk, hogy melyik napon hány futár volt, az egyes napok eredményét tömbben tároljuk
            int[] napok = new int[7];
            for (int i = 0; i < fuvarok.Length; i++)
            {
                // a fuvar napjához tartozó értéket 1-el növeljük
                napok[fuvarok[i].Nap - 1]++;
            }

            int index = 0;
            // végigmegyünk a napok tömbön, abból indulunk ki, hogy az elsö nap volt a legtöbb fuvar (index=0)
            // a tömböt a 2. naptól vizsgáljuk (i=1)
            for (int i = 1; i < napok.Length; i++)
            {
                // ha az i.napon több fuvar volt, mint az index. napon
                // akkor az indexet az adott napra állítjuk
                if (napok[i] > napok[index])
                    index = i;
            }
            // kiírjuk azt a napot, amelyiken a legtöbb fuvar volt (index+1)
            Console.WriteLine($"A letöbb fuvar a hét {index + 1}. napján volt.");
        }

        static void Feladat6()
        {
            Kiir(6);
            // mint az elözö feladatban, az egyes napokon megtett távolságokat egy tömbben tároljuk
            int[] napok = new int[7];
            for (int i = 0; i < fuvarok.Length; i++)
            {
                // a napi távhoz hozzáadjuk a i. fuvar távolságát
                napok[fuvarok[i].Nap - 1] += fuvarok[i].Tavolsag;
            }
            Console.WriteLine("A futár az alábbi távolságokat tette meg:");
            // végigmegyünk a napokon és kiírjuk a megtett távolságokat
            for (int i = 0; i < napok.Length; i++)
            {
                Console.WriteLine($"{i + 1}. nap: {napok[i]} km");
            }
        }

        static void Feladat7()
        {
            Kiir(7);
            Console.Write("Adja meg a megtett távolságot: ");
            // beolvasunk egy távolságot
            int tav = int.Parse(Console.ReadLine());
            // kiírjuk a táv díjazását
            Console.WriteLine($"A {tav} km hosszú fuvar díjazása {Ar(tav)} Ft.");
        }

        static int Feladat8()
        {
            // a teljes hét díjazását eltároljuk
            int hetiDijazas = 0;
            using (var writer = System.IO.File.CreateText(Ki))
            {
                // végigmegyünk minden fuvaron
                for (int i = 0; i < fuvarok.Length; i++)
                {
                    // megállapítjuk a fuvar díjazását
                    var dijazas = Ar(fuvarok[i].Tavolsag);
                    // fájlba írjuk a fuvar napjápt, a sorszámát és a díjazását
                    writer.WriteLine($"{fuvarok[i].Nap}. nap {fuvarok[i].Sorszam} út: {dijazas} Ft");
                    // a díjazást a heti összegehez adjuk
                    hetiDijazas += dijazas;
                }
            }
            // visszaadjuk a hét díjazását
            return hetiDijazas;
        }

        static void Feladat9(int hetiDijazas)
        {
            Kiir(9);
            // kiírjuk az elözö feladatban meghatározott heti díjazást
            Console.WriteLine($"A futár heti munkájáért {hetiDijazas} Ft-ot kap.");
        }

        // megadja egy adott távolság díjazását
        static int Ar(int km)
        {
            // 1-2 km (2<3!)
            if (km < 3)
                return 500;
            // 3-5
            else if (km < 6)
                return 700;
            // 6-10
            else if (km < 11)
                return 900;
            // 11-20
            else if (km < 21)
                return 1400;
            // 21-30
            else
                return 2000;
        }

        static void Kiir(int feladat)
        {
            Console.WriteLine($"{feladat}. feladat");
        }
    }
}
