using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            ApplyGlobalFormStyles();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            DatabaseHelper.InitializeDatabase();
            StyleNavigationButtons();
            ConfigureNavigation(null);
            ShowWelcomePanel();
        }

        private void ApplyGlobalFormStyles()
        {
            this.Size = new Size(1280, 760);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Global Artisan Marketplace - Premium Core Network";

            if (panelSideMenu != null)
            {
                panelSideMenu.BackColor = Color.FromArgb(28, 26, 24);
                panelSideMenu.Width = 260;
                panelSideMenu.Padding = new Padding(10, 20, 10, 20);
            }

            if (panelContent != null)
            {
                panelContent.BackColor = Color.FromArgb(248, 246, 242);
            }
        }

        private void StyleNavigationButtons()
        {
            if (panelSideMenu == null) return;

            foreach (Control c in panelSideMenu.Controls)
            {
                if (c is Button btn)
                {
                    btn.Size = new Size(240, 48);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(52, 48, 44);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(20, 18, 16);
                    btn.BackColor = Color.FromArgb(38, 35, 32);
                    btn.ForeColor = Color.FromArgb(255, 255, 255);
                    btn.Font = new Font("Segoe UI", 10.5F, FontStyle.Bold);
                    btn.Cursor = Cursors.Hand;
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    btn.Padding = new Padding(20, 0, 0, 0);
                    btn.Margin = new Padding(0, 0, 0, 8);
                }
            }
        }

        private void SetActiveButton(Button activeBtn)
        {
            if (panelSideMenu == null) return;

            foreach (Control c in panelSideMenu.Controls)
            {
                if (c is Button btn)
                {
                    btn.BackColor = Color.FromArgb(38, 35, 32);
                    btn.ForeColor = Color.FromArgb(255, 255, 255);
                }
            }

            if (activeBtn != null)
            {
                activeBtn.BackColor = Color.FromArgb(145, 95, 50);
                activeBtn.ForeColor = Color.White;
            }
        }

        private void UpdateSidebarLayout()
        {
            if (panelSideMenu == null) return;

            Button homeBtn = null;
            foreach (Control c in panelSideMenu.Controls)
            {
                if (c is Button btn && (btn.Name == "btnHome" || btn.Text == "Home"))
                {
                    homeBtn = btn;
                    break;
                }
            }

            List<Button> orderedButtons = new List<Button>();

            if (homeBtn != null) orderedButtons.Add(homeBtn);
            if (btnproducts != null) orderedButtons.Add(btnproducts);
            if (btnInventory != null) orderedButtons.Add(btnInventory);
            if (btnMyOrders != null) orderedButtons.Add(btnMyOrders);

            foreach (Control c in panelSideMenu.Controls)
            {
                if (c is Button btn && !orderedButtons.Contains(btn) && btn != btnlogin)
                {
                    orderedButtons.Add(btn);
                }
            }

            if (btnlogin != null) orderedButtons.Add(btnlogin);

            int currentY = 30;
            foreach (Button btn in orderedButtons)
            {
                if (btn != null && btn.Visible)
                {
                    btn.Location = new Point(10, currentY);
                    currentY += 56;
                }
            }
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
            userControl.BackColor = Color.FromArgb(248, 246, 242);
            this.panelContent.Controls.Add(userControl);
        }

        public void ConfigureNavigation(string currentRole)
        {
            loggedInRole = currentRole;

            if (string.IsNullOrEmpty(currentRole))
            {
                if (btnlogin != null) btnlogin.Text = "Login";
                if (btnproducts != null) { btnproducts.Text = "Browse Products"; btnproducts.Visible = true; }
                if (btnInventory != null) btnInventory.Visible = false;
                if (btnMyOrders != null) btnMyOrders.Visible = false;

                ShowWelcomePanel();
            }
            else if (currentRole == "Artisan")
            {
                if (btnlogin != null) btnlogin.Text = "Logout";
                if (btnproducts != null) { btnproducts.Text = "My Products"; btnproducts.Visible = true; }
                if (btnInventory != null) { btnInventory.Text = "Inventory Tracker"; btnInventory.Visible = true; }
                if (btnMyOrders != null) { btnMyOrders.Visible = true; }
            }
            else if (currentRole == "Customer")
            {
                if (btnlogin != null) btnlogin.Text = "Logout";
                if (btnproducts != null) { btnproducts.Text = "Marketplace"; btnproducts.Visible = true; }
                if (btnInventory != null) btnInventory.Visible = false;
                if (btnMyOrders != null) btnMyOrders.Visible = false;
            }

            UpdateSidebarLayout();
        }

        private void ShowWelcomePanel()
        {
            if (panelSideMenu != null)
            {
                foreach (Control c in panelSideMenu.Controls)
                {
                    if (c is Button btn && (btn.Name == "btnHome" || btn.Text == "Home"))
                    {
                        SetActiveButton(btn);
                        break;
                    }
                }
            }

            if (this.panelContent == null) return;

            this.panelContent.Controls.Clear();

            Panel welcomePanel = new Panel();
            welcomePanel.Dock = DockStyle.Fill;
            welcomePanel.BackColor = Color.FromArgb(248, 246, 242);

            Panel pnlHero = new Panel();
            pnlHero.Location = new Point(40, 40);
            pnlHero.Size = new Size(this.panelContent.Width - 80, 200);
            pnlHero.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pnlHero.BackColor = Color.FromArgb(44, 34, 24);
            welcomePanel.Controls.Add(pnlHero);

            Label lblTitle = new Label();
            lblTitle.Text = "Global Handcrafted Heritage Network";
            lblTitle.Font = new Font("Segoe UI", 26, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(245, 235, 220);
            lblTitle.Location = new Point(35, 40);
            lblTitle.AutoSize = true;
            pnlHero.Controls.Add(lblTitle);

            Label lblDesc = new Label();
            lblDesc.Text = "Welcome to a borderless marketplace engineered for elite traditional craft masters. We eliminate conventional intermediaries, enabling direct sovereign authentication, high-tier inventory synchronization, and verified cultural value tracing.";
            lblDesc.Font = new Font("Segoe UI", 11.5F, FontStyle.Regular);
            lblDesc.ForeColor = Color.FromArgb(215, 200, 185);
            lblDesc.Location = new Point(37, 95);
            lblDesc.Size = new Size(pnlHero.Width - 70, 75);
            pnlHero.Controls.Add(lblDesc);

            Label lblStatsTitle = new Label();
            lblStatsTitle.Text = "Live Operational Framework Infrastructure";
            lblStatsTitle.Font = new Font("Segoe UI", 15, FontStyle.Bold);
            lblStatsTitle.ForeColor = Color.FromArgb(48, 36, 26);
            lblStatsTitle.Location = new Point(40, 275);
            lblStatsTitle.AutoSize = true;
            welcomePanel.Controls.Add(lblStatsTitle);

            TableLayoutPanel tlpStats = new TableLayoutPanel();
            tlpStats.Location = new Point(40, 315);
            tlpStats.Size = new Size(this.panelContent.Width - 80, 150);
            tlpStats.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tlpStats.ColumnCount = 3;
            tlpStats.RowCount = 1;
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            tlpStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            welcomePanel.Controls.Add(tlpStats);

            Action<int, string, string, Color> AddStatCard = (columnIndex, title, value, primaryColor) =>
            {
                Panel pnlCard = new Panel();
                pnlCard.Dock = DockStyle.Fill;
                pnlCard.Margin = new Padding(12);
                pnlCard.BackColor = Color.White;
                pnlCard.Paint += (s, ev) =>
                {
                    using (Pen p = new Pen(Color.FromArgb(225, 215, 200), 1))
                    {
                        ev.Graphics.DrawRectangle(p, 0, 0, pnlCard.Width - 1, pnlCard.Height - 1);
                    }
                    using (SolidBrush b = new SolidBrush(primaryColor))
                    {
                        ev.Graphics.FillRectangle(b, 0, 0, 6, pnlCard.Height);
                    }
                };

                Label lblCardTitle = new Label { Text = title, Font = new Font("Segoe UI", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(120, 110, 100), Location = new Point(22, 25), AutoSize = true };
                Label lblCardValue = new Label { Text = value, Font = new Font("Segoe UI", 14F, FontStyle.Bold), ForeColor = Color.FromArgb(50, 40, 30), Location = new Point(22, 65), AutoSize = true };

                pnlCard.Controls.Add(lblCardTitle);
                pnlCard.Controls.Add(lblCardValue);
                tlpStats.Controls.Add(pnlCard, columnIndex, 0);
            };

            AddStatCard(0, "Enterprise Hub Matrices", "25+ Districts Standardized", Color.FromArgb(145, 95, 50));
            AddStatCard(1, "Verified Sovereign Artisans", "1,200+ Profiles Active", Color.FromArgb(65, 110, 75));
            AddStatCard(2, "Monitored Material Catalogs", "4,500+ Elements Registered", Color.FromArgb(100, 80, 65));

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

            SetActiveButton(button);
            LoginForm login = new LoginForm(this);
            LoadChildForm(login);
        }

        private void btnproducts_Click(object sender, EventArgs e)
        {
            SetActiveButton(sender as Button);

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
            SetActiveButton(sender as Button);
            MaterialTrackingPanel trackingPanel = new MaterialTrackingPanel();
            LoadUserControl(trackingPanel);
        }

        private void panelSideMenu_Paint(object sender, PaintEventArgs e) { }
        private void panelContent_Paint(object sender, PaintEventArgs e) { }

        private void btnMyOrders_Click(object sender, EventArgs e)
        {
            SetActiveButton(sender as Button);
            MyOrdersPanel ordersView = new MyOrdersPanel();
            LoadUserControl(ordersView);
        }
    }
}