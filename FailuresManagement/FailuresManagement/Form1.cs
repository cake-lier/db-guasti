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
                    StartPanel.Hide();
                    DesignerView.Show();
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
            } catch(FormatException)
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
    }
}
