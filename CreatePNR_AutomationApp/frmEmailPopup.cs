using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreatePNR_AutomationApp
{
    public partial class frmEmailPopup : Form
    {
        public string ReceipentEmail { get; set; }
        public bool IsClose { get; set; }
        public bool IsFromOk { get; set; }
        public frmEmailPopup()
        {
            InitializeComponent();
            btnOk.Enabled = false;
            txtReceipentEmail.Focus();
            IsFromOk = false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            ReceipentEmail = txtReceipentEmail.Text.Trim();
            IsFromOk = true;
            this.Close();
        }

        private void txtReceipentEmail_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = !string.IsNullOrEmpty(txtReceipentEmail.Text);
        }

        private void frmEmailPopup_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is CreatePNR_AutomationApp.frmEmailPopup && ((CreatePNR_AutomationApp.frmEmailPopup)sender).Text == "Receipent email" && !IsFromOk)
                IsClose = true;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
