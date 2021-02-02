using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaoDeCabelereiro.Banco
{
    public static class ConsultaHelper
    {
        private static string _insertInto = "INSERT INTO";
        public static string _values = "VALUES";

        public static string GetInsertInto(string tabela) => $"{_insertInto} {tabela} {_values}";
    }
}
