using iTasks.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Controller
{
    class GestorController
    {

        // LISTAR GESTORES
        public static List<Gestor> ToList()
        {
            using (var db = new iTasksContext())
            {
                return db.Gestores.ToList();
            }
        }

        // CRIAR GESTOR
        public static void Create(Gestor gestor)
        {
            using (var db = new iTasksContext())
            {
                db.Gestores.Add(gestor);
                db.SaveChanges();
            }
        }

        // ENCONTRAR GESTOR POR ID
        public static Gestor Find(int id)
        {
            using (var db = new iTasksContext())
            {
                return db.Gestores.Find(id);
            }
        }

        // ATUALIZAR GESTOR
        public static void Update(Gestor gestor)
        {
            using (var db = new iTasksContext())
            {
                // OBTEM O GESTOR SELECIONADO
                var gestorSelecionado = db.Gestores.Find(gestor.Id);

                if (gestorSelecionado != null)
                {
                    gestorSelecionado.Nome = gestor.Nome;
                    gestorSelecionado.Username = gestor.Username;
                    gestorSelecionado.Password = gestor.Password;
                    gestorSelecionado.Departamento = gestor.Departamento;
                    gestorSelecionado.GereUtilizadores = gestor.GereUtilizadores;
                    db.SaveChanges();
                }
            }
        }

        // ELIMINAR GESTOR
        public static void Delete(Gestor gestor)
        {
            using (var db = new iTasksContext())
            {
                // OBTEM O GESTOR SELECIONADO
                var gestorSelecionado = db.Gestores.Find(gestor.Id);

                if (gestorSelecionado != null)
                {
                    db.Gestores.Remove(gestorSelecionado);
                    db.SaveChanges();
                }
            }
        }

    }
}