# GALERIA OBRAZÓW (back-end)
## REST API Entity Framework

Back-endowa część projektu galerii obrazów - https://spichlerz-form-i-ksztaltow.netlify.app/
Link do swaggera: https://spichlerz.azurewebsites.net/swagger/index.html

Projekt powstał po ukończeniu kursów:
Praktyczny kurs ASP.NET Core REST Web API od podstaw (C#) - Jakub Kozera
Kompletny kurs C# dla developerów .NET od eksperta +praktyka - Jakub Kozera

#### Użyte technologie:
.NET 7 Entity Framework
Azure Webapp
język C#

front-end: Vue

### Funkcjonalność:

# Kontroler galerii

publicznie
- pobieranie danych wielu obrazów (paginacja, filtr, sortowanie) oraz ich adresy URL (GetAll)
- pobieranie rozszerzonych danych o konkretnym obrazie (GetById)
- pobieranie miniaturek dla wybranych kolekcji (GetAllThumbnails)

administrator
- dodawanie postów - wgrywanie plików oraz danych tekstowych (CreateGalleryPost)
- modyfikacja postów - opcjonalne wgrywanie nowych plików, zmiana danych (Update)
- usuwanie postu (Delete)
- usuwanie wszystkich postów (deleteAll)
- podmienianie id postów w celu zmiany domyślnej kolejności (UpdateOrder)

# Kontroler admina
- logowanie - autoryzacja, token JWT (Login)
- zmiana hasła - szyfrowanie haseł (Update)

Dodatkowo aplikacja wykorzystuje:
- modele DTO
- tworzenie bazy danych, zasilanie jej przykładowymi postami oraz kontem administratora
- loger oraz middleware do obsługi błędów

### Przypisy

- Autor: Jakub Szklarski

### Kontakt

jakubszklarski1@gmail.com
