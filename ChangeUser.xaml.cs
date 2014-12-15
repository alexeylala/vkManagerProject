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

namespace vkManageFinal
{
    /// <summary>
    /// Interaction logic for ChangeUser.xaml
    /// </summary>
    public partial class ChangeUser : Window
    {
        public ChangeUser()
        {
            InitializeComponent();
        }

        private void Authorize_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                CurrentUser.UserEmail = UserName.Text;
                CurrentUser.UserPassword = UserPassword.Password;

                this.Close();

            }//try
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
                this.Close();

            }   //catch
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Authorize_Click_1(null, null);

            }//if
            else if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
    }
}
