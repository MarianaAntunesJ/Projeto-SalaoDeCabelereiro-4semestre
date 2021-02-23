using SalaoDeCabelereiro.Banco;
using SalaoDeCabelereiro.ViewModel;
using System.Windows;

namespace SalaoDeCabelereiro.View
{
    public partial class Login : Window
    {
        private LoginViewModel _loginViewModel { get; set; } = new LoginViewModel();

        public Login()
        {
            InitializeComponent();
        }

        private void BtEntrar_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TxBUsuario.Text) && !string.IsNullOrEmpty(PBSenha.Password))
            {
                var profissaoUsuario = _loginViewModel.LoginValido(TxBUsuario.Text, FuncionarioDAO.GerarHashMd5(PBSenha.Password));
                if (profissaoUsuario != null)
                {
                    MenuView menuView = new MenuView(profissaoUsuario);
                    menuView.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuário e/ou senha inválida. Tente novamente", "Login Inválido");
                }
            }
            else
            {
                MessageBox.Show("Usuário e/ou senha não preenchida. Tente novamente", "Login Inválido");
            }
        }
    }
}
