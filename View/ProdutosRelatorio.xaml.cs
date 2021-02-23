using SalaoDeCabelereiro.Banco.RelatorioDAL;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SalaoDeCabelereiro.View
{
    public partial class ProdutosRelatorio : Page
    {
        private ProdutoRelatorioDAO _produtoRelatorioDAO { get; set; }

        public ProdutosRelatorio()
        {
            InitializeComponent();
            _produtoRelatorioDAO = new ProdutoRelatorioDAO();
        }

        private void RvProduto_Load(object sender, EventArgs e)
        {
            var listaProdutos = _produtoRelatorioDAO.Listar();

            var dataSource = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetProdutoRelatorio", listaProdutos);
            RvProduto.LocalReport.DataSources.Add(dataSource);
            RvProduto.LocalReport.ReportPath = "..\\..\\Relatorio\\ProdutosReport.rdlc";

            RvProduto.RefreshReport();
        }
    }
}
