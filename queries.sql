/*INSERT INTO Clienti (NumeroTelefono, Nome, Cognome, Recapito, Email) VALUES ('4567890123', 'Frank', 'Wheatstone', 'Glastonbury road 77, Leeds', 'ffwheat@outlook.com');
INSERT INTO Prodotti (PNC, SNC, Modello, DataAcquisto, DataInstallazione, CodiceGaranzia, CodiceCategoria) VALUES (45678901234, 45678901, 'RTYUIOPQWEA', '2013-11-01', '2013-11-05', NULL, 1);
INSERT INTO Interventi (NumeroTelefonoCliente, DataRichiesta, Stato, DataVisita, TempoImpiegato, Nazione, CodiceOperatore, CodiceTecnico) VALUES ('4567890123', '2019-07-21 10:30', 'C', '2019-07-21', 2, 'UK', 1, 4)
INSERT INTO Guasti(NumeroTelefonoCliente, DataRichiestaIntervento, PNC, SNC, DescrizioneCliente, DescrizioneTecnico, CategoriaProdotto, ComponentCode, CodiceTipoDifetto) VALUES ('4567890123', '2019-07-21 10:30', 45678901234, 45678901, 'After a few seconds the dishwasher stops washing', 'Il tubo a serpentina perde acqua', 1, 1, 1030);*/

/*DELETE FROM Guasti WHERE Guasti.NumeroTelefonoCliente = '4567890123' AND Guasti.DataRichiestaIntervento = '2019-07-21 10:30' AND Guasti.PNC = '45678901234' AND Guasti.SNC = '45678901';
DELETE FROM Interventi WHERE Interventi.NumeroTelefonoCliente = '4567890123' AND Interventi.DataRichiesta = '2019-07-21 10:30'
DELETE FROM Clienti WHERE Clienti.NumeroTelefono = '4567890123';
DELETE FROM Prodotti WHERE Prodotti.PNC = '45678901234' AND Prodotti.SNC = '45678901';*/

/*SELECT Interventi.NumeroTelefonoCliente, Interventi.DataRichiesta
FROM Interventi
WHERE Interventi.CodiceTecnico IS NULL
ORDER BY Interventi.DataRichiesta*/

/*UPDATE Interventi SET Interventi.CodiceTecnico = '4', Interventi.Stato = 'C', Interventi.TempoImpiegato = '1' FROM Interventi WHERE Interventi.DataRichiesta = '2012-12-12 00:00:00.000' AND Interventi.NumeroTelefonoCliente = '2345678901';
UPDATE Guasti SET Guasti.DescrizioneTecnico = '---', Guasti.CodiceTipoDifetto = '1010', Guasti.ComponentCode = '1' FROM Guasti WHERE Guasti.DataRichiestaIntervento = '2012-12-12 00:00:00.000' AND Guasti.NumeroTelefonoCliente = '2345678901' AND Guasti.PNC = '0987654321' AND Guasti.SNC = '09876543';
UPDATE Prodotti SET Prodotti.DataAcquisto = '2009-01-01', Prodotti.DataInstallazione = '2009-01-03', Prodotti.CodiceGaranzia = '0' FROM Prodotti WHERE Prodotti.PNC = '0987654321' AND Prodotti.SNC = '09876543';*/


/*SELECT Guasti.CategoriaProdotto, Guasti.PNC, Guasti.SNC, Prodotti.Modello, Guasti.DataRichiestaIntervento, Interventi.DataVisita, Prodotti.DataAcquisto, Prodotti.DataInstallazione, Interventi.Nazione, Guasti.DescrizioneCliente, Guasti.DescrizioneTecnico, Guasti.ComponentCode, Difetti.NomeComponente, Guasti.CodiceTipoDifetto, Difetti.NomeTipo AS 'NomeTipoDifetto', Ricambi.Codice AS 'CodiceRicambio', Ricambi.Nome AS 'NomeRicambio', Ricambi.CostoAcquisto, Ricambi.CostoInstallazione
FROM (((Guasti JOIN Interventi ON Guasti.NumeroTelefonoCliente = Interventi.NumeroTelefonoCliente AND Guasti.DataRichiestaIntervento = Interventi.DataRichiesta) JOIN Prodotti ON Guasti.PNC = Prodotti.PNC AND Guasti.SNC = Prodotti.SNC) JOIN Difetti ON Guasti.ComponentCode = Difetti.ComponentCode AND Guasti.CodiceTipoDifetto = Difetti.CodiceTipo) LEFT OUTER JOIN Ricambi ON Ricambi.ComponentCode = Difetti.ComponentCode AND Ricambi.CodiceTipoDifetto = Difetti.CodiceTipo
WHERE MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP)*/

