using System.Collections.Generic;

namespace BooksProject.Models
{
    public class Gender : BaseClass
    {
        public string Name { get; set; }

        // Relationship
        public virtual List<Book> Books { get; set; }
    }
}