using System.Windows.Forms;

namespace FailuresManagement
{
    /// <summary>
    /// Represents the form used by the management of the company. It allows to show statistics over employees and
    /// help centers productivity.
    /// </summary>
    public partial class ManagementForm : Form
    {
        private readonly GestioneGuastiDataContext db;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public ManagementForm()
        {
            db = new GestioneGuastiDataContext();
            InitializeComponent();
            MaximizeBox = false;
        }

        /*
         * At loading time of the form, it prepares all the DataGridViews with data from the database.
         */
        private void ManagementForm_Load(object sender, System.EventArgs e)
        {
            OperatorsCountView.DataSource = db.OperatorsIntervCount;
            TechniciansCountView.DataSource = db.TechniciansIntervCount;
            TechniciansAvgView.DataSource = db.TechniciansIntervAvg;
            CentersAvgView.DataSource = db.CentersIntervAvg;
        }

        /*
         * If this form is closed by user clicking on close button, and the closing event is not issued by the parent form,
         * then close also the parent form. This is done because exiting the application is only allowed by default by
         * closing all currently opened, so also opened but hidden, windows. When this form is showed, the parent form
         * is currently hidden, so it cannot be closed unless we tell it to do so.
         */
        private void ManagementForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                Owner.Close();
            }
        }
    }
}
