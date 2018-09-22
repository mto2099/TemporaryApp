using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luxus.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public Nullable<int> Rating { get; set; }
        public int Status { get; set; }
        public string UserID { get; set; }
        public string Image { get; set; }
        public Nullable<Boolean> Shared { get; set; }
        public Nullable<int> PageNo { get; set; }
        public virtual ApplicationUser User{ get; set; }
    }
}