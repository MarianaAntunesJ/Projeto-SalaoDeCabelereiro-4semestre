using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SalaoDeCabelereiro.ViewModel
{
    class ProdutoViewModel : INotifyPropertyChanged
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
            LimparUsuarioAtual();
            _produtoDAO = new ProdutoDAO();
            AtualizarLista();
        }

        public bool Salvar()
        {
            bool sucesso;
            if (_produto.Id == 0)
                sucesso = _produtoDAO.Inserir(_produto);
            else
                sucesso = _produtoDAO.Atualizar(_produto);
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
            Produtos = new ObservableCollection<ProdutoModel>(_produtoDAO.Listar());
        }

        public void Consultar(string busca)
        {
            Produtos = new ObservableCollection<ProdutoModel>(_produtoDAO.Consultar(busca));
        }

        public void Selecionar(int index)
        {
            Produto = Produtos[index];
        }


        public void LimparUsuarioAtual()
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
