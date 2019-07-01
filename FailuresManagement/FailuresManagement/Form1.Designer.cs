namespace FailuresManagement
{
    partial class MainWindow
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.LoginButton = new System.Windows.Forms.Button();
            this.LoginBox = new System.Windows.Forms.TextBox();
            this.Title = new System.Windows.Forms.Label();
            this.LoginTitle = new System.Windows.Forms.Label();
            this.StartPanel = new System.Windows.Forms.Panel();
            this.DesignerView = new System.Windows.Forms.TabControl();
            this.Keyword = new System.Windows.Forms.TabPage();
            this.SearchView = new System.Windows.Forms.DataGridView();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.SearchButton = new System.Windows.Forms.Button();
            this.ViewAll = new System.Windows.Forms.TabPage();
            this.AllFaultsView = new System.Windows.Forms.DataGridView();
            this.PNC = new System.Windows.Forms.TabPage();
            this.TopPNCView = new System.Windows.Forms.DataGridView();
            this.ComponentCode = new System.Windows.Forms.TabPage();
            this.TopComponentCodeView = new System.Windows.Forms.DataGridView();
            this.NumberSpareParts = new System.Windows.Forms.TabPage();
            this.TopSparePartsView = new System.Windows.Forms.DataGridView();
            this.CostSpareParts = new System.Windows.Forms.TabPage();
            this.TopCostSparePartsView = new System.Windows.Forms.DataGridView();
            this.Countries = new System.Windows.Forms.TabPage();
            this.TopNations = new System.Windows.Forms.DataGridView();
            this.TTFPurchase = new System.Windows.Forms.TabPage();
            this.TTFPurchaseView = new System.Windows.Forms.DataGridView();
            this.TTFInstallation = new System.Windows.Forms.TabPage();
            this.TTFInstallationView = new System.Windows.Forms.DataGridView();
            this.AvgTime = new System.Windows.Forms.TabPage();
            this.AvgTimeView = new System.Windows.Forms.DataGridView();
            this.TechnicianView = new System.Windows.Forms.Panel();
            this.FaultsView = new System.Windows.Forms.DataGridView();
            this.InterventionView = new System.Windows.Forms.DataGridView();
            this.ProductsView = new System.Windows.Forms.DataGridView();
            this.StartPanel.SuspendLayout();
            this.DesignerView.SuspendLayout();
            this.Keyword.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchView)).BeginInit();
            this.ViewAll.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AllFaultsView)).BeginInit();
            this.PNC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopPNCView)).BeginInit();
            this.ComponentCode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopComponentCodeView)).BeginInit();
            this.NumberSpareParts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopSparePartsView)).BeginInit();
            this.CostSpareParts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopCostSparePartsView)).BeginInit();
            this.Countries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopNations)).BeginInit();
            this.TTFPurchase.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TTFPurchaseView)).BeginInit();
            this.TTFInstallation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TTFInstallationView)).BeginInit();
            this.AvgTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AvgTimeView)).BeginInit();
            this.TechnicianView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FaultsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InterventionView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductsView)).BeginInit();
            this.SuspendLayout();
            // 
            // LoginButton
            // 
            this.LoginButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginButton.Location = new System.Drawing.Point(635, 317);
            this.LoginButton.Name = "LoginButton";
            this.LoginButton.Size = new System.Drawing.Size(75, 29);
            this.LoginButton.TabIndex = 0;
            this.LoginButton.Text = "Login";
            this.LoginButton.UseVisualStyleBackColor = true;
            this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // LoginBox
            // 
            this.LoginBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginBox.Location = new System.Drawing.Point(295, 285);
            this.LoginBox.Name = "LoginBox";
            this.LoginBox.Size = new System.Drawing.Size(415, 26);
            this.LoginBox.TabIndex = 1;
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 45F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(114, 65);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(824, 69);
            this.Title.TabIndex = 2;
            this.Title.Text = "Failures Management System";
            // 
            // LoginTitle
            // 
            this.LoginTitle.AutoSize = true;
            this.LoginTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LoginTitle.Location = new System.Drawing.Point(288, 230);
            this.LoginTitle.Name = "LoginTitle";
            this.LoginTitle.Size = new System.Drawing.Size(189, 39);
            this.LoginTitle.TabIndex = 3;
            this.LoginTitle.Text = "Credenziali";
            // 
            // StartPanel
            // 
            this.StartPanel.Controls.Add(this.Title);
            this.StartPanel.Controls.Add(this.LoginBox);
            this.StartPanel.Controls.Add(this.LoginTitle);
            this.StartPanel.Controls.Add(this.LoginButton);
            this.StartPanel.Location = new System.Drawing.Point(0, 0);
            this.StartPanel.Name = "StartPanel";
            this.StartPanel.Size = new System.Drawing.Size(1064, 682);
            this.StartPanel.TabIndex = 4;
            // 
            // DesignerView
            // 
            this.DesignerView.Controls.Add(this.Keyword);
            this.DesignerView.Controls.Add(this.ViewAll);
            this.DesignerView.Controls.Add(this.PNC);
            this.DesignerView.Controls.Add(this.ComponentCode);
            this.DesignerView.Controls.Add(this.NumberSpareParts);
            this.DesignerView.Controls.Add(this.CostSpareParts);
            this.DesignerView.Controls.Add(this.Countries);
            this.DesignerView.Controls.Add(this.TTFPurchase);
            this.DesignerView.Controls.Add(this.TTFInstallation);
            this.DesignerView.Controls.Add(this.AvgTime);
            this.DesignerView.Location = new System.Drawing.Point(0, 0);
            this.DesignerView.Name = "DesignerView";
            this.DesignerView.SelectedIndex = 0;
            this.DesignerView.Size = new System.Drawing.Size(1061, 679);
            this.DesignerView.TabIndex = 4;
            // 
            // Keyword
            // 
            this.Keyword.Controls.Add(this.SearchView);
            this.Keyword.Controls.Add(this.SearchBox);
            this.Keyword.Controls.Add(this.SearchButton);
            this.Keyword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Keyword.Location = new System.Drawing.Point(4, 22);
            this.Keyword.Name = "Keyword";
            this.Keyword.Padding = new System.Windows.Forms.Padding(3);
            this.Keyword.Size = new System.Drawing.Size(1053, 653);
            this.Keyword.TabIndex = 0;
            this.Keyword.Text = "Cerca per parola chiave";
            this.Keyword.UseVisualStyleBackColor = true;
            // 
            // SearchView
            // 
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle10.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SearchView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.SearchView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.SearchView.DefaultCellStyle = dataGridViewCellStyle11;
            this.SearchView.Location = new System.Drawing.Point(0, 46);
            this.SearchView.Name = "SearchView";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SearchView.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.SearchView.Size = new System.Drawing.Size(1053, 607);
            this.SearchView.TabIndex = 2;
            // 
            // SearchBox
            // 
            this.SearchBox.Location = new System.Drawing.Point(8, 14);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(958, 20);
            this.SearchBox.TabIndex = 1;
            // 
            // SearchButton
            // 
            this.SearchButton.Location = new System.Drawing.Point(973, 12);
            this.SearchButton.Name = "SearchButton";
            this.SearchButton.Size = new System.Drawing.Size(75, 23);
            this.SearchButton.TabIndex = 0;
            this.SearchButton.Text = "Cerca";
            this.SearchButton.UseVisualStyleBackColor = true;
            this.SearchButton.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // ViewAll
            // 
            this.ViewAll.Controls.Add(this.AllFaultsView);
            this.ViewAll.Location = new System.Drawing.Point(4, 22);
            this.ViewAll.Name = "ViewAll";
            this.ViewAll.Padding = new System.Windows.Forms.Padding(3);
            this.ViewAll.Size = new System.Drawing.Size(1053, 653);
            this.ViewAll.TabIndex = 1;
            this.ViewAll.Text = "Tutti i guasti";
            this.ViewAll.UseVisualStyleBackColor = true;
            // 
            // AllFaultsView
            // 
            this.AllFaultsView.AllowUserToAddRows = false;
            this.AllFaultsView.AllowUserToDeleteRows = false;
            this.AllFaultsView.AllowUserToOrderColumns = true;
            this.AllFaultsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AllFaultsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AllFaultsView.Location = new System.Drawing.Point(3, 3);
            this.AllFaultsView.Name = "AllFaultsView";
            this.AllFaultsView.ReadOnly = true;
            this.AllFaultsView.Size = new System.Drawing.Size(1047, 647);
            this.AllFaultsView.TabIndex = 0;
            // 
            // PNC
            // 
            this.PNC.Controls.Add(this.TopPNCView);
            this.PNC.Location = new System.Drawing.Point(4, 22);
            this.PNC.Name = "PNC";
            this.PNC.Size = new System.Drawing.Size(1053, 653);
            this.PNC.TabIndex = 2;
            this.PNC.Text = "Top 5 PNC";
            this.PNC.UseVisualStyleBackColor = true;
            // 
            // TopPNCView
            // 
            this.TopPNCView.AllowUserToAddRows = false;
            this.TopPNCView.AllowUserToDeleteRows = false;
            this.TopPNCView.AllowUserToOrderColumns = true;
            this.TopPNCView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TopPNCView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopPNCView.Location = new System.Drawing.Point(0, 0);
            this.TopPNCView.Name = "TopPNCView";
            this.TopPNCView.ReadOnly = true;
            this.TopPNCView.Size = new System.Drawing.Size(1053, 653);
            this.TopPNCView.TabIndex = 0;
            // 
            // ComponentCode
            // 
            this.ComponentCode.Controls.Add(this.TopComponentCodeView);
            this.ComponentCode.Location = new System.Drawing.Point(4, 22);
            this.ComponentCode.Name = "ComponentCode";
            this.ComponentCode.Size = new System.Drawing.Size(1053, 653);
            this.ComponentCode.TabIndex = 3;
            this.ComponentCode.Text = "Top 5 Component Code";
            this.ComponentCode.UseVisualStyleBackColor = true;
            // 
            // TopComponentCodeView
            // 
            this.TopComponentCodeView.AllowUserToAddRows = false;
            this.TopComponentCodeView.AllowUserToDeleteRows = false;
            this.TopComponentCodeView.AllowUserToOrderColumns = true;
            this.TopComponentCodeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TopComponentCodeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopComponentCodeView.Location = new System.Drawing.Point(0, 0);
            this.TopComponentCodeView.Name = "TopComponentCodeView";
            this.TopComponentCodeView.ReadOnly = true;
            this.TopComponentCodeView.Size = new System.Drawing.Size(1053, 653);
            this.TopComponentCodeView.TabIndex = 0;
            // 
            // NumberSpareParts
            // 
            this.NumberSpareParts.Controls.Add(this.TopSparePartsView);
            this.NumberSpareParts.Location = new System.Drawing.Point(4, 22);
            this.NumberSpareParts.Name = "NumberSpareParts";
            this.NumberSpareParts.Size = new System.Drawing.Size(1053, 653);
            this.NumberSpareParts.TabIndex = 4;
            this.NumberSpareParts.Text = "Top 5 Ricambi Usati";
            this.NumberSpareParts.UseVisualStyleBackColor = true;
            // 
            // TopSparePartsView
            // 
            this.TopSparePartsView.AllowUserToAddRows = false;
            this.TopSparePartsView.AllowUserToDeleteRows = false;
            this.TopSparePartsView.AllowUserToOrderColumns = true;
            this.TopSparePartsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TopSparePartsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopSparePartsView.Location = new System.Drawing.Point(0, 0);
            this.TopSparePartsView.Name = "TopSparePartsView";
            this.TopSparePartsView.ReadOnly = true;
            this.TopSparePartsView.Size = new System.Drawing.Size(1053, 653);
            this.TopSparePartsView.TabIndex = 0;
            // 
            // CostSpareParts
            // 
            this.CostSpareParts.Controls.Add(this.TopCostSparePartsView);
            this.CostSpareParts.Location = new System.Drawing.Point(4, 22);
            this.CostSpareParts.Name = "CostSpareParts";
            this.CostSpareParts.Size = new System.Drawing.Size(1053, 653);
            this.CostSpareParts.TabIndex = 5;
            this.CostSpareParts.Text = "Top 5 Interventi più costosi";
            this.CostSpareParts.UseVisualStyleBackColor = true;
            // 
            // TopCostSparePartsView
            // 
            this.TopCostSparePartsView.AllowUserToAddRows = false;
            this.TopCostSparePartsView.AllowUserToDeleteRows = false;
            this.TopCostSparePartsView.AllowUserToOrderColumns = true;
            this.TopCostSparePartsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TopCostSparePartsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopCostSparePartsView.Location = new System.Drawing.Point(0, 0);
            this.TopCostSparePartsView.Name = "TopCostSparePartsView";
            this.TopCostSparePartsView.ReadOnly = true;
            this.TopCostSparePartsView.Size = new System.Drawing.Size(1053, 653);
            this.TopCostSparePartsView.TabIndex = 0;
            // 
            // Countries
            // 
            this.Countries.Controls.Add(this.TopNations);
            this.Countries.Location = new System.Drawing.Point(4, 22);
            this.Countries.Name = "Countries";
            this.Countries.Size = new System.Drawing.Size(1053, 653);
            this.Countries.TabIndex = 6;
            this.Countries.Text = "Top 5 Paesi";
            this.Countries.UseVisualStyleBackColor = true;
            // 
            // TopNations
            // 
            this.TopNations.AllowUserToAddRows = false;
            this.TopNations.AllowUserToDeleteRows = false;
            this.TopNations.AllowUserToOrderColumns = true;
            this.TopNations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TopNations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopNations.Location = new System.Drawing.Point(0, 0);
            this.TopNations.Name = "TopNations";
            this.TopNations.ReadOnly = true;
            this.TopNations.Size = new System.Drawing.Size(1053, 653);
            this.TopNations.TabIndex = 0;
            // 
            // TTFPurchase
            // 
            this.TTFPurchase.Controls.Add(this.TTFPurchaseView);
            this.TTFPurchase.Location = new System.Drawing.Point(4, 22);
            this.TTFPurchase.Name = "TTFPurchase";
            this.TTFPurchase.Size = new System.Drawing.Size(1053, 653);
            this.TTFPurchase.TabIndex = 7;
            this.TTFPurchase.Text = "TTF da Acquisto";
            this.TTFPurchase.UseVisualStyleBackColor = true;
            // 
            // TTFPurchaseView
            // 
            this.TTFPurchaseView.AllowUserToAddRows = false;
            this.TTFPurchaseView.AllowUserToDeleteRows = false;
            this.TTFPurchaseView.AllowUserToOrderColumns = true;
            this.TTFPurchaseView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TTFPurchaseView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TTFPurchaseView.Location = new System.Drawing.Point(0, 0);
            this.TTFPurchaseView.Name = "TTFPurchaseView";
            this.TTFPurchaseView.ReadOnly = true;
            this.TTFPurchaseView.Size = new System.Drawing.Size(1053, 653);
            this.TTFPurchaseView.TabIndex = 0;
            // 
            // TTFInstallation
            // 
            this.TTFInstallation.Controls.Add(this.TTFInstallationView);
            this.TTFInstallation.Location = new System.Drawing.Point(4, 22);
            this.TTFInstallation.Name = "TTFInstallation";
            this.TTFInstallation.Size = new System.Drawing.Size(1053, 653);
            this.TTFInstallation.TabIndex = 8;
            this.TTFInstallation.Text = "TTF da Installazione";
            this.TTFInstallation.UseVisualStyleBackColor = true;
            // 
            // TTFInstallationView
            // 
            this.TTFInstallationView.AllowUserToAddRows = false;
            this.TTFInstallationView.AllowUserToDeleteRows = false;
            this.TTFInstallationView.AllowUserToOrderColumns = true;
            this.TTFInstallationView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TTFInstallationView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TTFInstallationView.Location = new System.Drawing.Point(0, 0);
            this.TTFInstallationView.Name = "TTFInstallationView";
            this.TTFInstallationView.ReadOnly = true;
            this.TTFInstallationView.Size = new System.Drawing.Size(1053, 653);
            this.TTFInstallationView.TabIndex = 0;
            // 
            // AvgTime
            // 
            this.AvgTime.Controls.Add(this.AvgTimeView);
            this.AvgTime.Location = new System.Drawing.Point(4, 22);
            this.AvgTime.Name = "AvgTime";
            this.AvgTime.Size = new System.Drawing.Size(1053, 653);
            this.AvgTime.TabIndex = 9;
            this.AvgTime.Text = "Media tempistiche";
            this.AvgTime.UseVisualStyleBackColor = true;
            // 
            // AvgTimeView
            // 
            this.AvgTimeView.AllowUserToAddRows = false;
            this.AvgTimeView.AllowUserToDeleteRows = false;
            this.AvgTimeView.AllowUserToOrderColumns = true;
            this.AvgTimeView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.AvgTimeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AvgTimeView.Location = new System.Drawing.Point(0, 0);
            this.AvgTimeView.Name = "AvgTimeView";
            this.AvgTimeView.ReadOnly = true;
            this.AvgTimeView.Size = new System.Drawing.Size(1053, 653);
            this.AvgTimeView.TabIndex = 0;
            // 
            // TechnicianView
            // 
            this.TechnicianView.Controls.Add(this.ProductsView);
            this.TechnicianView.Controls.Add(this.FaultsView);
            this.TechnicianView.Controls.Add(this.InterventionView);
            this.TechnicianView.Location = new System.Drawing.Point(0, 0);
            this.TechnicianView.Name = "TechnicianView";
            this.TechnicianView.Size = new System.Drawing.Size(1064, 682);
            this.TechnicianView.TabIndex = 3;
            // 
            // FaultsView
            // 
            this.FaultsView.AllowUserToAddRows = false;
            this.FaultsView.AllowUserToDeleteRows = false;
            this.FaultsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FaultsView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.FaultsView.Location = new System.Drawing.Point(295, 22);
            this.FaultsView.Name = "FaultsView";
            this.FaultsView.Size = new System.Drawing.Size(757, 205);
            this.FaultsView.TabIndex = 1;
            // 
            // InterventionView
            // 
            this.InterventionView.AllowUserToAddRows = false;
            this.InterventionView.AllowUserToDeleteRows = false;
            this.InterventionView.AllowUserToOrderColumns = true;
            this.InterventionView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InterventionView.Location = new System.Drawing.Point(13, 22);
            this.InterventionView.Name = "InterventionView";
            this.InterventionView.ReadOnly = true;
            this.InterventionView.Size = new System.Drawing.Size(269, 426);
            this.InterventionView.TabIndex = 0;
            this.InterventionView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InterventionView_CellContentClick);
            // 
            // ProductsView
            // 
            this.ProductsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProductsView.Location = new System.Drawing.Point(295, 233);
            this.ProductsView.Name = "ProductsView";
            this.ProductsView.Size = new System.Drawing.Size(756, 215);
            this.ProductsView.TabIndex = 2;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.TechnicianView);
            this.Controls.Add(this.DesignerView);
            this.Controls.Add(this.StartPanel);
            this.Name = "MainWindow";
            this.Text = "Failures Management System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.StartPanel.ResumeLayout(false);
            this.StartPanel.PerformLayout();
            this.DesignerView.ResumeLayout(false);
            this.Keyword.ResumeLayout(false);
            this.Keyword.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SearchView)).EndInit();
            this.ViewAll.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AllFaultsView)).EndInit();
            this.PNC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopPNCView)).EndInit();
            this.ComponentCode.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopComponentCodeView)).EndInit();
            this.NumberSpareParts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopSparePartsView)).EndInit();
            this.CostSpareParts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopCostSparePartsView)).EndInit();
            this.Countries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopNations)).EndInit();
            this.TTFPurchase.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TTFPurchaseView)).EndInit();
            this.TTFInstallation.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TTFInstallationView)).EndInit();
            this.AvgTime.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AvgTimeView)).EndInit();
            this.TechnicianView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FaultsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InterventionView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ProductsView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoginButton;
        private System.Windows.Forms.TextBox LoginBox;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label LoginTitle;
        private System.Windows.Forms.Panel StartPanel;
        private System.Windows.Forms.TabControl DesignerView;
        private System.Windows.Forms.TabPage ViewAll;
        private System.Windows.Forms.TabPage Keyword;
        private System.Windows.Forms.TabPage PNC;
        private System.Windows.Forms.TabPage ComponentCode;
        private System.Windows.Forms.TabPage NumberSpareParts;
        private System.Windows.Forms.TabPage CostSpareParts;
        private System.Windows.Forms.TabPage Countries;
        private System.Windows.Forms.TabPage TTFPurchase;
        private System.Windows.Forms.TabPage TTFInstallation;
        private System.Windows.Forms.TabPage AvgTime;
        private System.Windows.Forms.DataGridView AllFaultsView;
        private System.Windows.Forms.DataGridView TopPNCView;
        private System.Windows.Forms.DataGridView TopComponentCodeView;
        private System.Windows.Forms.DataGridView TopSparePartsView;
        private System.Windows.Forms.DataGridView TopCostSparePartsView;
        private System.Windows.Forms.DataGridView TopNations;
        private System.Windows.Forms.DataGridView TTFPurchaseView;
        private System.Windows.Forms.DataGridView TTFInstallationView;
        private System.Windows.Forms.DataGridView AvgTimeView;
        private System.Windows.Forms.DataGridView SearchView;
        private System.Windows.Forms.TextBox SearchBox;
        private System.Windows.Forms.Button SearchButton;
        private System.Windows.Forms.Panel TechnicianView;
        private System.Windows.Forms.DataGridView FaultsView;
        private System.Windows.Forms.DataGridView InterventionView;
        private System.Windows.Forms.DataGridView ProductsView;
    }
}

