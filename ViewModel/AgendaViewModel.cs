using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SalaoDeCabelereiro.ViewModel
{
    class AgendaViewModel
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

        public bool Salvar()
        {
            bool sucesso;
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
            Agendamento = new AgendaModel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }
    }
}
