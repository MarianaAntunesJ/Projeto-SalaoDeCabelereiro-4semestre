﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SalaoDeCabelereiro.View
{
    /// <summary>
    /// Interação lógica para MenuView.xam
    /// </summary>
    public partial class MenuView : Window
    {
        public MenuView()
        {
            InitializeComponent();
        }

        private void ControlaGridCursor(RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);
            GridCursor.Margin = new Thickness(10 + (150 * index), 30, 0, 0);
        }

        private void BtnMenu(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Content = null;
        }

        private void BtnAgenda(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new AgendaView());
        }

        private void BtnClientes(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new ClienteView());
        }

        private void BtnFuncionarios(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new FuncionarioView());
        }

        private void BtnProcedimentos(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new ProcedimentoView());
        }

        private void BtnProdutos(object sender, RoutedEventArgs e)
        {
            ControlaGridCursor(e);
            FrameMain.Navigate(new ProdutoView());
        }
    }
}
