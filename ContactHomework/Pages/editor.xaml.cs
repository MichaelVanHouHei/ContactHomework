using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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
    /// editor.xaml 的互動邏輯
    /// </summary>
    public partial class editor : Page
    {

        #region obs






        public Category SelectedCategory
        {
            get { return (Category)GetValue(SelectedCategoryProperty); }
            set { SetValue(SelectedCategoryProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedCategory.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedCategoryProperty =
            DependencyProperty.Register("SelectedCategory", typeof(Category), typeof(editor), new PropertyMetadata(new Category()));



        public Contact PContact
        {
            get { return (Contact)GetValue(PContactProperty); }
            set { SetValue(PContactProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PContact.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PContactProperty =
            DependencyProperty.Register("PContact", typeof(Contact), typeof(editor), new PropertyMetadata(new Contact()));





        public ObservableCollection<Category> CataCategories
        {
            get { return (ObservableCollection<Category>)GetValue(CataCategoriesProperty); }
            set { SetValue(CataCategoriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CataCategories.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CataCategoriesProperty =
            DependencyProperty.Register("CataCategories", typeof(ObservableCollection<Category>), typeof(editor), new PropertyMetadata(new ObservableCollection<Category>()));





        #endregion
        DataManager db = new DataManager();
        public editor(Contact _contact )
        {
            InitializeComponent();
            DataContext = this;
           
         //   mainRegion.DataContext = PContact;
            PContact = _contact;
          
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var allFields= PContact.GetType().GetProperties().Where(c=>c.Name != "ContactID" && c.Name != "CategoryName").Where(pi => pi.PropertyType == typeof(string))
                .Select(c => (string)c.GetValue(PContact)).Any(c => string.IsNullOrEmpty(c));
            if (allFields)
            {
                await GlobalConstant.mainWindow.ShowMessageAsync("all fields must be filled in", "fuck you ");
                return;
            }
            if (SelectedCategory == null)
            {
                await GlobalConstant.mainWindow.ShowMessageAsync("error", "you should assign category to the data");
                return;
            }
            PContact.CategoryID = SelectedCategory.ID;
            PContact.Category = SelectedCategory;
            var r = await GlobalConstant.mainWindow.ShowMessageAsync("are you sure to add/update", "",
                MessageDialogStyle.AffirmativeAndNegative);
            if (r == MessageDialogResult.Affirmative)
            {
               
                var isSuccessAdd = await db.addOrUpdateContact(PContact);
                if (isSuccessAdd)
                {
                    await GlobalConstant.mainWindow.ShowMessageAsync("Done", "Done");
                    PageManager.Close();
                }
                else
                {
                    await GlobalConstant.mainWindow.ShowMessageAsync("FUCK!!!!", "some exceptions occur");
                }
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var r = await GlobalConstant.mainWindow.ShowMessageAsync("are you sure to exit", "",
                MessageDialogStyle.AffirmativeAndNegative);
            if (r == MessageDialogResult.Affirmative)
            {
                PageManager.Close();
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
         
            CataCategories = new ObservableCollection<Category>(await db.getAllCategory(false));
            SelectedCategory = PContact.Category;
        }
    }
}
