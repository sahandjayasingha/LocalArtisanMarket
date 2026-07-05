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

        // Asynchronously reflects the catalog domain directly onto the tracking viewport layout
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

        // Real-time cell click synchronization to capture data linked to the selected ProductID
        private void dgvTelemetryGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTelemetryGrid.Rows[e.RowIndex];

                try
                {
                    currentSelectedProductId = Convert.ToInt32(row.Cells["ProductID"].Value);
                    lblSelectedProduct.Text = $"Active Tracking Product ID: {currentSelectedProductId}";

                    // Safely pulling telemetry data from the active data grid row matrix
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

            // CRITERIA RULE: Immediate numerical validation checks at the UI level
            if (!decimal.TryParse(txtMoistureLevel.Text.Trim(), out decimal moisture) || moisture < 0.00m || moisture > 100.00m)
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
                // Re-packaging tracking vectors into our safe ProductDTO construct to pass through the BLL
                ProductDTO telemetryPayload = new ProductDTO(
                    currentSelectedProductId,
                    rowCellString(dgvTelemetryGrid.CurrentRow, "ProductName"),
                    Convert.ToDecimal(dgvTelemetryGrid.CurrentRow.Cells["Price"].Value),
                    rowCellString(dgvTelemetryGrid.CurrentRow, "Description"),
                    Convert.ToInt32(dgvTelemetryGrid.CurrentRow.Cells["Stock"].Value),
                    txtSupplierInfo.Text.Trim(), // Maps directly to OriginHub field schema mapping criteria
                    rowCellString(dgvTelemetryGrid.CurrentRow, "CraftTechnique"),
                    moisture,
                    cmbProductionStage.Text.Trim()
                );

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
