-- Database Section
-- ________________ 

create database GestioneGuasti;

GO

use GestioneGuasti;

GO

-- Tables Section
-- _____________ 

create table Categorie (
     Codice numeric(2) not null,
     Nome varchar(20) not null,
     constraint IDCategorie primary key (Codice));

create table Centri_Assistenza (
     CodiceNazionale numeric(3) not null,
     Nazione char(2) not null,
     Sede varchar(40) not null,
     AreaCompetenza varchar(40) not null,
     constraint IDCentri_Assistenza primary key (CodiceNazionale, Nazione));

create table Clienti (
     NumeroTelefono varchar(10) not null,
     Nome varchar(20) not null,
     Cognome varchar(20) not null,
     Recapito varchar(40) not null,
     Email varchar(40),
     constraint IDClienti primary key (NumeroTelefono));

create table Difetti (
     ComponentCode numeric(5) not null,
     CodiceTipo numeric(4) not null,
     NomeComponente varchar(20) not null,
     NomeTipo varchar(20) not null,
     constraint IDDifetti primary key (ComponentCode, CodiceTipo));

create table Guasti (
     NumeroTelefonoCliente varchar(10) not null,
     DataRichiestaIntervento datetime not null,
     PNC numeric(11) not null,
     SNC numeric(8) not null,
     DescrizioneCliente varchar(100) not null,
     DescrizioneTecnico varchar(100),
     CategoriaProdotto numeric(2) not null,
     ComponentCode numeric(5),
     CodiceTipoDifetto numeric(4),
     constraint IDGuasti primary key (PNC, SNC, NumeroTelefonoCliente, DataRichiestaIntervento));

create table Interventi (
     NumeroTelefonoCliente varchar(10) not null,
     DataRichiesta datetime not null,
     Stato char(1) not null,
     DataVisita datetime not null,
     TempoImpiegato numeric(2),
     Nazione char(2) not null,
     CodiceOperatore numeric(6) not null,
     CodiceTecnico numeric(6),
     constraint IDInterventi_ID primary key (NumeroTelefonoCliente, DataRichiesta));

create table Operatori (
     Codice numeric(6) not null,
     CF char(16) not null,
     Nome varchar(20) not null,
     Cognome varchar(20) not null,
     DataNascita date not null,
     LuogoNascita varchar(40) not null,
     Residenza varchar(40) not null,
     Stipendio real not null,
     CodiceNazionaleCentro numeric(3) not null,
     NazioneCentro char(2) not null,
     constraint IDOperatori primary key (Codice),
     constraint IDOperatori_1 unique (CF));

create table Prodotti (
     PNC numeric(11) not null,
     SNC numeric(8) not null,
     Modello varchar(11) not null, ---!!!
     DataAcquisto date,
     DataInstallazione date,
     CodiceGaranzia numeric(2),
     CodiceCategoria numeric(2) not null,
     constraint IDProdotti primary key (PNC, SNC));

create table Progettisti (
     Codice numeric(6) not null,
     CF char(16) not null,
     Nome varchar(20) not null,
     Cognome varchar(20) not null,
     DataNascita date not null,
     LuogoNascita varchar(40) not null,
     Residenza varchar(40) not null,
     Stipendio real not null,
     constraint IDProgettisti_ID primary key (Codice),
     constraint IDProgettisti_1 unique (CF));

create table Interessi (
     CodiceCategoria numeric(2) not null,
     CodiceProgettista numeric(6) not null,
     constraint IDInteressi primary key (CodiceCategoria, CodiceProgettista));

create table Ricambi (
     Codice numeric(15) not null,
     Nome varchar(20) not null,
     CostoAcquisto smallmoney not null,
     CostoInstallazione smallmoney not null,
     ComponentCode numeric(5) not null,
     CodiceTipoDifetto numeric(4) not null,
     constraint IDRicambi primary key (Codice));

create table Tecnici (
     Codice numeric(6) not null,
     CF char(16) not null,
     Nome varchar(20) not null,
     Cognome varchar(20) not null,
     DataNascita date not null,
     LuogoNascita varchar(40) not null,
     Residenza varchar(40) not null,
     Stipendio real not null,
     DipendenteInterno bit not null,
     CodiceNazionaleCentro numeric(3) not null,
     NazioneCentro char(2) not null,
     constraint IDTecnici primary key (Codice),
     constraint IDTecnici_1 unique (CF));


-- Constraints Section
-- ___________________ 

alter table Guasti add constraint FKEffetto
     foreign key (PNC, SNC)
     references Prodotti;

alter table Guasti add constraint FKCausa_FK
     foreign key (ComponentCode, CodiceTipoDifetto)
     references Difetti;

alter table Guasti add constraint FKCausa_CHK
     check((ComponentCode is not null and CodiceTipoDifetto is not null)
           or (ComponentCode is null and CodiceTipoDifetto is null)); 

alter table Guasti add constraint FKMotivazione
     foreign key (NumeroTelefonoCliente, DataRichiestaIntervento)
     references Interventi;

alter table Interventi add constraint FKRichiesta
     foreign key (NumeroTelefonoCliente)
     references Clienti;

alter table Interventi add constraint FKApertura
     foreign key (CodiceOperatore)
     references Operatori;

alter table Interventi add constraint FKAssistenza
     foreign key (CodiceTecnico)
     references Tecnici;

alter table Operatori add constraint FKAfferenza_Op
     foreign key (CodiceNazionaleCentro, NazioneCentro)
     references Centri_Assistenza;

alter table Prodotti add constraint FKAppartenenza
     foreign key (CodiceCategoria)
     references Categorie;

alter table Interessi add constraint FKInteresse_Cat
     foreign key (CodiceCategoria)
     references Categorie;

alter table Interessi add constraint FKInteresse_Pro
     foreign key (CodiceProgettista)
     references Progettisti;

alter table Ricambi add constraint FKRiparazione
     foreign key (ComponentCode, CodiceTipoDifetto)
     references Difetti;

alter table Tecnici add constraint FKAfferenza_Tec
     foreign key (CodiceNazionaleCentro, NazioneCentro)
     references Centri_Assistenza;

/*GO

create function IDInterventi_CHK_func() returns bit as
begin
     if exists(select * from Guasti, Interventi where Guasti.NumeroTelefonoCliente = Interventi.NumeroTelefonoCliente and Guasti.DataRichiestaIntervento = Interventi.DataRichiesta)
          return 1
     return 0
end

GO

alter table Interventi with check add constraint IDInterventi_CHK
     check(dbo.IDInterventi_CHK_func() = 1);

GO

create function IDProgettisti_CHK_func() returns bit as
begin
     if exists(select * from Interessi, Progettisti where Interessi.CodiceProgettista = Progettisti.Codice)
          return 1
     return 0
end

GO

alter table Progettisti with check add constraint IDProgettisti_CHK
     check(dbo.IDProgettisti_CHK_func() = 1); 

GO*/
