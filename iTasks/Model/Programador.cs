using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Model
{
    class Programador : Utilizador
    {
        public NivelExperiencia NivelExperiencia { get; set; }
        public int IdGestor { get; set; } // FOREIGN KEY -> Gestor -> Utilizador(Id)
        [ForeignKey("IdGestor")]
        public Gestor Gestor { get; set; } // PERMITE SABER O GESTOR (EX: programador.Gestor.Nome)
    }
}
