using SalaoDeCabelereiro.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SalaoDeCabelereiro.Banco
{
    class AgendaDAO
    {


        private readonly string _tabela = "Agenda";
        private Conexao Conexao { get; set; }
        private SqlCommand Cmd { get; set; }

        public AgendaDAO()
        {
            Conexao = new Conexao();
            Cmd = new SqlCommand();
        }

        private void GetConexao()
        {
            Cmd.Connection = Conexao.RetornarConexao();
        }

        private bool DadosAgendamento(AgendaModel agendamento)
        {
            GetConexao();
            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@Cliente", agendamento.Cliente.Id);
            Cmd.Parameters.AddWithValue("@Funcionario", agendamento.Funcionario.Id);
            Cmd.Parameters.AddWithValue("@Procedimento", agendamento.Procedimento.Id);
            Cmd.Parameters.AddWithValue("@Data", Convert.ToDateTime(agendamento.Data).ToShortDateString());
            Cmd.Parameters.AddWithValue("@Horas", agendamento.Horas);
            Cmd.Parameters.AddWithValue("@Minutos", agendamento.Minutos);
            Cmd.Parameters.AddWithValue("@Ativo", true);

            if (Cmd.ExecuteNonQuery() == 1)
                return true;
            else
                return false;
        }

        public bool Inserir(AgendaModel agendamento)
        {
            Cmd.CommandText = $@"{ConsultaHelper.GetInsertInto(_tabela)} (@Cliente, @Funcionario, @Procedimento, @Data, @Horas, @Minutos, @Ativo)";
            return DadosAgendamento(agendamento);
        }

        private List<AgendaModel> GetProduto()
        {
            SqlDataReader rd = Cmd.ExecuteReader();
            List<AgendaModel> agendamentos = new List<AgendaModel>();

            while (rd.Read())
            {
                AgendaModel agendamento = new AgendaModel(
                        (int)rd[nameof(AgendaModel.Id)],
                        new ClienteModel() { Id = (int)rd[nameof(AgendaModel.Cliente)] },
                        new FuncionarioModel() { Id = (int)rd[nameof(AgendaModel.Funcionario)] },
                        new ProcedimentoModel() { Id = (int)rd[nameof(AgendaModel.Procedimento)] },
                        (DateTime)rd[nameof(AgendaModel.Data)],
                        (string)rd[nameof(AgendaModel.Horas)],
                        (string)rd[nameof(AgendaModel.Minutos)],
                        (bool)rd[nameof(AgendaModel.Ativo)]);

                agendamentos.Add(agendamento);
            }
            rd.Close();
            return agendamentos;
        }

        public List<AgendaModel> Listar()
        {
            GetConexao();
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)}";

            return GetProduto();
        }

        public List<AgendaModel> Consultar(string busca)
        {
            GetConexao();
            busca = $"%{busca}%";
            Cmd.CommandText = $"{ConsultaHelper.GetSelectFrom(_tabela)} WHERE Id LIKE @busca OR Cliente LIKE @busca";

            Cmd.Parameters.Clear();
            Cmd.Parameters.AddWithValue("@busca", busca);

            return GetProduto();
        }

        public bool Atualizar(AgendaModel agendamento)
        {
            GetConexao();
            Cmd.CommandText = $@"{ConsultaHelper.GetUpdateSet(_tabela)} Cliente = @Cliente, Funcionario = @Funcionario, Procedimento = @Procedimento, Data = @Data, Horas = @Horas, Minutos = @Minutos, Ativo = @Ativo  WHERE Id = @id";

            return DadosAgendamento(agendamento);
        }
    }
}
