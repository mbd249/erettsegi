SELECT nev
FROM nyelv, fordito, szemely
WHERE nyelv.id=nyelvid AND szemelyid=szemely.id AND 
( … );
