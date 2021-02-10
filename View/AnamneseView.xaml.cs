using System.Windows;
using System.Windows.Controls;


namespace SalaoDeCabelereiro.View
{
    /// <summary>
    /// Interação lógica para AnamneseView.xam
    /// </summary>
    public partial class AnamneseView : Page
    {
        public AnamneseView()
        {
            InitializeComponent();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RbCaracteristicaSim_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSair(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}
