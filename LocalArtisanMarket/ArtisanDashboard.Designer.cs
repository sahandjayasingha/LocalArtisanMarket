namespace LocalArtisanMarket
{
    partial class ArtisanDashboard
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
            this.lblProductName = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.lblOriginHub = new System.Windows.Forms.Label();
            this.txtOriginHub = new System.Windows.Forms.TextBox();
            this.lblCraftTechnique = new System.Windows.Forms.Label();
            this.txtCraftTechnique = new System.Windows.Forms.TextBox();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.btnCreate = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.Location = new System.Drawing.Point(20, 20);
            this.lblProductName.Size = new System.Drawing.Size(100, 23);
            this.lblProductName.Text = "Product Name:";
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(130, 20);
            this.txtProductName.Size = new System.Drawing.Size(200, 20);
            this.txtProductName.Name = "txtProductName";
            // 
            // lblPrice
            // 
            this.lblPrice.Location = new System.Drawing.Point(20, 50);
            this.lblPrice.Size = new System.Drawing.Size(100, 23);
            this.lblPrice.Text = "Price:";
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(130, 50);
            this.txtPrice.Size = new System.Drawing.Size(200, 20);
            this.txtPrice.Name = "txtPrice";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(20, 80);
            this.lblDescription.Size = new System.Drawing.Size(100, 23);
            this.lblDescription.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(130, 80);
            this.txtDescription.Size = new System.Drawing.Size(200, 20);
            this.txtDescription.Name = "txtDescription";
            // 
            // lblStock
            // 
            this.lblStock.Location = new System.Drawing.Point(20, 110);
            this.lblStock.Size = new System.Drawing.Size(100, 23);
            this.lblStock.Text = "Stock Quantity:";
            // 
            // txtStock
            // 
            this.txtStock.Location = new System.Drawing.Point(130, 110);
            this.txtStock.Size = new System.Drawing.Size(200, 20);
            this.txtStock.Name = "txtStock";
            // 
            // lblOriginHub
            // 
            this.lblOriginHub.Location = new System.Drawing.Point(20, 140);
            this.lblOriginHub.Size = new System.Drawing.Size(100, 23);
            this.lblOriginHub.Text = "Origin Hub:";
            // 
            // txtOriginHub
            // 
            this.txtOriginHub.Location = new System.Drawing.Point(130, 140);
            this.txtOriginHub.Size = new System.Drawing.Size(200, 20);
            this.txtOriginHub.Name = "txtOriginHub";
            // 
            // lblCraftTechnique
            // 
            this.lblCraftTechnique.Location = new System.Drawing.Point(20, 170);
            this.lblCraftTechnique.Size = new System.Drawing.Size(100, 23);
            this.lblCraftTechnique.Text = "Craft Technique:";
            // 
            // txtCraftTechnique
            // 
            this.txtCraftTechnique.Location = new System.Drawing.Point(130, 170);
            this.txtCraftTechnique.Size = new System.Drawing.Size(200, 20);
            this.txtCraftTechnique.Name = "txtCraftTechnique";
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(20, 210);
            this.btnCreate.Size = new System.Drawing.Size(75, 30);
            this.btnCreate.Text = "Create";
            this.btnCreate.Name = "btnCreate";
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(105, 210);
            this.btnUpdate.Size = new System.Drawing.Size(75, 30);
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Name = "btnUpdate";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(190, 210);
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.Text = "Delete";
            this.btnDelete.Name = "btnDelete";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(275, 210);
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.Text = "Clear";
            this.btnClear.Name = "btnClear";
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProducts.Location = new System.Drawing.Point(20, 260);
            this.dgvProducts.Size = new System.Drawing.Size(600, 200);
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.Name = "dgvProducts";
            // 
            // ArtisanDashboard
            // 
            this.Controls.Add(this.lblProductName);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.lblOriginHub);
            this.Controls.Add(this.txtOriginHub);
            this.Controls.Add(this.lblCraftTechnique);
            this.Controls.Add(this.txtCraftTechnique);
            this.Controls.Add(this.btnCreate);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dgvProducts);
            this.Size = new System.Drawing.Size(650, 480);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lblOriginHub;
        private System.Windows.Forms.TextBox txtOriginHub;
        private System.Windows.Forms.Label lblCraftTechnique;
        private System.Windows.Forms.TextBox txtCraftTechnique;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DataGridView dgvProducts;
    }
}
