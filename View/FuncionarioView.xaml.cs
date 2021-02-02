using SalaoDeCabelereiro.ViewModel;
using System;
using System.Collections.Generic;
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
    public partial class FuncionarioView : Page
    {
        private FuncionarioViewModel _funcionarioViewModel { get; set; }

        public FuncionarioView()
        {
            InitializeComponent();
            _funcionarioViewModel = new FuncionarioViewModel();
            DataContext = _funcionarioViewModel;
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
            else if (e.PropertyName == "Imagem")
                e.Column.Visibility = Visibility.Collapsed;
        }


        private void TxBPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DGFuncionarios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGFuncionarios.Items.IndexOf(DGFuncionarios.CurrentItem) >= 0)
            {

            }
        }
    }
}
