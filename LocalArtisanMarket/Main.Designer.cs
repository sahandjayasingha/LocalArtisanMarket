namespace LocalArtisanMarket
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelSideMenu = new System.Windows.Forms.Panel();
            this.btnlogin = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnproducts = new System.Windows.Forms.Button();
            this.btnhome = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.mainWorkspacePanel = new System.Windows.Forms.Panel();
            this.panelSideMenu.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSideMenu
            // 
            this.panelSideMenu.BackColor = System.Drawing.Color.DarkGray;
            this.panelSideMenu.Controls.Add(this.btnlogin);
            this.panelSideMenu.Controls.Add(this.btnInventory);
            this.panelSideMenu.Controls.Add(this.btnproducts);
            this.panelSideMenu.Controls.Add(this.btnhome);
            this.panelSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSideMenu.Location = new System.Drawing.Point(0, 0);
            this.panelSideMenu.Margin = new System.Windows.Forms.Padding(4);
            this.panelSideMenu.Name = "panelSideMenu";
            this.panelSideMenu.Size = new System.Drawing.Size(200, 450);
            this.panelSideMenu.TabIndex = 0;
            this.panelSideMenu.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSideMenu_Paint);
            // 
            // btnlogin
            // 
            this.btnlogin.Location = new System.Drawing.Point(4, 303);
            this.btnlogin.Margin = new System.Windows.Forms.Padding(4);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(194, 45);
            this.btnlogin.TabIndex = 3;
            this.btnlogin.Text = "Login";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click_1);
            // 
            // btnInventory
            // 
            this.btnInventory.Location = new System.Drawing.Point(4, 223);
            this.btnInventory.Margin = new System.Windows.Forms.Padding(4);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(194, 45);
            this.btnInventory.TabIndex = 2;
            this.btnInventory.Text = "Inventory";
            this.btnInventory.UseVisualStyleBackColor = true;
            // 
            // btnproducts
            // 
            this.btnproducts.Location = new System.Drawing.Point(4, 144);
            this.btnproducts.Margin = new System.Windows.Forms.Padding(4);
            this.btnproducts.Name = "btnproducts";
            this.btnproducts.Size = new System.Drawing.Size(194, 45);
            this.btnproducts.TabIndex = 1;
            this.btnproducts.Text = "Products";
            this.btnproducts.UseVisualStyleBackColor = true;
            
            
  
            this.btnhome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnhome.ForeColor = System.Drawing.Color.White;
            this.btnhome.Location = new System.Drawing.Point(4, 62);
            this.btnhome.Margin = new System.Windows.Forms.Padding(4);
            this.btnhome.Name = "btnhome";
            this.btnhome.Size = new System.Drawing.Size(194, 45);
            this.btnhome.TabIndex = 0;
            this.btnhome.Text = "Home";
            this.btnhome.UseVisualStyleBackColor = true;
            this.btnhome.Click += new System.EventHandler(this.btnlogin_Click_1);
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.panelContent.Controls.Add(this.mainWorkspacePanel);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(200, 0);
            this.panelContent.Margin = new System.Windows.Forms.Padding(4);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1293, 450);
            this.panelContent.TabIndex = 1;
            this.panelContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContent_Paint);
            // 
            // mainWorkspacePanel
            // 
            this.mainWorkspacePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainWorkspacePanel.Location = new System.Drawing.Point(0, 0);
            this.mainWorkspacePanel.Margin = new System.Windows.Forms.Padding(2);
            this.mainWorkspacePanel.Name = "mainWorkspacePanel";
            this.mainWorkspacePanel.Size = new System.Drawing.Size(1293, 450);
            this.mainWorkspacePanel.TabIndex = 0;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1493, 450);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSideMenu);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Main_Load);
            this.panelSideMenu.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelSideMenu;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel mainWorkspacePanel;
        public System.Windows.Forms.Button btnhome;
        public System.Windows.Forms.Button btnlogin;
        public System.Windows.Forms.Button btnInventory;
        public System.Windows.Forms.Button btnproducts;
    }
}