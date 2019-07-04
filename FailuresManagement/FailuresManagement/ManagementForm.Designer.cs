namespace FailuresManagement
{
    partial class ManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagementForm));
            this.ManagementView = new System.Windows.Forms.TabControl();
            this.OperatorsPage = new System.Windows.Forms.TabPage();
            this.OperatorsCountView = new System.Windows.Forms.DataGridView();
            this.TechniciansPage = new System.Windows.Forms.TabPage();
            this.TechniciansCountView = new System.Windows.Forms.DataGridView();
            this.AvgTechPage = new System.Windows.Forms.TabPage();
            this.TechniciansAvgView = new System.Windows.Forms.DataGridView();
            this.AvgCenterPage = new System.Windows.Forms.TabPage();
            this.CentersAvgView = new System.Windows.Forms.DataGridView();
            this.ManagementView.SuspendLayout();
            this.OperatorsPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OperatorsCountView)).BeginInit();
            this.TechniciansPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TechniciansCountView)).BeginInit();
            this.AvgTechPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TechniciansAvgView)).BeginInit();
            this.AvgCenterPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CentersAvgView)).BeginInit();
            this.SuspendLayout();
            // 
            // ManagementView
            // 
            this.ManagementView.Controls.Add(this.OperatorsPage);
            this.ManagementView.Controls.Add(this.TechniciansPage);
            this.ManagementView.Controls.Add(this.AvgTechPage);
            this.ManagementView.Controls.Add(this.AvgCenterPage);
            this.ManagementView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ManagementView.Location = new System.Drawing.Point(0, 1);
            this.ManagementView.Name = "ManagementView";
            this.ManagementView.SelectedIndex = 0;
            this.ManagementView.Size = new System.Drawing.Size(1064, 679);
            this.ManagementView.TabIndex = 5;
            // 
            // OperatorsPage
            // 
            this.OperatorsPage.Controls.Add(this.OperatorsCountView);
            this.OperatorsPage.Location = new System.Drawing.Point(4, 29);
            this.OperatorsPage.Name = "OperatorsPage";
            this.OperatorsPage.Padding = new System.Windows.Forms.Padding(3);
            this.OperatorsPage.Size = new System.Drawing.Size(1056, 646);
            this.OperatorsPage.TabIndex = 0;
            this.OperatorsPage.Text = "Interventi aperti operatori";
            this.OperatorsPage.UseVisualStyleBackColor = true;
            // 
            // OperatorsCountView
            // 
            this.OperatorsCountView.AllowUserToAddRows = false;
            this.OperatorsCountView.AllowUserToDeleteRows = false;
            this.OperatorsCountView.AllowUserToOrderColumns = true;
            this.OperatorsCountView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OperatorsCountView.Location = new System.Drawing.Point(0, 0);
            this.OperatorsCountView.Name = "OperatorsCountView";
            this.OperatorsCountView.ReadOnly = true;
            this.OperatorsCountView.Size = new System.Drawing.Size(1056, 653);
            this.OperatorsCountView.TabIndex = 0;
            // 
            // TechniciansPage
            // 
            this.TechniciansPage.Controls.Add(this.TechniciansCountView);
            this.TechniciansPage.Location = new System.Drawing.Point(4, 25);
            this.TechniciansPage.Name = "TechniciansPage";
            this.TechniciansPage.Padding = new System.Windows.Forms.Padding(3);
            this.TechniciansPage.Size = new System.Drawing.Size(1056, 650);
            this.TechniciansPage.TabIndex = 1;
            this.TechniciansPage.Text = "Interventi chiusi tecnici";
            this.TechniciansPage.UseVisualStyleBackColor = true;
            // 
            // TechniciansCountView
            // 
            this.TechniciansCountView.AllowUserToAddRows = false;
            this.TechniciansCountView.AllowUserToDeleteRows = false;
            this.TechniciansCountView.AllowUserToOrderColumns = true;
            this.TechniciansCountView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TechniciansCountView.Location = new System.Drawing.Point(-1, 0);
            this.TechniciansCountView.Name = "TechniciansCountView";
            this.TechniciansCountView.ReadOnly = true;
            this.TechniciansCountView.Size = new System.Drawing.Size(1057, 653);
            this.TechniciansCountView.TabIndex = 0;
            // 
            // AvgTechPage
            // 
            this.AvgTechPage.Controls.Add(this.TechniciansAvgView);
            this.AvgTechPage.Location = new System.Drawing.Point(4, 25);
            this.AvgTechPage.Name = "AvgTechPage";
            this.AvgTechPage.Padding = new System.Windows.Forms.Padding(3);
            this.AvgTechPage.Size = new System.Drawing.Size(1056, 650);
            this.AvgTechPage.TabIndex = 2;
            this.AvgTechPage.Text = "Tempo medio chiusura per tecnico";
            this.AvgTechPage.UseVisualStyleBackColor = true;
            // 
            // TechniciansAvgView
            // 
            this.TechniciansAvgView.AllowUserToAddRows = false;
            this.TechniciansAvgView.AllowUserToDeleteRows = false;
            this.TechniciansAvgView.AllowUserToOrderColumns = true;
            this.TechniciansAvgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TechniciansAvgView.Location = new System.Drawing.Point(0, 0);
            this.TechniciansAvgView.Name = "TechniciansAvgView";
            this.TechniciansAvgView.ReadOnly = true;
            this.TechniciansAvgView.Size = new System.Drawing.Size(1056, 653);
            this.TechniciansAvgView.TabIndex = 0;
            // 
            // AvgCenterPage
            // 
            this.AvgCenterPage.Controls.Add(this.CentersAvgView);
            this.AvgCenterPage.Location = new System.Drawing.Point(4, 25);
            this.AvgCenterPage.Name = "AvgCenterPage";
            this.AvgCenterPage.Padding = new System.Windows.Forms.Padding(3);
            this.AvgCenterPage.Size = new System.Drawing.Size(1056, 650);
            this.AvgCenterPage.TabIndex = 3;
            this.AvgCenterPage.Text = "Tempo medio chiusura per centro assistenza";
            this.AvgCenterPage.UseVisualStyleBackColor = true;
            // 
            // CentersAvgView
            // 
            this.CentersAvgView.AllowUserToAddRows = false;
            this.CentersAvgView.AllowUserToDeleteRows = false;
            this.CentersAvgView.AllowUserToOrderColumns = true;
            this.CentersAvgView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CentersAvgView.Location = new System.Drawing.Point(0, 0);
            this.CentersAvgView.Name = "CentersAvgView";
            this.CentersAvgView.ReadOnly = true;
            this.CentersAvgView.Size = new System.Drawing.Size(1056, 653);
            this.CentersAvgView.TabIndex = 0;
            // 
            // ManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.ManagementView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManagementForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Failure Management System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManagementForm_FormClosing);
            this.Load += new System.EventHandler(this.ManagementForm_Load);
            this.ManagementView.ResumeLayout(false);
            this.OperatorsPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.OperatorsCountView)).EndInit();
            this.TechniciansPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TechniciansCountView)).EndInit();
            this.AvgTechPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TechniciansAvgView)).EndInit();
            this.AvgCenterPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.CentersAvgView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ManagementView;
        private System.Windows.Forms.TabPage OperatorsPage;
        private System.Windows.Forms.DataGridView OperatorsCountView;
        private System.Windows.Forms.TabPage TechniciansPage;
        private System.Windows.Forms.DataGridView TechniciansCountView;
        private System.Windows.Forms.TabPage AvgTechPage;
        private System.Windows.Forms.DataGridView TechniciansAvgView;
        private System.Windows.Forms.TabPage AvgCenterPage;
        private System.Windows.Forms.DataGridView CentersAvgView;
    }
}