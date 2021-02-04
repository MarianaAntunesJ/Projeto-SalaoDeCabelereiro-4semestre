using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoDeCabelereiro.ViewModel
{
    class ClienteViewModel
    {

        private ClienteModel _cliente { get; set; }
        private ClienteDAO _clienteDAO;

        private ObservableCollection<ClienteModel> _funcionarios { get; set; }

        public ClienteModel Cliente
        {
            get { return _cliente; }
            set { _cliente = value; OnPropertyChanged("Cliente"); }
        }

        public ObservableCollection<ClienteModel> Funcionarios
        {
            get { return _funcionarios; }
            set { _funcionarios = value; OnPropertyChanged("Clientes"); }
        }

        public ClienteViewModel()
        {
            LimparUsuarioAtualTela();
            _clienteDAO = new ClienteDAO();
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            Funcionarios = new ObservableCollection<ClienteModel>(_clienteDAO.Listar());
        }

        public void LimparUsuarioAtualTela()
        {
            Cliente = new ClienteModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}
