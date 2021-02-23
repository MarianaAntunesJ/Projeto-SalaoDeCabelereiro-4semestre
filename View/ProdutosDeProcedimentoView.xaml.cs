using SalaoDeCabelereiro.ViewModel;
using System.Windows.Controls;

namespace SalaoDeCabelereiro.View
{
    public partial class ProdutosDeProcedimentoView : Page
    {
        private ProdutosDeProcedimentoViewModel _produtosDeProcedimentoViewModel { get; set; }
        private ProcedimentoView _paginaProcedimento;

        public ProdutosDeProcedimentoView(ProcedimentoViewModel procedimentoViewModel, ProcedimentoView paginaProcedimento)
        {
            InitializeComponent();
            _produtosDeProcedimentoViewModel = new ProdutosDeProcedimentoViewModel(procedimentoViewModel);
            DataContext = _produtosDeProcedimentoViewModel;
            _paginaProcedimento = paginaProcedimento;
        }

        private void DGProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGProdutos.Items.IndexOf(DGProdutos.CurrentItem) >= 0)
            {
                _produtosDeProcedimentoViewModel.Selecionar(DGProdutos.Items.IndexOf(DGProdutos.CurrentItem));
            }
        }

        private void BtnSalvar(object sender, System.Windows.RoutedEventArgs e)
        {
            _produtosDeProcedimentoViewModel.InsereListaProdutos();
            NavigationService.Navigate(_paginaProcedimento);
        }

        private void BtnCancelar(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(_paginaProcedimento);
        }

        private void TxBProduto_TextChanged(object sender, TextChangedEventArgs e)
        {
            _produtosDeProcedimentoViewModel.Consultar(TxBPesquisa.Text);
        }

        private void DGProdutos_Copy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGProdutos.Items.IndexOf(DGProdutosEscolhidos.CurrentItem) >= 0)
            {
                _produtosDeProcedimentoViewModel.Remover(DGProdutosEscolhidos.Items.IndexOf(DGProdutosEscolhidos.CurrentItem));
            }
        }
    }
}
