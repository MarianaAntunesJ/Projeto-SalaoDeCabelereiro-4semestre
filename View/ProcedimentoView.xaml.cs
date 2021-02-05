using SalaoDeCabelereiro.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace SalaoDeCabelereiro.View
{
    /// <summary>
    /// Interação lógica para ProcedimentoView.xam
    /// </summary>
    public partial class ProcedimentoView : Page
    {
        private ProcedimentoViewModel _procedimentoViewModel { get; set; }

        public ProcedimentoView()
        {
            InitializeComponent();
            _procedimentoViewModel = new ProcedimentoViewModel();
            DataContext = _procedimentoViewModel;
        }

        private void BtnProdutos_Click(object sender, RoutedEventArgs e)
        {
            FMProdutos.Visibility = Visibility.Visible;
            FMProdutos.Content = new ListaProdutosView();
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
            if (_procedimentoViewModel.Procedimento.AreaProfissional.Equals("Gerente"))
                CBProfissão.SelectedIndex = 0;
            else if (_procedimentoViewModel.Procedimento.AreaProfissional.Equals("Recepcionista"))
                CBProfissão.SelectedIndex = 1;
            else if (_procedimentoViewModel.Procedimento.AreaProfissional.Equals("Estoquista"))
                CBProfissão.SelectedIndex = 2;
            else if (_procedimentoViewModel.Procedimento.AreaProfissional.Equals("Cabelereiro"))
                CBProfissão.SelectedIndex = 3;
            else if (_procedimentoViewModel.Procedimento.AreaProfissional.Equals("Manicure"))
                CBProfissão.SelectedIndex = 4;
        }
    }
}
