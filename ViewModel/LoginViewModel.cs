using SalaoDeCabelereiro.Banco;

namespace SalaoDeCabelereiro.ViewModel
{
    class LoginViewModel
    {
        private FuncionarioDAO _funcionarioDAO { get; set; } = new FuncionarioDAO();

        public string LoginValido(string usuario, string senha) => _funcionarioDAO.VerificarLogin(usuario, senha);
    }
}
