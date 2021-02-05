using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SalaoDeCabelereiro.ViewModel
{
    class ProcedimentoViewModel : INotifyPropertyChanged
    {
        private ProcedimentoModel _procedimento { get; set; }
        private ProcedimentoDAO _procedimentoDAO;

        private ObservableCollection<ProcedimentoModel> _procedimentos { get; set; }

        public ProcedimentoModel Procedimento
        {
            get { return _procedimento; }
            set { _procedimento = value; OnPropertyChanged("Procedimento"); }
        }

        public ObservableCollection<ProcedimentoModel> Procedimentos
        {
            get { return _procedimentos; }
            set { _procedimentos = value; OnPropertyChanged("Produtos"); }
        }

        public ProcedimentoViewModel()
        {
            LimparUsuarioAtual();
            _procedimentoDAO = new ProcedimentoDAO();
            AtualizarLista();
        }

        public bool Salvar()
        {
            bool sucesso;
            if (_procedimento.Id == 0)
                sucesso = _procedimentoDAO.Inserir(_procedimento);
            else
                sucesso = _procedimentoDAO.Atualizar(_procedimento);
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
            Procedimentos = new ObservableCollection<ProcedimentoModel>(_procedimentoDAO.Listar());
        }

        public void Consultar(string busca)
        {
            Procedimentos = new ObservableCollection<ProcedimentoModel>(_procedimentoDAO.Consultar(busca));
        }

        public void Selecionar(int index)
        {
            Procedimento = Procedimentos[index];
        }

        public void LimparUsuarioAtual()
        {
            Procedimento = new ProcedimentoModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}