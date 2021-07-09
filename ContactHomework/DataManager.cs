using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactHomework
{
    public partial class Contact
    {
        public string CategoryName { get; set; }
    }
    public class DataManager
    {
      
        public async Task<User> checkLogin(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)) return null;
            using (var context = new ContactManagementEntities())
            {
                 var isExist = await  context.User.AnyAsync(x => x.Username.StartsWith( username) && x.Password.StartsWith( password));
                 if (isExist)
                 {
                     var user =  await context.User.FirstAsync(x => x.Username.StartsWith(username) && x.Password.StartsWith(password));
                     user.LastLoginDateTime=DateTime.Now;
                     context.User.AddOrUpdate(user);
                     await context.SaveChangesAsync();
                     return user;
                 }
            }
            return null;
        }
        /// <summary>
        /// this one is for reg purpose , no "Add or update" should include here
        /// </summary>
        /// <param name="user"></param>
        /// <returns>whether you login or not</returns>
        public async Task<bool> addUser(User user)
        {
            try
            {
                using (var context = new ContactManagementEntities())
                {
                    context.User.Add(user);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
        public async Task<bool> addOrUpdateContact(Contact contact)
        {
            try
            { 
                using (var context = new ContactManagementEntities())
                {
                    contact.Category = await context.Category.FirstAsync(c=>c.ID == contact.CategoryID);
                    context.Contact.AddOrUpdate(contact);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        public async Task<List<Category>> getAllCategory(bool ALL = true)
        {
            using (var context = new ContactManagementEntities())
            {
                
                var cata = await context.Category.Include(c=>c.Contact).ToListAsync();
                if(ALL)
                     cata.Insert( 0 , new Category(){ID=0 ,Name = "ALL",Contact=await getAllContacts()});
                return cata;
            }
        }
        public async Task<List<Contact>> getAllContacts(string catalogy="")
        {
            using (ContactManagementEntities context = new ContactManagementEntities())
            {
                 List<Contact> result = new List<Contact>();
                if (string.IsNullOrEmpty(catalogy) || catalogy=="ALL")
                {
                    result = await context.Contact.ToListAsync();
                }
                else
                {
                    result =  await context.Contact.Where(c=>c.Category.Name ==catalogy).ToListAsync();
                }
                result.ForEach(c=>c.CategoryName= c.Category.Name);
                return result;
            }
        }

    }
}
