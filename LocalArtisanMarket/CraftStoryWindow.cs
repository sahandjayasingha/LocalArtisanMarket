using System;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public partial class CraftStoryWindow : Form
    {

        public CraftStoryWindow(string productName, string origin, string technique, string historyText)
        {
            InitializeComponent();


            lblProductName.Text = productName;
            lblGeographicOrigin.Text = "Authentic Origin: " + origin;
            lblTechniqueUsed.Text = "Traditional Method: " + technique;
            txtStoryDescription.Text = historyText;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}