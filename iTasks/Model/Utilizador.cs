using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Model
{
    public class Utilizador
    {
        public int Id { get; set; }
        public String Nome { get; set; }

        [Index(IsUnique = true)] // USERNAME TEM DE SER ÚNICO
        [MaxLength(100)] // TAMANHO MÁXIMO DO USERNAME
        public String Username { get; set; }
        public String Password { get; set; }
    }
}
