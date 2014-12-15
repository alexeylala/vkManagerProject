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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Plugin;
using System.Threading;
using VkNet;
using VkNet.Model;
using System.Net;
using System.Windows.Media.Animation;
using Microsoft.Win32;
using WPFLocalizeExtension.Engine;
using System.Globalization;
using vkManageFinal.Properties;

namespace vkManageFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoadToXML loader = new LoadToXML();

        private int MouseY = 0;
        LoadToXML XmlLoader = new LoadToXML();

        public MainWindow()
        {

            InitializeComponent();

            try
            {

                CurrentUser.UserEmail = "osborn107@mail.ru";
                CurrentUser.UserPassword = "12qwaszx23wesdxc";

                SystemInfo.FilePathToImportExportXml = String.Empty;
                SystemInfo.GroupID_Search = long.MinValue;
                SystemInfo.GroupScreenName_Search = String.Empty;
                SystemInfo.UserID_Search = long.MinValue;
                SystemInfo.UserScreenName_Search = String.Empty;

                SystemInfo.vkPluginReference = new Vkontakte(CurrentUser.UserEmail, CurrentUser.UserPassword);

                long currentUserId = SystemInfo.vkPluginReference.GetAuthorizedUserId();

                if (currentUserId != -1)
                {
                    CurrentUser.UserId = currentUserId.ToString();

                }//if
                else {
                    
                    CurrentUser.UserEmail = String.Empty;
                    CurrentUser.UserPassword = String.Empty;
                    CurrentUser.UserId = String.Empty;

                }//else


                Uri userAvatar = new Uri(SystemInfo.vkPluginReference.GetUserPhoto200());

                BitmapImage userBitmapImage = new BitmapImage(userAvatar);
                UserPhoto.Source = userBitmapImage;
                SystemInfo.IsAuthorisedUser = true;

            }//try

            catch(Exception ex){

                MessageBox.Show("Не возможно пройти авторизацию пользователя");
                SystemInfo.IsAuthorisedUser = false;

                CurrentUser.UserEmail = String.Empty;
                CurrentUser.UserPassword = String.Empty;
                CurrentUser.UserId = String.Empty;

            }//catch

            
            int index = 0;
            
            if (!SystemInfo.IsAuthorisedUser) {

                MenuItem obj = (MainMenu.Items[0] as MenuItem);

                (obj.Items[0] as MenuItem).Header = "Авторизуватися";

            }//if

            PluginName.Text = SystemInfo.vkPluginReference.ShortNamePlugin;

            foreach (string ButtonName in SystemInfo.vkPluginReference.GetNavigatePanelFunctions())
            {

                Button NavigationButton = new Button();
                NavigationButton.Tag = index;
                NavigationButton.Content = ButtonName;
                NavigateMenu.Children.Add(NavigationButton);

                index++;

            }//foreach

            (NavigateMenu.Children[1] as Button).Click += new RoutedEventHandler(WorkWithUsers);
            (NavigateMenu.Children[2] as Button).Click += new RoutedEventHandler(WorkWithGroups);

            Type vkType = typeof(Vkontakte);

            var methods = vkType.GetMethods().Where(m => m.Name.EndsWith("_User"));


            foreach (var method in methods)
            {


                Button b = new Button();
                b.Content = method.CustomAttributes.ElementAt(0).NamedArguments.ElementAt(0).TypedValue.Value;

                FunctionsByNavigateItemPanel.Children.Add(b);

            }//foreach

            (FunctionsByNavigateItemPanel.Children[0] as Button).Click += new RoutedEventHandler(SelectUsersByIdScreenNameAction);
            (FunctionsByNavigateItemPanel.Children[1] as Button).Click += new RoutedEventHandler(SelectFollowersByIdScreenNameAction);
            (FunctionsByNavigateItemPanel.Children[2] as Button).Click += new RoutedEventHandler(SelectMyFollowersAction);
            (FunctionsByNavigateItemPanel.Children[3] as Button).Click += new RoutedEventHandler(SelectMyFriendsAction);
            (FunctionsByNavigateItemPanel.Children[4] as Button).Click += new RoutedEventHandler(SelectGroupMembersAction);

            SystemInfo.Counties = SystemInfo.vkPluginReference.GetCountries();

        }

        private void WorkWithGroups(object sender, RoutedEventArgs e) {
            

        }//WorkWithGroups

        private void WorkWithUsers(object sender, RoutedEventArgs e)
        {

        }//WorkWithUsers

        private void SelectUsersByIdScreenNameAction(object sender, RoutedEventArgs e)
        {
            if (CallBackList.Items.Count != 0)
            {

                if (MessageBox.Show("Зберегти вже вибраний список?","Запитання",MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    SearchUserById window = new SearchUserById();
                    window.ShowDialog();


                    if (SystemInfo.UserID_Search != long.MinValue)
                    {

                        (FunctionsByNavigateItemPanel.Children[0]).IsEnabled = false;
                        Thread select_users = new Thread(SelectUsers);
                        select_users.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук друзів користувача за його id";

                        select_users.Start();

                    }//if
                    else {

                        (FunctionsByNavigateItemPanel.Children[0]).IsEnabled = false;
                        Thread select_users = new Thread(SelectUsers);
                        select_users.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук друзів користувача за його screen name";

                        select_users.Start();
                    }//else

                }//if
                else
                {
                    CallBackList.Items.Clear();
                    SearchUserById window = new SearchUserById();
                    window.ShowDialog();


                    if (SystemInfo.UserID_Search != long.MinValue)
                    {

                        (FunctionsByNavigateItemPanel.Children[0]).IsEnabled = false;
                        Thread select_users = new Thread(SelectUsers);
                        select_users.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук друзів користувача за його id";

                        select_users.Start();

                    }//if
                    else
                    {
                        (FunctionsByNavigateItemPanel.Children[0]).IsEnabled = false;
                        Thread select_users = new Thread(SelectUsers);
                        select_users.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук друзів користувача за його screen name";

                        select_users.Start();
                    }//else

                }//else
            }//if
            else {

                SearchUserById window = new SearchUserById();
                window.ShowDialog();


                if (SystemInfo.UserID_Search != long.MinValue)
                {

                    (FunctionsByNavigateItemPanel.Children[0]).IsEnabled = false;
                    Thread select_users = new Thread(SelectUsers);
                    select_users.IsBackground = true;


                    StatusLabel.Foreground = Brushes.White;
                    StatusLabel.Content = "Статус: зараз виконується пошук друзів користувача за його id";

                    select_users.Start();

                }//if
                else
                {
                    (FunctionsByNavigateItemPanel.Children[0]).IsEnabled = false;
                    Thread select_users = new Thread(SelectUsers);
                    select_users.IsBackground = true;


                    StatusLabel.Foreground = Brushes.White;
                    StatusLabel.Content = "Статус: зараз виконується пошук друзів користувача за його screen name";

                    select_users.Start();
                }//else
            }//else
            

        }

        private void SelectUsers()
        {

            if (SystemInfo.UserID_Search != long.MinValue)
            {
                if(UsersResulSet.users != null)
                    UsersResulSet.users.Clear();

                if (UsersResulSet.myUsers != null)
                    UsersResulSet.myUsers.Clear();

                UsersResulSet.users = SystemInfo.vkPluginReference.GetFriedsFromSomeUser_User(SystemInfo.UserID_Search);
                SystemInfo.UserID_Search = long.MinValue;

                try
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {

                        foreach (var u in UsersResulSet.users)
                        {

                           
                            MyUser myUser = new MyUser();

                            myUser.Id = u.Id.ToString();
                            myUser.LastName = u.LastName;
                            myUser.Name = u.FirstName;

                            string birthday = u.BirthDate;

                            if(birthday != null){
                                if (birthday.Split('.').Count() == 3)
                                {
                                    
                                    int year = int.Parse(birthday.Split('.').ElementAt(2));
                                    DateTime now = DateTime.Now;
                                    birthday = (now.Year - year).ToString();
                                    
                                }//if
                                else
                                    birthday = "не вказан";

                            }//if
                            else
                                birthday = "не вказан";

                            myUser.Age = birthday;
                            myUser.Gender = (u.Sex == VkNet.Enums.Sex.Unknown ? "не вказан" : u.Sex.ToString());
                            myUser.Country = (u.Country != null ? u.Country.Title : "не вказана");
                            myUser.City = (u.City != null ? u.City.Title: "не вказано");
                            myUser.Region = ((u.City != null && u.City.Region != null)? u.City.Region : "не вказана");

                            CallBackList.Items.Add(myUser);
                            UsersResulSet.myUsers.Add(myUser);


                        }//foreach

                    }));

                    StatusLabel.Dispatcher.Invoke(new Action(
                () =>
                {

                    StatusLabel.Foreground = Brushes.Yellow;
                    StatusLabel.Content = "Статус: виконан пошук друзів користувача за його id. Кількіть друзів - " + CallBackList.Items.Count.ToString();
                    (FunctionsByNavigateItemPanel.Children[0]).IsEnabled = true;

                }
                ));

                }//try

                catch (Exception ex)
                {

                    MessageBox.Show("Можливо ви не пройшли авторізацію. Спробуйте змінити користвуча.");
                    StatusLabel.Dispatcher.Invoke(new Action(
                    () =>{

                    StatusLabel.Foreground = Brushes.Red;
                    StatusLabel.Content = "Статус: пошук користувачів закінчився з помилками";
                    (FunctionsByNavigateItemPanel.Children[0]).IsEnabled = true;

                    }//() =>
                ));
                }//catch

            }//if

            else if (SystemInfo.UserScreenName_Search != String.Empty)
            {
                if (UsersResulSet.users != null)
                    UsersResulSet.users.Clear();

                UsersResulSet.users = SystemInfo.vkPluginReference.GetFriedsFromSomeUser_User(SystemInfo.UserScreenName_Search);
                SystemInfo.UserScreenName_Search  = String.Empty;

                try
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {

                        foreach (var u in UsersResulSet.users)
                        {


                            MyUser myUser = new MyUser();

                            myUser.Id = u.Id.ToString();
                            myUser.LastName = u.LastName;
                            myUser.Name = u.FirstName;

                            string birthday = u.BirthDate;

                            if (birthday != null)
                            {
                                if (birthday.Split('.').Count() == 3)
                                {

                                    int year = int.Parse(birthday.Split('.').ElementAt(2));
                                    DateTime now = DateTime.Now;
                                    birthday = (now.Year - year).ToString();

                                }//if
                                else
                                    birthday = "не вказан";

                            }//if
                            else
                                birthday = "не вказан";

                            myUser.Age = birthday;
                            myUser.Gender = (u.Sex == VkNet.Enums.Sex.Unknown ? "не вказан" : u.Sex.ToString());
                            myUser.Country = (u.Country != null ? u.Country.Title : "не вказана");
                            myUser.City = (u.City != null ? u.City.Title : "не вказано");
                            myUser.Region = ((u.City != null && u.City.Region != null) ? u.City.Region : "не вказана");

                            CallBackList.Items.Add(myUser);
                            UsersResulSet.myUsers.Add(myUser);

                        }//foreach

                    }));

                    StatusLabel.Dispatcher.Invoke(new Action(
                () =>
                {

                    StatusLabel.Foreground = Brushes.Yellow;
                    StatusLabel.Content = "Статус: виконан пошук друзів користувача за його screen name. Кількіть друзів - " + CallBackList.Items.Count.ToString();
                    (FunctionsByNavigateItemPanel.Children[0]).IsEnabled = true;

                }
                ));

                }//try

                catch (Exception ex)
                {

                    MessageBox.Show("Можливо ви не пройшли авторізацію. Спробуйте змінити користвуча.");
                    StatusLabel.Dispatcher.Invoke(new Action(
                    () =>
                    {

                        StatusLabel.Foreground = Brushes.Red;
                        StatusLabel.Content = "Статус: пошук користувачів закінчився з помилками";
                        (FunctionsByNavigateItemPanel.Children[0]).IsEnabled = true;

                    }//() =>
                ));
                }//catch


            }//else
            else {
                MessageBox.Show("id користувача чи його screen name не отриман!");
                this.Dispatcher.Invoke(new Action(
                    () => { (FunctionsByNavigateItemPanel.Children[0]).IsEnabled = true; }
                    ));
                

            }//else
            

        }//SelectUser

        private void SelectFollowersByIdScreenNameAction(object sender, RoutedEventArgs e) {

            if (CallBackList.Items.Count != 0)
            {

                if (MessageBox.Show("Зберегти вже вибраний список?", "Запитання", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    SearchFollowers window = new SearchFollowers();
                    window.ShowDialog();


                    if (SystemInfo.UserID_Search != long.MinValue)
                    {

                        (FunctionsByNavigateItemPanel.Children[1]).IsEnabled = false;
                        Thread select_followers = new Thread(SelectFollowers);
                        select_followers.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук фоловерів користувача за його id";

                        select_followers.Start();

                    }//if
                    else
                    {
                        (FunctionsByNavigateItemPanel.Children[1]).IsEnabled = false;
                        Thread select_followers = new Thread(SelectFollowers);
                        select_followers.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук фоловерів користувача за його screen name";

                        select_followers.Start();

                    }//else

                }//if
                else
                {
                    CallBackList.Items.Clear();
                    SearchFollowers window = new SearchFollowers();
                    window.ShowDialog();


                    if (SystemInfo.UserID_Search != long.MinValue)
                    {

                        (FunctionsByNavigateItemPanel.Children[1]).IsEnabled = false;
                        Thread select_followers = new Thread(SelectFollowers);
                        select_followers.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук фоловерів користувача за його id";

                        select_followers.Start();

                    }//if
                    else
                    {
                        (FunctionsByNavigateItemPanel.Children[1]).IsEnabled = false;
                        Thread select_followers = new Thread(SelectFollowers);
                        select_followers.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук фоловерів користувача за його screen name";

                        select_followers.Start();

                    }//else

                }//else
            }//if
            else
            {

                SearchFollowers window = new SearchFollowers();
                window.ShowDialog();


                if (SystemInfo.UserID_Search != long.MinValue)
                {

                    (FunctionsByNavigateItemPanel.Children[1]).IsEnabled = false;
                    Thread select_followers = new Thread(SelectFollowers);
                    select_followers.IsBackground = true;


                    StatusLabel.Foreground = Brushes.White;
                    StatusLabel.Content = "Статус: зараз виконується пошук фоловерів користувача за його id";

                    select_followers.Start();

                }//if
                else
                {
                    (FunctionsByNavigateItemPanel.Children[1]).IsEnabled = false;
                    Thread select_followers = new Thread(SelectFollowers);
                    select_followers.IsBackground = true;


                    StatusLabel.Foreground = Brushes.White;
                    StatusLabel.Content = "Статус: зараз виконується пошук фоловерів користувача за його screen name";

                    select_followers.Start();

                }//else
            }//else
            

        }//SelectFollowersByIdScreenNameAction

        private void SelectFollowers() {

            if (SystemInfo.UserID_Search != long.MinValue)
            {
                if (UsersResulSet.users != null)
                    UsersResulSet.users.Clear();

                if (UsersResulSet.myUsers != null)
                    UsersResulSet.myUsers.Clear();

                UsersResulSet.users = SystemInfo.vkPluginReference.GetFollowersSomeUser_User(SystemInfo.UserID_Search);
                SystemInfo.UserID_Search = long.MinValue;

                try
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {

                        foreach (var u in UsersResulSet.users)
                        {


                            MyUser myUser = new MyUser();

                            myUser.Id = u.Id.ToString();
                            myUser.LastName = u.LastName;
                            myUser.Name = u.FirstName;

                            string birthday = u.BirthDate;

                            if (birthday != null)
                            {
                                if (birthday.Split('.').Count() == 3)
                                {

                                    int year = int.Parse(birthday.Split('.').ElementAt(2));
                                    DateTime now = DateTime.Now;
                                    birthday = (now.Year - year).ToString();

                                }//if
                                else
                                    birthday = "не вказан";

                            }//if
                            else
                                birthday = "не вказан";

                            myUser.Age = birthday;
                            myUser.Gender = (u.Sex == VkNet.Enums.Sex.Unknown ? "не вказан" : u.Sex.ToString());
                            myUser.Country = (u.Country != null ? u.Country.Title : "не вказана");
                            myUser.City = (u.City != null ? u.City.Title : "не вказано");
                            myUser.Region = ((u.City != null && u.City.Region != null) ? u.City.Region : "не вказана");

                            CallBackList.Items.Add(myUser);
                            UsersResulSet.myUsers.Add(myUser);

                        }//foreach

                    }));

                    StatusLabel.Dispatcher.Invoke(new Action(
                () =>
                {

                    StatusLabel.Foreground = Brushes.Yellow;
                    StatusLabel.Content = "Статус: виконан пошук фоловерів користувача за його id. Кількіть фоловерів - " + CallBackList.Items.Count.ToString();
                    (FunctionsByNavigateItemPanel.Children[1]).IsEnabled = true;

                }
                ));

                }//try

                catch (Exception ex)
                {

                    MessageBox.Show("Можливо ви не пройшли авторізацію. Спробуйте змінити користвуча.");
                    StatusLabel.Dispatcher.Invoke(new Action(
                    () =>
                    {

                        StatusLabel.Foreground = Brushes.Red;
                        StatusLabel.Content = "Статус: пошук фоловерів закінчився з помилками";
                        (FunctionsByNavigateItemPanel.Children[1]).IsEnabled = true;

                    }//() =>
                ));
                }//catch

            }//if

            else if (SystemInfo.UserScreenName_Search != String.Empty)
            {
                if (UsersResulSet.users != null)
                    UsersResulSet.users.Clear();


                UsersResulSet.users = SystemInfo.vkPluginReference.GetFollowersSomeUser_User(SystemInfo.UserScreenName_Search);
                SystemInfo.UserScreenName_Search = String.Empty;

                try
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {

                        foreach (var u in UsersResulSet.users)
                        {


                            MyUser myUser = new MyUser();

                            myUser.Id = u.Id.ToString();
                            myUser.LastName = u.LastName;
                            myUser.Name = u.FirstName;

                            string birthday = u.BirthDate;

                            if (birthday != null)
                            {
                                if (birthday.Split('.').Count() == 3)
                                {

                                    int year = int.Parse(birthday.Split('.').ElementAt(2));
                                    DateTime now = DateTime.Now;
                                    birthday = (now.Year - year).ToString();

                                }//if
                                else
                                    birthday = "не вказан";

                            }//if
                            else
                                birthday = "не вказан";

                            myUser.Age = birthday;
                            myUser.Gender = (u.Sex == VkNet.Enums.Sex.Unknown ? "не вказан" : u.Sex.ToString());
                            myUser.Country = (u.Country != null ? u.Country.Title : "не вказана");
                            myUser.City = (u.City != null ? u.City.Title : "не вказано");
                            myUser.Region = ((u.City != null && u.City.Region != null) ? u.City.Region : "не вказана");

                            CallBackList.Items.Add(myUser);


                        }//foreach

                    }));

                    StatusLabel.Dispatcher.Invoke(new Action(
                () =>
                {

                    StatusLabel.Foreground = Brushes.Yellow;
                    StatusLabel.Content = "Статус: виконан пошук фоловерів користувача за його screen name. Кількіть фоловерів - " + CallBackList.Items.Count.ToString();
                    (FunctionsByNavigateItemPanel.Children[1]).IsEnabled = true;

                }
                ));

                }//try

                catch (Exception ex)
                {

                    MessageBox.Show("Можливо ви не пройшли авторізацію. Спробуйте змінити користвуча.");
                    StatusLabel.Dispatcher.Invoke(new Action(
                    () =>
                    {

                        StatusLabel.Foreground = Brushes.Red;
                        StatusLabel.Content = "Статус: пошук фоловерів закінчився з помилками";
                        (FunctionsByNavigateItemPanel.Children[1]).IsEnabled = true;

                    }//() =>
                ));
                }//catch


            }//else
            else
            {
                MessageBox.Show("id користувача чи його screen name не отриман!");
            }//else
            

        }//SelectFollowers

        private void SelectMyFollowersAction(object sender, RoutedEventArgs e) {

            if (CallBackList.Items.Count != 0)
            {

                if (MessageBox.Show("Зберегти вже вибраний список?", "Запитання", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    
                    if (SystemInfo.IsAuthorisedUser)
                    {

                        (FunctionsByNavigateItemPanel.Children[2]).IsEnabled = false;
                        Thread select_My_followers = new Thread(SelectMyFollowers);
                        select_My_followers.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук власних фоловерів користувача";

                        select_My_followers.Start();

                    }//if
                    else
                    {

                        MessageBox.Show("Пошук не може бути виконан! Мабуть ви не авторизувалися!");

                    }//else

                }//if
                else
                {
                    CallBackList.Items.Clear();

                    if (SystemInfo.IsAuthorisedUser)
                    {

                        (FunctionsByNavigateItemPanel.Children[2]).IsEnabled = false;
                        Thread select_followers = new Thread(SelectMyFollowers);
                        select_followers.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук власних фоловерів користувача";

                        select_followers.Start();

                    }//if
                    else
                    {
                        MessageBox.Show("Пошук не може бути виконан! Мабуть ви не авторизувалися!");
                    }//else

                }//else
            }//if
            else
            {

                if (SystemInfo.IsAuthorisedUser)
                {

                    (FunctionsByNavigateItemPanel.Children[2]).IsEnabled = false;
                    Thread select_followers = new Thread(SelectMyFollowers);
                    select_followers.IsBackground = true;


                    StatusLabel.Foreground = Brushes.White;
                    StatusLabel.Content = "Статус: зараз виконується пошук власних фоловерів користувача";

                    select_followers.Start();

                }//if
                else
                {
                    MessageBox.Show("Пошук не може бути виконан! Мабуть ви не авторизувалися!");

                }//else

            }//else

        }//SelectMyFollowersAction

        private void SelectMyFollowers() {

            if (UsersResulSet.users != null)
                UsersResulSet.users.Clear();


            UsersResulSet.users = SystemInfo.vkPluginReference.GetMyFollowers_User();

            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                {

                    foreach (var u in UsersResulSet.users)
                    {


                        MyUser myUser = new MyUser();

                        myUser.Id = u.Id.ToString();
                        myUser.LastName = u.LastName;
                        myUser.Name = u.FirstName;

                        string birthday = u.BirthDate;

                        if (birthday != null)
                        {
                            if (birthday.Split('.').Count() == 3)
                            {

                                int year = int.Parse(birthday.Split('.').ElementAt(2));
                                DateTime now = DateTime.Now;
                                birthday = (now.Year - year).ToString();

                            }//if
                            else
                                birthday = "не вказан";

                        }//if
                        else
                            birthday = "не вказан";

                        myUser.Age = birthday;
                        myUser.Gender = (u.Sex == VkNet.Enums.Sex.Unknown ? "не вказан" : u.Sex.ToString());
                        myUser.Country = (u.Country != null ? u.Country.Title : "не вказана");
                        myUser.City = (u.City != null ? u.City.Title : "не вказано");
                        myUser.Region = ((u.City != null && u.City.Region != null) ? u.City.Region : "не вказана");

                        CallBackList.Items.Add(myUser);
                        UsersResulSet.myUsers.Add(myUser);

                    }//foreach

                }));

                StatusLabel.Dispatcher.Invoke(new Action(
            () =>
            {

                StatusLabel.Foreground = Brushes.Yellow;
                StatusLabel.Content = "Статус: виконан пошук власних фоловерів. Кількіть фоловерів - " + CallBackList.Items.Count.ToString();
                (FunctionsByNavigateItemPanel.Children[2]).IsEnabled = true;

            }
            ));

            }//try

            catch (Exception ex)
            {

                MessageBox.Show("Можливо ви не пройшли авторізацію. Спробуйте змінити користвуча.");
                StatusLabel.Dispatcher.Invoke(new Action(
                () =>
                {

                    StatusLabel.Foreground = Brushes.Red;
                    StatusLabel.Content = "Статус: пошук власних фоловерів закінчився з помилками";
                    (FunctionsByNavigateItemPanel.Children[2]).IsEnabled = true;

                }//() =>
            ));
            }//catch


        }//SelectMyFollowers

        private void SelectMyFriendsAction(object sender, RoutedEventArgs e) {

            if (CallBackList.Items.Count != 0)
            {

                if (MessageBox.Show("Зберегти вже вибраний список?", "Запитання", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {

                    if (SystemInfo.IsAuthorisedUser)
                    {

                        (FunctionsByNavigateItemPanel.Children[3]).IsEnabled = false;
                        Thread select_My_friend = new Thread(SelectMyFriends);
                        select_My_friend.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук власних друзів користувача";

                        select_My_friend.Start();

                    }//if
                    else
                    {

                        MessageBox.Show("Пошук не може бути виконан! Мабуть ви не авторизувалися!");

                    }//else

                }//if
                else
                {
                    CallBackList.Items.Clear();

                    if (SystemInfo.IsAuthorisedUser)
                    {

                        (FunctionsByNavigateItemPanel.Children[3]).IsEnabled = false;
                        Thread select_My_friend = new Thread(SelectMyFriends);
                        select_My_friend.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук власних друзів користувача";

                        select_My_friend.Start();

                    }//if
                    else
                    {
                        MessageBox.Show("Пошук не може бути виконан! Мабуть ви не авторизувалися!");
                    }//else

                }//else
            }//if
            else
            {

                if (SystemInfo.IsAuthorisedUser)
                {

                    (FunctionsByNavigateItemPanel.Children[3]).IsEnabled = false;
                    Thread select_My_friend = new Thread(SelectMyFriends);
                    select_My_friend.IsBackground = true;


                    StatusLabel.Foreground = Brushes.White;
                    StatusLabel.Content = "Статус: зараз виконується пошук власних друзів користувача";

                    select_My_friend.Start();

                }//if
                else
                {
                    MessageBox.Show("Пошук не може бути виконан! Мабуть ви не авторизувалися!");

                }//else

            }//else

        }//SelectMyFriendsAction

        private void SelectMyFriends() {

            if (UsersResulSet.users != null)
                UsersResulSet.users.Clear();


            UsersResulSet.users = SystemInfo.vkPluginReference.GetMyFriends_User();

            try
            {
                this.Dispatcher.Invoke(new Action(() =>
                {

                    foreach (var u in UsersResulSet.users)
                    {


                        MyUser myUser = new MyUser();

                        myUser.Id = u.Id.ToString();
                        myUser.LastName = u.LastName;
                        myUser.Name = u.FirstName;

                        string birthday = u.BirthDate;

                        if (birthday != null)
                        {
                            if (birthday.Split('.').Count() == 3)
                            {

                                int year = int.Parse(birthday.Split('.').ElementAt(2));
                                DateTime now = DateTime.Now;
                                birthday = (now.Year - year).ToString();

                            }//if
                            else
                                birthday = "не вказан";

                        }//if
                        else
                            birthday = "не вказан";

                        myUser.Age = birthday;
                        myUser.Gender = (u.Sex == VkNet.Enums.Sex.Unknown ? "не вказан" : u.Sex.ToString());
                        myUser.Country = (u.Country != null ? u.Country.Title : "не вказана");
                        myUser.City = (u.City != null ? u.City.Title : "не вказано");
                        myUser.Region = ((u.City != null && u.City.Region != null) ? u.City.Region : "не вказана");

                        CallBackList.Items.Add(myUser);
                        UsersResulSet.myUsers.Add(myUser);

                    }//foreach

                }));

                StatusLabel.Dispatcher.Invoke(new Action(
            () =>
            {

                StatusLabel.Foreground = Brushes.Yellow;
                StatusLabel.Content = "Статус: виконан пошук власних друзів. Кількіть друзів - " + CallBackList.Items.Count.ToString();
                (FunctionsByNavigateItemPanel.Children[3]).IsEnabled = true;

            }
            ));

            }//try

            catch (Exception ex)
            {

                MessageBox.Show("Можливо ви не пройшли авторізацію. Спробуйте змінити користвуча.");
                StatusLabel.Dispatcher.Invoke(new Action(
                () =>
                {

                    StatusLabel.Foreground = Brushes.Red;
                    StatusLabel.Content = "Статус: пошук власних друзів закінчився з помилками";
                    (FunctionsByNavigateItemPanel.Children[3]).IsEnabled = true;

                }//() =>
            ));
            }//catch

        }//SelectMyFriends

        private void SelectGroupMembersAction(object sender, RoutedEventArgs e) {

            if (CallBackList.Items.Count != 0)
            {

                if (MessageBox.Show("Зберегти вже вибраний список?", "Запитання", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    SelectMembersFromGroup window = new SelectMembersFromGroup();
                    window.ShowDialog();


                    if (SystemInfo.GroupID_Search != long.MinValue)
                    {

                        (FunctionsByNavigateItemPanel.Children[4]).IsEnabled = false;
                        Thread select_users_FromGroup = new Thread(SelectGroupMembers);
                        select_users_FromGroup.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук членів пабліка за його id";

                        select_users_FromGroup.Start();

                    }//if
                    else
                    {
                        (FunctionsByNavigateItemPanel.Children[4]).IsEnabled = false;
                        Thread select_users_FromGroup = new Thread(SelectGroupMembers);
                        select_users_FromGroup.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук членів пабліка за його screen name";

                        select_users_FromGroup.Start();
                    }//else

                }//if
                else
                {
                    CallBackList.Items.Clear();
                    SelectMembersFromGroup window = new SelectMembersFromGroup();
                    window.ShowDialog();


                    if (SystemInfo.GroupID_Search != long.MinValue)
                    {

                        (FunctionsByNavigateItemPanel.Children[4]).IsEnabled = false;
                        Thread select_users_FromGroup = new Thread(SelectGroupMembers);
                        select_users_FromGroup.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук членів пабліка за його id";

                        select_users_FromGroup.Start();

                    }//if
                    else
                    {
                        (FunctionsByNavigateItemPanel.Children[4]).IsEnabled = false;
                        Thread select_users_FromGroup = new Thread(SelectGroupMembers);
                        select_users_FromGroup.IsBackground = true;


                        StatusLabel.Foreground = Brushes.White;
                        StatusLabel.Content = "Статус: зараз виконується пошук членів пабліка за його screen name";

                        select_users_FromGroup.Start();

                    }//else

                }//else
            }//if
            else
            {

                SelectMembersFromGroup window = new SelectMembersFromGroup();
                window.ShowDialog();


                if (SystemInfo.GroupID_Search != long.MinValue)
                {

                    (FunctionsByNavigateItemPanel.Children[4]).IsEnabled = false;
                    Thread select_users_FromGroup = new Thread(SelectGroupMembers);
                    select_users_FromGroup.IsBackground = true;


                    StatusLabel.Foreground = Brushes.White;
                    StatusLabel.Content = "Статус: зараз виконується пошук членів пабліка за його id";

                    select_users_FromGroup.Start();

                }//if
                else
                {
                    (FunctionsByNavigateItemPanel.Children[4]).IsEnabled = false;
                    Thread select_users_FromGroup = new Thread(SelectGroupMembers);
                    select_users_FromGroup.IsBackground = true;


                    StatusLabel.Foreground = Brushes.White;
                    StatusLabel.Content = "Статус: зараз виконується пошук членів пабліка за його screen name";

                    select_users_FromGroup.Start();

                }//else
            }//else
            

        }//SelectGroupMembersAction

        private void SelectGroupMembers() {

            if (SystemInfo.GroupID_Search != long.MinValue)
            {
                if (UsersResulSet.users != null)
                    UsersResulSet.users.Clear();


                UsersResulSet.users = SystemInfo.vkPluginReference.GetUsersFromPublic_User(SystemInfo.GroupID_Search);
                SystemInfo.GroupID_Search = long.MinValue;

                try
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {

                        foreach (var u in UsersResulSet.users)
                        {


                            MyUser myUser = new MyUser();

                            myUser.Id = u.Id.ToString();
                            myUser.LastName = u.LastName;
                            myUser.Name = u.FirstName;

                            string birthday = u.BirthDate;

                            if (birthday != null)
                            {
                                if (birthday.Split('.').Count() == 3)
                                {

                                    int year = int.Parse(birthday.Split('.').ElementAt(2));
                                    DateTime now = DateTime.Now;
                                    birthday = (now.Year - year).ToString();

                                }//if
                                else
                                    birthday = "не вказан";

                            }//if
                            else
                                birthday = "не вказан";

                            myUser.Age = birthday;
                            myUser.Gender = (u.Sex == VkNet.Enums.Sex.Unknown ? "не вказан" : u.Sex.ToString());
                            myUser.Country = (u.Country != null ? u.Country.Title : "не вказана");
                            myUser.City = (u.City != null ? u.City.Title : "не вказано");
                            myUser.Region = ((u.City != null && u.City.Region != null) ? u.City.Region : "не вказана");

                            CallBackList.Items.Add(myUser);
                            UsersResulSet.myUsers.Add(myUser);

                        }//foreach

                    }));

                    StatusLabel.Dispatcher.Invoke(new Action(
                () =>
                {

                    StatusLabel.Foreground = Brushes.Yellow;
                    StatusLabel.Content = "Статус: виконан пошук членів пабліка за його id. Кількіть членів - " + CallBackList.Items.Count.ToString();
                    (FunctionsByNavigateItemPanel.Children[4]).IsEnabled = true;

                }
                ));

                }//try

                catch (Exception ex)
                {

                    MessageBox.Show("Можливо ви не пройшли авторізацію. Спробуйте змінити користвуча.");
                    StatusLabel.Dispatcher.Invoke(new Action(
                    () =>
                    {

                        StatusLabel.Foreground = Brushes.Red;
                        StatusLabel.Content = "Статус: пошук членів пабліка закінчився з помилками";
                        (FunctionsByNavigateItemPanel.Children[4]).IsEnabled = true;

                    }//() =>
                ));
                }//catch

            }//if

            else if (SystemInfo.GroupScreenName_Search != String.Empty)
            {
                if (UsersResulSet.users != null)
                    UsersResulSet.users.Clear();


                UsersResulSet.users = SystemInfo.vkPluginReference.GetUsersFromPublic_User(SystemInfo.GroupScreenName_Search);
                SystemInfo.GroupScreenName_Search = String.Empty;

                try
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {

                        foreach (var u in UsersResulSet.users)
                        {


                            MyUser myUser = new MyUser();

                            myUser.Id = u.Id.ToString();
                            myUser.LastName = u.LastName;
                            myUser.Name = u.FirstName;

                            string birthday = u.BirthDate;

                            if (birthday != null)
                            {
                                if (birthday.Split('.').Count() == 3)
                                {

                                    int year = int.Parse(birthday.Split('.').ElementAt(2));
                                    DateTime now = DateTime.Now;
                                    birthday = (now.Year - year).ToString();

                                }//if
                                else
                                    birthday = "не вказан";

                            }//if
                            else
                                birthday = "не вказан";

                            myUser.Age = birthday;
                            myUser.Gender = (u.Sex == VkNet.Enums.Sex.Unknown ? "не вказан" : u.Sex.ToString());
                            myUser.Country = (u.Country != null ? u.Country.Title : "не вказана");
                            myUser.City = (u.City != null ? u.City.Title : "не вказано");
                            myUser.Region = ((u.City != null && u.City.Region != null) ? u.City.Region : "не вказана");

                            CallBackList.Items.Add(myUser);


                        }//foreach

                    }));

                    StatusLabel.Dispatcher.Invoke(new Action(
                () =>
                {

                    StatusLabel.Foreground = Brushes.Yellow;
                    StatusLabel.Content = "Статус: виконан пошук членів пабліка за його screen name. Кількіть фоловерів - " + CallBackList.Items.Count.ToString();
                    (FunctionsByNavigateItemPanel.Children[4]).IsEnabled = true;

                }
                ));

                }//try

                catch (Exception ex)
                {

                    MessageBox.Show("Можливо ви не пройшли авторізацію. Спробуйте змінити користвуча.");
                    StatusLabel.Dispatcher.Invoke(new Action(
                    () =>
                    {

                        StatusLabel.Foreground = Brushes.Red;
                        StatusLabel.Content = "Статус: пошук членів пабліка закінчився з помилками";
                        (FunctionsByNavigateItemPanel.Children[4]).IsEnabled = true;

                    }//() =>
                ));
                }//catch


            }//else
            else
            {
                MessageBox.Show("id пабліка чи його screen name не отриман!");

            }//else
            

        }//SelectGroupMembers

        private void MainWWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Size s = e.NewSize;

            CallBackList.Height = (s.Height - Manipulations.Height - PluginNameWrap.Height - 80);
            WorkWithUserStackPanel.Height = MainWWindow.Height + 200;
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            if (CallBackList.SelectedIndex != -1)
            {

                SendMessage sm = new SendMessage((CallBackList.SelectedItem as MyUser).Id, (CallBackList.SelectedItem as MyUser).Name);
                sm.ShowDialog();

            }//if

            else
            {

                MessageBox.Show("Ви повинні виділити користувача!");

            }//else

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            
           
            try
            {
                ChangeUser cu = new ChangeUser();
                cu.ShowDialog();
                if(CurrentUser.UserEmail != String.Empty && CurrentUser.UserPassword != String.Empty){

                Thread t = new Thread(AuthoriseUser);
                t.IsBackground = true;

                t.Start();

                }//if

            }//try

            catch(Exception){
                
            }//catch
            
        }

        private void AuthoriseUser() {

                this.Dispatcher.Invoke(new Action(
                    () => {

                        try {

                            SystemInfo.vkPluginReference.Authorize(CurrentUser.UserEmail, CurrentUser.UserPassword);

                            UserPhoto.Dispatcher.Invoke(

                           new Action(() =>
                           {
                               Uri userAvatar = new Uri(SystemInfo.vkPluginReference.GetUserPhoto200());
                               BitmapImage bitmapUserPhoto = new BitmapImage(userAvatar);
                               UserPhoto.Source = bitmapUserPhoto;

                           })//new Action

                           );//Disp.Inv

                        }//try

                        catch(Exception ex){
                            MessageBox.Show("Не возможно пройти авторизацию пользователя с имейлом - " + CurrentUser.UserEmail);
                        }//catvh

                        if (!SystemInfo.IsAuthorisedUser)
                        {
                            MenuItem obj = (MainMenu.Items[0] as MenuItem);

                            (obj.Items[0] as MenuItem).Header = "Змінити користувача";
                            SystemInfo.IsAuthorisedUser = true;

                        }//if

                    }//()=>
                    ));

               
        }

        private void MakeSerchByFilters_Click(object sender, RoutedEventArgs e)
        {
            FilterPanel fp = new FilterPanel();
            
            fp.ShowDialog();

            if (SearchInfo.FilteredUsers != null )
            {

                CallBackList.Items.Clear();

                foreach (MyUser myUser in SearchInfo.FilteredUsers)
                {

                    CallBackList.Items.Add(myUser);

                }//foreach

                SearchInfo.FilteredUsers.Clear();

            }//if
            else
                MessageBox.Show("Фільтр не знайшов жодного користувача!");


        }

        private void Spiter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MouseY = (int)Mouse.GetPosition(MainWWindow).Y;
        }

        private void Spliter_MouseMove(object sender, MouseEventArgs e)
        {
            int currentMousePosition = (int)Mouse.GetPosition(MainWWindow).Y;
            
            if (currentMousePosition < MouseY && currentMousePosition > 30 && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                try
                {

                    int height = ((int)Manipulations.Height - (MouseY - currentMousePosition));

                    if (height > 60)
                    {

                        Manipulations.Height = (int)height;
                        CallBackList.Height += (MouseY - currentMousePosition);

                    }//if


                }//try
                catch (Exception ex)
                {
                }//try
            }//if
            else if (currentMousePosition > MouseY && Mouse.LeftButton == MouseButtonState.Pressed)
            {
                try
                {
                    int height = ((int)Manipulations.Height + (currentMousePosition - MouseY));

                    if (height < 400)
                    {

                        Manipulations.Height = (int)height;
                        CallBackList.Height -= (currentMousePosition - MouseY);

                    }//if


                }//try
                catch (Exception ex)
                {
                }//try

            }//else

        }

        private void Spliter_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MouseY = (int)Mouse.GetPosition(MainWWindow).Y;

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".xml";
            sfd.Filter = "(*.xml) Files | *.xml";

            bool ? result = sfd.ShowDialog();

            if(result.HasValue){

                SystemInfo.FilePathToImportExportXml = sfd.FileName;

                Thread export_users = new Thread(ExportUsersXml);
                export_users.IsBackground = true;


                StatusLabel.Foreground = Brushes.White;
                StatusLabel.Content = "Статус: зараз виконується емпорт користувачів до файлу";

                export_users.Start();


            }//if

        }

        private void ImportUsersXml() {

            if (SystemInfo.FilePathToImportExportXml != String.Empty)
            {
                List<MyUser> users = null;

                this.Dispatcher.Invoke(new Action(() =>
                {

                    loader.ExtractFromFile(SystemInfo.FilePathToImportExportXml, out users);

                    if (users != null)
                    {

                        foreach (MyUser user in users)
                        {

                            CallBackList.Items.Add(user);
                            UsersResulSet.myUsers.Add(user);

                        }//foreach

                        

                    }//if


                }));

                
                StatusLabel.Dispatcher.Invoke(new Action(() =>
                {

                    StatusLabel.Foreground = Brushes.Yellow;
                    StatusLabel.Content = "Статус: виконан імпорт користувачів з файлу. Усього користувачів - " + users.Count.ToString();

                }));
                
            }//if
            else {
                StatusLabel.Dispatcher.Invoke(new Action(() => {

                    StatusLabel.Foreground = Brushes.Red;
                    StatusLabel.Content = "Статус: не можливо виконати імпорт користувачів з файлу";

                }));
            }

            SystemInfo.FilePathToImportExportXml = String.Empty;

        }

        private void ExportUsersXml() {

            if (SystemInfo.FilePathToImportExportXml != String.Empty)
            {
                
                this.Dispatcher.Invoke(new Action(() =>
                {

                    if (UsersResulSet.myUsers.Count != 0)
                    {

                        XmlLoader.LoadToFile(SystemInfo.FilePathToImportExportXml, UsersResulSet.myUsers);

                    }//if
                   
                }));


                StatusLabel.Dispatcher.Invoke(new Action(() =>
                {

                    StatusLabel.Foreground = Brushes.Yellow;
                    StatusLabel.Content = "Статус: виконан експорт користувачів до файлу. Експортовано користувачів - " + UsersResulSet.myUsers.Count.ToString();

                }));

            }//if
            else
            {
                StatusLabel.Dispatcher.Invoke(new Action(() =>
                {

                    StatusLabel.Foreground = Brushes.Red;
                    StatusLabel.Content = "Статус: не можливо виконати експорт користувачів до файлу";

                }));
            }

            SystemInfo.FilePathToImportExportXml = String.Empty;

        }//ExportUsersXml

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "(*.xml) Files | *.xml";
            ofd.DefaultExt = ".xml";

            if(ofd.ShowDialog().HasValue){

                SystemInfo.FilePathToImportExportXml = ofd.FileName;

                Thread import_users = new Thread(ImportUsersXml);
                import_users.IsBackground = true;


                StatusLabel.Foreground = Brushes.White;
                StatusLabel.Content = "Статус: зараз виконується імпорт користувачів з файлу";

                import_users.Start();

                
            }

           
            
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            LocalizeDictionary.Instance.Culture = CultureInfo.GetCultureInfo("ru-Ru");
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            CallBackList.Items.Clear();

            foreach(MyUser user in UsersResulSet.myUsers ){
                CallBackList.Items.Add(user);
            }//foreach

        }

    }
}
