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

            ApplyPremiumStyle();

            this.Load += new System.EventHandler(this.ArtisanDashboard_Load);

            if (btnCreate != null) btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            if (btnUpdate != null) btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            if (btnDelete != null) btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            if (btnClear != null) btnClear.Click += new System.EventHandler(this.btnClear_Click);
            if (dgvProducts != null) dgvProducts.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvProducts_CellClick);
        }

        private void ApplyPremiumStyle()
        {
            this.BackColor = Color.FromArgb(248, 246, 242);

            foreach (Control c in this.Controls)
            {
                if (c is TextBox txt)
                {
                    txt.Font = new Font("Segoe UI", 10.5F);
                    txt.BorderStyle = BorderStyle.FixedSingle;
                    txt.BackColor = Color.White;
                }
                else if (c is Label lbl)
                {
                    lbl.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                    lbl.ForeColor = Color.FromArgb(48, 36, 26);
                }
            }

            StyleActionButton(btnCreate, "Create", Color.FromArgb(65, 110, 75));
            StyleActionButton(btnUpdate, "Update", Color.FromArgb(145, 95, 50));
            StyleActionButton(btnDelete, "Delete", Color.FromArgb(180, 70, 70));
            StyleActionButton(btnClear, "Clear", Color.FromArgb(120, 110, 100));

            if (dgvProducts != null)
            {
                dgvProducts.BackgroundColor = Color.White;
                dgvProducts.BorderStyle = BorderStyle.None;
                dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
                dgvProducts.EnableHeadersVisualStyles = false;
                dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(44, 34, 24);
                dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                dgvProducts.ColumnHeadersHeight = 45;
                dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 238, 235);
                dgvProducts.DefaultCellStyle.SelectionForeColor = Color.Black;
                dgvProducts.DefaultCellStyle.Font = new Font("Segoe UI", 10F);
                dgvProducts.RowTemplate.Height = 40;
                dgvProducts.AllowUserToAddRows = false;
                dgvProducts.AllowUserToDeleteRows = false;
                dgvProducts.ReadOnly = true;
                dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void StyleActionButton(Button btn, string text, Color bgColor)
        {
            if (btn == null) return;
            btn.Text = text;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = bgColor;
            btn.ForeColor = Color.White;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btn.Cursor = Cursors.Hand;
            btn.Height = 38;
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
            int startY = referenceControl != null ? referenceControl.Location.Y + 40 : 220;
            int controlWidth = referenceControl != null ? referenceControl.Width : 200;

            Font labelFont = new Font("Segoe UI", 10F, FontStyle.Bold);
            Font textFont = new Font("Segoe UI", 10.5F);

            lblImageLabel = new Label();
            lblImageLabel.Text = "Product Image:";
            lblImageLabel.Location = new Point(startX - 120, startY + 3);
            lblImageLabel.AutoSize = true;
            lblImageLabel.Font = labelFont;
            lblImageLabel.ForeColor = Color.FromArgb(48, 36, 26);

            txtImagePath = new TextBox();
            txtImagePath.Location = new Point(startX, startY);
            txtImagePath.Size = new Size(controlWidth - 95, 25);
            txtImagePath.ReadOnly = true;
            txtImagePath.Font = textFont;
            txtImagePath.BorderStyle = BorderStyle.FixedSingle;

            btnBrowseImage = new Button();
            StyleActionButton(btnBrowseImage, "Browse", Color.FromArgb(120, 110, 100));
            btnBrowseImage.Location = new Point(txtImagePath.Location.X + txtImagePath.Width + 5, txtImagePath.Location.Y - 1);
            btnBrowseImage.Size = new Size(90, 27);
            btnBrowseImage.Click += new System.EventHandler(this.btnBrowseImage_Click);

            lblStoryTextLabel = new Label();
            lblStoryTextLabel.Text = "Craft Story Text:";
            lblStoryTextLabel.Location = new Point(startX - 120, startY + 40);
            lblStoryTextLabel.AutoSize = true;
            lblStoryTextLabel.Font = labelFont;
            lblStoryTextLabel.ForeColor = Color.FromArgb(48, 36, 26);

            txtStoryText = new TextBox();
            txtStoryText.Location = new Point(startX, startY + 37);
            txtStoryText.Size = new Size(controlWidth, 25);
            txtStoryText.Font = textFont;
            txtStoryText.BorderStyle = BorderStyle.FixedSingle;

            lblStoryImageLabel = new Label();
            lblStoryImageLabel.Text = "Story Image:";
            lblStoryImageLabel.Location = new Point(startX - 120, startY + 75);
            lblStoryImageLabel.AutoSize = true;
            lblStoryImageLabel.Font = labelFont;
            lblStoryImageLabel.ForeColor = Color.FromArgb(48, 36, 26);

            txtStoryImagePath = new TextBox();
            txtStoryImagePath.Location = new Point(startX, startY + 72);
            txtStoryImagePath.Size = new Size(controlWidth - 95, 25);
            txtStoryImagePath.ReadOnly = true;
            txtStoryImagePath.Font = textFont;
            txtStoryImagePath.BorderStyle = BorderStyle.FixedSingle;

            btnBrowseStoryImage = new Button();
            StyleActionButton(btnBrowseStoryImage, "Browse", Color.FromArgb(120, 110, 100));
            btnBrowseStoryImage.Location = new Point(txtStoryImagePath.Location.X + txtStoryImagePath.Width + 5, txtStoryImagePath.Location.Y - 1);
            btnBrowseStoryImage.Size = new Size(90, 27);
            btnBrowseStoryImage.Click += new System.EventHandler(this.btnBrowseStoryImage_Click);

            this.Controls.Add(lblImageLabel);
            this.Controls.Add(txtImagePath);
            this.Controls.Add(btnBrowseImage);
            this.Controls.Add(lblStoryTextLabel);
            this.Controls.Add(txtStoryText);
            this.Controls.Add(lblStoryImageLabel);
            this.Controls.Add(txtStoryImagePath);
            this.Controls.Add(btnBrowseStoryImage);

            if (btnCreate != null) btnCreate.Location = new Point(btnCreate.Location.X, txtStoryImagePath.Location.Y + 45);
            if (btnUpdate != null) btnUpdate.Location = new Point(btnUpdate.Location.X, txtStoryImagePath.Location.Y + 45);
            if (btnDelete != null) btnDelete.Location = new Point(btnDelete.Location.X, txtStoryImagePath.Location.Y + 45);
            if (btnClear != null) btnClear.Location = new Point(btnClear.Location.X, txtStoryImagePath.Location.Y + 45);
            if (dgvProducts != null) dgvProducts.Location = new Point(dgvProducts.Location.X, btnCreate != null ? btnCreate.Location.Y + 55 : startY + 120);
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

                    if (this.Controls.ContainsKey("txtProductName")) this.Controls["txtProductName"].Text = selectedRow.Cells["ProductName"].Value?.ToString();
                    if (this.Controls.ContainsKey("txtPrice")) this.Controls["txtPrice"].Text = selectedRow.Cells["Price"].Value?.ToString();
                    if (this.Controls.ContainsKey("txtDescription")) this.Controls["txtDescription"].Text = selectedRow.Cells["Description"].Value?.ToString();
                    if (this.Controls.ContainsKey("txtStock")) this.Controls["txtStock"].Text = selectedRow.Cells["Stock"].Value?.ToString();
                    if (this.Controls.ContainsKey("txtOriginHub") && selectedRow.Cells["OriginHub"] != null) this.Controls["txtOriginHub"].Text = selectedRow.Cells["OriginHub"].Value?.ToString();
                    if (this.Controls.ContainsKey("txtCraftTechnique") && dgvProducts.Columns.Contains("CraftTechnique")) this.Controls["txtCraftTechnique"].Text = selectedRow.Cells["CraftTechnique"].Value?.ToString();

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

            string productName = this.Controls.ContainsKey("txtProductName") ? this.Controls["txtProductName"].Text.Trim() : "";
            string priceStr = this.Controls.ContainsKey("txtPrice") ? this.Controls["txtPrice"].Text.Trim() : "";
            string descStr = this.Controls.ContainsKey("txtDescription") ? this.Controls["txtDescription"].Text.Trim() : "";
            string stockStr = this.Controls.ContainsKey("txtStock") ? this.Controls["txtStock"].Text.Trim() : "";
            string originHub = this.Controls.ContainsKey("txtOriginHub") ? this.Controls["txtOriginHub"].Text.Trim() : "";
            string craftTech = this.Controls.ContainsKey("txtCraftTechnique") ? this.Controls["txtCraftTechnique"].Text.Trim() : "";

            decimal.TryParse(priceStr, out priceValue);
            int.TryParse(stockStr, out stockValue);

            return new ProductDTO(
                selectedProductId,
                productName,
                priceValue,
                descStr,
                stockValue,
                originHub,
                craftTech,
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

            if (this.Controls.ContainsKey("txtProductName")) this.Controls["txtProductName"].Text = "";
            if (this.Controls.ContainsKey("txtPrice")) this.Controls["txtPrice"].Text = "";
            if (this.Controls.ContainsKey("txtDescription")) this.Controls["txtDescription"].Text = "";
            if (this.Controls.ContainsKey("txtStock")) this.Controls["txtStock"].Text = "";
            if (this.Controls.ContainsKey("txtOriginHub")) this.Controls["txtOriginHub"].Text = "";
            if (this.Controls.ContainsKey("txtCraftTechnique")) this.Controls["txtCraftTechnique"].Text = "";

            if (txtImagePath != null) txtImagePath.Clear();
            if (txtStoryText != null) txtStoryText.Clear();
            if (txtStoryImagePath != null) txtStoryImagePath.Clear();
        }
    }
}