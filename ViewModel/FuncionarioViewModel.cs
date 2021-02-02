using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

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
            LimparUsuarioAtualTela();
            _funcionarioDAO = new FuncionarioDAO();
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            Funcionarios = new ObservableCollection<ClienteModel>(_funcionarioDAO.Listar());
        }

        public void LimparUsuarioAtualTela()
        {
            Funcionario = new FuncionarioModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}
