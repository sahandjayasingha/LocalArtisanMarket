using System;
using System.Drawing;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public class CraftStoryForm : Form
    {
        private Label lblCraftTitle;
        private Label lblRegion;
        private TextBox txtTechniqueStory;
        private PictureBox pbCraftImage;
        private Button btnClose;

        public CraftStoryForm(ProductDTO product)
        {
            InitializeComponent();
            SetupCustomUI(product);
        }

        private void InitializeComponent()
        {
            this.Size = new Size(450, 500);
            this.Text = "The Craft Story";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(245, 242, 235);
        }

        private void SetupCustomUI(ProductDTO product)
        {
            pbCraftImage = new PictureBox();
            pbCraftImage.Location = new Point(25, 25);
            pbCraftImage.Size = new Size(380, 180);
            pbCraftImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbCraftImage.BackColor = Color.LightGray;

            string imageToLoad = !string.IsNullOrWhiteSpace(product.StoryImagePath) ? product.StoryImagePath : product.ImagePath;

            if (!string.IsNullOrWhiteSpace(imageToLoad))
            {
                string fullPath = System.IO.Path.Combine(Application.StartupPath, imageToLoad);
                if (System.IO.File.Exists(fullPath))
                {
                    pbCraftImage.Image = Image.FromFile(fullPath);
                }
            }

            lblCraftTitle = new Label();
            lblCraftTitle.Text = product.ProductName;
            lblCraftTitle.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblCraftTitle.Location = new Point(25, 220);
            lblCraftTitle.Size = new Size(380, 30);
            lblCraftTitle.ForeColor = Color.FromArgb(60, 42, 25);

            lblRegion = new Label();
            lblRegion.Text = "Heritage Origin: " + product.OriginHub;
            lblRegion.Font = new Font("Segoe UI", 10, FontStyle.Italic);
            lblRegion.Location = new Point(25, 255);
            lblRegion.Size = new Size(380, 20);
            lblRegion.ForeColor = Color.DarkRed;

            txtTechniqueStory = new TextBox();
            txtTechniqueStory.Multiline = true;
            txtTechniqueStory.ReadOnly = true;
            txtTechniqueStory.TabStop = false;
            txtTechniqueStory.BackColor = Color.FromArgb(252, 250, 245);
            txtTechniqueStory.Font = new Font("Segoe UI", 10);
            txtTechniqueStory.Location = new Point(25, 285);
            txtTechniqueStory.Size = new Size(380, 120);
            txtTechniqueStory.ScrollBars = ScrollBars.Vertical;

            if (!string.IsNullOrWhiteSpace(product.StoryText))
            {
                txtTechniqueStory.Text = product.StoryText;
            }
            else
            {
                string technique = product.CraftTechnique.ToLower();
                if (technique.Contains("pottery") || technique.Contains("clay"))
                {
                    txtTechniqueStory.Text = "Crafted using the generational Mud-Spreading Mottling technique. Shaped entirely by hand on a traditional potter's wheel, sun-dried, and kiln-baked at precise temperatures to guarantee authentic Sri Lankan durability.";
                }
                else if (technique.Contains("weaving") || technique.Contains("cane"))
                {
                    txtTechniqueStory.Text = "Woven using the intricate Waling Weaving method. The indigenous cane is carefully selected, boiled to prevent splitting, shaved into fine splints, and dyed with natural organic extracts before being intricately interlaced.";
                }
                else
                {
                    txtTechniqueStory.Text = $"This masterpiece represents the fine art of {product.CraftTechnique}. Passed down through generations, every curve and texture reflects hours of dedicated craftsmanship and cultural heritage preservation.";
                }
            }

            btnClose = new Button();
            btnClose.Text = "Close Story";
            btnClose.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnClose.BackColor = Color.FromArgb(120, 80, 45);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(160, 420);
            btnClose.Size = new Size(120, 30);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(pbCraftImage);
            this.Controls.Add(lblCraftTitle);
            this.Controls.Add(lblRegion);
            this.Controls.Add(txtTechniqueStory);
            this.Controls.Add(btnClose);
        }
    }
}