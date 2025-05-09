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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace iTasks
{
    public partial class frmGereUtilizadores : Form
    {
        public frmGereUtilizadores()
        {
            InitializeComponent();

            // VerificarCamposGestor();

        }

        private void AtualizarListasGestor()
        {
            
            lstListaGestores.Items.Clear();
            lstListaGestores.Items.AddRange(GestorController.ToList().ToArray());
            
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
            // GESTOR
            AtualizarListasGestor();

            cbDepartamento.DataSource = Enum.GetValues(typeof(Departamento));
            cbDepartamento.SelectedIndex = -1;

            // PROGRAMADOR
            AtualizarListasProgramador();

            cbNivelProg.DataSource = Enum.GetValues(typeof(NivelExperiencia));
            cbNivelProg.SelectedIndex = -1;

            cbGestorProg.SelectedIndex = -1;

            using (var db = new iTasksContext())
            {
                cbGestorProg.Items.AddRange(db.Gestores.ToArray());
            }



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
                        
                        // CRIAR O GESTOR
                        var gestor = new Gestor()
                        {
                            Nome = txtNomeGestor.Text,
                            Username = txtUsernameGestor.Text,
                            Password = txtPasswordGestor.Text,
                            Departamento = (Departamento)cbDepartamento.SelectedItem,
                            GereUtilizadores = chkGereUtilizadores.Checked
                        };

                        GestorController.Create(gestor);
                            
          
                        AtualizarListasGestor();

                        MessageBox.Show("Gestor criado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimparCamposGestor();
                        chkGereUtilizadores.Checked = false;

                        btEliminarGestor.Enabled = false;

                        
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
                      
                        var gestor = GestorController.Find(int.Parse(txtIdGestor.Text));

                        if (gestor == null)
                        {
                            MessageBox.Show("Gestor não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // OBTER OS DADOS DO GESTOR PELOS CAMPOS
                        gestor.Nome = txtNomeGestor.Text;
                        gestor.Username = txtUsernameGestor.Text;
                        gestor.Password = txtPasswordGestor.Text;
                        gestor.Departamento = (Departamento)cbDepartamento.SelectedItem;
                        gestor.GereUtilizadores = chkGereUtilizadores.Checked;

                        // ATUALIZAR O GESTOR
                        GestorController.Update(gestor);



                        AtualizarListasGestor();

                        MessageBox.Show("Gestor atualizado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LimparCamposGestor();
                        btEliminarGestor.Enabled = false;



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
                // ENCONTRAR O GESTOR
                var gestor = GestorController.Find(int.Parse(txtIdGestor.Text));

                GestorController.Delete(gestor);

                AtualizarListasGestor();

                MessageBox.Show("Gestor eliminado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);


                LimparCamposGestor();
                btEliminarGestor.Enabled = false;
                
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

        private void txtNomeGestor_TextChanged(object sender, EventArgs e)
        {
            //VerificarCamposGestor();

        }

        private void txtUsernameGestor_TextChanged(object sender, EventArgs e)
        {
            //VerificarCamposGestor();
        }

        private void txtPasswordGestor_TextChanged(object sender, EventArgs e)
        {
            //VerificarCamposGestor();
        }

        private void cbDepartamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            //VerificarCamposGestor();
        }










        private void lstListaProgramadores_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstListaProgramadores.SelectedIndex == -1)
            {
                return;
            }

            btGravarProg.Text = "Atualizar Programador";

            // OBTER O GESTOR SELECIONADO
            Programador programador = (Programador)lstListaProgramadores.SelectedItem;


            // ATUALIZAR OS CAMPOS COM OS DADOS DO PROGRAMADOR
            txtIdProg.Text = programador.Id.ToString();
            txtNomeProg.Text = programador.Nome;
            txtUsernameProg.Text = programador.Username;
            txtPasswordProg.Text = programador.Password;

            cbNivelProg.SelectedItem = programador.NivelExperiencia;

            cbGestorProg.SelectedItem = programador.Gestor;

            btEliminarProg.Enabled = true;
        }


        private void btLimparSelecaoProgramadores_Click(object sender, EventArgs e)
        {
            LimparCamposProgramador();
            AtualizarListasProgramador();

            lstListaProgramadores.SelectedIndex = -1;
            btEliminarGestor.Enabled = false;
        }

        private void btGravarProg_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(txtNomeProg.Text) || string.IsNullOrWhiteSpace(txtUsernameProg.Text) || string.IsNullOrWhiteSpace(txtPasswordProg.Text) || cbNivelProg.SelectedItem == null || cbGestorProg.SelectedItem == null))
            {
                // CRIAR PROGRAMADOR
                if (lstListaProgramadores.SelectedIndex == -1)
                {
                    try
                    {
                        using (var db = new iTasksContext())
                        {
                            // CRIAR O PROGRAMADOR
                            var programador = new Programador()
                            {
                                Nome = txtNomeProg.Text,
                                Username = txtUsernameProg.Text,
                                Password = txtPasswordProg.Text,
                                NivelExperiencia = (NivelExperiencia)cbNivelProg.SelectedItem,
                                Gestor = (Gestor)cbGestorProg.SelectedItem
                            };
                            db.Programadores.Add(programador);
                            db.SaveChanges();

                            MessageBox.Show("Programador criado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            AtualizarListasProgramador();
                            LimparCamposProgramador();
                            

                            btEliminarProg.Enabled = false;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao criar o programador: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // ATUALIZAR PROGRAMADOR
                else if (lstListaProgramadores.SelectedIndex != -1)
                {
                    try
                    {
                        using (var db = new iTasksContext())
                        {
                            var programador = db.Programadores.Find(int.Parse(txtIdProg.Text));

                            if (programador == null)
                            {
                                MessageBox.Show("Programador não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }

                            // ATUALIZAR O PROGRAMADOR
                            programador.Nome = txtNomeProg.Text;
                            programador.Username = txtUsernameProg.Text;
                            programador.Password = txtPasswordProg.Text;
                            programador.NivelExperiencia = (NivelExperiencia)cbNivelProg.SelectedItem;
                            programador.Gestor = (Gestor)cbGestorProg.SelectedItem;

                            db.SaveChanges();

                            MessageBox.Show("Programador atualizado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            AtualizarListasProgramador();
                            LimparCamposProgramador();
                            btEliminarProg.Enabled = false;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao atualizar o programador: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Preencha todos os campos obrigatórios.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
