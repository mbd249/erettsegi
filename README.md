# Érettségi megoldások
A 2005-2019 elemt szintű informatika érettségi programozási feladatainak megoldása, követelmények összefoglalása, gyakorló feladatok.

# Miért?
Mivel a jelenlegi helyzetben az iskolák zárva vannak és így a felkészülés az érettségi vizsgákra is nehezebb (ha egyáltalán megtartják a vizsgákat?).
Hogy könnyebb legyen a felkészülés, összeszedtem azokat a tudnivalókat, amik szükségesek egy sikeres informatika érettséi programozási feladatának megoldásához.

# Megoldások
Az összes eddigi programozási feladat megoldását a Y{év}M{hónap} fájlokban találod.
Minden feladatsor részfeladatai külön kerültek megoldásra (kivéve a 2015. októberi feladatsort).
Az egyes függvényeket, ha csak a feladat nem írja elő a függvény nevét, akkor Feladat#-nak hívják.
Megpróbáltam minden egyes lépést a lehető legérthetőbben kommentekkel ellátni.
Mindegyik feladatnak több helyes megoldása van, az itt található kódok csak egy lehetséges megoldást mutatnak be.

## Különbségek a saját megoldásodtól
### Fájlok elérési útja
A beolvasandó és létrehozandó fájlok nem a program mappájában vannak, hanem a `data\{év}{hó}\` mappában. A forrásfájlok a Forrasok almappában találhatóak. A program futásakor a futtandó feladatsorhoz tartozó almappában egy megoldas nevű almappát hoz létre és ebben tárolja el a létrehozott fájl(oka)t. Ezért a feladatoknál nem csak a be- illetve kimeneti fájlok neve van megadva, hanem az elérési út a data mappában.

### Program.cs
Míg ha te oldassz meg egy feladatot, akkor ezt a Program.cs fájlban teszed, az én megoldásomban a Program.cs osztály nem tartalmaz feladatmegoldásokat, azonban az egyes feladatok megoldása úgy van felépítve, mintha a Program.cs fájlban lenne (pl. `static void Main(string[] args)`). A Program.cs teljes tartalmát hagyd figyelmen kívül, az csak arra szolgál, hogy a megfelelő feladat kerüljön futtatásra.

## Hogy használd?
### Ellenőrzésre
Ha megoldottál egy feladatot és szeretnéd ellenőrizni, hogy helyes-e az eredményed. Ez akkor is hasznos lehet, ha nem C#-ban oldottad meg a feladatot.

Ha csak ki akarod próbálni, hogy mit kellene, hogy a programod kiírjon, vagy mi a létrehozott fájl tartalma, akkor letöltheted a [programot](https://github.com/mbd249/erettsegi/releases/download/v1.0/megoldasok.zip) (34,1MB). Az állomány kicsomagolása után a FeladatValaszto.exe futtatása során kiválaszthatod a feladatot, majd a Start gombra kattintással elindul a feladathoz tartozó program. 
A Feladatlap gombra kattintva megnyithatod az adott vizsga feladatlapját. A data mappa az összes a feladatsorokhoz tartozó fájlt tartalmazza, így akár a többi, nem programozási feladatot is megoldhatod.
A feladatok neve melletti csillagok száma a feladat nehézségi szintjét jelöli (saját vélemény).

### Elakadtál?
Ha egy feladat megoldása során elakadtál és nem tudod, hogy hogyan tovább, akkor megnézheted a részfeladat kódját.


# Alapvető tudnivalók
Hogy tudjam, hogy mit kell tudni a feladatok megoldásához nekem is meg kellett oldanom őket.
Alapvetően tudni kell fájlból olvasni, fájlba írni, szöveget számmá és vissza alakítani. Képernyőre írni és onnan adatokat beolvasni.
Ezeket az ismereteket próbáltam összegyűjteni egy helyre.
Az eredményt a [csharp_101.pdf](csharp_101.pdf) fájlban találod.


# Gyakorló feladatok
Ha további gyakorlásra lenne szükséged, dolgozom néhány plusz feladaton, amik nehézségi szintje kb. az érettségi feladatokéval egyezik meg. A feladat leírásában található eredményeket kell kapnod, ha helyesen oldottad meg a feladatot.
1. [ÁFA](https://github.com/mbd249/erettsegi/tree/master/gyakorlo_feladatok/1_afa)
2. [Caesar-rejtjel](https://github.com/mbd249/erettsegi/tree/master/gyakorlo_feladatok/2_caesar)
3. [Torpedó](https://github.com/mbd249/erettsegi/tree/master/gyakorlo_feladatok/3_torpedo)

# Kérdésed van?
Ha lenne bármilyen kérdésed akkor vagy egy github issue-ban írd le, vagy csatlakozz a külön erre a célra létrehozott discord szerverhez ([https://discord.gg/R7ecNT5](https://discord.gg/R7ecNT5)) ahol megpróbálok minden kérdésre válaszolni.

# Fordítás kódból
Ha a lefordított program helyett saját magad szeretnéd lefordítani (soha nem lehetünk elég óvatosak), akkor Visual Studio 2017 vagy 2019 Community Edition-re lesz szükséged. Klónozd a repository-t vagy töltsd le a forráskódot. Nyisd meg az ErettsegiMegoldas.sln fájlt. Válaszd ki a FeladatValaszto mint indítási projektet és futtasd.
Ha egy feladatot szeretnél futás közben ellenőrizni, akkor ehelyett az EretsegiMegoldas projektet futtasd. A kívánt feladat futtatásához változtasd meg a Program.cs 17 sorában az évet és a hónapot.

``` cs
args = new string[] { "2019", "10" };
```

# Fontos!
Mind a megoldások, mind a minimum követelmény a saját véleményemen alapul és csak a saját felelősségedre használd, azokra garanciát nem vállalok.


_A feladatok kiírásánál, illetva a kód kommentjeiben az ő helyett ö, az ű helyett ü szerepel. Ennek oka, hogy a német billentyűzettel ezt a két betűt nem lehet beírni és nem akartam minden helyen ctrl+c - ctrl+v-vel beillesztgetni, mint itt._
