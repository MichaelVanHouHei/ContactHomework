using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
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
using ContactHomework.Pages;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ContactHomework
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            GlobalConstant.mainWindow = this;
            PageManager.Add(new LoginPage());
        }



        public string status
        {
            get { return (string)GetValue(statusProperty); }
            set { SetValue(statusProperty, value); }
        }

        // Using a DependencyProperty as the backing store for status.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty statusProperty =
            DependencyProperty.Register("status", typeof(string), typeof(MainWindow), new PropertyMetadata("還未登入"));


        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (GlobalConstant.isLogin)
            {
                var result = await this.ShowMessageAsync("Are you sure to logout?", "",
                    MessageDialogStyle.AffirmativeAndNegative);
                if (result == MessageDialogResult.Affirmative)
                {
                    status = "還未登入";
                    GlobalConstant.isLogin = false;
                    PageManager.Clear();
                }
            }
            
        }
    }
}
