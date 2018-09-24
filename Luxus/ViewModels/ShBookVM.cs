using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Luxus.Models;

namespace Luxus.ViewModels
{
    public class ShBookVM
    {
        public ShBookVM(Book b)
        {
            BookID = b.BookID;
            Title = b.Title;
            Author = b.Author;
            Desc = b.Desc;
            Rating = b.Rating;
            Status = b.Status;
            UserID = b.UserID;
            Image = b.Image;
            PageNo = b.PageNo;
            Shared = (b.Shared.HasValue && b.Shared.Value==true)?true:false;
        }

        public int BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Desc { get; set; }
        public Nullable<int> Rating { get; set; }
        public int Status { get; set; }
        public string UserID { get; set; }
        public string Image { get; set; }
        public bool Shared { get; set; }
        public Nullable<int> PageNo { get; set; }
    }
}