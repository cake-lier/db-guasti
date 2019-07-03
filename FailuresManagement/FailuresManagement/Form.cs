using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace FailuresManagement
{
    public partial class MainWindow : Form
    {
        private const string MissingDataTitle = "Mancanza dati";
        private const string MissingUsedTime = "È obbligatorio inserire il tempo utilizzato";
        private const string MissingDescription = "È obbligatorio inserire la descrizione";
        private const string MissingFlawCode = "È obbligatorio inserire il codice del tipo del difetto";
        private const string MissingComponentCode = "È obbligatorio inserire il componente affetto";
        private const string CredentialsErrorTitle = "Credenziali sbagliate";
        private const string CredentialsNoUser = "Le credenziali inserite non appartengono a nessun utente";
        private const string CredentialsFormatError = "Il formato delle credenziali è errato";
        private const string ConfirmTitle = "Conferma";
        private const string InsertDataQuestion = "Vuoi inserire questi dati? Saranno modificati solo quelli inerenti all'intervento correntemente selezionato";
        private const string SuccessTitle = "Successo";
        private const string InsertSuccess = "Inserimento effettuato con successo";
        private const string ErrorTitle = "Errore";
        private const string DataFormatError = "I dati non possono essere inseriti, è presente un errore di formattazione";
        private const string NonExistingFlawError = "I dati non possono essere inseriti, Component Code e il codice del tipo di difetto devono essere corretti";
        private const string AtLeastOneProductError = "È necessario associare questo intervento ad almeno un prodotto che ha subito un guasto";
        private const string WrongCategoryError = "La categoria del prodotto non è stata correttamente inserita";

        private GestioneGuastiDataContext db;
        private decimal employeeCode;
        private IQueryable<decimal> designerCategories;
        private DateTime lastInsertionTime;

        public MainWindow()
        {
            db = new GestioneGuastiDataContext();
            InitializeComponent();
        }

        private void InitTechnicianView()
        {
            InterventionView.DataSource = db.NewInterventions;
            InterventionView.Columns.Add("TempoImpiegato", "TempoImpiegato");
            InterventionView.Sort(InterventionView.Columns["DataRichiesta"], ListSortDirection.Ascending);
            InterventionView.ClearSelection();
            FaultsView.Rows.Clear();
            FaultsView.Columns.Clear();
            ProductsView.Rows.Clear();
            ProductsView.Columns.Clear();
        }

        private void InitDesignerView()
        {
            AllFaultsView.DataSource = from row in db.AllFaults
                                       where designerCategories.Contains(row.CategoriaProdotto)
                                       select row;
            TopPNCView.DataSource = from row in db.TopPNC
                                    where designerCategories.Contains(row.CategoriaProdotto)
                                    select row;
            TopComponentCodeView.DataSource = from row in db.TopComponentCode
                                              where designerCategories.Contains(row.CategoriaProdotto)
                                              select row;
            TopNations.DataSource = from row in db.TopNations
                                    where designerCategories.Contains(row.CategoriaProdotto)
                                    select row;
            TopSparePartsView.DataSource = from row in db.TopSpareParts
                                           where designerCategories.Contains(row.CategoriaProdotto)
                                           select row;
            TopCostSparePartsView.DataSource = from row in db.TopCostSpareParts
                                               where designerCategories.Contains(row.CategoriaProdotto)
                                               select row;
            AvgTimeView.DataSource = from row in db.AvgTime
                                     where designerCategories.Contains(row.CategoriaProdotto)
                                     select row;
            TTFPurchaseView.DataSource = from row in db.TTFPurchase
                                         where designerCategories.Contains(row.CategoriaProdotto)
                                         select row;
            TTFInstallationView.DataSource = from row in db.TTFInstallation
                                             where designerCategories.Contains(row.CategoriaProdotto)
                                             select row;
        }

        private void InitManagementView()
        {
            OperatorsCountView.DataSource = db.OperatorsIntervCount;
            TechniciansCountView.DataSource = db.TechniciansIntervCount;
            TechniciansAvgView.DataSource = db.TechniciansIntervAvg;
            CentersAvgView.DataSource = db.CentersIntervAvg;
        }

        private void InitOperatorView()
        {
            new List<string>
            {
                "PNC",
                "SNC",
                "Descrizione",
                "Modello",
                "DataAcquisto",
                "DataInstallazione",
                "CodiceGaranzia",
                "Categoria"
            }.ForEach(e => AddProductsView.Columns.Add(e, e));
            VisitDatePicker.CustomFormat = "dd/MM/yyyy HH:mm";
            VisitDatePicker.Value = new DateTime(DateTime.Now.Year,
                                                 DateTime.Now.Month,
                                                 DateTime.Now.Day,
                                                 DateTime.Now.Hour,
                                                 DateTime.Now.Minute,
                                                 0);
        }

        private void Form_Load(object sender, EventArgs e)
        {
            DesignerView.Hide();
            TechnicianView.Hide();
            ManagementView.Hide();
            OperatorView.Hide();
            AcceptButton = LoginButton;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoginBox.Text == "*")
                {
                    InitManagementView();
                    StartPanel.Hide();
                    AcceptButton = null;
                    ManagementView.Show();
                    return;
                }
                employeeCode = decimal.Parse(LoginBox.Text);
                designerCategories = from emp in db.Interessi
                                     where emp.CodiceProgettista == employeeCode
                                     select emp.CodiceCategoria;
                if (designerCategories.Count() >= 1)
                {
                    InitDesignerView();
                    StartPanel.Hide();
                    AcceptButton = SearchButton;
                    DesignerView.Show();
                }
                else if ((from op in db.Operatori where op.Codice == employeeCode select op).Count() == 1)
                {
                    InitOperatorView();
                    StartPanel.Hide();
                    AcceptButton = null;
                    OperatorView.Show();
                }
                else if ((from tec in db.Tecnici where tec.Codice == employeeCode select tec).Count() == 1)
                {
                    InitTechnicianView();
                    StartPanel.Hide();
                    AcceptButton = EditButton;
                    TechnicianView.Show();
                }
                else
                {
                    MessageBox.Show(CredentialsNoUser, CredentialsErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show(CredentialsFormatError, CredentialsErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchView.DataSource = from fault in db.Guasti
                                    where fault.CodiceTipoDifetto != null && fault.ComponentCode != null
                                          && fault.DataRichiestaIntervento.Month == DateTime.Now.Month
                                          && fault.DataRichiestaIntervento.Year == DateTime.Now.Year
                                          && fault.DescrizioneTecnico.Contains(SearchBox.Text)
                                          && designerCategories.Contains(fault.CategoriaProdotto)
                                    select new
                                    {
                                        fault.CategoriaProdotto,
                                        fault.PNC,
                                        fault.SNC,
                                        fault.DescrizioneCliente,
                                        fault.DescrizioneTecnico,
                                        fault.ComponentCode,
                                        fault.CodiceTipoDifetto
                                    };
        }

        private void InterventionView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var data = from fault in db.Guasti
                           where fault.NumeroTelefonoCliente 
                                 == (string)InterventionView["NumeroTelefonoCliente", e.RowIndex].Value
                                 && fault.DataRichiestaIntervento
                                    == (DateTime)InterventionView["DataRichiesta", e.RowIndex].Value
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
                    "DescrizioneTecnico",
                    "CodiceTipoDifetto",
                    "ComponentCode"
                }.ForEach(s => FaultsView.Columns.Add(s, s));
                ProductsView.Columns.Clear();
                ProductsView.DataSource = from row in data
                                          select new { row.Nome, row.PNC, row.SNC, row.Modello };
                new List<string>
                {
                    "DataAcquisto",
                    "DataInstallazione",
                    "CodiceGaranzia"
                }.ForEach(s => ProductsView.Columns.Add(s, s));
                var i = 0;
                var productsEnumerator = data.GetEnumerator();
                while (productsEnumerator.MoveNext())
                {
                    ProductsView["DataAcquisto", i].Value = productsEnumerator.Current.DataAcquisto?.ToShortDateString();
                    ProductsView["DataInstallazione", i].Value = productsEnumerator.Current.DataInstallazione?.ToShortDateString();
                    ProductsView["CodiceGaranzia", i].Value = productsEnumerator.Current.CodiceGaranzia?.ToString();
                    i++;
                }
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                var selectedRow = InterventionView.SelectedCells[0].RowIndex;
                var intervention = (from i in db.Interventi
                                    where i.NumeroTelefonoCliente
                                          == (string)InterventionView["NumeroTelefonoCliente", selectedRow].Value
                                          && i.DataRichiesta == (DateTime)InterventionView["DataRichiesta", selectedRow].Value
                                    select i).Single();
                intervention.CodiceTecnico = employeeCode;
                intervention.Stato = 'C';
                var usedTime = (string)InterventionView["TempoImpiegato", selectedRow].Value;
                if (usedTime == null)
                {
                    MessageBox.Show(MissingUsedTime, MissingDataTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                intervention.TempoImpiegato = decimal.Parse(usedTime);
                for (int i = 0; i < FaultsView.Rows.Count; i++)
                {
                    var updateRow = (from f in db.Guasti
                                     where f.PNC == (decimal)FaultsView["PNC", i].Value
                                           && f.SNC == (decimal)FaultsView["SNC", i].Value
                                           && f.NumeroTelefonoCliente 
                                              == (string)InterventionView["NumeroTelefonoCliente", selectedRow].Value
                                           && f.DataRichiestaIntervento 
                                              == (DateTime)InterventionView["DataRichiesta", selectedRow].Value
                                     select f).Single();
                    var description = (string)FaultsView["DescrizioneTecnico", i].Value;
                    if (description == null)
                    {
                        MessageBox.Show(MissingDescription, MissingDataTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    updateRow.DescrizioneTecnico = description;
                    var flawCode = (string)FaultsView["CodiceTipoDifetto", i].Value;
                    if (flawCode == null)
                    {
                        MessageBox.Show(MissingFlawCode, MissingDataTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    updateRow.CodiceTipoDifetto = decimal.Parse(flawCode);
                    var componentCode = (string)FaultsView["ComponentCode", i].Value;
                    if (componentCode == null)
                    {
                        MessageBox.Show(MissingComponentCode, MissingDataTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    var purchaseDate = (string)ProductsView["DataAcquisto", i].Value;
                    updateRow.DataAcquisto = purchaseDate == null ? null : (DateTime?)DateTime.Parse(purchaseDate);
                    var installationDate = (string)ProductsView["DataInstallazione", i].Value;
                    updateRow.DataInstallazione = installationDate == null ? null : (DateTime?)DateTime.Parse(installationDate);
                    var warrantyCode = (string)ProductsView["CodiceGaranzia", i].Value;
                    updateRow.CodiceGaranzia = warrantyCode == null ? null : (decimal?)decimal.Parse(warrantyCode);
                }
                if (MessageBox.Show(InsertDataQuestion, ConfirmTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                {
                    db.SubmitChanges();
                    MessageBox.Show(InsertSuccess, SuccessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    db = new GestioneGuastiDataContext();
                    InitTechnicianView();
                }
            }
            catch (InvalidCastException)
            {
                MessageBox.Show(DataFormatError, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show(NonExistingFlawError, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsTextBoxValid(TextBox textBox, string fieldName)
        {
            if (textBox.Text == "")
            {
                MessageBox.Show("È necessario inserire il campo \"" + fieldName + "\"", ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool IsFieldValid(object field, string fieldName)
        {
            if (field == null)
            {
                MessageBox.Show("È necessario inserire il campo \"" + fieldName + "\n", ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            lastInsertionTime = new DateTime(DateTime.Now.Year, 
                                             DateTime.Now.Month,
                                             DateTime.Now.Day,
                                             DateTime.Now.Hour,
                                             DateTime.Now.Minute,
                                             0);
            if (!IsTextBoxValid(TelephoneBox, "NumeroTelefono")
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
                CodiceOperatore = employeeCode,
                Stato = 'A',
                DataVisita = VisitDatePicker.Value,
                Nazione = (from op in db.Operatori
                           where op.Codice == employeeCode
                           select op.NazioneCentro).Single()
            });
            if (AddProductsView.Rows.Count == 1)
            {
                MessageBox.Show(AtLeastOneProductError, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                for (int i = 0; i < AddProductsView.Rows.Count - 1; i++)
                {
                    var stringPNC = (string)AddProductsView["PNC", i].Value;
                    if (!IsFieldValid(stringPNC, "PNC"))
                    {
                        return;
                    }
                    var PNC = decimal.Parse(stringPNC);
                    var stringSNC = (string)AddProductsView["SNC", i].Value;
                    if (!IsFieldValid(stringSNC, "SNC"))
                    {
                        return;
                    }
                    var SNC = decimal.Parse(stringSNC);
                    var model = (string)AddProductsView["Modello", i].Value;
                    if (!IsFieldValid(model, "Modello"))
                    {
                        return;
                    }
                    var description = (string)AddProductsView["Descrizione", i].Value;
                    if (!IsFieldValid(description, "Descrizione"))
                    {
                        return;
                    }
                    var category = (string)AddProductsView["Categoria", i].Value;
                    if (!IsFieldValid(category, "Categoria"))
                    {
                        return;
                    }
                    var purchaseDate = (string)AddProductsView["DataAcquisto", i].Value;
                    var installationDate = (string)AddProductsView["DataInstallazione", i].Value;
                    var warrantyCode = (string)AddProductsView["CodiceGaranzia", i].Value;
                    var productCategories = from c in db.Categorie
                                            where c.Nome == category
                                            select c.Codice;
                    if (productCategories.Count() == 0)
                    {
                        MessageBox.Show(WrongCategoryError, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                DeleteButton.Enabled = true;
                ClearButton.Enabled = true;
                InsertButton.Enabled = false;
            }
            catch (FormatException)
            {
                MessageBox.Show(DataFormatError, ErrorTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    else
                    {
                        MessageBox.Show("Il guasto alla riga " + i + " non può essere cancellato");
                    }
                }
                if ((from g in db.Guasti
                     where g.NumeroTelefonoCliente == TelephoneBox.Text
                           && g.DataRichiestaIntervento == lastInsertionTime
                     select g).Count() == 0)
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
                db.SubmitChanges();
            }
            else
            {
                MessageBox.Show("Non è possibile cancellare i dati senza il numero di telefono del cliente chiamante");
            }
            ClearButton_Click(sender, e);
        }
    }
}
