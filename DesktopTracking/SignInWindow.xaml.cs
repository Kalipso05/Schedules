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

namespace DesktopTracking
{
    /// <summary>
    /// Interaction logic for SignInWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();
        }

        private async void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var login = txbUsername.Text;
                var password = psbPassword.Password;

                AuthService authService = new AuthService();
                bool isAuthenticated = await authService.AuthenticateUserAsync(login, password);

                if (isAuthenticated)
                {
                    MainWindow window = new MainWindow();
                    window.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Authorization failed!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
