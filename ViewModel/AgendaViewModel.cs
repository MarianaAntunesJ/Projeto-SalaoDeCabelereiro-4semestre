using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SalaoDeCabelereiro.ViewModel
{
    class AgendaViewModel : INotifyPropertyChanged
    {

        private AgendaModel _agendamento { get; set; }
        private AgendaDAO _agendamentoDAO;

        private ObservableCollection<AgendaModel> _agendamentos { get; set; }

        public AgendaModel Agendamento
        {
            get { return _agendamento; }
            set { _agendamento = value; OnPropertyChanged("Agendamento"); }
        }

        public ObservableCollection<AgendaModel> Agendamentos
        {
            get { return _agendamentos; }
            set { _agendamentos = value; OnPropertyChanged("Agendamentos"); }
        }

        public AgendaViewModel()
        {
            LimparUsuarioAtual();
            _agendamentoDAO = new AgendaDAO();
            AtualizarLista();
        }

        public ObservableCollection<KeyValuePair<int, string>> CarregarFuncionarios()
        {
            var chavesValoresFuncionarios = new ObservableCollection<KeyValuePair<int, string>>();
            FuncionarioDAO funcionarioDAO = new FuncionarioDAO();
            var funcionarios = funcionarioDAO.Listar();
            foreach (FuncionarioModel item in funcionarios)
                chavesValoresFuncionarios.Add(new KeyValuePair<int, string>(item.Id, item.Nome));
            return chavesValoresFuncionarios;
                
        }

        public ObservableCollection<KeyValuePair<int, string>> CarregarClientes()
        {
            var chavesValoresClientes = new ObservableCollection<KeyValuePair<int, string>>();
            ClienteDAO clienteDAO = new ClienteDAO();
            var clientes = clienteDAO.Listar();
            foreach (ClienteModel item in clientes)
                chavesValoresClientes.Add(new KeyValuePair<int, string>(item.Id, item.Nome));
            return chavesValoresClientes;
        }

        public ObservableCollection<KeyValuePair<int, string>> CarregarProcedimentos()
        {
            var chavesValoresProcedimentos = new ObservableCollection<KeyValuePair<int, string>>();
            ProcedimentoDAO procedimentoDAO = new ProcedimentoDAO();
            var procedimentos = procedimentoDAO.Listar();
            foreach (ProcedimentoModel item in procedimentos)
                chavesValoresProcedimentos.Add(new KeyValuePair<int, string>(item.Id, item.Nome));
            return chavesValoresProcedimentos;

        }

        public bool Salvar(int clienteId, int profissionalId, int procedimentoId)
        {
            bool sucesso;
            _agendamento.Cliente = new ClienteModel() { Id = clienteId };
            _agendamento.Funcionario = new FuncionarioModel() { Id = profissionalId };
            _agendamento.Procedimento = new ProcedimentoModel() { Id = procedimentoId };
            if (_agendamento.Id == 0)
                sucesso = _agendamentoDAO.Inserir(_agendamento);
            else
                sucesso = _agendamentoDAO.Atualizar(_agendamento);
            if (sucesso)
            {
                AtualizarLista();
                LimparUsuarioAtual();
                return true;
            }
            return false;
        }

        private void AtualizarLista()
        {
            Agendamentos = new ObservableCollection<AgendaModel>(_agendamentoDAO.Listar());
        }

        public void Consultar(string busca)
        {
            Agendamentos = new ObservableCollection<AgendaModel>(_agendamentoDAO.Consultar(busca));
        }

        public void Selecionar(int index)
        {
            Agendamento = Agendamentos[index];
        }

        public void LimparUsuarioAtual()
        {
            Agendamento = new AgendaModel { Data = DateTime.Today };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}