/*SELECT Guasti.CategoriaProdotto, Guasti.PNC, Guasti.SNC, Guasti.DescrizioneCliente, Guasti.DescrizioneTecnico, Guasti.ComponentCode, Guasti.CodiceTipoDifetto
FROM Guasti
WHERE Guasti.CodiceTipoDifetto IS NOT NULL AND Guasti.ComponentCode IS NOT NULL AND MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP) AND CHARINDEX('resistenza', Guasti.DescrizioneTecnico) > 0*/

/*SELECT TOP(5) WITH TIES Guasti.CategoriaProdotto, Guasti.PNC, COUNT(*) AS '#'
FROM Guasti
WHERE Guasti.ComponentCode IS NOT NULL AND Guasti.CodiceTipoDifetto IS NOT NULL AND MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Guasti.CategoriaProdotto, Guasti.PNC ORDER BY 3 DESC;*/

/*SELECT TOP(5) WITH TIES Guasti.CategoriaProdotto, Guasti.ComponentCode, Difetti.NomeComponente, COUNT(*) AS '#'
FROM Guasti JOIN Difetti ON Guasti.CodiceTipoDifetto = Difetti.CodiceTipo AND Guasti.ComponentCode = Difetti.ComponentCode
WHERE Guasti.ComponentCode IS NOT NULL AND Guasti.CodiceTipoDifetto IS NOT NULL AND MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Guasti.CategoriaProdotto, Guasti.ComponentCode, Difetti.NomeComponente ORDER BY 4 DESC;*/

/*SELECT TOP(5) WITH TIES Guasti.CategoriaProdotto, Ricambi.Codice, Ricambi.Nome, Ricambi.CostoAcquisto, Ricambi.CostoInstallazione, COUNT(*) AS '#'
FROM (Guasti JOIN Difetti ON Guasti.ComponentCode = Difetti.ComponentCode AND Guasti.CodiceTipoDifetto = Difetti.CodiceTipo) JOIN Ricambi ON Difetti.ComponentCode = Ricambi.ComponentCode AND Difetti.CodiceTipo = Ricambi.CodiceTipoDifetto
WHERE MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Ricambi.Codice, Ricambi.Nome, Ricambi.CostoAcquisto, Ricambi.CostoInstallazione, Guasti.CategoriaProdotto ORDER BY 6 DESC;*/

/*SELECT TOP(5) WITH TIES Guasti.CategoriaProdotto, Guasti.DataRichiestaIntervento, SUM(Ricambi.CostoAcquisto + Ricambi.CostoInstallazione) AS 'CostoTotale'
FROM (Guasti JOIN Difetti ON Guasti.ComponentCode = Difetti.ComponentCode AND Guasti.CodiceTipoDifetto = Difetti.CodiceTipo) JOIN Ricambi ON Difetti.ComponentCode = Ricambi.ComponentCode AND Difetti.CodiceTipo = Ricambi.CodiceTipoDifetto
WHERE MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Guasti.NumeroTelefonoCliente, Guasti.DataRichiestaIntervento, Guasti.CategoriaProdotto ORDER BY 3 DESC;*/

/*SELECT Guasti.CategoriaProdotto, Interventi.Nazione, COUNT(*) AS '#'
FROM Guasti JOIN Interventi ON Guasti.NumeroTelefonoCliente = Interventi.NumeroTelefonoCliente AND Guasti.DataRichiestaIntervento = Interventi.DataRichiesta
WHERE Guasti.CodiceTipoDifetto IS NOT NULL AND Guasti.ComponentCode IS NOT NULL AND MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Interventi.Nazione, Guasti.CategoriaProdotto;*/

