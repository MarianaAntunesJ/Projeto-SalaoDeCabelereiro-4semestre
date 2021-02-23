using SalaoDeCabelereiro.Banco.RelatorioDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SalaoDeCabelereiro.View
{
    public partial class ClientesRelatorio : Page
    {
        private ClienteRelatorioDAO _clienteRelatorioDAO { get; set; }

        public ClientesRelatorio()
        {
            InitializeComponent();
            _clienteRelatorioDAO = new ClienteRelatorioDAO();
        }

        private void RvCliente_Load(object sender, EventArgs e)
        {
            var listaClientes = _clienteRelatorioDAO.Listar();

            var dataSource = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetClienteRelatorio", listaClientes);
            RvCliente.LocalReport.DataSources.Add(dataSource);
            RvCliente.LocalReport.ReportPath = "..\\..\\Relatorio\\ClientesReport.rdlc";

            RvCliente.RefreshReport();
        }
    }
}
