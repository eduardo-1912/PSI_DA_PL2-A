using iTasks.Model;
using iTasks.Controller;
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

            txtUsername.Text = "jose.carlos";
            txtPassword.Text = "123";

            VerificarCampos();
        }

        // VERIFICAR CAMPOS
        private void VerificarCampos()
        {
            // SE OS 2 CAMPOS ESTÃO PREENCHIDOS --> ATIVAR BOTÃO LOGIN
            if (!string.IsNullOrWhiteSpace(txtUsername.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                btLogin.Enabled = true;
            }
            else
            {
                btLogin.Enabled = false;
            }

            // SE O CAMPO PASSWORD TIVER ALGUM VALOR --> ATIVAR CHECKBOX MOSTRAR PASSWORD
            if (txtPassword.Text.Length > 0)
            {
                checkBoxMostrar.Enabled = true;
            }
            else
            {
                checkBoxMostrar.Enabled = false;
                checkBoxMostrar.Checked = false;
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


        // VERIFICAR USERNAME E PASSWORD
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            VerificarCampos();
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            VerificarCampos();
        }


        // BOTÃO LOGIN
        private void btLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                using (var db = new iTasksContext())
                {
                    // ENCONTRAR O UTILIZADOR NA BASE DE DADOS
                    var utilizador = db.Utilizadores.FirstOrDefault(u => u.Username == username && u.Password == password);

                    // SE NÃO HOUVER NENHUMA CORRESPONDÊNCIA --> MENSAGEM DE ERRO
                    if (utilizador == null)
                    {
                        MessageBox.Show("Dados inválidos.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // GUARDAR A SESSÃO (GUARDA NA APLICAÇÃO TODA)
                    Sessao.UtilizadorAtual = utilizador;

                    // MessageBox.Show($"Bem vindo {utilizador.Nome}", "Login com Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    var kanban = new frmKanban();
                    kanban.FormClosed += (s, args) => this.Close(); // QUANDO O frmKanban FECHAR, O frmLogin TAMBÉM FECHA
                    kanban.Show();
                    this.Hide();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um Erro:" + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }
    }
}