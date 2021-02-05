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
    /// <summary>
    /// Interação lógica para ListaProdutosView.xam
    /// </summary>
    public partial class ListaProdutosView : Page
    {
        private ProdutoViewModel _produtoViewModel { get; set; }

        public ListaProdutosView()
        {
            InitializeComponent();
            _produtoViewModel = new ProdutoViewModel();
            DataContext = _produtoViewModel;
        }

        private void DGProdutos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGProdutos.Items.IndexOf(DGProdutos.CurrentItem) >= 0)
            {
                _produtoViewModel.Selecionar(DGProdutos.Items.IndexOf(DGProdutos.CurrentItem));
            }
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
