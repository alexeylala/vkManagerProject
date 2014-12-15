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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace vkManageFinal
{
    /// <summary>
    /// Interaction logic for SendMessage.xaml
    /// </summary>
    public partial class SendMessage : Window
    {
        string uid;
        string userName;

        public SendMessage(string UserId, string Name)
        {

            InitializeComponent();
            uid = UserId;
            userName = Name;
            UserName.Text = ("(" + uid + ") - " + userName);

        }

        private void MessageWindow_KeyDown(object sender, KeyEventArgs e)
        {
            
            if(e.Key == Key.Enter){
                SendMessage_Click_1(null, null);
            }//if

            else if(e.Key == Key.Escape){
                this.Close();
            }//else
        }

        private void SendMessage_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {

                SystemInfo.vkPluginReference.SendMessageToUser(long.Parse(uid), Message.Text);
                DoubleAnimation apear = new DoubleAnimation();
                apear.From = 0;
                apear.To = 1;
                apear.Duration = TimeSpan.FromSeconds(1);
                apear.DecelerationRatio = 0.8;

                apear.Completed += (f, t) =>
                {

                    DoubleAnimation disapear = new DoubleAnimation();
                    disapear.From = 1;
                    disapear.To = 0;
                    disapear.Duration = TimeSpan.FromSeconds(1.5);

                    SuccessSend.BeginAnimation(OpacityProperty, disapear);
                };

                SuccessSend.BeginAnimation(OpacityProperty, apear);


            }//try

            catch (Exception ex)
            {

                MessageBox.Show("Не можливо відправити повідомлення!");

            }//catch
        }

    }
}
