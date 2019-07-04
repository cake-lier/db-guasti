namespace FailuresManagement
{
    partial class TechnicianForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TechnicianForm));
            this.TechnicianView = new System.Windows.Forms.Panel();
            this.EditButton = new System.Windows.Forms.Button();
            this.ProductsView = new System.Windows.Forms.DataGridView();
            this.FaultsView = new System.Windows.Forms.DataGridView();
            this.InterventionView = new System.Windows.Forms.DataGridView();
            this.TechnicianView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ProductsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FaultsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.InterventionView)).BeginInit();
            this.SuspendLayout();
            // 
            // TechnicianView
            // 
            this.TechnicianView.Controls.Add(this.EditButton);
            this.TechnicianView.Controls.Add(this.ProductsView);
            this.TechnicianView.Controls.Add(this.FaultsView);
            this.TechnicianView.Controls.Add(this.InterventionView);
            this.TechnicianView.Location = new System.Drawing.Point(0, -1);
            this.TechnicianView.Name = "TechnicianView";
            this.TechnicianView.Size = new System.Drawing.Size(1064, 682);
            this.TechnicianView.TabIndex = 4;
            // 
            // EditButton
            // 
            this.EditButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditButton.Location = new System.Drawing.Point(963, 640);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(88, 29);
            this.EditButton.TabIndex = 3;
            this.EditButton.Text = "Modifica";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // ProductsView
            // 
            this.ProductsView.AllowUserToAddRows = false;
            this.ProductsView.AllowUserToDeleteRows = false;
            this.ProductsView.AllowUserToOrderColumns = true;
            this.ProductsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProductsView.Location = new System.Drawing.Point(330, 343);
            this.ProductsView.Name = "ProductsView";
            this.ProductsView.Size = new System.Drawing.Size(721, 287);
            this.ProductsView.TabIndex = 2;
            // 
            // FaultsView
            // 
            this.FaultsView.AllowUserToAddRows = false;
            this.FaultsView.AllowUserToDeleteRows = false;
            this.FaultsView.AllowUserToOrderColumns = true;
            this.FaultsView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FaultsView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.FaultsView.Location = new System.Drawing.Point(330, 22);
            this.FaultsView.Name = "FaultsView";
            this.FaultsView.Size = new System.Drawing.Size(722, 304);
            this.FaultsView.TabIndex = 1;
            // 
            // InterventionView
            // 
            this.InterventionView.AllowUserToAddRows = false;
            this.InterventionView.AllowUserToDeleteRows = false;
            this.InterventionView.AllowUserToOrderColumns = true;
            this.InterventionView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.InterventionView.Location = new System.Drawing.Point(13, 22);
            this.InterventionView.MultiSelect = false;
            this.InterventionView.Name = "InterventionView";
            this.InterventionView.Size = new System.Drawing.Size(311, 608);
            this.InterventionView.TabIndex = 0;
            this.InterventionView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.InterventionView_CellClick);
            // 
            // TechnicianForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1064, 681);
            this.Controls.Add(this.TechnicianView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TechnicianForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Failure Management System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TechnicianForm_FormClosing);
            this.Load += new System.EventHandler(this.TechnicianForm_Load);
            this.TechnicianView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ProductsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FaultsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.InterventionView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel TechnicianView;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.DataGridView ProductsView;
        private System.Windows.Forms.DataGridView FaultsView;
        private System.Windows.Forms.DataGridView InterventionView;
    }
}