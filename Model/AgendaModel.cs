﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoDeCabelereiro.Model
{
    class AgendaModel
    {
        public int Id { get; set; }
        public ClienteModel Cliente { get; set; }
        public FuncionarioModel Funcionario { get; set; }
        public ProcedimentoModel Procedimento { get; set; }
        public DateTime Horario { get; set; }
        public bool Ativo { get; set; }

        public AgendaModel()
        {
        }

        public AgendaModel(int id, ClienteModel cliente, FuncionarioModel funcionario, ProcedimentoModel procedimento, DateTime horario, bool ativo)
        {
            Id = id;
            Cliente = cliente;
            Funcionario = funcionario;
            Procedimento = procedimento;
            Horario = horario;
            Ativo = ativo;
        }
    }
}
