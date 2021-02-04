using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SalaoDeCabelereiro.ViewModel
{
    class AgendaViewModel
    {

        private AgendaModel _agendamento { get; set; }
        private AgendaDAO _agendamentoDAO;

        private ObservableCollection<AgendaModel> _agendamentos { get; set; }

        public AgendaModel Agendamento
        {
            get { return _agendamento; }
            set { _agendamento = value; OnPropertyChanged("Agendamento"); }
        }

        public ObservableCollection<AgendaModel> Agendamentos
        {
            get { return _agendamentos; }
            set { _agendamentos = value; OnPropertyChanged("Agendamentos"); }
        }

        public AgendaViewModel()
        {
            LimparUsuarioAtualTela();
            _agendamentoDAO = new AgendaDAO();
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            Agendamentos = new ObservableCollection<AgendaModel>(_agendamentoDAO.Listar());
        }

        public void LimparUsuarioAtualTela()
        {
            Agendamento = new AgendaModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}
