using System;
using System.Data;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public partial class MaterialTrackingPanel : UserControl
    {
        private readonly ProductBusinessLogic _bll;
        private int currentSelectedProductId = -1;

        public MaterialTrackingPanel()
        {
            InitializeComponent();
            _bll = new ProductBusinessLogic();
        }

        private void MaterialTrackingPanel_Load(object sender, EventArgs e)
        {
            LoadTelemetryCatalog();
        }

        private void LoadTelemetryCatalog()
        {
            try
            {
                dgvTelemetryGrid.DataSource = _bll.GetCatalog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Telemetry sync failure: {ex.Message}", "System Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dgvTelemetryGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTelemetryGrid.Rows[e.RowIndex];

                try
                {
                    currentSelectedProductId = Convert.ToInt32(row.Cells["ProductID"].Value);
                    lblSelectedProduct.Text = $"Active Tracking Product ID: {currentSelectedProductId}";

                    txtMoistureLevel.Text = row.Cells["MoistureMetric"].Value?.ToString() ?? "0.00";
                    cmbProductionStage.Text = row.Cells["ProcessingStage"].Value?.ToString() ?? "Raw";
                    txtSupplierInfo.Text = row.Cells["OriginHub"].Value?.ToString() ?? "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Telemetry tracking synchronization error.", "Parsing Fault", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSaveTelemetry_Click(object sender, EventArgs e)
        {
            if (currentSelectedProductId == -1)
            {
                MessageBox.Show("Operational constraint: You must explicitly select a product from the grid matrix first.", "Target Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtMoistureLevel.Text, out decimal moisture) || moisture < 0 || moisture > 100)
            {
                MessageBox.Show("UI Validation Fault: Moisture level metrics must strictly resolve to a numeric value between 0.00% and 100.00%.", "Range Constraint Violation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbProductionStage.Text))
            {
                MessageBox.Show("UI Validation Fault: Processing state classification entry parameter cannot be empty.", "Missing Parameter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int pId = currentSelectedProductId;
                string pName = rowCellString(dgvTelemetryGrid.CurrentRow, "ProductName");
                decimal pPrice = Convert.ToDecimal(dgvTelemetryGrid.CurrentRow.Cells["Price"].Value);
                string pDesc = rowCellString(dgvTelemetryGrid.CurrentRow, "Description");
                int pStock = Convert.ToInt32(dgvTelemetryGrid.CurrentRow.Cells["Stock"].Value);
                string originHub = txtSupplierInfo.Text.Trim();
                string craftTech = rowCellString(dgvTelemetryGrid.CurrentRow, "CraftTechnique");
                string prodStage = cmbProductionStage.Text.Trim();
                string imgPath = dgvTelemetryGrid.CurrentRow.Cells["ImagePath"] != null ? rowCellString(dgvTelemetryGrid.CurrentRow, "ImagePath") : "";

                ProductDTO telemetryPayload = new ProductDTO(pId, pName, pPrice, pDesc, pStock, originHub, craftTech, moisture, prodStage, imgPath);

                _bll.ProcessProductUpdate(telemetryPayload);
                MessageBox.Show("Craft production metrics recorded successfully.", "Telemetry Live Sync", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTelemetryCatalog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Execution Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string rowCellString(DataGridViewRow row, string colName) => row.Cells[colName].Value?.ToString() ?? "";

        private void btnRefresh_Click(object sender, EventArgs e) => LoadTelemetryCatalog();
    }
}