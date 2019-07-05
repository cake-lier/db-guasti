using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace FailuresManagement
{
    /// <summary>
    /// Represents the forms accessed by a technician. It allows to insert new informations into a previously inserted
    /// intervention which is still open.
    /// </summary>
    public partial class TechnicianForm : Form
    {
        private const string MissingDataTitle = "Mancanza dati";
        private const string MissingData = "È necessario inserire il campo \"";
        private const string ConfirmTitle = "Conferma";
        private const string InsertDataQuestion = "Vuoi inserire questi dati? Saranno modificati solo quelli inerenti all'intervento correntemente selezionato";
        private const string SuccessTitle = "Successo";
        private const string InsertSuccess = "Inserimento effettuato con successo";
        private const string NonExistingFlawError = "I dati non possono essere inseriti, Component Code e il codice del tipo di difetto devono essere corretti";
        private const string ErrorTitle = "Errore";
        private const string DataError = "I dati presentano qualche errore, nel formato o nel contenuto, prego reinserirli";

        private GestioneGuastiDataContext db;
        private readonly decimal technicianCode;

        /// <summary>
        /// Default constructor. It needs the employee code of the technician accessing this form so as to associate it
        /// with the intervention is going to edit so as to let the system know who performed the restoration work for this
        /// intervention.
        /// </summary>
        /// <param name="technicianCode">The employee code of the technician accessing this form.</param>
        public TechnicianForm(decimal technicianCode)
        {
            this.technicianCode = technicianCode;
            InitializeComponent();
            AcceptButton = EditButton;
        }

        /*
         * 
         */
        private void InitTechnicianForm()
        {
            db = new GestioneGuastiDataContext();
            InterventionView.DataSource = db.NewInterventions;
            InterventionView.Columns["Numero_di_telefono_cliente"].ReadOnly = true;
            InterventionView.Columns["Data_richiesta"].ReadOnly = true;
            InterventionView.Columns.Add("Tempo_impiegato", "Tempo_impiegato");
            InterventionView.Sort(InterventionView.Columns["Data_richiesta"], ListSortDirection.Ascending);
            InterventionView.ClearSelection();
            FaultsView.Rows.Clear();
            FaultsView.Columns.Clear();
            ProductsView.Rows.Clear();
            ProductsView.Columns.Clear();
        }

        private void TechnicianForm_Load(object sender, EventArgs e)
        {
            InitTechnicianForm();
        }

        private void InterventionView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && InterventionView.Columns[e.ColumnIndex].Name != "Tempo_impiegato")
            {
                var data = from fault in db.Guasti
                           where fault.NumeroTelefonoCliente
                                 == (string)InterventionView["Numero_di_telefono_cliente", e.RowIndex].Value
                                 && fault.DataRichiestaIntervento
                                    == (DateTime)InterventionView["Data_richiesta", e.RowIndex].Value
                           select new
                           {
                               fault.DescrizioneCliente,
                               fault.Prodotti.Categorie.Nome,
                               fault.Prodotti.PNC,
                               fault.Prodotti.SNC,
                               fault.Prodotti.DataAcquisto,
                               fault.Prodotti.DataInstallazione,
                               fault.Prodotti.CodiceGaranzia,
                               fault.Prodotti.Modello
                           };
                FaultsView.Columns.Clear();
                FaultsView.DataSource = from row in data
                                        select new { row.PNC, row.SNC, row.DescrizioneCliente };
                FaultsView.Columns["PNC"].Visible = false;
                FaultsView.Columns["SNC"].Visible = false;
                new List<string>
                {
                    "Descrizione_tecnico",
                    "Codice_tipo_di_difetto",
                    "Component_code"
                }.ForEach(s => FaultsView.Columns.Add(s, s));
                ProductsView.Columns.Clear();
                ProductsView.DataSource = from row in data
                                          select new { row.Nome, row.PNC, row.SNC, row.Modello };
                new List<string>
                {
                    "Data_di_acquisto",
                    "Data_di_installazione",
                    "Codice_garanzia"
                }.ForEach(s => ProductsView.Columns.Add(s, s));
                var i = 0;
                var productsEnumerator = data.GetEnumerator();
                while (productsEnumerator.MoveNext())
                {
                    ProductsView["Data_di_acquisto", i].Value = productsEnumerator.Current.DataAcquisto?.ToShortDateString();
                    ProductsView["Data_di_installazione", i].Value = productsEnumerator.Current.DataInstallazione?.ToShortDateString();
                    ProductsView["Codice_garanzia", i].Value = productsEnumerator.Current.CodiceGaranzia?.ToString();
                    i++;
                }
            }
        }

        private bool IsFieldValid(object field, string fieldName)
        {
            if (field == null)
            {
                MessageBox.Show(MissingData + fieldName + "\"", MissingDataTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRow = InterventionView.SelectedCells[0].RowIndex;
                var intervention = (from i in db.Interventi
                                    where i.NumeroTelefonoCliente
                                          == (string)InterventionView["Numero_di_telefono_cliente", selectedRow].Value
                                          && i.DataRichiesta == (DateTime)InterventionView["Data_richiesta", selectedRow].Value
                                    select i).Single();
                intervention.CodiceTecnico = technicianCode;
                intervention.Stato = 'C';
                var usedTime = (string)InterventionView["Tempo_impiegato", selectedRow].Value;
                if (!IsFieldValid(usedTime, "Tempo_impiegato"))
                {
                    return;
                }
                intervention.TempoImpiegato = decimal.Parse(usedTime);
                for (int i = 0; i < FaultsView.Rows.Count; i++)
                {
                    var updateRow = (from f in db.Guasti
                                     where f.PNC == (decimal)FaultsView["PNC", i].Value
                                           && f.SNC == (decimal)FaultsView["SNC", i].Value
                                           && f.NumeroTelefonoCliente
                                              == (string)InterventionView["Numero_di_telefono_cliente", selectedRow].Value
                                           && f.DataRichiestaIntervento
                                              == (DateTime)InterventionView["Data_richiesta", selectedRow].Value
                                     select f).Single();
                    var description = (string)FaultsView["Descrizione_tecnico", i].Value;
                    if (!IsFieldValid(description, "Descrizione_tecnico"))
                    {
                        return;
                    }
                    updateRow.DescrizioneTecnico = description;
                    var flawCode = (string)FaultsView["Codice_tipo_di_difetto", i].Value;
                    if (!IsFieldValid(flawCode, "Codice_tipo_di_difetto"))
                    {
                        return;
                    }
                    updateRow.CodiceTipoDifetto = decimal.Parse(flawCode);
                    var componentCode = (string)FaultsView["Component_code", i].Value;
                    if (!IsFieldValid(componentCode, "Component_code"))
                    {
                        return;
                    }
                    updateRow.ComponentCode = decimal.Parse(componentCode);
                }
                for (int i = 0; i < ProductsView.Rows.Count; i++)
                {
                    var updateRow = (from p in db.Prodotti
                                     where p.PNC == (decimal)FaultsView["PNC", i].Value
                                           && p.SNC == (decimal)FaultsView["SNC", i].Value
                                     select p).Single();
                    var purchaseDate = (string)ProductsView["Data_di_acquisto", i].Value;
                    updateRow.DataAcquisto = purchaseDate == null ? null : (DateTime?)DateTime.Parse(purchaseDate);
                    var installationDate = (string)ProductsView["Data_di_installazione", i].Value;
                    updateRow.DataInstallazione = installationDate == null ? null : (DateTime?)DateTime.Parse(installationDate);
                    var warrantyCode = (string)ProductsView["Codice_garanzia", i].Value;
                    updateRow.CodiceGaranzia = warrantyCode == null ? null : (decimal?)decimal.Parse(warrantyCode);
                }
                if (MessageBox.Show(InsertDataQuestion, ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    db.SubmitChanges();
                    MessageBox.Show(InsertSuccess, SuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    InitTechnicianForm();
                }
            }
            catch (Exception ex) when (ex is InvalidCastException 
                                       || ex is FormatException 
                                       || ex is ArgumentException
                                       || ex is OverflowException)
            {
                MessageBox.Show(DataError, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show(NonExistingFlawError, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /*
         * If this form is closed by user clicking on close button, and the closing event is not issued by the parent form,
         * then close also the parent form. This is done because exiting the application is only allowed by default by
         * closing all currently opened, so also opened but hidden, windows. When this form is showed, the parent form
         * is currently hidden, so it cannot be closed unless we tell it to do so.
         */
        private void TechnicianForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                Owner.Close();
            }
        }
    }
}
