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
        private const string CredentialsErrorTitle = "Credenziali sbagliate";
        private const string CredentialsNoUser = "Le credenziali inserite non appartengono a nessun utente";
        private const string CredentialsFormatError = "Il formato delle credenziali è errato";

        private readonly GestioneGuastiDataContext db;

        public MainWindow()
        {
            db = new GestioneGuastiDataContext();
            InitializeComponent();
            AcceptButton = LoginButton;
        }

        private void LaunchSubForm(Form subform)
        {
            Hide();
            subform.Owner = this;
            subform.Show();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (LoginBox.Text == "*")
                {
                    LaunchSubForm(new ManagementForm());
                    return;
                }
                var employeeCode = decimal.Parse(LoginBox.Text);
                if ((from des in db.Progettisti where des.Codice == employeeCode select des).Count() == 1)
                {
                    LaunchSubForm(new DesignerForm(employeeCode));
                }
                else if ((from op in db.Operatori where op.Codice == employeeCode select op).Count() == 1)
                {
                    LaunchSubForm(new OperatorForm(employeeCode));
                }
                else if ((from tec in db.Tecnici where tec.Codice == employeeCode select tec).Count() == 1)
                {
                    LaunchSubForm(new TechnicianForm(employeeCode));
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
    }
}
