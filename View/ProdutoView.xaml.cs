using SalaoDeCabelereiro.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace SalaoDeCabelereiro.View
{
    /// <summary>
    /// Interação lógica para ProdutoView.xam
    /// </summary>
    public partial class ProdutoView : Page
    {
        private ProdutoViewModel _produtoViewModel { get; set; }

        public ProdutoView()
        {
            InitializeComponent();
            _produtoViewModel = new ProdutoViewModel();
            DataContext = _produtoViewModel;
        }

        private void TxBPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            _produtoViewModel.Consultar(TxBPesquisa.Text);
        }

        private void BtNovoCadastro_Click(object sender, RoutedEventArgs e)
        {
            _produtoViewModel.LimparUsuarioAtual();
            CBAtivo.IsChecked = false;
        }

        private void BtSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (_produtoViewModel.Salvar())
                MessageBox.Show("Produto salvo!", "Salvo");
            else
                MessageBox.Show("Produto não foi salvo.", "Erro");
        }

        private void DGProduto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGProduto.Items.IndexOf(DGProduto.CurrentItem) >= 0)
            {
                _produtoViewModel.Selecionar(DGProduto.Items.IndexOf(DGProduto.CurrentItem));
            }
        }
    }
}
