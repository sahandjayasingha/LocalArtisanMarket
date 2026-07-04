using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public partial class LoginForm : Form
    {
        private Main _mainForm;

        public static string CurrentUserID { get; private set; } = null;
        public static string CurrentUserRole { get; private set; } = null;

        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(Main mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
        }

        private void btnLoginSubmit_Click(object sender, EventArgs e)
        {
            string email = txtLoginEmail.Text.Trim();
            string password = txtLoginPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both email and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = "SELECT UserID, Role FROM Users WHERE Email = @Email AND PasswordHash = @Password";

                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Email", email),
                    new SqlParameter("@Password", password)
                };

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dt != null && dt.Rows.Count > 0)
                {
                    CurrentUserID = dt.Rows[0]["UserID"].ToString();
                    CurrentUserRole = dt.Rows[0]["Role"].ToString();

                    MessageBox.Show("Login Successful! Welcome back.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    if (_mainForm != null)
                    {
                        _mainForm.ConfigureNavigation(CurrentUserRole);
                    }

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Email or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e) { }
        private void txtLoginEmail_TextChanged(object sender, EventArgs e) { }
        private void txtLoginPassword_TextChanged(object sender, EventArgs e) { }
        private void txtEmail_TextChanged(object sender, EventArgs e) { }
        private void txtPassword_TextChanged(object sender, EventArgs e) { }
        private void txtFullName_TextChanged(object sender, EventArgs e) { }
        private void cmbRole_SelectedIndexChanged(object sender, EventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
    }
}