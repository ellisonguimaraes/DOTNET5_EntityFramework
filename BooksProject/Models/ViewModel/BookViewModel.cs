using System;
using System.Collections.Generic;

namespace BooksProject.Models.ViewModel
{
    public class BookViewModel : BaseClassViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime PubDate { get; set; }

        public EditorViewModel Editor { get; set; }
        public GenderViewModel Gender { get; set; }
        public IdentifierViewModel Identifier { get; set; }
        
        public List<AuthorViewModel> Authors { get; set; }
    }
}