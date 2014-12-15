﻿using System;
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
    /// Interaction logic for SelectMembersFromGroup.xaml
    /// </summary>
    public partial class SelectMembersFromGroup : Window
    {
        public SelectMembersFromGroup()
        {
            InitializeComponent();
        }

        private void ExecuteSearch_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                SystemInfo.GroupID_Search = long.Parse(GroupId.Text);
                this.Close();

            }//try
            catch (Exception ex)
            {
                try
                {

                    SystemInfo.GroupScreenName_Search = GroupId.Text;
                    this.Close();
                }//try

                catch (Exception excep)
                {
                    MessageBox.Show(excep.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);

                }//catch


            }//catch
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                try
                {

                    SystemInfo.GroupID_Search = long.Parse(GroupId.Text);
                    this.Close();

                }//try
                catch (Exception ex)
                {
                    try
                    {

                        SystemInfo.GroupScreenName_Search = GroupId.Text;
                        this.Close();
                    }//try

                    catch (Exception excep)
                    {
                        MessageBox.Show(excep.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);

                    }//catch


                }//catch

            }//if

            else if (e.Key == Key.Escape)
            {

                this.Close();

            }//else if

        }
    }
}
