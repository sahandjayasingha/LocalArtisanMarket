namespace LocalArtisanMarket
{
    partial class MaterialTrackingPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.lblSelectedProduct = new System.Windows.Forms.Label();
            this.lblMoisture = new System.Windows.Forms.Label();
            this.txtMoistureLevel = new System.Windows.Forms.TextBox();
            this.lblStage = new System.Windows.Forms.Label();
            this.cmbProductionStage = new System.Windows.Forms.ComboBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtSupplierInfo = new System.Windows.Forms.TextBox();
            this.dgvTelemetryGrid = new System.Windows.Forms.DataGridView();
            this.btnSaveTelemetry = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelemetryGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // lblSelectedProduct
            // 
            this.lblSelectedProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.lblSelectedProduct.Location = new System.Drawing.Point(20, 15);
            this.lblSelectedProduct.Size = new System.Drawing.Size(400, 23);
            this.lblSelectedProduct.Text = "Active Tracking Product ID: None Selected";
            // 
            // lblMoisture
            // 
            this.lblMoisture.Location = new System.Drawing.Point(20, 50);
            this.lblMoisture.Size = new System.Drawing.Size(120, 23);
            this.lblMoisture.Text = "Moisture Level (%):";
            // 
            // txtMoistureLevel
            // 
            this.txtMoistureLevel.Location = new System.Drawing.Point(150, 50);
            this.txtMoistureLevel.Size = new System.Drawing.Size(180, 20);
            this.txtMoistureLevel.Name = "txtMoistureLevel";
            // 
            // lblStage
            // 
            this.lblStage.Location = new System.Drawing.Point(20, 85);
            this.lblStage.Size = new System.Drawing.Size(120, 23);
            this.lblStage.Text = "Production Stage:";
            // 
            // cmbProductionStage
            // 
            this.cmbProductionStage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProductionStage.Items.AddRange(new object[] { "Raw", "Processing", "Cured", "Finished Quality Check" });
            this.cmbProductionStage.Location = new System.Drawing.Point(150, 85);
            this.cmbProductionStage.Size = new System.Drawing.Size(180, 21);
            this.cmbProductionStage.Name = "cmbProductionStage";
            // 
            // lblSupplier
            // 
            this.lblSupplier.Location = new System.Drawing.Point(20, 120);
            this.lblSupplier.Size = new System.Drawing.Size(120, 23);
            this.lblSupplier.Text = "Supplier Info:";
            // 
            // txtSupplierInfo
            // 
            this.txtSupplierInfo.Location = new System.Drawing.Point(150, 120);
            this.txtSupplierInfo.Size = new System.Drawing.Size(180, 20);
            this.txtSupplierInfo.Name = "txtSupplierInfo";
            // 
            // btnSaveTelemetry
            // 
            this.btnSaveTelemetry.Location = new System.Drawing.Point(20, 160);
            this.btnSaveTelemetry.Size = new System.Drawing.Size(140, 30);
            this.btnSaveTelemetry.Text = "Update Telemetry";
            this.btnSaveTelemetry.Name = "btnSaveTelemetry";
            this.btnSaveTelemetry.Click += new System.EventHandler(this.btnSaveTelemetry_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(175, 160);
            this.btnRefresh.Size = new System.Drawing.Size(100, 30);
            this.btnRefresh.Text = "Refresh Grid";
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgvTelemetryGrid
            // 
            this.dgvTelemetryGrid.AllowUserToAddRows = false;
            this.dgvTelemetryGrid.AllowUserToDeleteRows = false;
            this.dgvTelemetryGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTelemetryGrid.Location = new System.Drawing.Point(20, 210);
            this.dgvTelemetryGrid.Size = new System.Drawing.Size(600, 240);
            this.dgvTelemetryGrid.ReadOnly = true;
            this.dgvTelemetryGrid.Name = "dgvTelemetryGrid";
            this.dgvTelemetryGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTelemetryGrid_CellClick);
            // 
            // MaterialTrackingPanel
            // 
            this.Controls.Add(this.lblSelectedProduct);
            this.Controls.Add(this.lblMoisture);
            this.Controls.Add(this.txtMoistureLevel);
            this.Controls.Add(this.lblStage);
            this.Controls.Add(this.cmbProductionStage);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.txtSupplierInfo);
            this.Controls.Add(this.btnSaveTelemetry);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.dgvTelemetryGrid);
            this.Size = new System.Drawing.Size(650, 480);
            this.Load += new System.EventHandler(this.MaterialTrackingPanel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTelemetryGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblSelectedProduct;
        private System.Windows.Forms.Label lblMoisture;
        private System.Windows.Forms.TextBox txtMoistureLevel;
        private System.Windows.Forms.Label lblStage;
        private System.Windows.Forms.ComboBox cmbProductionStage;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplierInfo;
        private System.Windows.Forms.Button btnSaveTelemetry;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridView dgvTelemetryGrid;
    }
}
