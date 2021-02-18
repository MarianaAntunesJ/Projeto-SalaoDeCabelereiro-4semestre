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
        private static string _selectFrom = "SELECT * FROM";
        private static string _update = "UPDATE";
        public static string _values = "VALUES";
        public static string _set = "SET";

        public static string GetInsertInto(string tabela) => $"{_insertInto} {tabela} OUTPUT INSERTED.Id {_values}";
        public static string GetSelectFrom(string tabela) => $"{_selectFrom} {tabela}";
        public static string GetUpdateSet(string tabela) => $"{_update} {tabela} {_set}";
    }
}
