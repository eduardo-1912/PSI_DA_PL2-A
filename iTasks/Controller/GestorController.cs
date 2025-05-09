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
        public static List<Gestor> ShowAll()
        {
            using (var db = new iTasksContext())
            {
                return db.Gestores.ToList();
            }
        }

    }
}
