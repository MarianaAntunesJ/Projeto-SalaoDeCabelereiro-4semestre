using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoDeCabelereiro.Relatorio
{
    class ProcedimentoRelatorio
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string AreaProfissional { get; set; }

        public ProcedimentoRelatorio(int id, string nome, string areaProfissional)
        {
            Id = id;
            Nome = nome;
            AreaProfissional = areaProfissional;
        }
    }
}
