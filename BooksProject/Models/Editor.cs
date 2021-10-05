using System.Collections.Generic;

namespace BooksProject.Models
{
    public class Editor : BaseClass
    {
        public string Name { get; set; }

        // Relationship
        public virtual List<Book> Books { get; set; }
    }
}