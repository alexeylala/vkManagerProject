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
using VkNet.Enums.Filters;

namespace vkManageFinal
{
    /// <summary>
    /// Interaction logic for FilterPanel.xaml
    /// </summary>
    public partial class FilterPanel : Window
    {
        public FilterPanel()
        {
            InitializeComponent();

            foreach (string country in SystemInfo.Counties.Values) {

                Countries.Items.Add(country);
            
            }//foreach

        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter){

                List<MyUser> resultUsers = new List<MyUser>();

                //Пол мужской
                if (SearchInfo.ManGender)
                {
                    resultUsers = UsersResulSet.myUsers.Where(user => user.Gender.Equals("Male")).ToList();

                }//if
                else {//женский
                    
                    resultUsers = UsersResulSet.myUsers.Where(user => user.Gender.Equals("Female")).ToList();

                }//else

                if(SearchInfo.YearFrom != -1){//Если выделено по возрасту от 
                    resultUsers = resultUsers.Where(user => 
                    {
                        try
                        {
                            int number = int.Parse(user.Age) ;
                            return number >= SearchInfo.YearFrom;
                        }//try
                        catch(Exception){
                            return false;
                        }//catch

                    }).ToList();
                    
                    SearchInfo.YearFrom = -1;

                }//if

                if (SearchInfo.YearTo != -1){//Если выделено по возрасту до

                    resultUsers = resultUsers.Where(user => {
                        try
                        {
                            int number = int.Parse(user.Age);
                            return number <= SearchInfo.YearTo;
                        }//try
                        catch (Exception)
                        {
                            return false;

                        }//catch

                    }).ToList();
                    
                    SearchInfo.YearTo = -1;

                }//if

                if(SearchInfo.SelectedCountry != String.Empty){//Если выбрана страна

                    resultUsers = resultUsers.Where(user =>user.Country.Equals(SearchInfo.SelectedCountry)).ToList();

                    SearchInfo.SelectedCountry = String.Empty;

                }//if

                if (SearchInfo.SelectedCity != String.Empty)
                {//Если выбрана страна

                    resultUsers = resultUsers.Where(user => user.City.Equals(SearchInfo.SelectedCity)).ToList();
                    SearchInfo.SelectedCity = String.Empty;

                }//if

                SearchInfo.FilteredUsers = new List<MyUser>();

                SearchInfo.FilteredUsers.AddRange(resultUsers);

                this.Close();

            }//if
            else if (e.Key == Key.Escape) {
                this.Close();
            }
        }

        private void Gender_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Gender.SelectedIndex == 0)
                SearchInfo.ManGender = true;

            else if(Gender.SelectedIndex == 1)
                SearchInfo.ManGender = false;
        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            try{

                SearchInfo.YearFrom = int.Parse(YearFrom.Text);
            }

            catch(Exception){

                MessageBox.Show("Можливо ви ввели не цифру! Перевірте введений рік");
                try { YearFrom.Text = YearFrom.Text.Remove(YearFrom.Text.Length - 1); }
                catch(Exception){ }

            }//catch

        }

        private void YearTo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                SearchInfo.YearTo = int.Parse(YearTo.Text);
            }//try

            catch (Exception)
            {
                MessageBox.Show("Можливо ви ввели не цифру! Перевірте введений рік");
                try { YearTo.Text = YearTo.Text.Remove(YearTo.Text.Length - 1); }
                catch (Exception) { }

            }//catch
        }

        private void YearBirthdayFrom_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                SearchInfo.YearBirthdayYear = int.Parse(YearBirthdayFrom.Text);

            }//try

            catch (Exception)
            {
                MessageBox.Show("Можливо ви ввели не цифру! Перевірте введений рік");
                try { YearBirthdayFrom.Text = YearBirthdayFrom.Text.Remove(YearBirthdayFrom.Text.Length - 1); }
                catch (Exception) { }

            }//catch
        }

        private void Month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Month.SelectedIndex != 0){
                SearchInfo.YearBirthdayMonth = Month.SelectedIndex;
            }//if
        }

        private void Countries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Countries.SelectedIndex != 0){

                int index = Countries.SelectedIndex;
                KeyValuePair<int,string> pair = SystemInfo.Counties.ElementAt(index-1);
                SearchInfo.SelectedCountry = pair.Value;

                SystemInfo.Cities = SystemInfo.vkPluginReference.GetCities(pair.Key);
                Cities.Items.Clear();

                foreach (string city in SystemInfo.Cities) {
                    Cities.Items.Add(city);
                }

            }//if
        }

        private void Cities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Cities.SelectedIndex != 0 && Cities.SelectedIndex != -1 && Cities.SelectedItem != null)
            {
                try { SearchInfo.SelectedCity = Cities.SelectedValue.ToString(); }
                catch { }

            }//if
            
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            List<MyUser> resultUsers = new List<MyUser>();

            //Пол мужской
            if (SearchInfo.ManGender)
            {
                resultUsers = UsersResulSet.myUsers.Where(user => user.Gender.Equals("Male")).ToList();

            }//if
            else
            {//женский

                resultUsers = UsersResulSet.myUsers.Where(user => user.Gender.Equals("Female")).ToList();

            }//else

            if (SearchInfo.YearFrom != -1)
            {//Если выделено по возрасту от 
                resultUsers = resultUsers.Where(user =>
                {
                    try
                    {
                        int number = int.Parse(user.Age);
                        return number >= SearchInfo.YearFrom;
                    }//try
                    catch (Exception)
                    {
                        return false;
                    }//catch

                }).ToList();

                SearchInfo.YearFrom = -1;

            }//if

            if (SearchInfo.YearTo != -1)
            {//Если выделено по возрасту до

                resultUsers = resultUsers.Where(user =>
                {
                    try
                    {
                        int number = int.Parse(user.Age);
                        return number <= SearchInfo.YearTo;
                    }//try
                    catch (Exception)
                    {
                        return false;

                    }//catch

                }).ToList();

                SearchInfo.YearTo = -1;

            }//if

            if (SearchInfo.SelectedCountry != String.Empty)
            {//Если выбрана страна

                resultUsers = resultUsers.Where(user => user.Country.Equals(SearchInfo.SelectedCountry)).ToList();

                SearchInfo.SelectedCountry = String.Empty;

            }//if

            if (SearchInfo.SelectedCity != String.Empty)
            {//Если выбрана страна

                resultUsers = resultUsers.Where(user => user.City.Equals(SearchInfo.SelectedCity)).ToList();
                SearchInfo.SelectedCity = String.Empty;

            }//if

            SearchInfo.FilteredUsers = new List<MyUser>();

            SearchInfo.FilteredUsers.AddRange(resultUsers);

            this.Close();

        }

    }
}

