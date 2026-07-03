using System;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            ConfigureNavigation(null);
        }

        public void LoadChildForm(Form childForm)
        {
            if (this.panelContent == null || childForm == null) return;

            if (this.panelContent.Controls.Count > 0)
            {
                this.panelContent.Controls.RemoveAt(0);
            }

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
            if (string.IsNullOrEmpty(currentRole))
            {
                btnlogin.Text = "Login";
                btnproducts.Visible = true;
                btnInventory.Visible = false;
            }
            else if (currentRole == "Artisan")
            {
                btnlogin.Text = "Logout";
                btnproducts.Visible = true;
                btnInventory.Visible = true;
            }
            else if (currentRole == "Customer")
            {
                btnlogin.Text = "Logout";
                btnproducts.Visible = true;
                btnInventory.Visible = false;
            }
        }

        private void btnlogin_Click_1(object sender, EventArgs e)
        {
            if (btnlogin.Text == "Logout")
            {
                this.panelContent.Controls.Clear();
                ConfigureNavigation(null);
                MessageBox.Show("Logged out successfully!", "Session Ended", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LoginForm login = new LoginForm(this);
            LoadChildForm(login);
        }

        
        private void btnhome_Click(object sender, EventArgs e)
        {
            
            this.panelContent.Controls.Clear();

           
        }

        private void panelSideMenu_Paint(object sender, PaintEventArgs e) { }
        private void panelContent_Paint(object sender, PaintEventArgs e) { }
    }
}