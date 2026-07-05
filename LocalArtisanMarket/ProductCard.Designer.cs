namespace LocalArtisanMarket
{
    partial class ProductCard
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.btnAddToCart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureBox1.Location = new System.Drawing.Point(25, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(195, 170);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(22, 202);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(44, 16);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Name";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(22, 263);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(37, 16);
            this.lblPrice.TabIndex = 2;
            this.lblPrice.Text = "price";
            // 
            // numQuantity
            // 
            this.numQuantity.Location = new System.Drawing.Point(167, 202);
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(38, 22);
            this.numQuantity.TabIndex = 3;
            // 
            // btnAddToCart
            // 
            this.btnAddToCart.BackColor = System.Drawing.Color.Yellow;
            this.btnAddToCart.Font = new System.Drawing.Font("Stencil", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddToCart.Location = new System.Drawing.Point(119, 263);
            this.btnAddToCart.Name = "btnAddToCart";
            this.btnAddToCart.Size = new System.Drawing.Size(101, 23);
            this.btnAddToCart.TabIndex = 4;
            this.btnAddToCart.Text = "Add to Cart";
            this.btnAddToCart.UseVisualStyleBackColor = false;
            // 
            // ProductCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnAddToCart);
            this.Controls.Add(this.numQuantity);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.Name = "ProductCard";
            this.Size = new System.Drawing.Size(243, 333);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.Button btnAddToCart;
    }
}
