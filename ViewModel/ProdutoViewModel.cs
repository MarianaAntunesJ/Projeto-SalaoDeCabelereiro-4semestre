using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SalaoDeCabelereiro.ViewModel
{
    class ProdutoViewModel
    {
        private ProdutoModel _produto { get; set; }
        private ProdutoDAO _produtoDAO;

        private ObservableCollection<ProdutoModel> _produtos { get; set; }

        public ProdutoModel Produto
        {
            get { return _produto; }
            set { _produto = value; OnPropertyChanged("Produto"); }
        }

        public ObservableCollection<ProdutoModel> Produtos
        {
            get { return _produtos; }
            set { _produtos = value; OnPropertyChanged("Produtos"); }
        }

        public ProdutoViewModel()
        {
            LimparUsuarioAtualTela();
            _produtoDAO = new ProdutoDAO();
            AtualizarLista();
        }

        private void AtualizarLista()
        {
            Produtos = new ObservableCollection<ProdutoModel>(_produtoDAO.Listar());
        }

        public void LimparUsuarioAtualTela()
        {
            Produto = new ProdutoModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}
