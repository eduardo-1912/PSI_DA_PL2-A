using iTasks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace iTasks
{
    internal static class Program
    {
        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // CRIA A BASE DE DADOS CASO NÃO EXISTA
            using (var db = new iTasksContext())
            {
                db.Database.CreateIfNotExists();

                if (!db.Gestores.Any())
                {
                    var gestor = new Gestor
                    {
                        Nome = "Gestor Teste",
                        Username = "gestor_teste",
                        Password = "123",
                        Departamento = Departamento.IT,
                        GereUtilizadores = true
                    };

                    var programador = new Programador
                    {
                        Nome = "Programador Teste",
                        Username = "programador_teste",
                        Password = "123",
                        NivelExperiencia = NivelExperiencia.Senior,
                        Gestor = gestor
                    };

                    db.Gestores.Add(gestor);
                    db.Programadores.Add(programador);
                    db.SaveChanges();
                }
            }

            
            Application.Run(new frmLogin());
        }

        // instanciar um controller
        // meter private ProductController controller nas views;
        // public formController
        // formulários sempre iniciados com o controller
        // ProductForm formBase = new ProductForm(controller);
        // Form2 formSecundario = new Form2(controller);
        // Application.Run(formBase);


    }
}

