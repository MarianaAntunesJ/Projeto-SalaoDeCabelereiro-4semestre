using SalaoDeCabelereiro.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace SalaoDeCabelereiro.View
{
    public partial class ProcedimentoView : Page
    {
        public object NavigationCacheMode { get; }
        private ProcedimentoViewModel _procedimentoViewModel { get; set; }
        private ProdutosDeProcedimentoView _produtosDeProcedimentoView;

        public ProcedimentoView()
        {
            InitializeComponent();
            _procedimentoViewModel = new ProcedimentoViewModel();
            DataContext = _procedimentoViewModel;
        }

        private void BtnProdutos_Click(object sender, RoutedEventArgs e)
        {
            if (_produtosDeProcedimentoView == null)
                _produtosDeProcedimentoView = new ProdutosDeProcedimentoView(_procedimentoViewModel, this);
            NavigationService.Navigate(_produtosDeProcedimentoView);
        }

        private void TxBPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            _procedimentoViewModel.Consultar(TxBPesquisa.Text);
        }

        private void BtNovoCadastro_Click(object sender, RoutedEventArgs e)
        {
            _procedimentoViewModel.LimparUsuarioAtual();
            CBAtivo.IsChecked = false;
        }

        private void BtSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (_procedimentoViewModel.Salvar())
                MessageBox.Show("Procedimento salvo!", "Salvo");
            else
                MessageBox.Show("Procedimento não foi salvo.", "Erro");
        }

        private void DGProcedimentos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGProcedimentos.Items.IndexOf(DGProcedimentos.CurrentItem) >= 0)
            {
                _procedimentoViewModel.Selecionar(DGProcedimentos.Items.IndexOf(DGProcedimentos.CurrentItem));
            }
            if (_procedimentoViewModel.Procedimento.AreaProfissional.Equals("Cabelereiro"))
                CBProfissão.SelectedIndex = 0;
            else if (_procedimentoViewModel.Procedimento.AreaProfissional.Equals("Manicure"))
                CBProfissão.SelectedIndex = 1;
        }

        private void DGProcedimentos_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Produtos")
                e.Column = null;
        }
    }
}
