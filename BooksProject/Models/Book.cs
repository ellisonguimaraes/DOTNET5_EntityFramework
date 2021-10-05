using System;
using System.Collections.Generic;

namespace BooksProject.Models
{
    public class Book : BaseClass
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime PubDate { get; set; }

        // Relationship
        public int EditorId { get; set; }
        public virtual Editor Editor { get; set; }

        public int GenderId { get; set; }
        public virtual Gender Gender { get; set; }

        public int IdentifierId { get; set; }
        public virtual Identifier Identifier { get; set; }

        public virtual List<AuthorBook> AuthorBooks { get; set; }
    }
}
