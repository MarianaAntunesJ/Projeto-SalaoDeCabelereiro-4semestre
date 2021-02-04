using SalaoDeCabelereiro.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;


namespace SalaoDeCabelereiro.View
{
    public partial class FuncionarioView : Page
    {
        private FuncionarioViewModel _funcionarioViewModel { get; set; }

        public FuncionarioView()
        {
            InitializeComponent();
            _funcionarioViewModel = new FuncionarioViewModel();
            DataContext = _funcionarioViewModel;
        }

        private void TxBPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            _funcionarioViewModel.Consultar(TxBPesquisa.Text);
        }

        private void DGFuncionarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGFuncionarios.Items.IndexOf(DGFuncionarios.CurrentItem) >= 0)
            {
                _funcionarioViewModel.Selecionar(DGFuncionarios.Items.IndexOf(DGFuncionarios.CurrentItem));
            }
            if (_funcionarioViewModel.Funcionario.Profissao.Equals("Gerente"))
                CBProfissão.SelectedIndex = 0;
            else if (_funcionarioViewModel.Funcionario.Profissao.Equals("Recepcionista"))
                CBProfissão.SelectedIndex = 1;
            else if (_funcionarioViewModel.Funcionario.Profissao.Equals("Estoquista"))
                CBProfissão.SelectedIndex = 2;
            else if (_funcionarioViewModel.Funcionario.Profissao.Equals("Cabelereiro"))
                CBProfissão.SelectedIndex = 3;
            else if (_funcionarioViewModel.Funcionario.Profissao.Equals("Manicure"))
                CBProfissão.SelectedIndex = 4;
        }

        private void BtSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (PbSenha.Password == PbConfirmarSenha.Password)
            {
                if (_funcionarioViewModel.Salvar(_funcionarioViewModel.GerarHashMd5(PbSenha.Password)))
                    MessageBox.Show("Cliente salvo!", "Salvo");
                else
                    MessageBox.Show("Cliente não foi salvo.", "Erro");
            }
            else
                MessageBox.Show("Senhas não coincidem", "Erro");
            LimparTela();
        }

        private void BtSair_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtNovoCadastro_Click(object sender, RoutedEventArgs e)
        {
            _funcionarioViewModel.LimparUsuarioAtual();
            CBAtivo.IsChecked = false;
            LimparTela();
        }

        private void LimparTela()
        {
            PbSenha.Password = null;
            PbConfirmarSenha.Password = null;            
        }
    }
}
