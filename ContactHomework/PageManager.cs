using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ContactHomework.Pages;
 
namespace ContactHomework
{
    public static class GlobalConstant
    {
        public static MainWindow mainWindow { get; set; }
        public static string status = "";
        public static User currentUser;
        public static bool isLogin = false;
    }
    public static class PageManager
    {
        public static List<Page> pages = new List<Page>();
        public static int currentPage = -1;
      
        public static void Clear()
        {
            pages.Clear();
            currentPage = 0;
            pages.Add(new LoginPage());
            GlobalConstant.mainWindow.ContentFrame.Navigate(pages[currentPage]);
        }
        static PageManager() 
        {
         
        }

        public static void Add(Page page)
        {

            if (pages.Contains(page))
            {
                currentPage = pages.FindIndex( x=>x.Uid == page.Uid);
                GlobalConstant.mainWindow.ContentFrame.Navigate(page);
            }
            else
            {
                currentPage++;
                pages.Add(page);
                GlobalConstant.mainWindow.ContentFrame.Navigate(pages[currentPage]);
            }
           
      
        
        }

        public static void Close()
        {
            
            if (--currentPage < 0)
            {
                currentPage = 0;
                pages.Add(new LoginPage());
                GlobalConstant.mainWindow.ContentFrame.Navigate(pages[currentPage]);
                return;
            }
            GlobalConstant.mainWindow.ContentFrame.Navigate(pages[currentPage]);
            pages.Remove(pages[currentPage + 1]);
           
        }
    }
}
