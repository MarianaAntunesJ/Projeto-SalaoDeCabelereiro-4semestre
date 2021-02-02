namespace SalaoDeCabelereiro.Model
{
    public class FuncionarioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }
        public string Profissao { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }

        public FuncionarioModel()
        {
        }

        public FuncionarioModel(int id, string nome, string telefone, string cpf, string profissao, string email, bool ativo, string usuario, string senha)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
            Profissao = profissao;
            Email = email;
            Ativo = ativo;
            Usuario = usuario;
            Senha = senha;
        }
    }
}
