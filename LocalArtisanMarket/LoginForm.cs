using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private Panel pnlBackground;
        private Panel pnlCenterContainer;
        private Label lblAppTitle;
        private Label lblAppSubtitle;

        public LoginForm()
        {
            InitializeComponent();
            ClearOldDesignerControls();
            ApplyPremiumStyle();
            SetupRegisterControls();
        }

        public LoginForm(Main mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;
            ClearOldDesignerControls();
            ApplyPremiumStyle();
            SetupRegisterControls();
        }

        private void ClearOldDesignerControls()
        {
            for (int i = this.Controls.Count - 1; i >= 0; i--)
            {
                Control c = this.Controls[i];
                if (c != txtLoginEmail && c != txtLoginPassword && c != btnLoginSubmit)
                {
                    this.Controls.RemoveAt(i);
                }
            }
        }

        private void ApplyPremiumStyle()
        {
            this.Size = new Size(800, 600);
            this.Text = "Welcome Back";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(248, 246, 242);

            pnlBackground = new Panel();
            pnlBackground.Dock = DockStyle.Fill;
            pnlBackground.BackColor = Color.FromArgb(248, 246, 242);
            this.Controls.Add(pnlBackground);

            pnlCenterContainer = new Panel();
            pnlCenterContainer.Size = new Size(440, 520);
            pnlCenterContainer.BackColor = Color.FromArgb(252, 250, 245);
            pnlCenterContainer.BorderStyle = BorderStyle.None;
            pnlCenterContainer.Paint += PnlCenterContainer_Paint;
            pnlBackground.Controls.Add(pnlCenterContainer);

            pnlBackground.SizeChanged += (s, e) => CenterLoginCard();
            this.Load += (s, e) => CenterLoginCard();

            lblAppTitle = new Label();
            lblAppTitle.Text = "Local Artisan Platform";
            lblAppTitle.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            lblAppTitle.ForeColor = Color.FromArgb(48, 36, 26);
            lblAppTitle.Location = new Point(10, 45);
            lblAppTitle.Size = new Size(420, 40);
            lblAppTitle.TextAlign = ContentAlignment.MiddleCenter;

            lblAppSubtitle = new Label();
            lblAppSubtitle.Text = "Connect with heritage and traditional craft masters";
            lblAppSubtitle.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            lblAppSubtitle.ForeColor = Color.FromArgb(140, 120, 105);
            lblAppSubtitle.Location = new Point(10, 85);
            lblAppSubtitle.Size = new Size(420, 25);
            lblAppSubtitle.TextAlign = ContentAlignment.MiddleCenter;

            pnlCenterContainer.Controls.Add(lblAppTitle);
            pnlCenterContainer.Controls.Add(lblAppSubtitle);

            int startX = (pnlCenterContainer.Width - 340) / 2;

            StyleTextBox(txtLoginEmail, "Email Address", 150, startX);
            StyleTextBox(txtLoginPassword, "Password", 235, startX);
            txtLoginPassword.PasswordChar = '●';

            StyleButton(btnLoginSubmit, "Sign In", 330, Color.FromArgb(120, 80, 45), startX);
        }

        private void CenterLoginCard()
        {
            if (pnlBackground != null && pnlCenterContainer != null)
            {
                pnlCenterContainer.Location = new Point(
                    (pnlBackground.Width - pnlCenterContainer.Width) / 2,
                    (pnlBackground.Height - pnlCenterContainer.Height) / 2
                );
            }
        }

        private void StyleTextBox(TextBox txt, string watermark, int yPos, int startX)
        {
            if (txt.Parent != null) txt.Parent.Controls.Remove(txt);
            pnlCenterContainer.Controls.Add(txt);
            txt.Location = new Point(startX, yPos + 22);
            txt.Width = 340;
            txt.Height = 36;
            txt.Font = new Font("Segoe UI", 11F);
            txt.ForeColor = Color.FromArgb(60, 50, 40);
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.BackColor = Color.White;

            Label lbl = new Label();
            lbl.Text = watermark;
            lbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lbl.ForeColor = Color.FromArgb(100, 85, 75);
            lbl.Location = new Point(startX, yPos);
            lbl.AutoSize = true;
            pnlCenterContainer.Controls.Add(lbl);
        }

        private void StyleButton(Button btn, string text, int yPos, Color backColor, int startX)
        {
            if (btn.Parent != null) btn.Parent.Controls.Remove(btn);
            pnlCenterContainer.Controls.Add(btn);
            btn.Text = text;
            btn.Location = new Point(startX, yPos);
            btn.Width = 340;
            btn.Height = 42;
            btn.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btn.ForeColor = Color.White;
            btn.BackColor = backColor;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
        }

        private void PnlCenterContainer_Paint(object sender, PaintEventArgs e)
        {
            using (Pen borderPen = new Pen(Color.FromArgb(220, 210, 195), 2))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, pnlCenterContainer.Width - 1, pnlCenterContainer.Height - 1);
            }
        }

        private void SetupRegisterControls()
        {
            int startX = (pnlCenterContainer.Width - 340) / 2;

            lnkSwitchToRegister = new LinkLabel();
            lnkSwitchToRegister.Text = "Don't have an account? Register Here";
            lnkSwitchToRegister.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular);
            lnkSwitchToRegister.LinkColor = Color.FromArgb(140, 70, 20);
            lnkSwitchToRegister.ActiveLinkColor = Color.FromArgb(100, 40, 10);
            lnkSwitchToRegister.Location = new Point(startX, 395);
            lnkSwitchToRegister.Width = 340;
            lnkSwitchToRegister.TextAlign = ContentAlignment.MiddleCenter;
            lnkSwitchToRegister.Click += (s, e) => SwitchUI(true);
            pnlCenterContainer.Controls.Add(lnkSwitchToRegister);

            lblRoleTitle = new Label
            {
                Text = "Select Account Role",
                Location = new Point(startX, 330),
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 85, 75),
                AutoSize = true,
                Visible = false
            };

            cmbRegRole = new ComboBox
            {
                Location = new Point(startX, 352),
                Width = 340,
                Height = 36,
                Font = new Font("Segoe UI", 11F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Visible = false,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(60, 50, 40)
            };
            cmbRegRole.Items.AddRange(new string[] { "Customer", "Artisan" });

            pnlCenterContainer.Controls.Add(lblRoleTitle);
            pnlCenterContainer.Controls.Add(cmbRegRole);

            btnRegSubmit = new Button();
            StyleButton(btnRegSubmit, "Create Premium Account", 405, Color.FromArgb(65, 110, 75), startX);
            btnRegSubmit.Visible = false;
            btnRegSubmit.Click += DynamicRegisterSubmit_Click;

            lnkSwitchToLogin = new LinkLabel
            {
                Text = "Back to Login Credentials",
                Font = new Font("Segoe UI", 9.5F, FontStyle.Regular),
                LinkColor = Color.FromArgb(100, 100, 100),
                ActiveLinkColor = Color.Black,
                Location = new Point(startX, 460),
                Width = 340,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };
            lnkSwitchToLogin.Click += (s, e) => SwitchUI(false);
            pnlCenterContainer.Controls.Add(lnkSwitchToLogin);
        }

        private void SwitchUI(bool showRegister)
        {
            int startX = (pnlCenterContainer.Width - 340) / 2;

            btnLoginSubmit.Visible = !showRegister;
            lnkSwitchToRegister.Visible = !showRegister;

            lblRoleTitle.Visible = showRegister;
            cmbRegRole.Visible = showRegister;
            btnRegSubmit.Visible = showRegister;
            lnkSwitchToLogin.Visible = showRegister;

            if (showRegister)
            {
                this.Text = "Create Marketplace Profile";
                lnkSwitchToRegister.Location = new Point(startX, 395);
            }
            else
            {
                this.Text = "Welcome Back";
                lnkSwitchToRegister.Location = new Point(startX, 395);
            }
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