namespace Password_Hasher {
    partial class frmPasswordHasher {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            grpMain = new GroupBox();
            txtGeneratedHash = new TextBox();
            lblPasword = new Label();
            txtInput = new TextBox();
            lblPasswordHasher = new Label();
            grpMain.SuspendLayout();
            SuspendLayout();
            // 
            // grpMain
            // 
            grpMain.Controls.Add(txtGeneratedHash);
            grpMain.Controls.Add(lblPasword);
            grpMain.Controls.Add(txtInput);
            grpMain.Controls.Add(lblPasswordHasher);
            grpMain.Location = new Point(12, 12);
            grpMain.Name = "grpMain";
            grpMain.Size = new Size(381, 283);
            grpMain.TabIndex = 0;
            grpMain.TabStop = false;
            // 
            // txtGeneratedHash
            // 
            txtGeneratedHash.Location = new Point(6, 121);
            txtGeneratedHash.Multiline = true;
            txtGeneratedHash.Name = "txtGeneratedHash";
            txtGeneratedHash.ReadOnly = true;
            txtGeneratedHash.Size = new Size(369, 156);
            txtGeneratedHash.TabIndex = 4;
            txtGeneratedHash.Text = "Start typing to generate hash!";
            // 
            // lblPasword
            // 
            lblPasword.AutoSize = true;
            lblPasword.ForeColor = Color.White;
            lblPasword.Location = new Point(6, 80);
            lblPasword.Name = "lblPasword";
            lblPasword.Size = new Size(79, 21);
            lblPasword.TabIndex = 3;
            lblPasword.Text = "Password:";
            // 
            // txtInput
            // 
            txtInput.Location = new Point(91, 77);
            txtInput.Name = "txtInput";
            txtInput.Size = new Size(284, 29);
            txtInput.TabIndex = 1;
            txtInput.TextChanged += txtInput_TextChanged;
            // 
            // lblPasswordHasher
            // 
            lblPasswordHasher.AutoSize = true;
            lblPasswordHasher.ForeColor = Color.White;
            lblPasswordHasher.Location = new Point(125, 25);
            lblPasswordHasher.Name = "lblPasswordHasher";
            lblPasswordHasher.Size = new Size(129, 21);
            lblPasswordHasher.TabIndex = 0;
            lblPasswordHasher.Text = "Password Hasher";
            // 
            // frmPasswordHasher
            // 
            AutoScaleDimensions = new SizeF(9F, 21F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(0, 22, 65);
            ClientSize = new Size(404, 309);
            Controls.Add(grpMain);
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4);
            MaximizeBox = false;
            Name = "frmPasswordHasher";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Password Hasher";
            grpMain.ResumeLayout(false);
            grpMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpMain;
        private TextBox txtInput;
        private Label lblPasswordHasher;
        private Label lblPasword;
        private TextBox txtGeneratedHash;
    }
}