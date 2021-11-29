Aby zbudować plik wykonywalny ReportGenerator.exe do folderu EXE (znajdującego się "folder wyżej"), należy dwukrotnie kliknąć (uruchomić) plik makeEXE.bat. Zbudowany program będzie poprawnie działającą aplikacją konsolową.

Uwaga 1: bez wprowadzania zmian w plikach z kodem źródłowym, skrypt makeEXE.bat powinien tworzyć działającą aplikację konsolową.
UWAGA 2: stworzony program będzie działał poprawnie jeżeli przed uruchomieniem skryptu makeEXE.bat w pliku ReportGenerator/Program.cs pozostanie zakomentowana (wstawienie "//" na początku) linijka 15., a odkomentowana pozostanie linijka 17.
Uwaga 3: pozostawiając stworzony plik .exe w folderze EXE oraz posiadając "obok" folderu EXE folder Examples, w aplikacji możliwe będzie odczytywanie przykładowych grafów z plików .txt z folderu Examples oraz wykorzystanie ich w aplikacji.

========================================================================================================

Projekt z kodem źródłowym aplikacji można również uruchomić przy użyciu programu Microsoft Visual Studio 2019. Można to zrobić np. otwierając plik GraphDistance.sln przy użyciu ww. programu.
Aby uruchomić aplikację bezpośrednio ze środowiska programistycznego, należy po otworzeniu GraphDistance.sln w Visual Studio 2019 klikąć klawisz F5 bądź kombinację klawiszy Ctrl+F5.
Przygotowane testy jednostkowe można uruchomić przy pomocy kombinacji klawiszy Ctrl+F5, A.

UWAGA: skompilowany program będzie działał poprawnie jeżeli przed kompilacją w pliku ReportGenerator/Program.cs zostanie odkomentowana linijka 15., a zakomentowana zostanie linijka 17.

