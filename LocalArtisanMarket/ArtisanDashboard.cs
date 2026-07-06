using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public partial class ArtisanDashboard : UserControl
    {
        private int selectedProductId = -1;
        private readonly ProductBusinessLogic _bll;

        private Label lblImageLabel;
        private TextBox txtImagePath;
        private Button btnBrowseImage;

        private Label lblStoryTextLabel;
        private TextBox txtStoryText;
        private Label lblStoryImageLabel;
        private TextBox txtStoryImagePath;
        private Button btnBrowseStoryImage;

        public static string ConnectionString = @"Data Source=(Localdb)\MSSQLLocalDB;Initial Catalog=LocalArtisanMarketDb;Integrated Security=True;Connect Timeout=30;";

        public ArtisanDashboard()
        {
            InitializeComponent();
            _bll = new ProductBusinessLogic();

            this.Load += new System.EventHandler(this.ArtisanDashboard_Load);

            if (btnCreate != null) btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            if (btnUpdate != null) btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            if (btnDelete != null) btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            if (btnClear != null) btnClear.Click += new System.EventHandler(this.btnClear_Click);
            if (dgvProducts != null) dgvProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellClick);
        }

        private void ArtisanDashboard_Load(object sender, EventArgs e)
        {
            CreateImageControlsRuntime();
            RefreshGrid();
        }

        private void CreateImageControlsRuntime()
        {
            if (txtImagePath != null) return;

            Control referenceControl = null;
            if (this.Controls.ContainsKey("txtCraftTechnique")) referenceControl = this.Controls["txtCraftTechnique"];
            else if (this.Controls.ContainsKey("txtCraft")) referenceControl = this.Controls["txtCraft"];

            int startX = referenceControl != null ? referenceControl.Location.X : 214;
            int startY = referenceControl != null ? referenceControl.Location.Y + 35 : 220;
            int controlWidth = referenceControl != null ? referenceControl.Width : 200;
            Font controlFont = referenceControl != null ? referenceControl.Font : this.Font;

            lblImageLabel = new Label();
            lblImageLabel.Text = "Product Image:";
            lblImageLabel.Location = new Point(startX - 110, startY + 3);
            lblImageLabel.AutoSize = true;
            lblImageLabel.Font = controlFont;

            txtImagePath = new TextBox();
            txtImagePath.Location = new Point(startX, startY);
            txtImagePath.Size = new Size(controlWidth - 95, 20);
            txtImagePath.ReadOnly = true;

            btnBrowseImage = new Button();
            btnBrowseImage.Text = "Browse";
            btnBrowseImage.Location = new Point(txtImagePath.Location.X + txtImagePath.Width + 5, txtImagePath.Location.Y - 2);
            btnBrowseImage.Size = new Size(90, 24);
            btnBrowseImage.Click += new System.EventHandler(this.btnBrowseImage_Click);

            lblStoryTextLabel = new Label();
            lblStoryTextLabel.Text = "Craft Story Text:";
            lblStoryTextLabel.Location = new Point(startX - 110, startY + 35);
            lblStoryTextLabel.AutoSize = true;
            lblStoryTextLabel.Font = controlFont;

            txtStoryText = new TextBox();
            txtStoryText.Location = new Point(startX, startY + 32);
            txtStoryText.Size = new Size(controlWidth, 20);

            lblStoryImageLabel = new Label();
            lblStoryImageLabel.Text = "Story Image:";
            lblStoryImageLabel.Location = new Point(startX - 110, startY + 65);
            lblStoryImageLabel.AutoSize = true;
            lblStoryImageLabel.Font = controlFont;

            txtStoryImagePath = new TextBox();
            txtStoryImagePath.Location = new Point(startX, startY + 62);
            txtStoryImagePath.Size = new Size(controlWidth - 95, 20);
            txtStoryImagePath.ReadOnly = true;

            btnBrowseStoryImage = new Button();
            btnBrowseStoryImage.Text = "Browse";
            btnBrowseStoryImage.Location = new Point(txtStoryImagePath.Location.X + txtStoryImagePath.Width + 5, txtStoryImagePath.Location.Y - 2);
            btnBrowseStoryImage.Size = new Size(90, 24);
            btnBrowseStoryImage.Click += new System.EventHandler(this.btnBrowseStoryImage_Click);

            this.Controls.Add(lblImageLabel);
            this.Controls.Add(txtImagePath);
            this.Controls.Add(btnBrowseImage);
            this.Controls.Add(lblStoryTextLabel);
            this.Controls.Add(txtStoryText);
            this.Controls.Add(lblStoryImageLabel);
            this.Controls.Add(txtStoryImagePath);
            this.Controls.Add(btnBrowseStoryImage);

            if (btnCreate != null) btnCreate.Location = new Point(btnCreate.Location.X, txtStoryImagePath.Location.Y + 40);
            if (btnUpdate != null) btnUpdate.Location = new Point(btnUpdate.Location.X, txtStoryImagePath.Location.Y + 40);
            if (btnDelete != null) btnDelete.Location = new Point(btnDelete.Location.X, txtStoryImagePath.Location.Y + 40);
            if (btnClear != null) btnClear.Location = new Point(btnClear.Location.X, txtStoryImagePath.Location.Y + 40);
            if (dgvProducts != null) dgvProducts.Location = new Point(dgvProducts.Location.X, btnCreate.Location.Y + 45);
        }

        private void RefreshGrid()
        {
            try
            {
                dgvProducts.DataSource = null;
                dgvProducts.DataSource = _bll.GetCatalog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Application Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string imagesDir = System.IO.Path.Combine(Application.StartupPath, "ProductImages");
                        if (!System.IO.Directory.Exists(imagesDir))
                        {
                            System.IO.Directory.CreateDirectory(imagesDir);
                        }

                        string extension = System.IO.Path.GetExtension(ofd.FileName);
                        string newFileName = Guid.NewGuid().ToString() + extension;
                        string destPath = System.IO.Path.Combine(imagesDir, newFileName);

                        System.IO.File.Copy(ofd.FileName, destPath, true);

                        txtImagePath.Text = System.IO.Path.Combine("ProductImages", newFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Image processing error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnBrowseStoryImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files(*.jpg; *.jpeg; *.png; *.bmp)|*.jpg; *.jpeg; *.png; *.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string imagesDir = System.IO.Path.Combine(Application.StartupPath, "ProductImages");
                        if (!System.IO.Directory.Exists(imagesDir)) System.IO.Directory.CreateDirectory(imagesDir);

                        string extension = System.IO.Path.GetExtension(ofd.FileName);
                        string newFileName = "story_" + Guid.NewGuid().ToString() + extension;
                        string destPath = System.IO.Path.Combine(imagesDir, newFileName);

                        System.IO.File.Copy(ofd.FileName, destPath, true);
                        txtStoryImagePath.Text = System.IO.Path.Combine("ProductImages", newFileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Story Image processing error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
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

                    if (selectedRow.Cells["OriginHub"] != null) txtOriginHub.Text = selectedRow.Cells["OriginHub"].Value?.ToString();

                    if (dgvProducts.Columns.Contains("CraftTechnique"))
                        txtCraftTechnique.Text = selectedRow.Cells["CraftTechnique"].Value?.ToString();

                    txtImagePath.Text = dgvProducts.Columns.Contains("ImagePath") && selectedRow.Cells["ImagePath"].Value != null ? selectedRow.Cells["ImagePath"].Value.ToString() : "";
                    txtStoryText.Text = dgvProducts.Columns.Contains("StoryText") && selectedRow.Cells["StoryText"].Value != null ? selectedRow.Cells["StoryText"].Value.ToString() : "";
                    txtStoryImagePath.Text = dgvProducts.Columns.Contains("StoryImagePath") && selectedRow.Cells["StoryImagePath"].Value != null ? selectedRow.Cells["StoryImagePath"].Value.ToString() : "";
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

            return new ProductDTO(
                selectedProductId,
                txtProductName.Text.Trim(),
                priceValue,
                txtDescription.Text.Trim(),
                stockValue,
                txtOriginHub.Text.Trim(),
                (txtCraftTechnique != null ? txtCraftTechnique.Text.Trim() : ""),
                0.0m,
                "Raw",
                (txtImagePath != null ? txtImagePath.Text.Trim() : ""),
                (txtStoryText != null ? txtStoryText.Text.Trim() : ""),
                (txtStoryImagePath != null ? txtStoryImagePath.Text.Trim() : "")
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
            if (txtCraftTechnique != null) txtCraftTechnique.Clear();
            if (txtImagePath != null) txtImagePath.Clear();
            if (txtStoryText != null) txtStoryText.Clear();
            if (txtStoryImagePath != null) txtStoryImagePath.Clear();
        }
    }
}