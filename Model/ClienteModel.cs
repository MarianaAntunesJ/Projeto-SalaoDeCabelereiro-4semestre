using System;

namespace SalaoDeCabelereiro.Model
{
    class ClienteModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public string Sexo { get; set; }
        public DateTime DataNascimento { get; set; }
        public AnamneseModel FichaAnamnese { get; set; }
        public bool Ativo { get; set; }

        public ClienteModel()
        {
        }

        public ClienteModel(int id, string nome, string telefone, string cpf, string sexo, DateTime dataNascimento, AnamneseModel fichaAnamnese, bool ativo)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
            Sexo = sexo;
            DataNascimento = dataNascimento;
            FichaAnamnese = fichaAnamnese;
            Ativo = ativo;
        }
    }
}
