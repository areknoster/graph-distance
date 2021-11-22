# GraphDistance

## WSTĘP

Projekt został poświęcony problemowi poszukiwania odległości między grafami. Realizowany jest w ramach przedmiotu "Teoria Algorytmów i Obliczeń" (TAiO) na kierunku Informatyka.

## ZAWARTOŚĆ REPOZYTORIUM

Folder EXE zawiera wygenerowany plik wykonywalny aplikacji: RaportGeneartor.exe.

W folderze Examples znajdują się przykładowe pliki, z których każdy zawiera dane na temat dwóch grafów, między którymi odległość można policzyć w programie.

W folderze Source umieszczono pliki z kodem źródłowym aplikacji, w tym skrypt makeEXE.bat generujący plik wykonywalny do folderu EXE.

UWAGA 1: plik wykonywalny RaportGenerator.exe musi znajdować się w folderze EXE, aby poprawnie odczytywać pliki z folderu Examples.
UWAGA 2: folder Examples jest otwarty na rozszerzenia, tzn. użytkownik może umieścić w nim swój plik .txt zawierający dane grafów do porównania, który w programie zostany odczytany jako istniejący przykładowy plik.
UWAGA 3: poprawne wykonanie skryptu makeEXE.bat wymaga wprowadzenia zmian w kodzie źródłowym aplikacji.

## DZIAŁANIE PROGRAMU

Na początku użytkownik musi podać timeout - czas, po którym zostaną przerwane obliczenia każdego zastosowanego algorytmu obliczania odległości między grafami.

W menu programu dostępne są opcje:
- porównywanie grafów zapisanych w pliku .txt, do którego użytkownik podaje bezwzględną ścieżkę,
- porównanie grafów zapisanych w plikach z folderu Examples,
- zakończenie działania programu.
Niewłaściwy wybór opcji skutkuje zamknięciem programu.

Aby program działał poprawnie, należy postępować zgodnie z instrukcjami wyświetlanymi na konsoli. Wszelkie pojawiające się błędy również zostaną obsłużone i przedstawione użytkownikowi.

Odległość między grafami obliczana jest na podstawie maksymalnego wspólnego podgrafu indukowanego wierzchołkowo, zgodnie ze wzorem
d(G1, G2) = 1 - |mcs(G1, G2)| / max{|G1|, |G2|},
gdzie:
- |mcs(G1, G2)| to rozmiar maksymalnego wspólnego podgrafu grafów G1 i G2,
- |G1| i |G2| to rozmiary odpowiednio grafów G1 i G2.
Rozmiar grafu jest rozumiany jako liczba jego wierzchołków.

Poszukiwanie odległości odbywa sie przy pomocy algorytmu dokładnego lub pewnego algorytmu aproksymacyjnego, w zależności od wyboru użytkownika. Dla zastosowanego algorytmu zostanie wypisany jego czas działania oraz uzyskany rezultat (znaleziony podgraf).


Autorzy: Piotr Kryczka, Arkadiusz Noster, Adam Ryl


