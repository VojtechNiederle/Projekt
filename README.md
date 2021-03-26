### 25.3.
#### Funkce:
```
-Hra je podle pravidel kdo si vezme poslední sirku prohrává
-Konzolovka
-Pozná když někdo prohraje
-Nedovolí hráči vzít sirku z jiného řádku v jednom tahu
-hráč zvolí sirku zadáním souřadnic pole M např: 00 nebo 12 
-hráč ukončí tah zadáním čísla 99
```
#### Meotdy:
```
Print - tiskne pole M do konzole
Suma(x) - součet jedniček v řádku x
Start() - dokud není součet všech hodnot pole M roven 1 nechá hráče hrát jinak vyhlásí vítěze a ukončí cyklus
        - do proměnné num ukládá číslo zadané hráčem a konvertuje ho z string na int
```
#### AI:

([Stránka s vysvětlením algoritmu](https://www.algoritmy.net/article/30057/Nim))
([Stránka s XOR](https://zone.ni.com/reference/en-XX/help/375482B-01/multisim/xor4/))
```
-asi nejjednodužší bude převést sumy jednotlivejch řádků do binárky a a potom je XOR odečíst
tím získáme kolik sirek je třeba odebrat. Detekce z kterého řádku bude program odebírat 
ještě není domyšlena.
NIM:       BIN:
1          001  
111        011
11111      101
1111111    111
---------------
XOR:       000 (Matice je ve stavu ve kterém ji chce AI mít)

NIM:       BIN:
1          001  
111        011
10011      011
1111111    111
---------------
XOR:       110 (Od posledního řádku musíme odečíst 110 potom to bude v cajku)
```
-vzhledem k aktualnim pravidlům hry tímto algoritmem nejde vyhrát. Algoritmus bude fungovat buď ošetřením posledních tahů,
nebo změny pravidel na to že kto poslední vezme sirku vyhrál.
