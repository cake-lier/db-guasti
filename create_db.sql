/****** Object:  Database [GestioneGuasti-2019]    Script Date: 06/07/2019 00:13:28 ******/
CREATE DATABASE [GestioneGuasti-2019];
GO
ALTER DATABASE [GestioneGuasti-2019] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET ARITHABORT OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GestioneGuasti-2019] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GestioneGuasti-2019] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GestioneGuasti-2019] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [GestioneGuasti-2019] SET  MULTI_USER 
GO
ALTER DATABASE [GestioneGuasti-2019] SET ENCRYPTION ON
GO
ALTER DATABASE [GestioneGuasti-2019] SET QUERY_STORE = ON
GO
ALTER DATABASE [GestioneGuasti-2019] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = ALL, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
/****** Object:  UserDefinedFunction [dbo].[IDInterventi_CHK_func]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[IDInterventi_CHK_func]() returns bit as
begin
     if exists(select * from Guasti, Interventi where Guasti.NumeroTelefonoCliente = Interventi.NumeroTelefonoCliente and Guasti.DataRichiestaIntervento = Interventi.DataRichiesta)
          return 1
     return 0
end
GO
/****** Object:  UserDefinedFunction [dbo].[IDProgettisti_CHK_func]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create function [dbo].[IDProgettisti_CHK_func]() returns bit as
begin
     if exists(select * from Interessi, Progettisti where Interessi.CodiceProgettista = Progettisti.Codice)
          return 1
     return 0
end
GO
/****** Object:  Table [dbo].[Interventi]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interventi](
	[NumeroTelefonoCliente] [varchar](10) NOT NULL,
	[DataRichiesta] [datetime] NOT NULL,
	[Stato] [char](1) NOT NULL,
	[DataVisita] [datetime] NOT NULL,
	[TempoImpiegato] [numeric](2, 0) NULL,
	[Zona] [varchar](40) NOT NULL,
	[CodiceOperatore] [numeric](6, 0) NOT NULL,
	[CodiceTecnico] [numeric](6, 0) NULL,
 CONSTRAINT [IDInterventi_ID] PRIMARY KEY CLUSTERED 
(
	[NumeroTelefonoCliente] ASC,
	[DataRichiesta] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tecnici]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tecnici](
	[Codice] [numeric](6, 0) NOT NULL,
	[CF] [char](16) NOT NULL,
	[Nome] [varchar](20) NOT NULL,
	[Cognome] [varchar](20) NOT NULL,
	[DataNascita] [date] NOT NULL,
	[LuogoNascita] [varchar](40) NOT NULL,
	[Residenza] [varchar](40) NOT NULL,
	[Stipendio] [real] NOT NULL,
	[DipendenteInterno] [bit] NOT NULL,
	[CodiceNazionaleCentro] [numeric](3, 0) NOT NULL,
	[NazioneCentro] [char](2) NOT NULL,
 CONSTRAINT [IDTecnici] PRIMARY KEY CLUSTERED 
(
	[Codice] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TechniciansIntervAvg]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TechniciansIntervAvg] AS SELECT Tecnici.CF, Tecnici.Nome, Tecnici.Cognome, AVG(Interventi.TempoImpiegato) AS 'TempoMedioImpiegato'
FROM Interventi JOIN Tecnici ON Interventi.CodiceTecnico = Tecnici.Codice
GROUP BY Tecnici.Codice, Tecnici.CF, Tecnici.Nome, Tecnici.Cognome
GO
/****** Object:  Table [dbo].[Centri_Assistenza]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Centri_Assistenza](
	[CodiceNazionale] [numeric](3, 0) NOT NULL,
	[Nazione] [char](2) NOT NULL,
	[Sede] [varchar](40) NOT NULL,
	[AreaCompetenza] [varchar](40) NOT NULL,
 CONSTRAINT [IDCentri_Assistenza] PRIMARY KEY CLUSTERED 
(
	[CodiceNazionale] ASC,
	[Nazione] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Operatori]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Operatori](
	[Codice] [numeric](6, 0) NOT NULL,
	[CF] [char](16) NOT NULL,
	[Nome] [varchar](20) NOT NULL,
	[Cognome] [varchar](20) NOT NULL,
	[DataNascita] [date] NOT NULL,
	[LuogoNascita] [varchar](40) NOT NULL,
	[Residenza] [varchar](40) NOT NULL,
	[Stipendio] [real] NOT NULL,
	[CodiceNazionaleCentro] [numeric](3, 0) NOT NULL,
	[NazioneCentro] [char](2) NOT NULL,
 CONSTRAINT [IDOperatori] PRIMARY KEY CLUSTERED 
(
	[Codice] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CentersIntervAvg]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CentersIntervAvg] AS SELECT Centri_Assistenza.CodiceNazionale, Centri_Assistenza.Nazione, Centri_Assistenza.Sede, Centri_Assistenza.AreaCompetenza, AVG(CAST(DATEDIFF(d, Interventi.DataRichiesta, Interventi.DataVisita) AS REAL)) AS 'TempoMedioRiparazione'
FROM (Interventi JOIN Operatori ON Interventi.CodiceOperatore = Operatori.Codice) JOIN Centri_Assistenza ON Operatori.CodiceNazionaleCentro = Centri_Assistenza.CodiceNazionale AND Operatori.NazioneCentro = Centri_Assistenza.Nazione
GROUP BY Centri_Assistenza.CodiceNazionale, Centri_Assistenza.Nazione, Centri_Assistenza.Sede, Centri_Assistenza.AreaCompetenza
GO
/****** Object:  Table [dbo].[Difetti]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Difetti](
	[ComponentCode] [numeric](5, 0) NOT NULL,
	[CodiceTipo] [numeric](4, 0) NOT NULL,
	[NomeComponente] [varchar](20) NOT NULL,
	[NomeTipo] [varchar](20) NOT NULL,
 CONSTRAINT [IDDifetti] PRIMARY KEY CLUSTERED 
(
	[ComponentCode] ASC,
	[CodiceTipo] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Guasti]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Guasti](
	[NumeroTelefonoCliente] [varchar](10) NOT NULL,
	[DataRichiestaIntervento] [datetime] NOT NULL,
	[PNC] [numeric](11, 0) NOT NULL,
	[SNC] [numeric](8, 0) NOT NULL,
	[DescrizioneCliente] [varchar](100) NOT NULL,
	[DescrizioneTecnico] [varchar](100) NULL,
	[CategoriaProdotto] [numeric](2, 0) NOT NULL,
	[ComponentCode] [numeric](5, 0) NULL,
	[CodiceTipoDifetto] [numeric](4, 0) NULL,
 CONSTRAINT [IDGuasti] PRIMARY KEY CLUSTERED 
(
	[PNC] ASC,
	[SNC] ASC,
	[NumeroTelefonoCliente] ASC,
	[DataRichiestaIntervento] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prodotti]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prodotti](
	[PNC] [numeric](11, 0) NOT NULL,
	[SNC] [numeric](8, 0) NOT NULL,
	[Modello] [varchar](11) NOT NULL,
	[DataAcquisto] [date] NULL,
	[DataInstallazione] [date] NULL,
	[CodiceGaranzia] [numeric](2, 0) NULL,
	[CodiceCategoria] [numeric](2, 0) NOT NULL,
 CONSTRAINT [IDProdotti] PRIMARY KEY CLUSTERED 
(
	[PNC] ASC,
	[SNC] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ricambi]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ricambi](
	[Codice] [numeric](15, 0) NOT NULL,
	[Nome] [varchar](20) NOT NULL,
	[CostoAcquisto] [smallmoney] NOT NULL,
	[CostoInstallazione] [smallmoney] NOT NULL,
	[ComponentCode] [numeric](5, 0) NOT NULL,
	[CodiceTipoDifetto] [numeric](4, 0) NOT NULL,
 CONSTRAINT [IDRicambi] PRIMARY KEY CLUSTERED 
(
	[Codice] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AllFaults]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[AllFaults] AS SELECT Guasti.CategoriaProdotto, Guasti.PNC, Guasti.SNC, Prodotti.Modello, Guasti.DataRichiestaIntervento, Interventi.DataVisita, Prodotti.DataAcquisto, Prodotti.DataInstallazione, Interventi.Zona, Guasti.DescrizioneCliente, Guasti.DescrizioneTecnico, Guasti.ComponentCode, Difetti.NomeComponente, Guasti.CodiceTipoDifetto, Difetti.NomeTipo AS 'NomeTipoDifetto', Ricambi.Codice AS 'CodiceRicambio', Ricambi.Nome AS 'NomeRicambio', Ricambi.CostoAcquisto, Ricambi.CostoInstallazione
FROM (((Guasti JOIN Interventi ON Guasti.NumeroTelefonoCliente = Interventi.NumeroTelefonoCliente AND Guasti.DataRichiestaIntervento = Interventi.DataRichiesta) JOIN Prodotti ON Guasti.PNC = Prodotti.PNC AND Guasti.SNC = Prodotti.SNC) JOIN Difetti ON Guasti.ComponentCode = Difetti.ComponentCode AND Guasti.CodiceTipoDifetto = Difetti.CodiceTipo) LEFT OUTER JOIN Ricambi ON Ricambi.ComponentCode = Difetti.ComponentCode AND Ricambi.CodiceTipoDifetto = Difetti.CodiceTipo
WHERE MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP)
GO
/****** Object:  View [dbo].[TopPNC]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TopPNC] AS SELECT TOP(5) WITH TIES Guasti.CategoriaProdotto, Guasti.PNC, COUNT(*) AS '#'
FROM Guasti
WHERE Guasti.ComponentCode IS NOT NULL AND Guasti.CodiceTipoDifetto IS NOT NULL AND MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Guasti.CategoriaProdotto, Guasti.PNC ORDER BY 3 DESC;
GO
/****** Object:  View [dbo].[TopComponentCode]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TopComponentCode] AS SELECT TOP(5) WITH TIES Guasti.CategoriaProdotto, Guasti.ComponentCode, Difetti.NomeComponente, COUNT(*) AS '#'
FROM Guasti JOIN Difetti ON Guasti.CodiceTipoDifetto = Difetti.CodiceTipo AND Guasti.ComponentCode = Difetti.ComponentCode
WHERE Guasti.ComponentCode IS NOT NULL AND Guasti.CodiceTipoDifetto IS NOT NULL AND MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Guasti.CategoriaProdotto, Guasti.ComponentCode, Difetti.NomeComponente ORDER BY 4 DESC;
GO
/****** Object:  View [dbo].[TopSpareParts]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TopSpareParts] AS SELECT TOP(5) WITH TIES Guasti.CategoriaProdotto, Ricambi.Codice, Ricambi.Nome, Ricambi.CostoAcquisto, Ricambi.CostoInstallazione, COUNT(*) AS '#'
FROM (Guasti JOIN Difetti ON Guasti.ComponentCode = Difetti.ComponentCode AND Guasti.CodiceTipoDifetto = Difetti.CodiceTipo) JOIN Ricambi ON Difetti.ComponentCode = Ricambi.ComponentCode AND Difetti.CodiceTipo = Ricambi.CodiceTipoDifetto
WHERE MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Ricambi.Codice, Ricambi.Nome, Ricambi.CostoAcquisto, Ricambi.CostoInstallazione, Guasti.CategoriaProdotto ORDER BY 6 DESC;
GO
/****** Object:  View [dbo].[TopCostSpareParts]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TopCostSpareParts] AS SELECT TOP(5) WITH TIES Guasti.CategoriaProdotto, Guasti.DataRichiestaIntervento, SUM(Ricambi.CostoAcquisto + Ricambi.CostoInstallazione) AS 'CostoTotale'
FROM (Guasti JOIN Difetti ON Guasti.ComponentCode = Difetti.ComponentCode AND Guasti.CodiceTipoDifetto = Difetti.CodiceTipo) JOIN Ricambi ON Difetti.ComponentCode = Ricambi.ComponentCode AND Difetti.CodiceTipo = Ricambi.CodiceTipoDifetto
WHERE MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Guasti.NumeroTelefonoCliente, Guasti.DataRichiestaIntervento, Guasti.CategoriaProdotto ORDER BY 3 DESC;
GO
/****** Object:  View [dbo].[TopZones]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[TopZones] AS SELECT Guasti.CategoriaProdotto, Interventi.Zona, COUNT(*) AS '#'
FROM Guasti JOIN Interventi ON Guasti.NumeroTelefonoCliente = Interventi.NumeroTelefonoCliente AND Guasti.DataRichiestaIntervento = Interventi.DataRichiesta
WHERE Guasti.CodiceTipoDifetto IS NOT NULL AND Guasti.ComponentCode IS NOT NULL AND MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Interventi.Zona, Guasti.CategoriaProdotto;
GO
/****** Object:  View [dbo].[TTFPurchase]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TTFPurchase] AS SELECT Guasti.CategoriaProdotto, Prodotti.PNC, Prodotti.SNC, Prodotti.CodiceGaranzia, Prodotti.Modello, DATEDIFF(d, Prodotti.DataAcquisto, Guasti.DataRichiestaIntervento) AS 'TTF (giorni)'
FROM Guasti JOIN Prodotti ON Guasti.PNC = Prodotti.PNC AND Guasti.SNC = Prodotti.SNC
WHERE Guasti.CodiceTipoDifetto IS NOT NULL AND Guasti.ComponentCode IS NOT NULL AND MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP) AND Prodotti.DataAcquisto IS NOT NULL;
GO
/****** Object:  View [dbo].[TTFInstallation]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TTFInstallation] AS SELECT Guasti.CategoriaProdotto, Prodotti.PNC, Prodotti.SNC, Prodotti.CodiceGaranzia, Prodotti.Modello, DATEDIFF(d, Prodotti.DataInstallazione, Guasti.DataRichiestaIntervento) AS 'TTF (giorni)'
FROM Guasti JOIN Prodotti ON Guasti.PNC = Prodotti.PNC AND Guasti.SNC = Prodotti.SNC
WHERE Guasti.CodiceTipoDifetto IS NOT NULL AND Guasti.ComponentCode IS NOT NULL AND MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP) AND Prodotti.DataInstallazione IS NOT NULL;
GO
/****** Object:  View [dbo].[AvgTime]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[AvgTime] AS SELECT Guasti.CategoriaProdotto, Difetti.CodiceTipo, Difetti.NomeTipo, AVG(Interventi.TempoImpiegato) AS 'TempoMedioRiparazione'
FROM (Guasti JOIN Difetti ON Guasti.ComponentCode = Difetti.ComponentCode AND Guasti.CodiceTipoDifetto = Difetti.CodiceTipo) JOIN Interventi ON Guasti.NumeroTelefonoCliente = Interventi.NumeroTelefonoCliente AND Guasti.DataRichiestaIntervento = Interventi.DataRichiesta
WHERE MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP) AND Interventi.TempoImpiegato IS NOT NULL
GROUP BY Guasti.CategoriaProdotto, Difetti.CodiceTipo, Difetti.NomeTipo;
GO
/****** Object:  View [dbo].[NewInterventions]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[NewInterventions] AS SELECT Interventi.NumeroTelefonoCliente, Interventi.DataRichiesta
FROM Interventi
WHERE Interventi.CodiceTecnico IS NULL
--ORDER BY Interventi.DataRichiesta
GO
/****** Object:  View [dbo].[OperatorsIntervCount]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[OperatorsIntervCount] AS SELECT Operatori.CF, Operatori.Nome, Operatori.Cognome, COUNT(*) AS '#'
FROM Interventi JOIN Operatori ON Interventi.CodiceOperatore = Operatori.Codice
WHERE MONTH(Interventi.DataRichiesta) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Interventi.DataRichiesta) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Operatori.Codice, Operatori.CF, Operatori.Nome, Operatori.Cognome
GO
/****** Object:  View [dbo].[TechniciansIntervCount]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[TechniciansIntervCount] AS SELECT Tecnici.CF, Tecnici.Nome, Tecnici.Cognome, COUNT(*) AS '#'
FROM Interventi JOIN Tecnici ON Interventi.CodiceTecnico = Tecnici.Codice
WHERE MONTH(Interventi.DataRichiesta) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Interventi.DataRichiesta) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Tecnici.Codice, Tecnici.CF, Tecnici.Nome, Tecnici.Cognome
GO
/****** Object:  Table [dbo].[Categorie]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorie](
	[Codice] [numeric](2, 0) NOT NULL,
	[Nome] [varchar](20) NOT NULL,
 CONSTRAINT [IDCategorie] PRIMARY KEY CLUSTERED 
(
	[Codice] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clienti]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clienti](
	[NumeroTelefono] [varchar](10) NOT NULL,
	[Nome] [varchar](20) NOT NULL,
	[Cognome] [varchar](20) NOT NULL,
	[Recapito] [varchar](40) NOT NULL,
	[Email] [varchar](40) NULL,
 CONSTRAINT [IDClienti] PRIMARY KEY CLUSTERED 
(
	[NumeroTelefono] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Interessi]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interessi](
	[CodiceCategoria] [numeric](2, 0) NOT NULL,
	[CodiceProgettista] [numeric](6, 0) NOT NULL,
 CONSTRAINT [IDInteressi] PRIMARY KEY CLUSTERED 
(
	[CodiceCategoria] ASC,
	[CodiceProgettista] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Progettisti]    Script Date: 06/07/2019 00:13:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Progettisti](
	[Codice] [numeric](6, 0) NOT NULL,
	[CF] [char](16) NOT NULL,
	[Nome] [varchar](20) NOT NULL,
	[Cognome] [varchar](20) NOT NULL,
	[DataNascita] [date] NOT NULL,
	[LuogoNascita] [varchar](40) NOT NULL,
	[Residenza] [varchar](40) NOT NULL,
	[Stipendio] [real] NOT NULL,
 CONSTRAINT [IDProgettisti_ID] PRIMARY KEY CLUSTERED 
(
	[Codice] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Categorie] ([Codice], [Nome]) VALUES (CAST(1 AS Numeric(2, 0)), N'Lavatrice')
INSERT [dbo].[Categorie] ([Codice], [Nome]) VALUES (CAST(2 AS Numeric(2, 0)), N'Lavastoviglie')
INSERT [dbo].[Categorie] ([Codice], [Nome]) VALUES (CAST(3 AS Numeric(2, 0)), N'Forno')
INSERT [dbo].[Centri_Assistenza] ([CodiceNazionale], [Nazione], [Sede], [AreaCompetenza]) VALUES (CAST(1 AS Numeric(3, 0)), N'IT', N'Roma', N'Centro Italia')
INSERT [dbo].[Centri_Assistenza] ([CodiceNazionale], [Nazione], [Sede], [AreaCompetenza]) VALUES (CAST(2 AS Numeric(3, 0)), N'IT', N'Milano', N'Italia Nord-Ovest')
INSERT [dbo].[Centri_Assistenza] ([CodiceNazionale], [Nazione], [Sede], [AreaCompetenza]) VALUES (CAST(3 AS Numeric(3, 0)), N'IT', N'Napoli', N'Sud Italia')
INSERT [dbo].[Centri_Assistenza] ([CodiceNazionale], [Nazione], [Sede], [AreaCompetenza]) VALUES (CAST(102 AS Numeric(3, 0)), N'IT', N'Venezia', N'Italia Nord-Est')
INSERT [dbo].[Clienti] ([NumeroTelefono], [Nome], [Cognome], [Recapito], [Email]) VALUES (N'1234567890', N'Mario', N'Rossi', N'Via del console 55, Bologna', NULL)
INSERT [dbo].[Clienti] ([NumeroTelefono], [Nome], [Cognome], [Recapito], [Email]) VALUES (N'2345678901', N'Luigi', N'Bianchi', N'Via Garibaldi 7, Alessandria', N'lbianchi@outlook.com')
INSERT [dbo].[Clienti] ([NumeroTelefono], [Nome], [Cognome], [Recapito], [Email]) VALUES (N'3456789012', N'Carlo', N'Verdi', N'Piazza Aristotele 40, Lecce', N'verdic@gmail.com')
INSERT [dbo].[Difetti] ([ComponentCode], [CodiceTipo], [NomeComponente], [NomeTipo]) VALUES (CAST(1 AS Numeric(5, 0)), CAST(1010 AS Numeric(4, 0)), N'Tubo serpentina', N'Piegatura')
INSERT [dbo].[Difetti] ([ComponentCode], [CodiceTipo], [NomeComponente], [NomeTipo]) VALUES (CAST(1 AS Numeric(5, 0)), CAST(1030 AS Numeric(4, 0)), N'Tubo serpentina', N'Rottura')
INSERT [dbo].[Difetti] ([ComponentCode], [CodiceTipo], [NomeComponente], [NomeTipo]) VALUES (CAST(2 AS Numeric(5, 0)), CAST(1050 AS Numeric(4, 0)), N'Resistenza', N'Guasto elettrico')
INSERT [dbo].[Guasti] ([NumeroTelefonoCliente], [DataRichiestaIntervento], [PNC], [SNC], [DescrizioneCliente], [DescrizioneTecnico], [CategoriaProdotto], [ComponentCode], [CodiceTipoDifetto]) VALUES (N'1234567890', CAST(N'2019-07-07T18:30:00.000' AS DateTime), CAST(12345678901 AS Numeric(11, 0)), CAST(12345678 AS Numeric(8, 0)), N'La lavatrice perde acqua', N'Sostituito tubo a serpentina rotto', CAST(1 AS Numeric(2, 0)), CAST(1 AS Numeric(5, 0)), CAST(1030 AS Numeric(4, 0)))
INSERT [dbo].[Guasti] ([NumeroTelefonoCliente], [DataRichiestaIntervento], [PNC], [SNC], [DescrizioneCliente], [DescrizioneTecnico], [CategoriaProdotto], [ComponentCode], [CodiceTipoDifetto]) VALUES (N'1234567890', CAST(N'2019-07-20T09:21:00.000' AS DateTime), CAST(12345678901 AS Numeric(11, 0)), CAST(12345678 AS Numeric(8, 0)), N'La lavatrice fa rumore', N'Aggiustata piegatura angolo tubo serpentina', CAST(1 AS Numeric(2, 0)), CAST(1 AS Numeric(5, 0)), CAST(1010 AS Numeric(4, 0)))
INSERT [dbo].[Guasti] ([NumeroTelefonoCliente], [DataRichiestaIntervento], [PNC], [SNC], [DescrizioneCliente], [DescrizioneTecnico], [CategoriaProdotto], [ComponentCode], [CodiceTipoDifetto]) VALUES (N'2345678901', CAST(N'2019-07-03T12:51:00.000' AS DateTime), CAST(23456789012 AS Numeric(11, 0)), CAST(23456789 AS Numeric(8, 0)), N'La lavastoviglie non asciuga i piatti', N'Cambiata resistenza interna', CAST(2 AS Numeric(2, 0)), CAST(2 AS Numeric(5, 0)), CAST(1050 AS Numeric(4, 0)))
INSERT [dbo].[Guasti] ([NumeroTelefonoCliente], [DataRichiestaIntervento], [PNC], [SNC], [DescrizioneCliente], [DescrizioneTecnico], [CategoriaProdotto], [ComponentCode], [CodiceTipoDifetto]) VALUES (N'2345678901', CAST(N'2019-07-07T17:49:00.000' AS DateTime), CAST(23456789012 AS Numeric(11, 0)), CAST(23456789 AS Numeric(8, 0)), N'La lavastoviglie non funziona', NULL, CAST(2 AS Numeric(2, 0)), NULL, NULL)
INSERT [dbo].[Guasti] ([NumeroTelefonoCliente], [DataRichiestaIntervento], [PNC], [SNC], [DescrizioneCliente], [DescrizioneTecnico], [CategoriaProdotto], [ComponentCode], [CodiceTipoDifetto]) VALUES (N'3456789012', CAST(N'2019-06-09T11:51:00.000' AS DateTime), CAST(34567890123 AS Numeric(11, 0)), CAST(34567890 AS Numeric(8, 0)), N'Il forno non si accende', N'Bruciata resistenza dal calore, sostituita', CAST(3 AS Numeric(2, 0)), CAST(1 AS Numeric(5, 0)), CAST(1030 AS Numeric(4, 0)))
INSERT [dbo].[Guasti] ([NumeroTelefonoCliente], [DataRichiestaIntervento], [PNC], [SNC], [DescrizioneCliente], [DescrizioneTecnico], [CategoriaProdotto], [ComponentCode], [CodiceTipoDifetto]) VALUES (N'3456789012', CAST(N'2019-07-10T11:11:00.000' AS DateTime), CAST(34567890123 AS Numeric(11, 0)), CAST(34567890 AS Numeric(8, 0)), N'La luce non si accende', NULL, CAST(3 AS Numeric(2, 0)), NULL, NULL)
INSERT [dbo].[Guasti] ([NumeroTelefonoCliente], [DataRichiestaIntervento], [PNC], [SNC], [DescrizioneCliente], [DescrizioneTecnico], [CategoriaProdotto], [ComponentCode], [CodiceTipoDifetto]) VALUES (N'3456789012', CAST(N'2019-07-11T19:11:00.000' AS DateTime), CAST(34567890123 AS Numeric(11, 0)), CAST(34567890 AS Numeric(8, 0)), N'La modalità ventilato non si inserisce', N'Resistenza danneggiata', CAST(3 AS Numeric(2, 0)), CAST(2 AS Numeric(5, 0)), CAST(1050 AS Numeric(4, 0)))
INSERT [dbo].[Interessi] ([CodiceCategoria], [CodiceProgettista]) VALUES (CAST(1 AS Numeric(2, 0)), CAST(7 AS Numeric(6, 0)))
INSERT [dbo].[Interessi] ([CodiceCategoria], [CodiceProgettista]) VALUES (CAST(2 AS Numeric(2, 0)), CAST(7 AS Numeric(6, 0)))
INSERT [dbo].[Interessi] ([CodiceCategoria], [CodiceProgettista]) VALUES (CAST(3 AS Numeric(2, 0)), CAST(8 AS Numeric(6, 0)))
INSERT [dbo].[Interventi] ([NumeroTelefonoCliente], [DataRichiesta], [Stato], [DataVisita], [TempoImpiegato], [Zona], [CodiceOperatore], [CodiceTecnico]) VALUES (N'1234567890', CAST(N'2019-07-07T18:30:00.000' AS DateTime), N'C', CAST(N'2019-07-09T15:45:00.000' AS DateTime), CAST(3 AS Numeric(2, 0)), N'Centro Italia', CAST(1 AS Numeric(6, 0)), CAST(4 AS Numeric(6, 0)))
INSERT [dbo].[Interventi] ([NumeroTelefonoCliente], [DataRichiesta], [Stato], [DataVisita], [TempoImpiegato], [Zona], [CodiceOperatore], [CodiceTecnico]) VALUES (N'1234567890', CAST(N'2019-07-20T09:21:00.000' AS DateTime), N'C', CAST(N'2019-07-24T10:30:00.000' AS DateTime), CAST(2 AS Numeric(2, 0)), N'Centro Italia', CAST(1 AS Numeric(6, 0)), CAST(901 AS Numeric(6, 0)))
INSERT [dbo].[Interventi] ([NumeroTelefonoCliente], [DataRichiesta], [Stato], [DataVisita], [TempoImpiegato], [Zona], [CodiceOperatore], [CodiceTecnico]) VALUES (N'2345678901', CAST(N'2019-07-03T12:51:00.000' AS DateTime), N'C', CAST(N'2019-07-07T16:46:00.000' AS DateTime), CAST(2 AS Numeric(2, 0)), N'Centro Italia', CAST(1 AS Numeric(6, 0)), CAST(4 AS Numeric(6, 0)))
INSERT [dbo].[Interventi] ([NumeroTelefonoCliente], [DataRichiesta], [Stato], [DataVisita], [TempoImpiegato], [Zona], [CodiceOperatore], [CodiceTecnico]) VALUES (N'2345678901', CAST(N'2019-07-07T17:49:00.000' AS DateTime), N'A', CAST(N'2019-07-12T00:00:00.000' AS DateTime), NULL, N'Centro Italia', CAST(1 AS Numeric(6, 0)), NULL)
INSERT [dbo].[Interventi] ([NumeroTelefonoCliente], [DataRichiesta], [Stato], [DataVisita], [TempoImpiegato], [Zona], [CodiceOperatore], [CodiceTecnico]) VALUES (N'3456789012', CAST(N'2019-06-09T11:51:00.000' AS DateTime), N'C', CAST(N'2019-06-11T12:00:00.000' AS DateTime), CAST(3 AS Numeric(2, 0)), N'Sud Italia', CAST(3 AS Numeric(6, 0)), CAST(902 AS Numeric(6, 0)))
INSERT [dbo].[Interventi] ([NumeroTelefonoCliente], [DataRichiesta], [Stato], [DataVisita], [TempoImpiegato], [Zona], [CodiceOperatore], [CodiceTecnico]) VALUES (N'3456789012', CAST(N'2019-07-10T11:11:00.000' AS DateTime), N'A', CAST(N'2019-07-20T00:00:00.000' AS DateTime), NULL, N'Sud Italia', CAST(3 AS Numeric(6, 0)), NULL)
INSERT [dbo].[Interventi] ([NumeroTelefonoCliente], [DataRichiesta], [Stato], [DataVisita], [TempoImpiegato], [Zona], [CodiceOperatore], [CodiceTecnico]) VALUES (N'3456789012', CAST(N'2019-07-11T19:11:00.000' AS DateTime), N'C', CAST(N'2019-07-14T08:50:00.000' AS DateTime), CAST(1 AS Numeric(2, 0)), N'Sud Italia', CAST(3 AS Numeric(6, 0)), CAST(902 AS Numeric(6, 0)))
INSERT [dbo].[Operatori] ([Codice], [CF], [Nome], [Cognome], [DataNascita], [LuogoNascita], [Residenza], [Stipendio], [CodiceNazionaleCentro], [NazioneCentro]) VALUES (CAST(1 AS Numeric(6, 0)), N'QWERTYUIOPASDFGH', N'Franco', N'Alti', CAST(N'1988-12-13' AS Date), N'Latina', N'Viale dei giardini 33, Roma', 1500, CAST(1 AS Numeric(3, 0)), N'IT')
INSERT [dbo].[Operatori] ([Codice], [CF], [Nome], [Cognome], [DataNascita], [LuogoNascita], [Residenza], [Stipendio], [CodiceNazionaleCentro], [NazioneCentro]) VALUES (CAST(2 AS Numeric(6, 0)), N'WERTYUIOPASDFGHJ', N'Maria', N'Rosini', CAST(N'1973-03-20' AS Date), N'Bergamo', N'Vicolo Scandellari 1, Bergamo', 1450.5, CAST(2 AS Numeric(3, 0)), N'IT')
INSERT [dbo].[Operatori] ([Codice], [CF], [Nome], [Cognome], [DataNascita], [LuogoNascita], [Residenza], [Stipendio], [CodiceNazionaleCentro], [NazioneCentro]) VALUES (CAST(3 AS Numeric(6, 0)), N'ERTYUIOPASDFGHJK', N'Francesca', N'Carofalo', CAST(N'1990-09-01' AS Date), N'Benevento', N'Via Vittorio Emanuele 170, Napoli', 1600.23, CAST(3 AS Numeric(3, 0)), N'IT')
INSERT [dbo].[Prodotti] ([PNC], [SNC], [Modello], [DataAcquisto], [DataInstallazione], [CodiceGaranzia], [CodiceCategoria]) VALUES (CAST(12345678901 AS Numeric(11, 0)), CAST(12345678 AS Numeric(8, 0)), N'QWERTYUIOPQ', CAST(N'2011-11-04' AS Date), NULL, CAST(11 AS Numeric(2, 0)), CAST(1 AS Numeric(2, 0)))
INSERT [dbo].[Prodotti] ([PNC], [SNC], [Modello], [DataAcquisto], [DataInstallazione], [CodiceGaranzia], [CodiceCategoria]) VALUES (CAST(23456789012 AS Numeric(11, 0)), CAST(23456789 AS Numeric(8, 0)), N'WERTYUIOPQ', NULL, CAST(N'2015-09-07' AS Date), CAST(10 AS Numeric(2, 0)), CAST(2 AS Numeric(2, 0)))
INSERT [dbo].[Prodotti] ([PNC], [SNC], [Modello], [DataAcquisto], [DataInstallazione], [CodiceGaranzia], [CodiceCategoria]) VALUES (CAST(34567890123 AS Numeric(11, 0)), CAST(34567890 AS Numeric(8, 0)), N'ERTYUIOPQW', CAST(N'2019-01-25' AS Date), CAST(N'2019-02-01' AS Date), CAST(11 AS Numeric(2, 0)), CAST(3 AS Numeric(2, 0)))
INSERT [dbo].[Progettisti] ([Codice], [CF], [Nome], [Cognome], [DataNascita], [LuogoNascita], [Residenza], [Stipendio]) VALUES (CAST(7 AS Numeric(6, 0)), N'ASDFGHJKLZXCVBNM', N'Marco', N'Presti', CAST(N'1993-04-21' AS Date), N'Savona', N'Via gelsomini 4, Savona', 1721.33)
INSERT [dbo].[Progettisti] ([Codice], [CF], [Nome], [Cognome], [DataNascita], [LuogoNascita], [Residenza], [Stipendio]) VALUES (CAST(8 AS Numeric(6, 0)), N'ASDFGHJKLMNBVCXZ', N'Alessandro', N'Galli', CAST(N'1978-08-08' AS Date), N'Pavia', N'Corso del plebiscito 11, Pavia', 1800)
INSERT [dbo].[Ricambi] ([Codice], [Nome], [CostoAcquisto], [CostoInstallazione], [ComponentCode], [CodiceTipoDifetto]) VALUES (CAST(1 AS Numeric(15, 0)), N'Tubo F7', 0.1200, 1.1000, CAST(1 AS Numeric(5, 0)), CAST(1030 AS Numeric(4, 0)))
INSERT [dbo].[Ricambi] ([Codice], [Nome], [CostoAcquisto], [CostoInstallazione], [ComponentCode], [CodiceTipoDifetto]) VALUES (CAST(2 AS Numeric(15, 0)), N'Tubo F8', 0.1200, 1.1000, CAST(1 AS Numeric(5, 0)), CAST(1010 AS Numeric(4, 0)))
INSERT [dbo].[Ricambi] ([Codice], [Nome], [CostoAcquisto], [CostoInstallazione], [ComponentCode], [CodiceTipoDifetto]) VALUES (CAST(3 AS Numeric(15, 0)), N'Bullone I3', 0.0500, 0.1000, CAST(1 AS Numeric(5, 0)), CAST(1010 AS Numeric(4, 0)))
INSERT [dbo].[Tecnici] ([Codice], [CF], [Nome], [Cognome], [DataNascita], [LuogoNascita], [Residenza], [Stipendio], [DipendenteInterno], [CodiceNazionaleCentro], [NazioneCentro]) VALUES (CAST(4 AS Numeric(6, 0)), N'RTYUIOPASDFGHJKL', N'Leonardo', N'Cavalli', CAST(N'1991-06-14' AS Date), N'Torino', N'Via Chiesaroli 77, Sesto San Giovanni', 1321.34, 1, CAST(2 AS Numeric(3, 0)), N'IT')
INSERT [dbo].[Tecnici] ([Codice], [CF], [Nome], [Cognome], [DataNascita], [LuogoNascita], [Residenza], [Stipendio], [DipendenteInterno], [CodiceNazionaleCentro], [NazioneCentro]) VALUES (CAST(901 AS Numeric(6, 0)), N'TYUIOPASDFGHJKL ', N'Camillo', N'Semprini', CAST(N'1980-11-05' AS Date), N'Roma', N'Piazza indipendenza 24, Frosinone', 1400, 0, CAST(1 AS Numeric(3, 0)), N'IT')
INSERT [dbo].[Tecnici] ([Codice], [CF], [Nome], [Cognome], [DataNascita], [LuogoNascita], [Residenza], [Stipendio], [DipendenteInterno], [CodiceNazionaleCentro], [NazioneCentro]) VALUES (CAST(902 AS Numeric(6, 0)), N'YUIASDFGHJKLOP  ', N'Francesca', N'Cartone', CAST(N'1981-07-30' AS Date), N'Napoli', N'Viale della repubblica 110, Napoli', 1401.55, 0, CAST(3 AS Numeric(3, 0)), N'IT')
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDOperatori_1]    Script Date: 06/07/2019 00:13:31 ******/
ALTER TABLE [dbo].[Operatori] ADD  CONSTRAINT [IDOperatori_1] UNIQUE NONCLUSTERED 
(
	[CF] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDProgettisti_1]    Script Date: 06/07/2019 00:13:31 ******/
