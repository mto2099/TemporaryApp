using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luxus.ViewModels
{
    public class SharedBooksVM
    {
        public SharedBooksVM()
        {

        }

        public SharedBooksVM(int bookID, string title, string author, string desc, int rating, string userID, string image, Boolean shared)
        {
            BookID = bookID;
            Title = title;
            Author = author;
            Desc = desc;
            Rating = rating;
            UserID = userID;
            Image = image;
            Shared = shared;
        }
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Desc { get; set; }
        public Nullable<int> Rating { get; set; }
        public string UserID { get; set; }
        public string Image { get; set; }
        public Boolean Shared { get; set; }
    }
}