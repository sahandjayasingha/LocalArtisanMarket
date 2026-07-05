using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace LocalArtisanMarket
{
    public partial class LoginForm : Form
    {
        private Main _mainForm;

        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=LocalArtisanMarketDB;Integrated Security=True;Connect Timeout=30;";

        public static string CurrentUserID { get; private set; } = null;
        public static string CurrentUserRole { get; private set; } = null;

        private ComboBox cmbRegRole;
        private Button btnRegSubmit;
        private LinkLabel lnkSwitchToRegister;
        private LinkLabel lnkSwitchToLogin;
        private Label lblRoleTitle;

        public LoginForm()
        {
            InitializeComponent();
            SetupRegisterControls();
        }

        public LoginForm(Main mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            SetupRegisterControls();
        }

        private void SetupRegisterControls()
        {
            lnkSwitchToRegister = new LinkLabel();
            lnkSwitchToRegister.Text = "Don't have an account? Register Here";
            lnkSwitchToRegister.Location = new Point(txtLoginPassword.Location.X, btnLoginSubmit.Location.Y + btnLoginSubmit.Height + 15);
            lnkSwitchToRegister.AutoSize = true;
            lnkSwitchToRegister.Click += (s, e) => SwitchUI(true);
            this.Controls.Add(lnkSwitchToRegister);

            lblRoleTitle = new Label { Text = "Select Role", Location = new Point(txtLoginPassword.Location.X, txtLoginPassword.Location.Y + txtLoginPassword.Height + 15), AutoSize = true, Visible = false };
            cmbRegRole = new ComboBox { Location = new Point(txtLoginPassword.Location.X, lblRoleTitle.Location.Y + 20), Width = txtLoginPassword.Width, DropDownStyle = ComboBoxStyle.DropDownList, Visible = false };
            cmbRegRole.Items.AddRange(new string[] { "Customer", "Artisan" });
            this.Controls.Add(lblRoleTitle);
            this.Controls.Add(cmbRegRole);

            btnRegSubmit = new Button { Text = "Register Account", Location = new Point(txtLoginPassword.Location.X, cmbRegRole.Location.Y + cmbRegRole.Height + 15), Width = txtLoginPassword.Width, Height = btnLoginSubmit.Height, BackColor = Color.LightBlue, Visible = false };
            btnRegSubmit.Click += DynamicRegisterSubmit_Click;
            this.Controls.Add(btnRegSubmit);

            lnkSwitchToLogin = new LinkLabel { Text = "Back to Login", Location = new Point(txtLoginPassword.Location.X, btnRegSubmit.Location.Y + btnRegSubmit.Height + 15), AutoSize = true, Visible = false };
            lnkSwitchToLogin.Click += (s, e) => SwitchUI(false);
            this.Controls.Add(lnkSwitchToLogin);
        }

        private void SwitchUI(bool showRegister)
        {
            btnLoginSubmit.Visible = !showRegister;
            lnkSwitchToRegister.Visible = !showRegister;

            lblRoleTitle.Visible = showRegister;
            cmbRegRole.Visible = showRegister;
            btnRegSubmit.Visible = showRegister;
            lnkSwitchToLogin.Visible = showRegister;
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

            if ((email.Equals("artisan@gmail.com", StringComparison.OrdinalIgnoreCase) || email.Equals("customer@gmail.com", StringComparison.OrdinalIgnoreCase)) && password == "123")
            {
                CurrentUserID = "1";
                CurrentUserRole = email.StartsWith("customer", StringComparison.OrdinalIgnoreCase) ? "Customer" : "Artisan";

                MessageBox.Show("Login Successful! Welcome back.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (_mainForm != null)
                {
                    _mainForm.ConfigureNavigation(CurrentUserRole);
                }

                this.Close();
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT UserID, Role FROM Users WHERE Email = @Email AND Password = @Password";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password);

                        conn.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CurrentUserID = reader["UserID"].ToString();
                                CurrentUserRole = reader["Role"].ToString();

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
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database connection error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DynamicRegisterSubmit_Click(object sender, EventArgs e)
        {
            string email = txtLoginEmail.Text.Trim();
            string password = txtLoginPassword.Text.Trim();

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both Email and Password for registration.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbRegRole.SelectedItem == null)
            {
                MessageBox.Show("Please select a Role (Customer or Artisan).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string role = cmbRegRole.SelectedItem.ToString();
            string defaultFullName = email.Contains("@") ? email.Split('@')[0] : "New User";

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@Email", email);
                        int userCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (userCount > 0)
                        {
                            MessageBox.Show("This Email is already registered!", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO Users (Email, Password, Role, FullName) VALUES (@Email, @Password, @Role, @FullName)";
                    using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@Email", email);
                        insertCmd.Parameters.AddWithValue("@Password", password);
                        insertCmd.Parameters.AddWithValue("@Role", role);
                        insertCmd.Parameters.AddWithValue("@FullName", defaultFullName);

                        int rowsAffected = insertCmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registration Successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            SwitchUI(false);
                            txtLoginEmail.Clear();
                            txtLoginPassword.Clear();
                            cmbRegRole.SelectedIndex = -1;
                        }
                        else
                        {
                            MessageBox.Show("Failed to save user into database.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database registration error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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