using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public partial class MyOrdersPanel : UserControl
    {
        public MyOrdersPanel()
        {
            InitializeComponent();
            LoadOrders();
        }

        public void LoadOrders()
        {
            try
            {

                DataTable ordersData = DatabaseHelper.GetArtisanOrders();


                dgvOrders.DataSource = null;
                dgvOrders.Columns.Clear();

          
                dgvOrders.DataSource = ordersData;

       
                dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvOrders.RowHeadersVisible = false;
                dgvOrders.AllowUserToAddRows = false;
                dgvOrders.ReadOnly = true;

              
                DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
                deleteBtn.Name = "DeleteBtn";
                deleteBtn.HeaderText = "Action";
                deleteBtn.Text = "Delete";
                deleteBtn.UseColumnTextForButtonValue = true;
                deleteBtn.FlatStyle = FlatStyle.Flat;
                deleteBtn.DefaultCellStyle.ForeColor = Color.Red; 

                dgvOrders.Columns.Add(deleteBtn);

            
                dgvOrders.CellContentClick -= DgvOrders_CellContentClick;
                dgvOrders.CellContentClick += DgvOrders_CellContentClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading orders: " + ex.Message);
            }
        }

        private void DgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0 && dgvOrders.Columns[e.ColumnIndex].Name == "DeleteBtn")
            {
                
                string token = dgvOrders.Rows[e.RowIndex].Cells["Token Number"].Value.ToString();

              
                DialogResult dialogResult = MessageBox.Show(
                    $"Are you sure you want to permanently delete order {token}?",
                    "Confirm Deletion",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (dialogResult == DialogResult.Yes)
                {
                  
                    bool success = DatabaseHelper.DeleteOrder(token);

                    if (success)
                    {
                        
                        LoadOrders();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete the order. It may have already been removed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}