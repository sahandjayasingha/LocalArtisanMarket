namespace LocalArtisanMarket
{
    partial class CraftStoryWindow
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
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblGeographicOrigin = new System.Windows.Forms.Label();
            this.lblTechniqueUsed = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.txtStoryDescription = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.Location = new System.Drawing.Point(452, 89);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(81, 24);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "product";
            // 
            // lblGeographicOrigin
            // 
            this.lblGeographicOrigin.AutoSize = true;
            this.lblGeographicOrigin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeographicOrigin.Location = new System.Drawing.Point(453, 137);
            this.lblGeographicOrigin.Name = "lblGeographicOrigin";
            this.lblGeographicOrigin.Size = new System.Drawing.Size(167, 16);
            this.lblGeographicOrigin.TabIndex = 1;
            this.lblGeographicOrigin.Text = "Authentic Origin: Molagoda";
            // 
            // lblTechniqueUsed
            // 
            this.lblTechniqueUsed.AutoSize = true;
            this.lblTechniqueUsed.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTechniqueUsed.Location = new System.Drawing.Point(453, 165);
            this.lblTechniqueUsed.Name = "lblTechniqueUsed";
            this.lblTechniqueUsed.Size = new System.Drawing.Size(265, 16);
            this.lblTechniqueUsed.TabIndex = 2;
            this.lblTechniqueUsed.Text = "Traditional Method: Mud-spreading mottling";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(456, 269);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "CLOSE";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // txtStoryDescription
            // 
            this.txtStoryDescription.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtStoryDescription.Location = new System.Drawing.Point(312, 178);
            this.txtStoryDescription.Name = "txtStoryDescription";
            this.txtStoryDescription.ReadOnly = true;
            this.txtStoryDescription.Size = new System.Drawing.Size(100, 96);
            this.txtStoryDescription.TabIndex = 4;
            this.txtStoryDescription.Text = "";
            // 
            // CraftStoryWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtStoryDescription);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTechniqueUsed);
            this.Controls.Add(this.lblGeographicOrigin);
            this.Controls.Add(this.lblProductName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "CraftStoryWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CraftStoryWindow";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.Label lblGeographicOrigin;
        private System.Windows.Forms.Label lblTechniqueUsed;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RichTextBox txtStoryDescription;
    }
}