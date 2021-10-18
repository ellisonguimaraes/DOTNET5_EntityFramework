using System;
using System.Collections.Generic;

namespace BooksProject.Models.InputModel
{
    public class BookInputModel : BaseClassInputModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime PubDate { get; set; }

        public EditorInputModel Editor { get; set; }
        public GenderInputModel Gender { get; set; }
        public IdentifierInputModel Identifier { get; set; }

        public List<AuthorInputModel> Authors { get; set; }
    }
}