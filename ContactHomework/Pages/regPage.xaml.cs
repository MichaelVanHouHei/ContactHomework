using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace ContactHomework.Pages
{
    /// <summary>
    ///     regPage.xaml 的互動邏輯
    /// </summary>
    public partial class regPage : Page
    {
        // Using a DependencyProperty as the backing store for User.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UserProperty =
            DependencyProperty.Register("User", typeof(User), typeof(regPage), new PropertyMetadata(new User()));

        private readonly DataManager db = new DataManager();


        public regPage()
        {
            InitializeComponent();
            DataContext = this;
             
        }
        

        public User User
        {
            get => (User) GetValue(UserProperty);
            set => SetValue(UserProperty, value);
        }

        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var r = await GlobalConstant.mainWindow.ShowMessageAsync("are you to leave", "",
                MessageDialogStyle.AffirmativeAndNegative);
            if (r == MessageDialogResult.Affirmative) PageManager.Close();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var allFields = User.GetType().GetProperties().Where(pi => pi.PropertyType == typeof(string))
                .Select(c => (string) c.GetValue(User)).Any(c => string.IsNullOrEmpty(c));
            if (allFields)
            {
                await GlobalConstant.mainWindow.ShowMessageAsync("all fields must be filled in", "fuck you ");
                return;
            }

            User.Password = MD5.getMD5(User.Password);
            User.LastLoginDateTime = DateTime.Now;
            var r = await db.addUser(User);
            if (r)
            {
                await GlobalConstant.mainWindow.ShowMessageAsync("Done", "Done");
                PageManager.Close();
            }
            else
            {
                await GlobalConstant.mainWindow.ShowMessageAsync("exception", "exception");
            }
        }
    }
}