using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SalaoDeCabelereiro.ViewModel
{
    class ClienteViewModel : INotifyPropertyChanged
    {

        private ClienteModel _cliente { get; set; }
        private ClienteDAO _clienteDAO;

        private ObservableCollection<ClienteModel> _clientes { get; set; }

        public ClienteModel Cliente
        {
            get { return _cliente; }
            set { _cliente = value; OnPropertyChanged("Cliente"); }
        }

        public ObservableCollection<ClienteModel> Clientes
        {
            get { return _clientes; }
            set { _clientes = value; OnPropertyChanged("Clientes"); }
        }

        public ClienteViewModel()
        {
            LimparUsuarioAtual();
            _clienteDAO = new ClienteDAO();
            AtualizarLista();
            _cliente = new ClienteModel();
        }

        public bool Salvar()
        {
            bool sucesso;
            if (_cliente.Id == 0)
                sucesso = _clienteDAO.Inserir(_cliente);
            else
                sucesso = _clienteDAO.Atualizar(_cliente);
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
            Clientes = new ObservableCollection<ClienteModel>(_clienteDAO.Listar());
        }

        public void Consultar(string busca)
        {
            Clientes = new ObservableCollection<ClienteModel>(_clienteDAO.Consultar(busca));
        }

        public void Selecionar(int index)
        {
            Cliente = Clientes[index];
        }

        public void LimparUsuarioAtual()
        {
            Cliente = new ClienteModel { DataNascimento = DateTime.Today }; ;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}
