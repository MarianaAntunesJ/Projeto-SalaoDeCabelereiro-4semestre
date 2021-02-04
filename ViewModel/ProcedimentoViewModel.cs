using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SalaoDeCabelereiro.ViewModel
{
    class ProcedimentoViewModel
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
            LimparUsuarioAtualTela();
            _procedimentoDAO = new ProcedimentoDAO();
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            Procedimentos = new ObservableCollection<ProcedimentoModel>(_procedimentoDAO.Listar());
        }

        public void LimparUsuarioAtualTela()
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