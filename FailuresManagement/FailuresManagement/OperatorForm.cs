using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace FailuresManagement
{
    /// <summary>
    /// Represents the form used by the operators. It allows to insert a new intervention or delete it just after the insertion.
    /// </summary>
    public partial class OperatorForm : Form
    {
        private const string dateTimeFormat = "dd/MM/yyyy HH:mm";
        private const string MissingDataTitle = "Mancanza dati";
        private const string MissingData = "È necessario inserire il campo \"";
        private const string ErrorTitle = "Errore";
        private const string DataFormatError = "I dati non possono essere inseriti, è presente un errore o di formato o di contenuto";
        private const string AtLeastOneProductError = "È necessario associare questo intervento ad almeno un prodotto che ha subito un guasto";
        private const string WrongCategoryError = "La categoria del prodotto non è stata correttamente inserita";
        private const string SuccessTitle = "Successo";
        private const string InsertSuccess = "Inserimento effettuato con successo";
        private const string ProductFaultNotFound = "Il guasto o il relativo prodotto non sono stati trovati, non verranno cancellati";
        private const string CannotDeleteFault = "Non è possibile cancellare il guasto alla riga ";
        private const string InterventionNotFound = "L'intervento non è stato trovato, non verrà cancellato";
        private const string CannotDeleteIntervention = "Non è possibile cancellare i dati senza il numero di telefono del cliente chiamante";

        private DateTime lastInsertionTime;
        private readonly GestioneGuastiDataContext db;
        private readonly decimal operatorCode;

        /// <summary>
        /// Default constructor. It needs the employee code of the operator accessing the form so as to label all the
        /// registered interventions with her code and associate those interventions with her.
        /// </summary>
        /// <param name="operatorCode">The employee code of the operator accessing this form.</param>
        public OperatorForm(decimal operatorCode)
        {
            db = new GestioneGuastiDataContext();
            this.operatorCode = operatorCode;
            InitializeComponent();
            MaximizeBox = false;
        }

        /*
         * At loading time, prepares all the columns in the DataGridView for the insertion of products and faults
         * associated with the intervention which is going to be opened.
         */
        private void OperatorForm_Load(object sender, EventArgs e)
        {
            new List<string>
            {
                "PNC",
                "SNC",
                "Descrizione",
                "Modello",
                "Data_di_acquisto",
                "Data_di_installazione",
                "Codice_garanzia",
                "Categoria"
            }.ForEach(c => AddProductsView.Columns.Add(c, c));
            VisitDatePicker.CustomFormat = dateTimeFormat;
            VisitDatePicker.Value = new DateTime(DateTime.Now.Year,
                                                 DateTime.Now.Month,
                                                 DateTime.Now.Day,
                                                 DateTime.Now.Hour,
                                                 DateTime.Now.Minute,
                                                 0);
        }

        /*
         * Checks if a TextBox contains some text or not.
         */
        private bool IsTextBoxValid(TextBox textBox, string fieldName)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(MissingData + fieldName + "\"", MissingDataTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /*
         * Checks if a field contains some data, so it isn't null, or not.
         */
        private bool IsFieldValid(object field, string fieldName)
        {
            if (field == null)
            {
                MessageBox.Show(MissingData + fieldName + "\"", MissingDataTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        /*
         * Reverts all the insertions pending to be submitted to the database by deleting it so as to letting know the
         * database there's nothing to be done.
         */
        private void RevertChanges()
        {
            foreach (var insert in db.GetChangeSet().Inserts)
            {
                db.GetTable(insert.GetType()).DeleteOnSubmit(insert);
            }
            db.SubmitChanges();
        }

        /*
         * Manages the behavior of the "insert" button. If all the data is valid, checks if the customer is already into
         * the database, and if it isn't, it inserts it. It inserts then the new interventions and then checks if every product
         * inserted into the DataGridView is already present into the database and, if one is not, it inserts into the database.
         * Finally, it inserts all the faults associated with this new intervention. At every stage, if something goes wrong,
         * it rollbacks the insertions and issues a MessageBox with an error message.
         */
        private void InsertButton_Click(object sender, EventArgs e)
        {
            lastInsertionTime = new DateTime(DateTime.Now.Year,
                                             DateTime.Now.Month,
                                             DateTime.Now.Day,
                                             DateTime.Now.Hour,
                                             DateTime.Now.Minute,
                                             0);
            if (!IsTextBoxValid(TelephoneBox, "Numero_di_telefono")
                || !IsTextBoxValid(NameBox, "Nome")
                || !IsTextBoxValid(SurnameBox, "Cognome")
                || !IsTextBoxValid(AddressBox, "Recapito"))
            {
                return;
            }
            if ((from c in db.Clienti
                 where c.NumeroTelefono == TelephoneBox.Text
                 select c).Count() == 0)
            {
                db.Clienti.InsertOnSubmit(new Clienti
                {
                    NumeroTelefono = TelephoneBox.Text,
                    Nome = NameBox.Text,
                    Cognome = SurnameBox.Text,
                    Recapito = AddressBox.Text,
                    Email = EmailBox.Text == "" ? null : EmailBox.Text
                });
            }
            db.Interventi.InsertOnSubmit(new Interventi
            {
                NumeroTelefonoCliente = TelephoneBox.Text,
                DataRichiesta = lastInsertionTime,
                CodiceOperatore = operatorCode,
                Stato = 'A',
                DataVisita = VisitDatePicker.Value,
                Zona = (from op in db.Operatori
                        where op.Codice == operatorCode
                        select op.Centri_Assistenza.AreaCompetenza).Single()
            });
            if (AddProductsView.Rows.Count == 1)
            {
                MessageBox.Show(AtLeastOneProductError, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                RevertChanges();
                return;
            }
            try
            {
                for (int i = 0; i < AddProductsView.Rows.Count - 1; i++)
                {
                    var stringPNC = (string)AddProductsView["PNC", i].Value;
                    if (!IsFieldValid(stringPNC, "PNC"))
                    {
                        RevertChanges();
                        return;
                    }
                    var PNC = decimal.Parse(stringPNC);
                    var stringSNC = (string)AddProductsView["SNC", i].Value;
                    if (!IsFieldValid(stringSNC, "SNC"))
                    {
                        RevertChanges();
                        return;
                    }
                    var SNC = decimal.Parse(stringSNC);
                    var model = (string)AddProductsView["Modello", i].Value;
                    if (!IsFieldValid(model, "Modello"))
                    {
                        RevertChanges();
                        return;
                    }
                    var description = (string)AddProductsView["Descrizione", i].Value;
                    if (!IsFieldValid(description, "Descrizione"))
                    {
                        RevertChanges();
                        return;
                    }
                    var category = (string)AddProductsView["Categoria", i].Value;
                    if (!IsFieldValid(category, "Categoria"))
                    {
                        RevertChanges();
                        return;
                    }
                    var purchaseDate = (string)AddProductsView["Data_di_acquisto", i].Value;
                    var installationDate = (string)AddProductsView["Data_di_installazione", i].Value;
                    var warrantyCode = (string)AddProductsView["Codice_garanzia", i].Value;
                    var productCategories = from c in db.Categorie
                                            where c.Nome == category
                                            select c.Codice;
                    if (productCategories.Count() == 0)
                    {
                        MessageBox.Show(WrongCategoryError, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        RevertChanges();
                        return;
                    }
                    if ((from p in db.Prodotti
                         where p.PNC == PNC && p.SNC == SNC
                         select p).Count() == 0)
                    {
                        db.Prodotti.InsertOnSubmit(new Prodotti
                        {
                            PNC = PNC,
                            SNC = SNC,
                            Modello = model,
                            DataAcquisto = purchaseDate == null ? null : (DateTime?)DateTime.Parse(purchaseDate),
                            DataInstallazione = installationDate == null ? null : (DateTime?)DateTime.Parse(installationDate),
                            CodiceGaranzia = warrantyCode == null ? null : (decimal?)decimal.Parse(warrantyCode),
                            CodiceCategoria = productCategories.First(),
                        });
                    }
                    db.Guasti.InsertOnSubmit(new Guasti
                    {
                        NumeroTelefonoCliente = TelephoneBox.Text,
                        DataRichiestaIntervento = lastInsertionTime,
                        PNC = PNC,
                        SNC = SNC,
                        DescrizioneCliente = description,
                        CategoriaProdotto = productCategories.First(),
                    });
                }
                db.SubmitChanges();
                MessageBox.Show(InsertSuccess, SuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DeleteButton.Enabled = true;
                ClearButton.Enabled = true;
                InsertButton.Enabled = false;
            }
            catch (Exception ex) when (ex is FormatException
                                       || ex is SqlException 
                                       || ex is ArgumentException
                                       || ex is OverflowException)
            {
                MessageBox.Show(DataFormatError, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                RevertChanges();
            }
        }

        /*
         * Manages the behavior of the "clear" button. It simply empties the DataGridView and all the TextBox fields.
         */
        private void ClearButton_Click(object sender, EventArgs e)
        {
            TelephoneBox.Text = null;
            VisitDatePicker.Value = new DateTime(DateTime.Now.Year,
                                                 DateTime.Now.Month,
                                                 DateTime.Now.Day,
                                                 DateTime.Now.Hour,
                                                 DateTime.Now.Minute,
                                                 0);
            NameBox.Text = null;
            SurnameBox.Text = null;
            AddressBox.Text = null;
            EmailBox.Text = null;
            AddProductsView.Rows.Clear();
            DeleteButton.Enabled = false;
            InsertButton.Enabled = true;
            ClearButton.Enabled = false;
        }

        /*
         * Manages the behavior of the "delete" button. If valid data is still into the form, for each fault (or
         * equally for each product) into the DataGridView it deletes it and checks if the associated product is
         * used in another fault. If it is, then it doesn't try to delete it, otherwise it does. After deleting
         * all the faults, if it successfully deleted all faults associated with an intervention, deletes the
         * intervention itself. Then checks if the user associated to the intervention was into the database for
         * other interventions or not. If she wasn't, it deletes her.
         */
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (TelephoneBox.Text != "")
            {
                for (int i = 0; i < AddProductsView.Rows.Count - 1; i++)
                {
                    var stringPNC = (string)AddProductsView["PNC", i].Value;
                    var PNC = stringPNC == null ? null : (decimal?)decimal.Parse(stringPNC);
                    var stringSNC = (string)AddProductsView["SNC", i].Value;
                    var SNC = stringSNC == null ? null : (decimal?)decimal.Parse(stringSNC);
                    if (PNC != null && SNC != null)
                    {
                        try
                        {
                            db.Guasti.DeleteOnSubmit((from g in db.Guasti
                                                      where g.NumeroTelefonoCliente == TelephoneBox.Text
                                                            && g.DataRichiestaIntervento == lastInsertionTime
                                                            && g.PNC == PNC
                                                            && g.SNC == SNC
                                                      select g).Single());
                            db.SubmitChanges();
                            if ((from g in db.Guasti where g.PNC == PNC && g.SNC == SNC select g).Count() == 0)
                            {
                                db.Prodotti.DeleteOnSubmit((from p in db.Prodotti
                                                            where p.PNC == PNC && p.SNC == SNC
                                                            select p).Single());
                                db.SubmitChanges();
                            }
                        }
                        catch (InvalidOperationException)
                        {
                            MessageBox.Show(ProductFaultNotFound, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ClearButton_Click(sender, e);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show(CannotDeleteFault + i, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        ClearButton_Click(sender, e);
                        return;
                    }
                }
                if ((from g in db.Guasti
                     where g.NumeroTelefonoCliente == TelephoneBox.Text
                           && g.DataRichiestaIntervento == lastInsertionTime
                     select g).Count() == 0)
                {
                    try
                    {
                        db.Interventi.DeleteOnSubmit((from i in db.Interventi
                                                      where i.NumeroTelefonoCliente == TelephoneBox.Text
                                                            && i.DataRichiesta == lastInsertionTime
                                                      select i).Single());
                        db.SubmitChanges();
                        if ((from i in db.Interventi
                             where i.NumeroTelefonoCliente == TelephoneBox.Text
                             select i).Count() == 0)
                        {
                            db.Clienti.DeleteOnSubmit((from c in db.Clienti
                                                       where c.NumeroTelefono == TelephoneBox.Text
                                                       select c).Single());
                            db.SubmitChanges();
                        }
                    }
                    catch (InvalidOperationException)
                    {
                        MessageBox.Show(InterventionNotFound, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(CannotDeleteIntervention, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ClearButton_Click(sender, e);
        }

        /*
         * If this form is closed by user clicking on close button, and the closing event is not issued by the parent form,
         * then close also the parent form. This is done because exiting the application is only allowed by default by
         * closing all currently opened, so also opened but hidden, windows. When this form is showed, the parent form
         * is currently hidden, so it cannot be closed unless we tell it to do so.
         */
        private void OperatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                Owner.Close();
            }
        }
    }
}
