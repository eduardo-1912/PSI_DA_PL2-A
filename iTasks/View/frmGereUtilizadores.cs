using iTasks.Model;
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
    public partial class frmGereUtilizadores : Form
    {
        public frmGereUtilizadores()
        {
            InitializeComponent();

        }

        private void AtualizarListasGestor()
        {
            using (var db = new iTasksContext())
            {
                lstListaGestores.Items.Clear();
                lstListaGestores.Items.AddRange(db.Gestores.ToArray());
            }
        }

        private void LimparCamposGestor()
        {
            txtIdGestor.Clear();
            txtNomeGestor.Clear();
            txtUsernameGestor.Clear();
            txtPasswordGestor.Clear();
            cbDepartamento.SelectedIndex = -1;
            chkGereUtilizadores.Checked = false;
            btGravarGestor.Text = "Criar Gestor";
            lstListaGestores.SelectedIndex = -1;
        }

        private void AtualizarListasProgramador()
        {
            using (var db = new iTasksContext())
            {
                lstListaProgramadores.Items.Clear();
                lstListaProgramadores.Items.AddRange(db.Programadores.ToArray());
            }
        }

        private void LimparCamposProgramador()
        {
            txtIdProg.Clear();
            txtNomeProg.Clear();
            txtUsernameProg.Clear();
            txtPasswordProg.Clear();
            cbNivelProg.SelectedIndex = -1;
            cbGestorProg.SelectedIndex = -1;
        }

        private void frmGereUtilizadores_Load(object sender, EventArgs e)
        {
            AtualizarListasGestor();

            cbDepartamento.DataSource = Enum.GetValues(typeof(Departamento));
            cbDepartamento.SelectedIndex = -1;
        }

        private void lstListaGestores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstListaGestores.SelectedIndex == -1)
            {
                return;
            }

            btGravarGestor.Text = "Atualizar Gestor";

            // OBTER O GESTOR SELECIONADO
            Gestor gestor = (Gestor)lstListaGestores.SelectedItem;


            // ATUALIZAR OS CAMPOS COM OS DADOS DO GESTOR
            txtIdGestor.Text = gestor.Id.ToString();
            txtNomeGestor.Text = gestor.Nome;
            txtUsernameGestor.Text = gestor.Username;
            txtPasswordGestor.Text = gestor.Password;

            cbDepartamento.SelectedItem = gestor.Departamento;

            // CHECKBOX GERE UTILIZADORES
            if (gestor.GereUtilizadores)
            {
                chkGereUtilizadores.Checked = true;
            }
            else
            {
                chkGereUtilizadores.Checked = false;
            }


            int idGestorSelecionado = int.Parse(txtIdGestor.Text);

            if (idGestorSelecionado == Sessao.UtilizadorAtual.Id)
            {
                btEliminarGestor.Enabled = false;
                return;
            }

            
            btEliminarGestor.Enabled = true;


        }

        // CRIAR/ATUALIZAR GESTOR
        private void btGravarGestor_Click(object sender, EventArgs e)
        {

            if (!(string.IsNullOrWhiteSpace(txtNomeGestor.Text) || string.IsNullOrWhiteSpace(txtUsernameGestor.Text) || string.IsNullOrWhiteSpace(txtPasswordGestor.Text) || cbDepartamento.SelectedItem == null))
            {
                // CRIAR GESTOR
                if (lstListaGestores.SelectedIndex == -1)
                {
                    try
                    {
                        using (var db = new iTasksContext())
                        {
                            // CRIAR O GESTOR
                            var gestor = new Gestor()
                            {
                                Nome = txtNomeGestor.Text,
                                Username = txtUsernameGestor.Text,
                                Password = txtPasswordGestor.Text,
                                Departamento = (Departamento)cbDepartamento.SelectedItem,
                                GereUtilizadores = chkGereUtilizadores.Checked
                            };
                            db.Gestores.Add(gestor);
                            db.SaveChanges();

                            MessageBox.Show("Gestor criado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AtualizarListasGestor();
                            LimparCamposGestor();
                            chkGereUtilizadores.Checked = false;

                            btEliminarGestor.Enabled = false;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao criar o gestor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // ATUALIZAR GESTOR
                else if (lstListaGestores.SelectedIndex != -1)
                {
                    try
                    {
                        using (var db = new iTasksContext())
                        {
                            var gestor = db.Gestores.Find(int.Parse(txtIdGestor.Text));

                            if (gestor == null)
                            {
                                MessageBox.Show("Gestor não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // ATUALIZAR O GESTOR
                            gestor.Nome = txtNomeGestor.Text;
                            gestor.Username = txtUsernameGestor.Text;
                            gestor.Password = txtPasswordGestor.Text;
                            gestor.Departamento = (Departamento)cbDepartamento.SelectedItem;
                            gestor.GereUtilizadores = chkGereUtilizadores.Checked;

                            db.SaveChanges();

                            MessageBox.Show("Gestor atualizado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            AtualizarListasGestor();
                            LimparCamposGestor();
                            btEliminarGestor.Enabled = false;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao atualizar o gestor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        // ELIMINAR GESTOR
        private void btEliminarGestor_Click(object sender, EventArgs e)
        {

            try
            {
                using (var db = new iTasksContext())
                {
                    // ENCONTRAR O GESTOR
                    
                    var gestor = db.Gestores.Find(int.Parse(txtIdGestor.Text));

                    db.Gestores.Remove(gestor);
                    db.SaveChanges();

                    MessageBox.Show("Gestor eliminado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AtualizarListasGestor();
                    LimparCamposGestor();
                    btEliminarGestor.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao eliminar o gestor: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // LIMPAR SELEÇÃO GESTORES
        private void btLimparSelecaoGestores_Click(object sender, EventArgs e)
        {
            LimparCamposGestor();
            AtualizarListasGestor();

            lstListaGestores.SelectedIndex = -1;
            btEliminarGestor.Enabled = false;
        }
    }
}
