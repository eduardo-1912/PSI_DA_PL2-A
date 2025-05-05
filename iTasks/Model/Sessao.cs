using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTasks.Model
{
    public static class Sessao
    {
        // GUARDAR O UTILIZADOR QUE INICIOU SESSÃO
        // ASSIM PODEMOS GUARDAR TODO O UTILIZADOR EM VEZ DE TER DE GUARDAR APENAS O ID OU TODOS OS DADOS EM SEPARADO
        public static Utilizador UtilizadorAtual { get; set; }

    }
}
