using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoDeCabelereiro.Model
{
    class ClienteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string telefone { get; set; }
        public string CPF { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public AnamneseModel FichaAnamnese { get; set; }
        public bool Ativo { get; set; }
    }
}
