using Luxus.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Luxus.DAL
{
    public class DbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        //public void SeedBook(ApplicationDbContext context)
        //{
        //    var book = new List<Book>
        //    {

        //        new Book{Desc="TestDesc", Title="Harry Potter", Author="J.K. Rowling", Rating=3,Shared=true,Status=1, UserID="ddc7d1d1-397a-4fc0-8afe-ff70e77f1b30"},
        //        new Book{Desc="TestDesc2", Title="Forrest Gump", Author="Tom Hanks", Rating=3,Shared=true,Status=2, UserID="ddc7d1d1-397a-4fc0-8afe-ff70e77f1b30"},
        //        new Book{Desc="TestDesc3", Title="Super Super", Author="Ash", Image="", Rating=3,Shared=true,Status=4, UserID="ddc7d1d1-397a-4fc0-8afe-ff70e77f1b30"},

        //    };
        //    book.ForEach(s => context.Books.Add(s));
        //    context.SaveChanges();

        //}
    }
}