using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SalaoDeCabelereiro.View
{
    public partial class MenuView : Window
    {


        public MenuView(string profissaoUsuario)
        {
            InitializeComponent();
            if (profissaoUsuario == "Recepcionista")
            {
                BtnFuncionarios.Visibility = Visibility.Hidden;
                BtnProcedimentos.Visibility = Visibility.Hidden;
                BtnProdutos.Visibility = Visibility.Hidden;
            }

            else if (profissaoUsuario == "Estoquista")
            {
                BtnFuncionarios.Visibility = Visibility.Hidden;
                BtnProcedimentos.Visibility = Visibility.Hidden;
                BtnClientes.Visibility = Visibility.Hidden;
                BtnAgenda.Visibility = Visibility.Hidden;
            }

            else if (profissaoUsuario == "ProfissionalBeleza")
            {
                BtnFuncionarios.Visibility = Visibility.Hidden;
                BtnProcedimentos.Visibility = Visibility.Hidden;
                BtnClientes.Visibility = Visibility.Hidden;
                BtnAgenda.Visibility = Visibility.Hidden;
            }

            FrameMain.Navigate(new PrincipalView());
        }

        public MenuView()
        {
        }

        private void ControlaGridCursor(RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            GridCursor.Margin = new Thickness(10 + (150 * index), 30, 0, 0);
        }

        private void BtnMenu_Click(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new PrincipalView());
        }

        private void BtnAgenda_Click(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new AgendaView());
        }

        private void BtnClientes_Click(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new ClienteView());
        }

        private void BtnFuncionarios_Click(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new FuncionarioView());
        }

        private void BtnProcedimentos_Click(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new ProcedimentoView());
        }

        private void BtnProdutos_Click(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new ProdutoView());
        }

        private void BtnRelatorios_Click(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new Relatorios());
        }
    }
}
