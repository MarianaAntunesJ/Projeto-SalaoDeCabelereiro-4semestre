using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SalaoDeCabelereiro.ViewModel
{
    class ProdutosDeProcedimentoViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ProdutoModel> _produtosEscolhidos { get; set; }
        private ObservableCollection<ProdutoModel> _todosProdutos { get; set; }
        private ProdutoDAO _produtoDAO;
        private ProcedimentoViewModel _procedimentoViewModel { get; set; }

        public ObservableCollection<ProdutoModel> ProdutosEscolhidos
        {
            get { return _produtosEscolhidos; }
            set { _produtosEscolhidos = value; OnPropertyChanged("ProdutosEscolhidos"); }
        }

        public ObservableCollection<ProdutoModel> TodosProdutos
        {
            get { return _todosProdutos; }
            set { _todosProdutos = value; OnPropertyChanged("TodosProdutos"); }
        }

        public ProdutosDeProcedimentoViewModel(ProcedimentoViewModel procedimentoViewModel)
        {
            _procedimentoViewModel = procedimentoViewModel;
            _produtoDAO = new ProdutoDAO();
            ProdutosEscolhidos = new ObservableCollection<ProdutoModel>();
            AtualizarListaTodosProdutos();
        }

        public void InsereListaProdutos()
        {
            _procedimentoViewModel.InsereProdutosDeProcedimento(ProdutosEscolhidos);
        }

        public void Selecionar(int index)
        {
            ProdutosEscolhidos.Add(TodosProdutos[index]);
        }

        private void AtualizarListaTodosProdutos()
        {
            TodosProdutos = new ObservableCollection<ProdutoModel>(_produtoDAO.Listar());
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

    }
}
