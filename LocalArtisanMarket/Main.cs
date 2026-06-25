using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            // Keep this empty or put initialization logic here
        }

        private void panelSideMenu_Paint(object sender, PaintEventArgs e)
        {
            // Handles any custom painting for your side panel
        }

        /// <summary>
        /// Global Method to load child forms into the main content panel
        /// </summary>
        public void LoadChildForm(Form childForm)
        {
            // 1. Prevent crashes if the panel or form doesn't exist
            if (this.panelContent == null || childForm == null) return;

            // 2. Clear out the previous form inside the panel
            if (this.panelContent.Controls.Count > 0)
            {
                this.panelContent.Controls.RemoveAt(0);
            }

            // 3. Setup the new form to fit perfectly inside the panel
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            childForm.FormBorderStyle = FormBorderStyle.None; // Removes outer borders

            // 4. Add it to the panel display
            this.panelContent.Controls.Add(childForm);
            this.panelContent.Tag = childForm;
            childForm.Show();
            childForm.BringToFront(); // Ensures it sits on top of backgrounds
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // When Costa's LoginForm is ready, you will uncomment the line below:
            // LoadChildForm(new LoginForm());
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}