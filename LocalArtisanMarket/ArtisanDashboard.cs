using System;
using System.Data;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public partial class ArtisanDashboard : UserControl
    {
        private int selectedProductId = -1;
        private readonly ProductBusinessLogic _bll;

        // Keeping your connection variable public as required by the other layers
        public static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=LocalArtisanMarketDb;Integrated Security=True;";

        public ArtisanDashboard()
        {
            InitializeComponent();
            _bll = new ProductBusinessLogic();
        }

     


        private void ArtisanDashboard_Load(object sender, EventArgs e)
        {
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            try
            {
                dgvProducts.DataSource = _bll.GetCatalog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                ProductDTO newProduct = GatherInputData();
                _bll.ProcessProductCreation(newProduct);

                MessageBox.Show("Inventory registration tracking processing complete.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validation / System Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedProductId == -1)
            {
                MessageBox.Show("Operational constraint: Active data row grid target record must be explicitly mapped first.", "Action Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ProductDTO targetProduct = GatherInputData();
                

                _bll.ProcessProductUpdate(targetProduct);

                MessageBox.Show("Inventory tracking state configuration updates processed accurately.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Validation / System Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedProductId == -1)
            {
                MessageBox.Show("Operational constraint: Targeted entry select definition missing parameter tracking coordinates.", "Action Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmation = MessageBox.Show("Are you sure you want to proceed with permanent store stock entity removal adjustments?", "Confirm State Adjustments", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmation != DialogResult.Yes) return;

            try
            {
                _bll.ProcessProductDeletion(selectedProductId);

                MessageBox.Show("Store item classification structural parameters cleared from registry schema.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearInputs();
                RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Encapsulated Entity Mapping: Bypasses column indexing to protect cell mutations
        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvProducts.Rows[e.RowIndex];

                try
                {
                    selectedProductId = Convert.ToInt32(selectedRow.Cells["ProductID"].Value);
                    txtProductName.Text = selectedRow.Cells["ProductName"].Value?.ToString();
                    txtPrice.Text = selectedRow.Cells["Price"].Value?.ToString();
                    txtDescription.Text = selectedRow.Cells["Description"].Value?.ToString();
                    txtStock.Text = selectedRow.Cells["Stock"].Value?.ToString();
                    txtOriginHub.Text = selectedRow.Cells["OriginHub"].Value?.ToString();
                    txtCraftTechnique.Text = selectedRow.Cells["CraftTechnique"].Value?.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Entity translation mapping runtime mismatch conflict identified.", "UI Data Parsing Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private ProductDTO GatherInputData()
        {
            decimal priceValue = 0;
            int stockValue = 0;

            decimal.TryParse(txtPrice.Text.Trim(), out priceValue);
            int.TryParse(txtStock.Text.Trim(), out stockValue);

            // Added default telemetry placeholders (0.0m and "Raw") until Insara links her module controls
            return new ProductDTO(
                selectedProductId,
                txtProductName.Text.Trim(),
                priceValue,
                txtDescription.Text.Trim(),
                stockValue,
                txtOriginHub.Text.Trim(),
                txtCraftTechnique.Text.Trim(),
                0.0m,    // Default Moisture Metric telemetry vector parameter
                "Raw"    // Default Processing Stage tracking status payload vector
            );
        }



        private void btnClear_Click(object sender, EventArgs e) => ClearInputs();

        private void ClearInputs()
        {
            selectedProductId = -1;
            txtProductName.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            txtStock.Clear();
            txtOriginHub.Clear();
            txtCraftTechnique.Clear();
        }
    }
}
