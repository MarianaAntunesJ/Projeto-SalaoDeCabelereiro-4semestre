using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace SalaoDeCabelereiro.ViewModel
{
    public class FuncionarioViewModel : INotifyPropertyChanged
    {
        private FuncionarioModel _funcionario { get; set; }
        private FuncionarioDAO _funcionarioDAO;

        private ObservableCollection<FuncionarioModel> _funcionarios { get; set; }

        public FuncionarioModel Funcionario
        {
            get { return _funcionario; }
            set { _funcionario = value; OnPropertyChanged("Funcionario"); }
        }

        public ObservableCollection<FuncionarioModel> Funcionarios
        {
            get { return _funcionarios; }
            set { _funcionarios = value; OnPropertyChanged("Funcionarios"); }
        }

        public FuncionarioViewModel()
        {
            LimparUsuarioAtual();
            _funcionarioDAO = new FuncionarioDAO();
            AtualizarLista();
        }

        //ToDo: ValidaCliente()

        public bool Salvar(string senha)
        {
            bool sucesso;
            _funcionario.Senha = senha;
            if (_funcionario.Id == 0)
                sucesso = _funcionarioDAO.Inserir(_funcionario);
            else
                sucesso = _funcionarioDAO.Atualizar(_funcionario);
            if (sucesso)
            {
                AtualizarLista();
                LimparUsuarioAtual();
                return true;
            }
            return false;
        }

        private void AtualizarLista()
        {
            Funcionarios = new ObservableCollection<FuncionarioModel>(_funcionarioDAO.Listar());
        }

        public void Consultar(string busca)
        {
            Funcionarios = new ObservableCollection<FuncionarioModel>(_funcionarioDAO.Consultar(busca));
        }

        public void Selecionar(int index)
        {
            Funcionario = Funcionarios[index];
        }

        public void LimparUsuarioAtual()
        {
            Funcionario = new FuncionarioModel();
        }

        public string GerarHashMd5(string entrada)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Converter a String para array de bytes, que é como a biblioteca trabalha.
                byte[] data = md5Hash.ComputeHash(Encoding.Default.GetBytes(entrada));

                // Cria-se um StringBuilder para recompôr a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop para formatar cada byte como uma String em hexadecimal
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}
