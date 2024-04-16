namespace Password_Hasher {
    public partial class frmPasswordHasher : Form {
        public frmPasswordHasher() {
            InitializeComponent();

            // Set icon
            this.Icon = Properties.Resources.icon;
        } // end method

        // Hashes what user enters when event occurs
        private void txtInput_TextChanged(object sender, EventArgs e) {
            // Check length of text
            if (txtInput.Text.Length > 0) {
                txtGeneratedHash.Text = PasswordHasher.HashPassword(txtInput.Text);
            } // end if
        } // end method
    } // end class
} // end namespace