using SalaoDeCabelereiro.Model;
using SalaoDeCabelereiro.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalaoDeCabelereiro.View
{
    public partial class AgendaView : Page
    {
        private AgendaViewModel _agendaViewModel { get; set; }

        private ObservableCollection<KeyValuePair<int, string>> _funcionarios; 

        public AgendaView() //PareiAqui: verificar se Id do combobox profissional está certo, continuar a implementar a tela agenda, fazer tela pagamento 
        {
            InitializeComponent();
            _agendaViewModel = new AgendaViewModel();
            DataContext = _agendaViewModel;

            _funcionarios = _agendaViewModel.CarregarFuncionarios();
            CBProfissional.DisplayMemberPath = "Value";
            CBProfissional.SelectedValuePath = "Key";
            foreach(var item in _funcionarios)
                CBProfissional.Items.Add(item);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            _agendaViewModel.Consultar(TxBPesquisa.Text);
        }

        private void BtNovoCadastro_Click(object sender, RoutedEventArgs e)
        {
            _agendaViewModel.LimparUsuarioAtual();
            CBAtivo.IsChecked = false;
        }

        private void BtSalvar_Click(object sender, RoutedEventArgs e)
        {
            if(_agendaViewModel.Salvar())
                MessageBox.Show("Procedimento salvo!", "Salvo");
            else
                MessageBox.Show("Procedimento não foi salvo.", "Erro");
        }

        private void DGAgenda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
