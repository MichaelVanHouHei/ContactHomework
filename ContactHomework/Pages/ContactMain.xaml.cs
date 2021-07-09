using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MahApps.Metro.Controls.Dialogs;

namespace ContactHomework.Pages
{
    /// <summary>
    /// ContactMain.xaml 的互動邏輯
    /// </summary>
    public partial class ContactMain : Page
    {



        #region Obser


        public Contact SelectedContact
        {
            get { return (Contact)GetValue(SelectedContactProperty); }
            set { SetValue(SelectedContactProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedContact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedContactProperty =
            DependencyProperty.Register("SelectedContact", typeof(Contact), typeof(ContactMain), new PropertyMetadata(null ));


        public Category DefaultCategory
        {
            get { return (Category)GetValue(DefaultCategoryProperty); }
            set { SetValue(DefaultCategoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DefaultCategory.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DefaultCategoryProperty =
            DependencyProperty.Register("DefaultCategory", typeof(Category), typeof(ContactMain), new PropertyMetadata(new Category()));



        public ObservableCollection<Category> CatasCategories
        {
            get { return (ObservableCollection<Category>)GetValue(CatasCategoriesProperty); }
            set { SetValue(CatasCategoriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CatasCategories.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CatasCategoriesProperty =
            DependencyProperty.Register("CatasCategories", typeof(ObservableCollection<Category>), typeof(ContactMain), new PropertyMetadata(new ObservableCollection<Category>()));



        public ObservableCollection<Contact> ContactObList
        {
            get { return (ObservableCollection<Contact>)GetValue(ContactObListProperty); }
            set { SetValue(ContactObListProperty, value); }
        }
        // Using a DependencyProperty as the backing store for ContactObList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContactObListProperty =
            DependencyProperty.Register("ContactObList", typeof(ObservableCollection<Contact>), typeof(ContactMain), new PropertyMetadata(new ObservableCollection<Contact>()));
#endregion
        DataManager db = new DataManager();
        public ContactMain()
        {
            InitializeComponent();
            DataContext = this;
            
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await GlobalConstant.mainWindow.ShowMessageAsync("Welcome back", "Hello " + GlobalConstant.currentUser.DisplayName);
            CatasCategories = new ObservableCollection<Category>(await db.getAllCategory());
            DefaultCategory = CatasCategories.First(x => x.ID == 0);
        }

        private void cataComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DefaultCategory != null)
            {
                ContactObList = new ObservableCollection<Contact>(DefaultCategory.Contact);
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PageManager.Add(new editor(new Contact()));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedContact != null)
            {
                PageManager.Add(new editor(SelectedContact));
            }
            else
            {
                await GlobalConstant.mainWindow.ShowMessageAsync("exception", "you have to select the data below");
            }
        }
    }
}
