using Luxus.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Luxus.ViewModels
{
    public class SharedBooksVM
    {
        public ApplicationUser User { get; set; }

        public List<Book> Books { get; set; }
    }
}