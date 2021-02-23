using SalaoDeCabelereiro.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace SalaoDeCabelereiro.View
{
    public partial class AgendaView : Page
    {
        private AgendaViewModel _agendaViewModel { get; set; }

        private ObservableCollection<KeyValuePair<int, string>> _funcionarios;
        private ObservableCollection<KeyValuePair<int, string>> _procedimentos;
        private ObservableCollection<KeyValuePair<int, string>> _clientes;

        public AgendaView() 
        {
            InitializeComponent();
            _agendaViewModel = new AgendaViewModel();
            DataContext = _agendaViewModel;

            _funcionarios = _agendaViewModel.CarregarFuncionarios();
            CBProfissional.DisplayMemberPath = "Value";
            CBProfissional.SelectedValuePath = "Key";

            foreach (var item in _funcionarios)
                CBProfissional.Items.Add(item);

            _procedimentos = _agendaViewModel.CarregarProcedimentos();
            CBProcedimento.DisplayMemberPath = "Value";
            CBProcedimento.SelectedValuePath = "Key";

            foreach (var item in _procedimentos)
                CBProcedimento.Items.Add(item);

            _clientes = _agendaViewModel.CarregarClientes();
            CBNomeDoCliente.DisplayMemberPath = "Value";
            CBNomeDoCliente.SelectedValuePath = "Key";

            foreach (var item in _clientes)
                CBNomeDoCliente.Items.Add(item);
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
            int clienteId = int.Parse(CBNomeDoCliente.SelectedValue.ToString());
            int profissionalId = int.Parse(CBProfissional.SelectedValue.ToString());
            int procedimentoId = int.Parse(CBProcedimento.SelectedValue.ToString());
            if (_agendaViewModel.Salvar(clienteId, profissionalId, procedimentoId))
                MessageBox.Show("Agendamento salvo!", "Salvo");
            else
                MessageBox.Show("Agendamento não foi salvo.", "Erro");
        }

        private void DGAgenda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGAgenda.Items.IndexOf(DGAgenda.CurrentItem) >= 0)
            {
                _agendaViewModel.Selecionar(DGAgenda.Items.IndexOf(DGAgenda.CurrentItem));

                int contador1 = 0;
                for (int i = 1; i <= 17; i++)
                {
                    if (_agendaViewModel.Agendamento.Horas.Equals($"{i}"))
                        CbHoras.SelectedIndex = contador1;
                    contador1++;
                }
                int j = 0;
                int contador = 0;
                while (j <= 55)
                {
                    if (_agendaViewModel.Agendamento.Minutos.Equals($"{j}"))
                    {
                        CbHoras.SelectedIndex = contador;
                        break;
                    }
                    j += 5;
                    contador++;
                }
            }
        }

        private void DGAgenda_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyType == typeof(DateTime))
                (e.Column as DataGridTextColumn).Binding.StringFormat = "dd/MM/yyyy";
            if (e.PropertyName == "Funcionario" || e.PropertyName == "Procedimento")
                e.Column = null;
        }
    }
}
