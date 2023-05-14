namespace alarmeWinForm
{
    partial class btnzone2
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnZ1 = new System.Windows.Forms.Button();
            this.btnZ2 = new System.Windows.Forms.Button();
            this.btnZ3 = new System.Windows.Forms.Button();
            this.btnState = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnZ1
            // 
            this.btnZ1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnZ1.Font = new System.Drawing.Font("Segoe UI Black", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnZ1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnZ1.Location = new System.Drawing.Point(40, 127);
            this.btnZ1.Name = "btnZ1";
            this.btnZ1.Size = new System.Drawing.Size(155, 66);
            this.btnZ1.TabIndex = 0;
            this.btnZ1.Text = "zone 1";
            this.btnZ1.UseVisualStyleBackColor = false;
            // 
            // btnZ2
            // 
            this.btnZ2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnZ2.Font = new System.Drawing.Font("Segoe UI Black", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnZ2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnZ2.Location = new System.Drawing.Point(40, 224);
            this.btnZ2.Name = "btnZ2";
            this.btnZ2.Size = new System.Drawing.Size(155, 66);
            this.btnZ2.TabIndex = 1;
            this.btnZ2.Text = "zone 2";
            this.btnZ2.UseVisualStyleBackColor = false;
            this.btnZ2.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnZ3
            // 
            this.btnZ3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnZ3.Font = new System.Drawing.Font("Segoe UI Black", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnZ3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnZ3.Location = new System.Drawing.Point(40, 314);
            this.btnZ3.Name = "btnZ3";
            this.btnZ3.Size = new System.Drawing.Size(155, 66);
            this.btnZ3.TabIndex = 2;
            this.btnZ3.Text = "zone 3";
            this.btnZ3.UseVisualStyleBackColor = false;
            // 
            // btnState
            // 
            this.btnState.BackColor = System.Drawing.Color.Red;
            this.btnState.Font = new System.Drawing.Font("Segoe UI Black", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnState.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnState.Location = new System.Drawing.Point(40, 38);
            this.btnState.Name = "btnState";
            this.btnState.Size = new System.Drawing.Size(155, 66);
            this.btnState.TabIndex = 3;
            this.btnState.Text = "OFF";
            this.btnState.UseVisualStyleBackColor = false;
            // 
            // btnzone2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(232, 419);
            this.Controls.Add(this.btnState);
            this.Controls.Add(this.btnZ3);
            this.Controls.Add(this.btnZ2);
            this.Controls.Add(this.btnZ1);
            this.Name = "btnzone2";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);

            this.btnState.Click += new System.EventHandler(this.btnState_Click);
            this.btnZ1.Click += new System.EventHandler(this.btnZ1_Click);
            this.btnZ2.Click += new System.EventHandler(this.btnZ2_Click); // Correction ici
            this.btnZ3.Click += new System.EventHandler(this.btnZ3_Click);


            this.ResumeLayout(false);

        }

        #endregion

        private Button btnZ1;
        private Button btnZ2;
        private Button btnZ3;
        private Button btnState;

    }
}



