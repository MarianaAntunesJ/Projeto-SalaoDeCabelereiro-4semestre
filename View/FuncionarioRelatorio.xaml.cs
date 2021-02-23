using System.Windows.Controls;
using System;
using SalaoDeCabelereiro.Banco.RelatorioDAL;

namespace SalaoDeCabelereiro.View
{
    public partial class FuncionarioRelatorio : Page
    {
        private FuncionarioRelatorioDAO _funcionarioRelatorioDAO { get; set; }

        public FuncionarioRelatorio()
        {
            InitializeComponent();
            _funcionarioRelatorioDAO = new FuncionarioRelatorioDAO();
        }

        private void RvFuncionario_Load(object sender, EventArgs e)
        {
            var listaFuncionario = _funcionarioRelatorioDAO.Listar();

            var dataSource = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetFuncionarioRelatorio", listaFuncionario);
            RvFuncionario.LocalReport.DataSources.Add(dataSource);
            RvFuncionario.LocalReport.ReportPath = "..\\..\\Relatorio\\FuncionariosReport.rdlc";

            RvFuncionario.RefreshReport();
        }
    }
}
