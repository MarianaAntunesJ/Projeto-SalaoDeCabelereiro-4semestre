using Microsoft.Win32;
using SalaoDeCabelereiro.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SalaoDeCabelereiro.View
{
    /// <summary>
    /// Interação lógica para ProdutoView.xam
    /// </summary>
    public partial class ProdutoView : Page
    {
        private ProdutoViewModel _produtoViewModel { get; set; }
        private readonly string _caminhoImagemPadrao = "..\\..\\Imagens\\ImgAqui.PNG";

        public ProdutoView()
        {
            InitializeComponent();
            _produtoViewModel = new ProdutoViewModel();
            DataContext = _produtoViewModel;
            DefinirImagem(_caminhoImagemPadrao);
        }

        private void TxBPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            _produtoViewModel.Consultar(TxBPesquisa.Text);
        }

        private void BtNovoCadastro_Click(object sender, RoutedEventArgs e)
        {
            _produtoViewModel.LimparUsuarioAtual();
            CBAtivo.IsChecked = false;
            DefinirImagem(_caminhoImagemPadrao);
        }

        private void BtSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (_produtoViewModel.Salvar())
            {
                DefinirImagem(_caminhoImagemPadrao);
                MessageBox.Show("Produto salvo!", "Salvo");
            }                
            else
                MessageBox.Show("Produto não foi salvo.", "Erro");
        }

        private void DGProduto_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGProduto.Items.IndexOf(DGProduto.CurrentItem) >= 0)
            {
                _produtoViewModel.Selecionar(DGProduto.Items.IndexOf(DGProduto.CurrentItem));

                if (_produtoViewModel.Produto.Imagem[0] != 0)
                    ImgProduto.Source = ByteToImage(_produtoViewModel.Produto.Imagem);
                else
                    DefinirImagem(_caminhoImagemPadrao);
            }
        }

        private void DefinirImagem(string caminhoImagem)
        {
            ImgProduto.Source = new ImageSourceConverter().ConvertFromString(caminhoImagem) as ImageSource;
        }

        private void ImgProduto_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                FileStream stream = File.OpenRead(dlg.FileName);
                _produtoViewModel.Produto.Imagem = new byte[stream.Length];
                stream.Read(_produtoViewModel.Produto.Imagem, 0, _produtoViewModel.Produto.Imagem.Length);
                stream.Close();
                ImgProduto.Source = ByteToImage(_produtoViewModel.Produto.Imagem);
            }
        }

        public ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            biImg.BeginInit();
            MemoryStream ms = new MemoryStream(imageData);

            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }

        private void DGProduto_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "Imagem")
                e.Column = null;
        }
    }
}
