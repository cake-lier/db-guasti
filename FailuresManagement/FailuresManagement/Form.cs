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
        private GestioneGuastiDataContext db;
        private decimal employeeCode;
        private IQueryable<decimal> designerCategories;

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
            new List<string> { "PNC", "SNC", "Modello", "DataAcquisto", "DataInstallazione", "CodiceGaranzia", "Categoria" }
                             .ForEach(e => AddProductsView.Columns.Add(e, e));
            new List<string> { "PNC", "SNC", "Descrizione" }
                             .ForEach(e => AddFaultsView.Columns.Add(e, e));
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
                    MessageBox.Show("Le credenziali inserite non appartengono a nessun utente", "Credenziali sbagliate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Il formato delle credenziali è errato", "Credenziali sbagliate", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                           where fault.NumeroTelefonoCliente == (string)InterventionView["NumeroTelefonoCliente", e.RowIndex].Value
                                 && fault.DataRichiestaIntervento == (DateTime)InterventionView["DataRichiesta", e.RowIndex].Value
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
                new List<string> { "DescrizioneTecnico", "CodiceTipoDifetto", "ComponentCode" }.ForEach(s => FaultsView.Columns
                                                                                                                       .Add(s, s));
                ProductsView.Columns.Clear();
                ProductsView.DataSource = from row in data
                                          select new { row.Nome, row.PNC, row.SNC, row.Modello };
                new List<string> { "DataAcquisto", "DataInstallazione", "CodiceGaranzia" }.ForEach(s => ProductsView.Columns
                                                                                                                    .Add(s, s));
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
                                    where i.NumeroTelefonoCliente == (string)InterventionView["NumeroTelefonoCliente", selectedRow]
                                                                                             .Value
                                          && i.DataRichiesta == (DateTime)InterventionView["DataRichiesta", selectedRow].Value
                                    select i).Single();
                intervention.CodiceTecnico = employeeCode;
                intervention.Stato = 'C';
                var usedTime = (string)InterventionView["TempoImpiegato", selectedRow].Value;
                if (usedTime == null)
                {
                    MessageBox.Show("È obbligatorio inserire il tempo utilizzato", "Mancanza dati", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                intervention.TempoImpiegato = decimal.Parse(usedTime);
                for (int i = 0; i < FaultsView.Rows.Count; i++)
                {
                    var updateRow = (from f in db.Guasti
                                     where f.PNC == (decimal)FaultsView["PNC", i].Value && f.SNC == (decimal)FaultsView["SNC", i].Value

                                           && f.NumeroTelefonoCliente == (string)InterventionView["NumeroTelefonoCliente", selectedRow].Value
                                           && f.DataRichiestaIntervento == (DateTime)InterventionView["DataRichiesta", selectedRow]
                                                                                                     .Value
                                     select f).Single();
                    var description = (string)FaultsView["DescrizioneTecnico", i].Value;
                    if (description == null)
                    {
                        MessageBox.Show("È obbligatorio inserire la descrizione", "Mancanza dati", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    updateRow.DescrizioneTecnico = description;
                    var flawCode = (string)FaultsView["CodiceTipoDifetto", i].Value;
                    if (flawCode == null)
                    {
                        MessageBox.Show("È obbligatorio inserire il codice del tipo del difetto", "Mancanza dati", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    updateRow.CodiceTipoDifetto = decimal.Parse(flawCode);
                    var componentCode = (string)FaultsView["ComponentCode", i].Value;
                    if (componentCode == null)
                    {
                        MessageBox.Show("È obbligatorio inserire il componente affetto", "Mancanza dati", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    updateRow.ComponentCode = decimal.Parse(componentCode);
                }
                for (int i = 0; i < ProductsView.Rows.Count; i++)
                {
                    var updateRow = (from p in db.Prodotti
                                     where p.PNC == (decimal)FaultsView["PNC", i].Value && p.SNC == (decimal)FaultsView["SNC", i].Value
                                     select p).Single();
                    var purchaseDate = (string)ProductsView["DataAcquisto", i].Value;
                    updateRow.DataAcquisto = purchaseDate == null ? null : (DateTime?)DateTime.Parse(purchaseDate);
                    var installationDate = (string)ProductsView["DataInstallazione", i].Value;
                    updateRow.DataInstallazione = installationDate == null ? null : (DateTime?)DateTime.Parse(installationDate);
                    var warrantyCode = (string)ProductsView["CodiceGaranzia", i].Value;
                    updateRow.CodiceGaranzia = warrantyCode == null ? null : (decimal?)decimal.Parse(warrantyCode);
                }
                if (MessageBox.Show("Vuoi inserire questi dati? Saranno modificati solo quelli inerenti all'intervento correntemente selezionato",
                                   "Conferma",
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question)
                   == DialogResult.Yes)
                {
                    db.SubmitChanges();
                    MessageBox.Show("Inserimento effettuato con successo",
                                    "Successo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    db = new GestioneGuastiDataContext();
                    InitTechnicianView();
                }
            }
            catch (InvalidCastException)
            {
                MessageBox.Show("I dati non possono essere inseriti, è presente un errore di formattazione",
                                "Errore dati",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            catch (SqlException)
            {
                MessageBox.Show("I dati non possono essere inseriti, Component Code e il codice del tipo di difetto devono essere corretti",
                                "Errore dati",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void InsertButton_Click(object sender, EventArgs e)
        {
            var currentTime = DateTime.Now;
            db.Interventi.InsertOnSubmit(new Interventi
            {
                NumeroTelefonoCliente = TelephoneBox.Text,
                DataRichiesta = currentTime,
                CodiceOperatore = employeeCode,
                Stato = 'A',
                DataVisita = DateTime.Parse(VisitDateBox.Text),
                Nazione = (from op in db.Operatori
                           where op.Codice == employeeCode
                           select op.NazioneCentro).Single()
            });
            //Check per null di DataAcquisto, DataInstallazione & CodiceGaranzia prima di inserire (valido!!!)
            //Unire inserimento prodotti e guasti
            //Check preventivo per ogni inserimento
            //Check perchè non siano null i valori richiesti
            for (int i = 0; i < AddProductsView.Rows.Count; i++)
            {
                db.Prodotti.InsertOnSubmit(new Prodotti
                {
                    PNC = decimal.Parse((string)AddProductsView["PNC", i].Value),
                    SNC = decimal.Parse((string)AddProductsView["SNC", i].Value),
                    Modello = (string)AddProductsView["Modello", i].Value,
                    //DataAcquisto = ,
                    //DataInstallazione = ,
                    //CodiceGaranzia = ,
                    CodiceCategoria = (from c in db.Categorie where c.Nome == (string)AddProductsView["CodiceCategoria", i].Value select c.Codice).Single(),
                });
            }
            for (int i = 0; i < AddFaultsView.Rows.Count; i++)
            {
                db.Guasti.InsertOnSubmit(new Guasti
                                         {
                                              NumeroTelefonoCliente = TelephoneBox.Text,
                                              DataRichiestaIntervento = currentTime,
                                              //PNC = "",
                                              //SNC = "",
                                              //DescrizioneCliente = "",
                                              //CategoriaProdotto = ""
                                         });
            }
            
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            TelephoneBox.Text = null;
            VisitDateBox.Text = null;
            AddProductsView.Rows.Clear();
            AddFaultsView.Rows.Clear();
        }
    }
}
