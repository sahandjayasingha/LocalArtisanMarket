using System;
using System.Drawing;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public partial class Main : Form
    {
        private string loggedInRole;

        public Main()
        {
            InitializeComponent();
            loggedInRole = null;
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ConfigureNavigation(null);
            ShowWelcomePanel();
        }

        public void LoadChildForm(Form childForm)
        {
            if (this.panelContent == null || childForm == null) return;

            this.panelContent.Controls.Clear();

            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            childForm.FormBorderStyle = FormBorderStyle.None;

            this.panelContent.Controls.Add(childForm);
            this.panelContent.Tag = childForm;
            childForm.Show();
            childForm.BringToFront();
        }

        public void LoadUserControl(UserControl userControl)
        {
            if (this.panelContent == null || userControl == null) return;

            this.panelContent.Controls.Clear();
            userControl.Dock = DockStyle.Fill;
            this.panelContent.Controls.Add(userControl);
        }

        public void ConfigureNavigation(string currentRole)
        {
            loggedInRole = currentRole;

            if (string.IsNullOrEmpty(currentRole))
            {
                btnlogin.Text = "Login";
                btnproducts.Text = "Browse Products";
                btnproducts.Visible = true;
                btnInventory.Visible = false;
                ShowWelcomePanel();
            }
            else if (currentRole == "Artisan")
            {
                btnlogin.Text = "Logout";
                btnproducts.Text = "My Products";
                btnproducts.Visible = true;
                btnInventory.Text = "Inventory Tracker";
                btnInventory.Visible = true;
            }
            else if (currentRole == "Customer")
            {
                btnlogin.Text = "Logout";
                btnproducts.Text = "Marketplace";
                btnproducts.Visible = true;
                btnInventory.Visible = false;
            }
        }

        private void ShowWelcomePanel()
        {
            this.panelContent.Controls.Clear();

            Panel welcomePanel = new Panel();
            welcomePanel.Dock = DockStyle.Fill;
            welcomePanel.BackColor = Color.FromArgb(30, 30, 30);

            Label lblTitle = new Label();
            lblTitle.Text = "Welcome to Local Artisan Market";
            lblTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblTitle.ForeColor = Color.White;
            lblTitle.Location = new Point(50, 50);
            lblTitle.AutoSize = true;

            Label lblDesc = new Label();
            lblDesc.Text = "Empowering traditional Sri Lankan craftspeople from regions like Molagoda (Pottery) and Radawadunna (Cane Crafts).\n\nBrowse beautiful authentic products or login to manage your workspace.";
            lblDesc.Font = new Font("Segoe UI", 12, FontStyle.Regular);
            lblDesc.ForeColor = Color.LightGray;
            lblDesc.Location = new Point(50, 110);
            lblDesc.Size = new Size(600, 150);

            welcomePanel.Controls.Add(lblTitle);
            welcomePanel.Controls.Add(lblDesc);

            this.panelContent.Controls.Add(welcomePanel);
        }

        private void btnlogin_Click_1(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            if (button.Text == "Logout")
            {
                ConfigureNavigation(null);
                MessageBox.Show("Logged out successfully!", "Session Ended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LoginForm login = new LoginForm(this);
            LoadChildForm(login);
        }

        private void btnproducts_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(loggedInRole))
            {
                CustomerDashboard guestDashboard = new CustomerDashboard();
                LoadUserControl(guestDashboard);
            }
            else if (loggedInRole == "Artisan")
            {
                ArtisanDashboard artisanDashboard = new ArtisanDashboard();
                LoadUserControl(artisanDashboard);
            }
            else if (loggedInRole == "Customer")
            {
                CustomerDashboard customerDashboard = new CustomerDashboard();
                LoadUserControl(customerDashboard);
            }
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            ShowWelcomePanel();
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            if (loggedInRole == "Artisan")
            {
                MessageBox.Show("Loading Inventory Tracking Module...", "Inventory Tracker", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void panelSideMenu_Paint(object sender, PaintEventArgs e) { }
        private void panelContent_Paint(object sender, PaintEventArgs e) { }
    }
}