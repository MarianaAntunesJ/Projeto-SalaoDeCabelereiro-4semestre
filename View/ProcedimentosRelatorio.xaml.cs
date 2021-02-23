using SalaoDeCabelereiro.Banco.RelatorioDAL;
using System;
using System.Windows.Controls;


namespace SalaoDeCabelereiro.View
{
    public partial class ProcedimentosRelatorio : Page
    {
        private ProcedimentoRelatorioDAO _produtoRelatorioDAO { get; set; }

        public ProcedimentosRelatorio()
        {
            InitializeComponent();
            _produtoRelatorioDAO = new ProcedimentoRelatorioDAO();
        }

        private void RvProcedimento_Load(object sender, EventArgs e)
        {
            var listaProcedimentos = _produtoRelatorioDAO.Listar();

            var dataSource = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetProcedimentoRelatorio", listaProcedimentos);
            RvProcedimento.LocalReport.DataSources.Add(dataSource);
            RvProcedimento.LocalReport.ReportPath = "..\\..\\Relatorio\\ProcedimentosReport.rdlc";

            RvProcedimento.RefreshReport();
        }
    }
}
