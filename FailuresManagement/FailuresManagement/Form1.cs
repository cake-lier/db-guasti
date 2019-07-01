using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FailuresManagement
{
    public partial class MainWindow : Form
    {
        private readonly GestioneGuastiDataContext db;
        private IQueryable<decimal> designerCategories;

        public MainWindow()
        {
            db = new GestioneGuastiDataContext();
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DesignerView.Hide();
            TechnicianView.Hide();
            AcceptButton = LoginButton;
            FaultsView.ReadOnly = false;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                decimal empCode = decimal.Parse(LoginBox.Text);
                designerCategories = from emp in db.Interessi
                                     where emp.CodiceProgettista == empCode
                                     select emp.CodiceCategoria;
                if (designerCategories.Count() >= 1)
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
                    StartPanel.Hide();
                    AcceptButton = null;
                    DesignerView.Show();
                }
                else if ((from op in db.Operatori where op.Codice == empCode select op).Count() == 1)
                {
                    StartPanel.Hide();
                    AcceptButton = null;
                    //OperatorView.Show();
                }
                else if ((from tec in db.Tecnici where tec.Codice == empCode select tec).Count() == 1)
                {
                    InterventionView.DataSource = db.NewInterventions;
                    InterventionView.ClearSelection();
                    StartPanel.Hide();
                    AcceptButton = null;
                    TechnicianView.Show();
                }
                else
                {
                    LoginBox.BackColor = Color.Red;
                }
            }
            catch (FormatException)
            {
                LoginBox.BackColor = Color.Red;
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
                                    select new { fault.CategoriaProdotto, fault.PNC, fault.SNC, fault.DescrizioneCliente,
                                                 fault.DescrizioneTecnico, fault.ComponentCode, fault.CodiceTipoDifetto };
        }

        private void InterventionView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                FaultsView.Columns.Clear();
                FaultsView.DataSource = from fault in db.Guasti
                                        where fault.NumeroTelefonoCliente == (string)InterventionView[0, e.RowIndex].Value
                                              && fault.DataRichiestaIntervento == (DateTime)InterventionView[1, e.RowIndex].Value
                                        select new { fault.DescrizioneCliente };
                new List<string> { "DescrizioneTecnico", "CodiceTipoDifetto", "ComponentCode" }.ForEach(s => FaultsView.Columns
                                                                                                                       .Add(s, s));
                var products = from fault in db.Guasti
                                          where fault.NumeroTelefonoCliente == (string)InterventionView[0, e.RowIndex].Value
                                                && fault.DataRichiestaIntervento == (DateTime)InterventionView[1, e.RowIndex].Value
                                          select new { fault.Prodotti.Categorie.Nome, fault.Prodotti.PNC, fault.Prodotti.SNC,
                                                       fault.Prodotti.DataAcquisto, fault.Prodotti.DataInstallazione, fault.Prodotti.CodiceGaranzia, fault.Prodotti.Modello };
                ProductsView.DataSource = from knowns in products
                                          select new { knowns.Nome, knowns.PNC, knowns.SNC, knowns.Modello };
                new List<string> { "DataAcquisto", "DataInstallazione", "CodiceGaranzia" }.ForEach(s => ProductsView.Columns
                                                                                                                    .Add(s, s));
                var i = 0;
                var productsEnumerator = products.GetEnumerator();
                while (productsEnumerator.MoveNext())
                {
                    ProductsView[4, i].Value = productsEnumerator.Current.DataAcquisto;
                    ProductsView[5, i].Value = productsEnumerator.Current.DataInstallazione;
                    ProductsView[6, i].Value = productsEnumerator.Current.CodiceGaranzia;
                    i++;
                }
            }
        }

    }
}
