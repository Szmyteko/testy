## Kolejność prac
    1. Utworzenie modeli -> zrobione (należy jednak później dodać relacje)
    2. Zaimplementowanie ról dla użytkowników - admin/najemca/wynajmujący -> gotowe (Admin domyślny tworzy się przy uruchomieniu aplikacji, jeśli wcześniej nie istniał drugi o domyślnych danych logowania)
    3. Przygotowanie domyślnego stanu bazy danych, który będzie zawierał domyślne konto administratora -> Gotowe, zrobione zostały pod to migracje.
    4. Podstawowe przygotowanie kontrolerów oraz widoków, aby nawigacja na stronie była w pełni gotowa i miała już warunkowe wyświetlanie np. opcji w nawigacji Layoutu. (CRUD można nawigować później w trakcie rozwijania kontrolerów) -> częściowo wprowadzone, korpus pod rozbudowę gotowy
    4!. Skompletowanie logowania i rejestracji użytkowników. Rozbudowa pliku Register.cshtml tak, aby można było wybrać rolę najemca/wynajmujący oraz dodać pole z numerem telefonu. -> ZROBIONE
    4!!. Dodanie CRUD dla wynajmującego, aby mógł dodawać ogłoszenia na lokale
    4!!!. Zbudowanie widoku umożliwiającego przeglądanie ofert + wynajem (z rolą najemcy)
    4!!!!. Zbudowanie widoku dla Tenant/Index.cshtml, aby można było zajrzeć w szczegóły najmu lokalu oraz przeglądać swoje wynajmy
    5. Rozwijanie kolejnych kontrolerów:
        - Tenant
        - Payment
        - RentalAgreement
        - Property
        - MaintenanceRequest
    6. Dodanie panelu administratora (dodano), który będzie mógł z jednego widoku zarządzać uprawnieniami użytkowników w aplikacji (tym sposobem będzie mógł dodać kolejnego administratora przez zmianę roli zwykłego użytkownika)
    7. Obsługa błędów, dodanie widoków, które będą zawierały informacje np. o błędnym ID użytkownika. (Zabezpieczenie przez nieautoryzowanymi zmianami przez innych użytkowników)
    
    
## Wejście do aplikacji
Jako niezalogowany użytkownik mamy dostęp tylko do strony domowej (powitalnej) oraz do okna rejestracji. Po udanej rejestracji i logowaniu w zależności od wybranej roli wyświetlają się kolejne opcje nawigacji.
Admin widzi wszystko oraz ma panel do zarządzania użytkownikami. Najemcy mogą przeglądać ogłoszenia, swoje wynajęte lokale, podglądać szczegóły lokali oraz powiązanych z nimi umów oraz płatności. 
Wynajmujący ma dostęp do swoich ogłoszeń oraz do podglądu oraz edycji danych związanych z lokalem, może również wysyłać powiadomienia do najemcy o terminie płatności za lokal.

## Wyjściowy stan aplikacji (pierwsze uruchomienie)
Zaciągnięcie początkowego stanu bazy danych (bez lokali, z jednym domyślnym adminem) poprzez:
    1. DO UZUPEŁNIENIA W PRZYSZŁOŚCI.
    
## Docker
Trzeba dodać komendy dla skonfigurowania dockera dla aplikacji, aby w każdym środowisku mogła zostać otwarta. Dockerfile czy po prostu komenda do pobrania i uruchomienia kontenera z Windows Server.
POBRANIE MSSQL DLA ARM64: docker pull --platform linux/arm64 mcr.microsoft.com/mssql/server:latest
POBRANIE MSSQL DLA AMD64: docker pull --platform linux/amd64 mcr.microsoft.com/mssql/server:latest
Następnie uruchomienie utworzonego kontenera.

## Ustawienia appsettings.json
DefaultConnection dla naszej potrzeby budowania aplikacji, a następnie uruchomienia jej gdzieś indziej. Ze względu na to, że korzystamy z komputerów Mac musieliśmy odrobinkę wydłużyć DefaultConnection poprzez dodanie do niej kilku parametrów:
    "DefaultConnection": "Server=localhost,1433;Database=BeFit;User Id=SA;Password=Pa$$w0rd123;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False;TrustServerCertificate=True;Integrated Security=False;"

## NuGet
Do poprawnego działania aplikacji trzeba w menedżerze pakietów NuGet zainstalować:
    - Microsoft.VisualStudio.Web.CodeGeneration.Design
    - Microsoft.AspNetCore.Identity.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.SqlServer
    - Microsoft.AspNetCore.Identity.UI
    - Microsoft.EntityFrameworkCore.Tools
    - Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore

## Notatki