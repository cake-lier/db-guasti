using System.Windows.Forms;

namespace FailuresManagement
{
    public partial class ManagementForm : Form
    {
        private readonly GestioneGuastiDataContext db;

        public ManagementForm()
        {
            db = new GestioneGuastiDataContext();
            InitializeComponent();
        }

        private void ManagementForm_Load(object sender, System.EventArgs e)
        {
            OperatorsCountView.DataSource = db.OperatorsIntervCount;
            TechniciansCountView.DataSource = db.TechniciansIntervCount;
            TechniciansAvgView.DataSource = db.TechniciansIntervAvg;
            CentersAvgView.DataSource = db.CentersIntervAvg;
        }

        private void ManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                Owner.Close();
            }
        }
    }
}