/*SELECT Guasti.CategoriaProdotto, Prodotti.PNC, Prodotti.SNC, Prodotti.CodiceGaranzia, Prodotti.Modello, DATEDIFF(d, Prodotti.DataAcquisto, Guasti.DataRichiestaIntervento) AS 'TTF (giorni)'
FROM Guasti JOIN Prodotti ON Guasti.PNC = Prodotti.PNC AND Guasti.SNC = Prodotti.SNC
WHERE Guasti.CodiceTipoDifetto IS NOT NULL AND Guasti.ComponentCode IS NOT NULL AND MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP) AND Prodotti.DataAcquisto IS NOT NULL;*/

/*SELECT Guasti.CategoriaProdotto, Prodotti.PNC, Prodotti.SNC, Prodotti.CodiceGaranzia, Prodotti.Modello, DATEDIFF(d, Prodotti.DataInstallazione, Guasti.DataRichiestaIntervento) AS 'TTF (giorni)'
FROM Guasti JOIN Prodotti ON Guasti.PNC = Prodotti.PNC AND Guasti.SNC = Prodotti.SNC
WHERE Guasti.CodiceTipoDifetto IS NOT NULL AND Guasti.ComponentCode IS NOT NULL AND MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP) AND Prodotti.DataInstallazione IS NOT NULL;*/

/*SELECT Guasti.CategoriaProdotto, Difetti.CodiceTipo, Difetti.NomeTipo, AVG(Interventi.TempoImpiegato) AS 'TempoMedioRiparazione'
FROM (Guasti JOIN Difetti ON Guasti.ComponentCode = Difetti.ComponentCode AND Guasti.CodiceTipoDifetto = Difetti.CodiceTipo) JOIN Interventi ON Guasti.NumeroTelefonoCliente = Interventi.NumeroTelefonoCliente AND Guasti.DataRichiestaIntervento = Interventi.DataRichiesta
WHERE MONTH(Guasti.DataRichiestaIntervento) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Guasti.DataRichiestaIntervento) = YEAR(CURRENT_TIMESTAMP) AND Interventi.TempoImpiegato IS NOT NULL
GROUP BY Guasti.CategoriaProdotto, Difetti.CodiceTipo, Difetti.NomeTipo ORDER BY 4 DESC;*/

/*SELECT Operatori.CF, Operatori.Nome, Operatori.Cognome, COUNT(*) AS '#'
FROM Interventi JOIN Operatori ON Interventi.CodiceOperatore = Operatori.Codice
WHERE MONTH(Interventi.DataRichiesta) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Interventi.DataRichiesta) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Operatori.Codice, Operatori.CF, Operatori.Nome, Operatori.Cognome*/

/*SELECT Tecnici.CF, Tecnici.Nome, Tecnici.Cognome, COUNT(*) AS '#'
FROM Interventi JOIN Tecnici ON Interventi.CodiceTecnico = Tecnici.Codice
WHERE MONTH(Interventi.DataRichiesta) = MONTH(CURRENT_TIMESTAMP) AND YEAR(Interventi.DataRichiesta) = YEAR(CURRENT_TIMESTAMP)
GROUP BY Tecnici.Codice, Tecnici.CF, Tecnici.Nome, Tecnici.Cognome*/

/*SELECT Tecnici.CF, Tecnici.Nome, Tecnici.Cognome, AVG(Interventi.TempoImpiegato) AS 'TempoMedioImpiegato'
FROM Interventi JOIN Tecnici ON Interventi.CodiceTecnico = Tecnici.Codice
GROUP BY Tecnici.Codice, Tecnici.CF, Tecnici.Nome, Tecnici.Cognome*/

/*SELECT Centri_Assistenza.CodiceNazionale, Centri_Assistenza.Nazione, Centri_Assistenza.Sede, Centri_Assistenza.AreaCompetenza, AVG(CAST(DATEDIFF(d, Interventi.DataRichiesta, Interventi.DataVisita) AS REAL)) AS 'TempoMedioRiparazione'
FROM (Interventi JOIN Operatori ON Interventi.CodiceOperatore = Operatori.Codice) JOIN Centri_Assistenza ON Operatori.CodiceNazionaleCentro = Centri_Assistenza.CodiceNazionale AND Operatori.NazioneCentro = Centri_Assistenza.Nazione
GROUP BY Centri_Assistenza.CodiceNazionale, Centri_Assistenza.Nazione, Centri_Assistenza.Sede, Centri_Assistenza.AreaCompetenza*/
