# ZadanieRekrutacyjneWebApi
Przed uruchomieniem api:
1. Skonfigurować połączenie z lokalną bazą danych podmieniając connection stringa w pliku appsettings.json
2. W pakiecie menadżera pakietu wywołać polecenie update-database, dzięki któremu Entity Framework utworzy tabele w bazie danych.
3. Najpierw uruchomić metodę POST, dopiero potem GET.
