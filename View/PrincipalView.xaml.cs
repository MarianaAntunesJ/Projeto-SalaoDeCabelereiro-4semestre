using System;
using System.Windows.Controls;

namespace SalaoDeCabelereiro.View
{
    /// <summary>
    /// Interação lógica para PrincipalView.xam
    /// </summary>
    public partial class PrincipalView : Page
    {
        public PrincipalView()
        {
            InitializeComponent();

            LbTime.Content = DateTime.Now.ToShortDateString();
        }
    }
}