ALTER TABLE [dbo].[Progettisti] ADD  CONSTRAINT [IDProgettisti_1] UNIQUE NONCLUSTERED 
(
	[CF] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IDTecnici_1]    Script Date: 06/07/2019 00:13:31 ******/
ALTER TABLE [dbo].[Tecnici] ADD  CONSTRAINT [IDTecnici_1] UNIQUE NONCLUSTERED 
(
	[CF] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Guasti]  WITH CHECK ADD  CONSTRAINT [FKCausa_FK] FOREIGN KEY([ComponentCode], [CodiceTipoDifetto])
REFERENCES [dbo].[Difetti] ([ComponentCode], [CodiceTipo])
GO
ALTER TABLE [dbo].[Guasti] CHECK CONSTRAINT [FKCausa_FK]
GO
ALTER TABLE [dbo].[Guasti]  WITH CHECK ADD  CONSTRAINT [FKEffetto] FOREIGN KEY([PNC], [SNC])
REFERENCES [dbo].[Prodotti] ([PNC], [SNC])
GO
ALTER TABLE [dbo].[Guasti] CHECK CONSTRAINT [FKEffetto]
GO
ALTER TABLE [dbo].[Guasti]  WITH CHECK ADD  CONSTRAINT [FKMotivazione] FOREIGN KEY([NumeroTelefonoCliente], [DataRichiestaIntervento])
REFERENCES [dbo].[Interventi] ([NumeroTelefonoCliente], [DataRichiesta])
GO
ALTER TABLE [dbo].[Guasti] CHECK CONSTRAINT [FKMotivazione]
GO
ALTER TABLE [dbo].[Interessi]  WITH CHECK ADD  CONSTRAINT [FKInteresse_Cat] FOREIGN KEY([CodiceCategoria])
REFERENCES [dbo].[Categorie] ([Codice])
GO
ALTER TABLE [dbo].[Interessi] CHECK CONSTRAINT [FKInteresse_Cat]
GO
ALTER TABLE [dbo].[Interessi]  WITH CHECK ADD  CONSTRAINT [FKInteresse_Pro] FOREIGN KEY([CodiceProgettista])
REFERENCES [dbo].[Progettisti] ([Codice])
GO
ALTER TABLE [dbo].[Interessi] CHECK CONSTRAINT [FKInteresse_Pro]
GO
ALTER TABLE [dbo].[Interventi]  WITH CHECK ADD  CONSTRAINT [FKApertura] FOREIGN KEY([CodiceOperatore])
REFERENCES [dbo].[Operatori] ([Codice])
GO
ALTER TABLE [dbo].[Interventi] CHECK CONSTRAINT [FKApertura]
GO
ALTER TABLE [dbo].[Interventi]  WITH CHECK ADD  CONSTRAINT [FKAssistenza] FOREIGN KEY([CodiceTecnico])
REFERENCES [dbo].[Tecnici] ([Codice])
GO
ALTER TABLE [dbo].[Interventi] CHECK CONSTRAINT [FKAssistenza]
GO
ALTER TABLE [dbo].[Interventi]  WITH CHECK ADD  CONSTRAINT [FKRichiesta] FOREIGN KEY([NumeroTelefonoCliente])
REFERENCES [dbo].[Clienti] ([NumeroTelefono])
GO
ALTER TABLE [dbo].[Interventi] CHECK CONSTRAINT [FKRichiesta]
GO
ALTER TABLE [dbo].[Operatori]  WITH CHECK ADD  CONSTRAINT [FKAfferenza_Op] FOREIGN KEY([CodiceNazionaleCentro], [NazioneCentro])
REFERENCES [dbo].[Centri_Assistenza] ([CodiceNazionale], [Nazione])
GO
ALTER TABLE [dbo].[Operatori] CHECK CONSTRAINT [FKAfferenza_Op]
GO
ALTER TABLE [dbo].[Prodotti]  WITH CHECK ADD  CONSTRAINT [FKAppartenenza] FOREIGN KEY([CodiceCategoria])
REFERENCES [dbo].[Categorie] ([Codice])
GO
ALTER TABLE [dbo].[Prodotti] CHECK CONSTRAINT [FKAppartenenza]
GO
ALTER TABLE [dbo].[Ricambi]  WITH CHECK ADD  CONSTRAINT [FKRiparazione] FOREIGN KEY([ComponentCode], [CodiceTipoDifetto])
REFERENCES [dbo].[Difetti] ([ComponentCode], [CodiceTipo])
GO
ALTER TABLE [dbo].[Ricambi] CHECK CONSTRAINT [FKRiparazione]
GO
ALTER TABLE [dbo].[Tecnici]  WITH CHECK ADD  CONSTRAINT [FKAfferenza_Tec] FOREIGN KEY([CodiceNazionaleCentro], [NazioneCentro])
REFERENCES [dbo].[Centri_Assistenza] ([CodiceNazionale], [Nazione])
GO
ALTER TABLE [dbo].[Tecnici] CHECK CONSTRAINT [FKAfferenza_Tec]
GO
ALTER TABLE [dbo].[Guasti]  WITH CHECK ADD  CONSTRAINT [FKCausa_CHK] CHECK  (([ComponentCode] IS NOT NULL AND [CodiceTipoDifetto] IS NOT NULL OR [ComponentCode] IS NULL AND [CodiceTipoDifetto] IS NULL))
GO
ALTER TABLE [dbo].[Guasti] CHECK CONSTRAINT [FKCausa_CHK]
GO
ALTER TABLE [dbo].[Interventi]  WITH CHECK ADD  CONSTRAINT [IDInterventi_CHK] CHECK  (([dbo].[IDInterventi_CHK_func]()=(1)))
GO
ALTER TABLE [dbo].[Interventi] CHECK CONSTRAINT [IDInterventi_CHK]
GO
ALTER TABLE [dbo].[Progettisti]  WITH CHECK ADD  CONSTRAINT [IDProgettisti_CHK] CHECK  (([dbo].[IDProgettisti_CHK_func]()=(1)))
GO
ALTER TABLE [dbo].[Progettisti] CHECK CONSTRAINT [IDProgettisti_CHK]
GO
ALTER DATABASE [GestioneGuasti-2019] SET  READ_WRITE 
GO
