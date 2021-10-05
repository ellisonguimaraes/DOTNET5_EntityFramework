using System;
using System.Collections.Generic;

namespace BooksProject.Models
{
    public class Author : BaseClass
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }

        // Relationship
        public virtual List<AuthorBook> AuthorBooks { get; set; }
    }
}