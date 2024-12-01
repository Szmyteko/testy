## Kolejność prac
    1. Utworzenie modeli -> zrobione (należy jednak później dodać relacje)
    2. Zaimplementowanie ról dla użytkowników - admin/najemca/wynajmujący
    3. Przygotowanie domyślnego stanu bazy danych, który będzie zawierał domyślne konto administratora
    4. Podstawowe przygotowanie kontrolerów oraz widoków, aby nawigacja na stronie była w pełni gotowa i miała już warunkowe wyświetlanie np. opcji w nawigacji Layoutu. (CRUD można nawigować później w trakcie rozwijania kontrolerów)
    5. Rozwijanie kolejnych kontrolerów:
        - Tenant
        - Payment
        - RentalAgreement
        - Property
        - MaintenanceRequest
    6. Dodanie panelu administratora, który będzie mógł z jednego widoku zarządzać uprawnieniami użytkowników w aplikacji (tym sposobem będzie mógł dodać kolejnego administratora przez zmianę roli zwykłego użytkownika)
    7. Obsługa błędów, dodanie widoków, które będą zawierały informacje np. o błędnym ID użytkownika. (Zabezpieczenie przez nieautoryzowanymi zmianami przez innych użytkowników)
    
    
## Wejście do aplikacji
Jako niezalogowany użytkownik mamy dostęp tylko do strony domowej (powitalnej) oraz do okna rejestracji. Po udanej rejestracji i logowaniu w zależności od wybranej roli wyświetlają się kolejne opcje nawigacji.
Admin widzi wszystko oraz ma panel do zarządzania użytkownikami. Najemcy mogą przeglądać ogłoszenia, swoje wynajęte lokale, podglądać szczegóły lokali oraz powiązanych z nimi umów oraz płatności. 
Wynajmujący ma dostęp do swoich ogłoszeń oraz do podglądu oraz edycji danych związanych z lokalem, może również wysyłać powiadomienia do najemcy o terminie płatności za lokal.

## Wyjściowy stan aplikacji (pierwsze uruchomienie)
Zaciągnięcie początkowego stanu bazy danych (bez lokali, z jednym domyślnym adminem) poprzez:
    1. 
    
## Notatki
