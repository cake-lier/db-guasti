using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace FailuresManagement
{
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

        public OperatorForm(decimal operatorCode)
        {
            db = new GestioneGuastiDataContext();
            this.operatorCode = operatorCode;
            InitializeComponent();
            MaximizeBox = false;
        }

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

        private bool IsTextBoxValid(TextBox textBox, string fieldName)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show(MissingData + fieldName + "\"", MissingDataTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
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

        private void RevertChanges()
        {
            foreach (var insert in db.GetChangeSet().Inserts)
            {
                db.GetTable(insert.GetType()).DeleteOnSubmit(insert);
            }
            db.SubmitChanges();
        }

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
                Nazione = (from op in db.Operatori
                           where op.Codice == operatorCode
                           select op.NazioneCentro).Single()
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

        private void OperatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                Owner.Close();
            }
        }
    }
}
