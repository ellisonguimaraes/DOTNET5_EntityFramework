using System;
using System.Collections.Generic;

namespace BooksProject.Models.ViewModel
{
    public class AuthorViewModel : BaseClassViewModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        public List<BookViewModel> Books { get; set; }
    }
}