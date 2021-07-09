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
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ContactHomework.Pages
{
    /// <summary>
    /// Login.xaml 的互動邏輯
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            DataContext = this;
        }
        DataManager db = new DataManager();


        public string username
        {
            get { return (string)GetValue(usernameProperty); }
            set { SetValue(usernameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for username.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty usernameProperty =
            DependencyProperty.Register("username", typeof(string), typeof(LoginPage), new PropertyMetadata(""));



        public string password
        {
            get { return (string)GetValue(passwordProperty); }
            set { SetValue(passwordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for password.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty passwordProperty =
            DependencyProperty.Register("password", typeof(string), typeof(LoginPage), new PropertyMetadata(""));


        private async void Button_Click(object sender, RoutedEventArgs e)
        { 
            var user = await db.checkLogin(username,MD5.getMD5(password));
           if (user != null)
           {
               
               GlobalConstant.currentUser = user;
               PageManager.Add(new ContactMain());
               GlobalConstant.isLogin = true;
               GlobalConstant.mainWindow.status = $"{user.DisplayName}---登出";
           }
           else
           {
               await GlobalConstant.mainWindow.ShowMessageAsync("error","wrong username or passwod");
           }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PageManager.Add(new regPage());
        }
    }
}
