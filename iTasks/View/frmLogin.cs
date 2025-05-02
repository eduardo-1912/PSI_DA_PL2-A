using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();

            // PASSWORD ESCONDIDA POR DEFAULT
            txtPassword.UseSystemPasswordChar = true;

            btLogin.Enabled = false;
            checkBoxMostrar.Enabled = false;

            if (!string.IsNullOrWhiteSpace(txtUsername.Text) &&
              !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                btLogin.Enabled = true;
                checkBoxMostrar.Enabled = true;
            }
            else
            {
                btLogin.Enabled = false;
                checkBoxMostrar.Enabled = false;
            }

        }

        // MOSTRAR/ESCONDER PASSWORD
        private void checkBoxMostrar_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMostrar.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }





        private void btLogin_Click(object sender, EventArgs e)
        {




        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            VerificarUsername();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            VerificarCampos();
            checkBoxMostrar.Enabled = true;
        }

        private void VerificarUsername()
        {
            if (!string.IsNullOrWhiteSpace(txtUsername.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                btLogin.Enabled = true;
            }
            else
            {
                btLogin.Enabled = false;
            }
                

        }



    }
}