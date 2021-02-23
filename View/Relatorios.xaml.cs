using SalaoDeCabelereiro.Relatorio;
using System.Windows;
using System.Windows.Controls;


namespace SalaoDeCabelereiro.View
{
    public partial class Relatorios : Page
    {
        public Relatorios()
        {
            InitializeComponent();
        }

        private void Produtos_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new ProdutosRelatorio());
        }

        private void Funcionarios_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new FuncionarioRelatorio());
        }

        private void Procedimentos_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new ProcedimentosRelatorio());
        }

        private void Clientes_Click(object sender, RoutedEventArgs e)
        {
            FrameMain.Navigate(new ClientesRelatorio());
        }
    }
}
