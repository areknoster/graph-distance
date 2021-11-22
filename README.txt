# GraphDistance

Projekt został poświęcony problemowi poszukiwania odległości między grafami. Realizowany jest w ramach przedmiotu "Teoria Algorytmów i Obliczeń" (TAiO) na kierunku Informatyka.

Folder EXE zawiera pliki z kodem źródłowym aplikacji. Projekt należy otworzyć w środowisku Visual Studio 2019.

W folderze Examples znajdują się przykładowe dane wejściowe, z których program korzysta.

W menu programu dostępne są dwie opcje:
- porównywanie grafów zapisanych w pliku .txt, do którrgo użytkownik podaje ścieżkę,
- porównanie grafów zapisanych w plikach z folderu Examples.

Odległość między grafami obliczana jest na podstawie maksymalnego wspólnego podgrafu indukowanego wierzchołkowo, zgodnie ze wzorem
d(G1, G2) = 1 - |mcs(G1, G2)| / max{|G1|, |G2|},
gdzie:
- |mcs(G1, G2)| to rozmiar maksymalnego wspólnego podgrafu grafów G1 i G2,
- |G1| i |G2| to rozmiary odpowiednio grafów G1 i G2.
Rozmiar grafu jest rozumiany jako liczba jego wierzchołków.

Poszukiwanie odległości odbywa sie przy pomocy algorytmu dokładnego lub pewnego algorytmu aproksymacyjnego, w zależności od wyboru użytkownika. Dla zastosowanego algorytmu program wypisuje jego czas działania oraz odpowiedni rezultat.


Autorzy: Piotr Kryczka, Arkadiusz Noster, Adam Ryl


