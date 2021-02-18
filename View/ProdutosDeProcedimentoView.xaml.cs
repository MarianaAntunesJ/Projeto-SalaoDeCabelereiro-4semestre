using SalaoDeCabelereiro.ViewModel;
using System.Windows.Controls;

namespace SalaoDeCabelereiro.View
{
    public partial class ProdutosDeProcedimentoView : Page
    {
        private ProdutosDeProcedimentoViewModel _produtosDeProcedimentoViewModel { get; set; }

        public ProdutosDeProcedimentoView(ProcedimentoViewModel procedimentoViewModel)
        {
            InitializeComponent();
            _produtosDeProcedimentoViewModel = new ProdutosDeProcedimentoViewModel(procedimentoViewModel);
            DataContext = _produtosDeProcedimentoViewModel;
        }

        private void DGProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGProdutos.Items.IndexOf(DGProdutos.CurrentItem) >= 0)
            {
                _produtosDeProcedimentoViewModel.Selecionar(DGProdutos.Items.IndexOf(DGProdutos.CurrentItem));
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _produtosDeProcedimentoViewModel.InsereListaProdutos();
            FMProcedimento.Navigate(new ProcedimentoView());
        }
    }
}
