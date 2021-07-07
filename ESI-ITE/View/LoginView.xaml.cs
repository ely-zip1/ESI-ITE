using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ESI_ITE.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView: UserControl
    {
        public LoginView( )
        {
            InitializeComponent();
            MyGlobals.loginView = this;
        }

        private void txtPassword_PasswordChanged( object sender, RoutedEventArgs e )
        {
            //var securePass = new SecureString();
            //foreach(char character in ((PasswordBox)sender).Password )
            //{
            //    securePass.AppendChar(character);
            //}

            //viewModel.TxtPassword = securePass;

            viewModel.TxtPassword = txtPassword.Password;

            if ( string.IsNullOrWhiteSpace(txtPassword.Password) && string.IsNullOrWhiteSpace(txtUsername.Text) )
            {
                txtErrorMessage.Text = "";
                btnLogin.IsEnabled = false;
            }
            else if ( string.IsNullOrWhiteSpace(txtPassword.Password) && !string.IsNullOrWhiteSpace(txtUsername.Text) )
            {
                txtErrorMessage.Text = "Password cannot be empty!";
                btnLogin.IsEnabled = false;
            }
            else if ( txtPassword.Password.Length >= 3 && txtUsername.Text.Length >= 3 )
            {
                txtErrorMessage.Text = "";
                btnLogin.IsEnabled = true;
            }
            else
            {
                btnLogin.IsEnabled = false;
                txtErrorMessage.Text = "";
            }
        }

        private void txtUsername_TextChanged( object sender, TextChangedEventArgs e )
        {
            if ( string.IsNullOrWhiteSpace(txtUsername.Text) )
            {
                txtErrorMessage.Text = "Username cannot be empty!";
                btnLogin.IsEnabled = false;
            }
            else if ( txtPassword.Password.Length >= 3 && txtUsername.Text.Length >= 3 )
            {
                txtErrorMessage.Text = "";
                btnLogin.IsEnabled = true;
            }
            else if ( !string.IsNullOrWhiteSpace(txtUsername.Text) )
            {
                txtErrorMessage.Text = "";
                btnLogin.IsEnabled = false;
            }
        }

        private void btnCancel_Click( object sender, RoutedEventArgs e )
        {
            txtUsername.Text = "";
            txtPassword.Password = "";
            txtErrorMessage.Text = "";
        }

        private void UserControl_Loaded( object sender, RoutedEventArgs e )
        {

            txtUsername.Text = "ELI";
            txtPassword.Password = "123";
        }
    }
}
