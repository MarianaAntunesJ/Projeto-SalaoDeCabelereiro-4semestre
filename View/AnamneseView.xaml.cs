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
