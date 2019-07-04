*   Requisiti per compilare l'applicativo
    **  Visual Studio 2019
    **  .Net Framework 4.7.2
    **  Moduli Windows Forms e LINQ to SQL presenti

*   Requisiti per eseguire l'applicativo
    **  Sistema operativo Windows 10

*   Per poter eseguire l'applicativo, basta fare doppio clic sopra ed utilizzarlo, non è necessario alcun tipo di installazione precedente.
    Il database è contenuto all'interno di un server cloud di Microsoft Azure e pertanto è possibile accedervi indipendentemente dalla
    presenza di una copia locale dello stesso o meno.

*   Per ricreare il database da zero in locale è presente lo script "create_db.sql" che è stato fatto generare in automatico da Microsoft SQL
    Server Management Studio e testato su Windows 10 su SQL Server Management Studio stesso. Occorre prima creare il database, poi
    connettendosi ad esso si possono lanciare tutti i comandi che seguono "CREATE DATABASE" per impostare le sue proprietà e poi popolarlo
    con tabelle, viste e infine con i dati stessi.

*   Per poter provare le varie parti dell'applicazione si possono usare le seguenti credenziali:
    **  "1", "2", "3" : Operatore
    **  "4", "901", "902" : Tecnico
    **  "7" : Progettista Lavatrici e Lavastoviglie
    **  "8" : Progettista Forni
    **  "*" : Amministratore
