using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace FailuresManagement
{
    /// <summary>
    /// Represents the form used by the designers of the company. It allows to show statistics over faults during the
    /// current month and search for keywords into their description.
    /// </summary>
    public partial class DesignerForm : Form
    {
        private readonly IQueryable<decimal> designerCategories;
        private readonly GestioneGuastiDataContext db;

        /// <summary>
        /// Default constructor. Needs the employee code of the designer accessing the form, so as to show her only
        /// the faults relevant to her analysis.
        /// </summary>
        /// <param name="designerCode">the employee code of the designer accessing this form</param>
        public DesignerForm(decimal designerCode)
        {
            db = new GestioneGuastiDataContext();
            designerCategories = from emp in db.Interessi
                                 where emp.CodiceProgettista == designerCode
                                 select emp.CodiceCategoria;
            InitializeComponent();
            MaximizeBox = false;
        }

        /*
         * At loading time, prepares all the DataGridViews with data gathered from the database.
         */
        private void DesignerForm_Load(object sender, EventArgs e)
        {
            AllFaultsView.DataSource = from row in db.AllFaults
                                       where designerCategories.Contains(row.CategoriaProdotto)
                                       select new
                                       {
                                           Categoria = (from c in db.Categorie
                                                        where c.Codice == row.CategoriaProdotto
                                                        select c.Nome).Single(),
                                           row.PNC,
                                           row.SNC,
                                           row.Modello,
                                           Data_di_richiesta_intervento = row.DataRichiestaIntervento,
                                           Data_di_visita = row.DataVisita,
                                           Data_di_acquisto = row.DataAcquisto.HasValue
                                                           ? row.DataAcquisto.Value.ToShortDateString()
                                                           : null,
                                           Data_di_installazione = row.DataInstallazione.HasValue
                                                                ? row.DataInstallazione.Value.ToShortDateString()
                                                                : null,
                                           row.Zona,
                                           Descrizione_cliente = row.DescrizioneCliente,
                                           Descrizione_tecnico = row.DescrizioneTecnico,
                                           Component_code = row.ComponentCode,
                                           Nome_del_componente = row.NomeComponente,
                                           Codice_tipo_di_difetto = row.CodiceTipoDifetto,
                                           Nome_tipo_di_difetto = row.NomeTipoDifetto,
                                           Codice_ricambio = row.CodiceRicambio,
                                           Nome_del_ricambio = row.NomeRicambio,
                                           Costo_di_acquisto = row.CostoAcquisto,
                                           Costo_di_installazione = row.CostoInstallazione
                                       };
            TopPNCView.DataSource = from row in db.TopPNC
                                    where designerCategories.Contains(row.CategoriaProdotto)
                                    select new
                                    {
                                        Categoria = (from c in db.Categorie
                                                     where c.Codice == row.CategoriaProdotto
                                                     select c.Nome).Single(),
                                        row.PNC,
                                        row.Numero
                                    };
            TopComponentCodeView.DataSource = from row in db.TopComponentCode
                                              where designerCategories.Contains(row.CategoriaProdotto)
                                              select new
                                              {
                                                  Categoria = (from c in db.Categorie
                                                               where c.Codice == row.CategoriaProdotto
                                                               select c.Nome).Single(),
                                                  Component_code = row.ComponentCode,
                                                  Nome_del_componente = row.NomeComponente,
                                                  row.Numero
                                              };
            TopZones.DataSource = from row in db.TopZones
                                    where designerCategories.Contains(row.CategoriaProdotto)
                                    select new
                                    {
                                        Categoria = (from c in db.Categorie
                                                     where c.Codice == row.CategoriaProdotto
                                                     select c.Nome).Single(),
                                        row.Zona,
                                        row.Numero
                                    };
            TopSparePartsView.DataSource = from row in db.TopSpareParts
                                           where designerCategories.Contains(row.CategoriaProdotto)
                                           select new
                                           {
                                               Categoria = (from c in db.Categorie
                                                            where c.Codice == row.CategoriaProdotto
                                                            select c.Nome).Single(),
                                               row.Codice,
                                               row.Nome,
                                               Costo_di_acquisto = row.CostoAcquisto,
                                               Costo_di_installazione = row.CostoInstallazione,
                                               row.Numero
                                           };
            TopCostSparePartsView.DataSource = from row in db.TopCostSpareParts
                                               where designerCategories.Contains(row.CategoriaProdotto)
                                               select new
                                               {
                                                   Categoria = (from c in db.Categorie
                                                                where c.Codice == row.CategoriaProdotto
                                                                select c.Nome).Single(),
                                                   Data_di_richiesta_intervento = row.DataRichiestaIntervento,
                                                   Costo_totale = row.CostoTotale
                                               };
            AvgTimeView.DataSource = from row in db.AvgTime
                                     where designerCategories.Contains(row.CategoriaProdotto)
                                     select new
                                     {
                                         Categoria = (from c in db.Categorie
                                                      where c.Codice == row.CategoriaProdotto
                                                      select c.Nome).Single(),
                                         Codice_tipo_di_difetto = row.CodiceTipo,
                                         Nome_tipo_di_difetto = row.NomeTipo,
                                         Tempo_medio_di_riparazione = row.TempoMedioRiparazione
                                     };
            AvgTimeView.Sort(AvgTimeView.Columns["Tempo_medio_di_riparazione"], ListSortDirection.Ascending);
            TTFPurchaseView.DataSource = from row in db.TTFPurchase
                                         where designerCategories.Contains(row.CategoriaProdotto)
                                         select new
                                         {
                                             Categoria = (from c in db.Categorie
                                                          where c.Codice == row.CategoriaProdotto
                                                          select c.Nome).Single(),
                                             row.PNC,
                                             row.SNC,
                                             row.Modello,
                                             Codice_garanzia = row.CodiceGaranzia,
                                             row.TTF_in_giorni
                                         };
            TTFInstallationView.DataSource = from row in db.TTFInstallation
                                             where designerCategories.Contains(row.CategoriaProdotto)
                                             select new
                                             {
                                                 Categoria = (from c in db.Categorie
                                                              where c.Codice == row.CategoriaProdotto
                                                              select c.Nome).Single(),
                                                 row.PNC,
                                                 row.SNC,
                                                 row.Modello,
                                                 Codice_garanzia = row.CodiceGaranzia,
                                                 row.TTF_in_giorni
                                             };
        }

        /*
         * Manages the behavior of the "search" button: it creates a new query having as a result all faults of this month
         * which have into the "technician description" field the string specified into the TextBox related to this button.
         * Then shows the data by putting it into a DataGridView.
         */
        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (SearchBox.Text == "")
            {
                SearchView.DataSource = null;
                return;
            }
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

        /*
         * If this form is closed by user clicking on close button, and the closing event is not issued by the parent form,
         * then close also the parent form. This is done because exiting the application is only allowed by default by
         * closing all currently opened, so also opened but hidden, windows. When this form is showed, the parent form
         * is currently hidden, so it cannot be closed unless we tell it to do so.
         */
        private void DesignerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.FormOwnerClosing)
            {
                Owner.Close();
            }
        }
    }
}
