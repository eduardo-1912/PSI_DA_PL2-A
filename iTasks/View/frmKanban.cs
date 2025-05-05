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
    public partial class frmKanban : Form
    {
        public frmKanban()
        {
            InitializeComponent();

            // UTILIZADOR ATUAL
            var utilizador = Sessao.UtilizadorAtual;


            // ATUALIZAR LABEL COM O NOME DO UTILIZADOR
            lblNomeUtilizador.Text += utilizador.Nome;

            // OCULTAR FUNÇÕES DO GESTOR PARA OS PROGRAMADORES
            if (utilizador is Programador programador)
            {
                utilizadoresToolStripMenuItem.Visible = false;
                btNova.Visible = false;
            }

        }

        // FICHEIRO --> SAIR
        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem a certeza que deseja sair?", "Sair da Aplicação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }    
        }

        // FICHEIRO --> EXPORTAR PARA CSV
        private void exportarParaCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        // GESTÃO DA APLICAÇÃO --> GERIR UTILIZADORES
        private void gerirUtilizadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gereUtilizadores = new frmGereUtilizadores();
            gereUtilizadores.Show();
        }

        // GESTÃO DA APLICAÇÃO --> GERIR TIPOS DE TAREFAS
        private void gerirTiposDeTarefasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var gereTiposTarefas = new frmGereTiposTarefas();
            gereTiposTarefas.Show();
        }

        // LISTAGENS --> TAREFAS CONCLUÍDAS
        private void tarefasTerminadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var consultarTarefasConcluidas = new frmConsultarTarefasConcluidas();
            consultarTarefasConcluidas.Show();
        }

        // LISTAGENS --> TAREFAS EM CURSO
        private void tarefasEmCursoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var consultarTarefasEmCurso = new frmConsultaTarefasEmCurso();
            consultarTarefasEmCurso.Show();
        }


    }
}
