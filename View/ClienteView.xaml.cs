using SalaoDeCabelereiro.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SalaoDeCabelereiro.View
{
    /// <summary>
    /// Interação lógica para ClienteView.xam
    /// </summary>
    public partial class ClienteView : Page
    {
        private ClienteViewModel _clienteViewModel { get; set; }


        public ClienteView()
        {
            InitializeComponent();
            _clienteViewModel = new ClienteViewModel();
            DataContext = _clienteViewModel;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
        }

        private void BtnAnamnese(object sender, RoutedEventArgs e)
        {
            /*SecondFrame.Visibility = Visibility.Visible;
            SecondFrame.Content = new AnamneseView();*/
        }

        private void TxBPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            _clienteViewModel.Consultar(TxBPesquisa.Text);
        }

        private void BtNovoCadastro_Click(object sender, RoutedEventArgs e)
        {
            _clienteViewModel.LimparUsuarioAtual();
            CBAtivo.IsChecked = false;
        }

        private void BtSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (_clienteViewModel.Salvar())
                MessageBox.Show("Cliente salvo!", "Salvo");
            else
                MessageBox.Show("Cliente não foi salvo.", "Erro");
        }

        private void BtSair00_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DGClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGClientes.Items.IndexOf(DGClientes.CurrentItem) >= 0)
            {
                _clienteViewModel.Selecionar(DGClientes.Items.IndexOf(DGClientes.CurrentItem));
            }
            if (_clienteViewModel.Cliente.Sexo.Equals("Feminino"))
                CBSexo.SelectedIndex = 0;
            else if (_clienteViewModel.Cliente.Sexo.Equals("Masculino"))
                CBSexo.SelectedIndex = 1;
            else if (_clienteViewModel.Cliente.Sexo.Equals("Outro"))
                CBSexo.SelectedIndex = 2;
        }
    }
}
