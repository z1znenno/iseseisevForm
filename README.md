# iseseisevForm

C# (Windows Forms) rakendus, mis sisaldab kolme minirakendust: matemaatiline mäng, sobitamise mäng ja pildivaatur.

## Rakendused

**Matemaatiline mäng**
- Ülesanne on lahendada 4 tehet (+, −, ×, ÷) enne aja lõppu.
- Kolm raskusastet: Lihtne (60 s), Keskmine (45 s), Raske (30 s).
- Punktid = 100 × kordaja + järelejäänud sekundid × 10 × kordaja.
- Enne mängu tuleb sisestada nimi ja e-posti aadress, tulemused saadetakse e-postile.

**Sobitamise mäng**
- Ülesanne on leida samade kaartide paarid.
- Kolm raskusastet: 2×4, 4×4 ja 6×6.
- Kaartide tagakülje pildi saab valida nupuga „Vali pilt“.

**Pildivaatur (Galery)**
- Piltide sirvimine ringiratast, oma piltide lisamine (jpg, jpeg, png).
- Pildile joonistamine valitud värviga.
- Akna taustavärvi muutmine.
- Salvestamine vormingutes PNG, JPEG, BMP, GIF ja TIFF.

## Nõuded

- Windows ja Visual Studio
- .NET Framework (Windows Forms)
- Kaust `pildid` failidega `image1.jpg` – `image7.jpg`
- Fail `matchBG.jpg` programmi käivitatava faili kaustas
- Fail `math_background.jpg` (pole kohustuslik)
- Internetiühendus, kui soovid tulemusi e-postile saata

## Käivitamine

1. Klooni repositoorium:
```
   git clone https://github.com/z1znenno/iseseisevForm.git
```
2. Ava Visual Studios fail `iseseisevForm.slnx`.
3. Vajuta F5.

## Autor

Oleg Bereževski, Tallinna Tehnoloogiakolledž, 2026
