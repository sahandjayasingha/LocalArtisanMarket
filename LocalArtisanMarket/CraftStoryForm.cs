using System;
using System.Drawing;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public class CraftStoryForm : Form
    {
        private Label lblCraftTitle;
        private Label lblRegion;
        private Label lblStoryText;
        private Panel pnlStoryContainer;
        private PictureBox pbCraftImage;
        private Button btnClose;
        private Panel pnlBackground;

        public CraftStoryForm(ProductDTO product)
        {
            InitializeComponent();
            SetupCustomUI(product);
        }

        private void InitializeComponent()
        {
            this.Size = new Size(500, 600);
            this.Text = "Authentic Heritage Story";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void SetupCustomUI(ProductDTO product)
        {
            pnlBackground = new Panel();
            pnlBackground.Dock = DockStyle.Fill;
            pnlBackground.BackColor = Color.FromArgb(248, 246, 242);
            this.Controls.Add(pnlBackground);

            pbCraftImage = new PictureBox();
            pbCraftImage.Location = new Point(30, 30);
            pbCraftImage.Size = new Size(425, 220);
            pbCraftImage.SizeMode = PictureBoxSizeMode.Zoom;
            pbCraftImage.BackColor = Color.FromArgb(240, 238, 235);
            pbCraftImage.BorderStyle = BorderStyle.FixedSingle;
            pnlBackground.Controls.Add(pbCraftImage);

            string imageToLoad = !string.IsNullOrWhiteSpace(product.StoryImagePath) ? product.StoryImagePath : product.ImagePath;

            if (!string.IsNullOrWhiteSpace(imageToLoad))
            {
                string fullPath = System.IO.Path.Combine(Application.StartupPath, imageToLoad);
                if (System.IO.File.Exists(fullPath))
                {
                    pbCraftImage.Image = Image.FromFile(fullPath);
                    pbCraftImage.BorderStyle = BorderStyle.None;
                }
            }

            lblCraftTitle = new Label();
            lblCraftTitle.Text = product.ProductName;
            lblCraftTitle.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            lblCraftTitle.Location = new Point(25, 270);
            lblCraftTitle.Size = new Size(430, 35);
            lblCraftTitle.ForeColor = Color.FromArgb(48, 36, 26);
            pnlBackground.Controls.Add(lblCraftTitle);

            lblRegion = new Label();
            lblRegion.Text = "✦ Heritage Origin: " + product.OriginHub;
            lblRegion.Font = new Font("Segoe UI", 10.5F, FontStyle.Italic | FontStyle.Bold);
            lblRegion.Location = new Point(28, 305);
            lblRegion.Size = new Size(430, 25);
            lblRegion.ForeColor = Color.FromArgb(145, 95, 50);
            pnlBackground.Controls.Add(lblRegion);

            pnlStoryContainer = new Panel();
            pnlStoryContainer.Location = new Point(30, 340);
            pnlStoryContainer.Size = new Size(425, 130);
            pnlStoryContainer.AutoScroll = true;
            pnlStoryContainer.BackColor = Color.FromArgb(248, 246, 242);
            pnlBackground.Controls.Add(pnlStoryContainer);

            lblStoryText = new Label();
            lblStoryText.Font = new Font("Segoe UI", 10.5F, FontStyle.Regular);
            lblStoryText.ForeColor = Color.FromArgb(80, 70, 60);
            lblStoryText.Location = new Point(0, 0);
            lblStoryText.MaximumSize = new Size(405, 0);
            lblStoryText.AutoSize = true;
            pnlStoryContainer.Controls.Add(lblStoryText);

            if (!string.IsNullOrWhiteSpace(product.StoryText))
            {
                lblStoryText.Text = product.StoryText;
            }
            else
            {
                string technique = product.CraftTechnique.ToLower();
                if (technique.Contains("pottery") || technique.Contains("clay"))
                {
                    lblStoryText.Text = "Crafted using the generational Mud-Spreading Mottling technique. Shaped entirely by hand on a traditional potter's wheel, sun-dried, and kiln-baked at precise temperatures to guarantee authentic Sri Lankan durability. Every curve and texture reflects hours of dedicated craftsmanship.";
                }
                else if (technique.Contains("weaving") || technique.Contains("cane"))
                {
                    lblStoryText.Text = "Woven using the intricate Waling Weaving method. The indigenous cane is carefully selected, boiled to prevent splitting, shaved into fine splints, and dyed with natural organic extracts before being intricately interlaced by our master artisans.";
                }
                else
                {
                    lblStoryText.Text = $"This masterpiece represents the fine art of {product.CraftTechnique}. Passed down through generations, every curve and texture reflects hours of dedicated craftsmanship and cultural heritage preservation.";
                }
            }

            btnClose = new Button();
            btnClose.Text = "Return to Marketplace";
            btnClose.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            btnClose.BackColor = Color.FromArgb(65, 110, 75);
            btnClose.ForeColor = Color.White;
            btnClose.Location = new Point(30, 490);
            btnClose.Size = new Size(425, 45);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Cursor = Cursors.Hand;
            btnClose.Click += (s, e) => this.Close();
            pnlBackground.Controls.Add(btnClose);
        }
    }
}