using System.Collections.Generic;
using BooksProject.Enums;

namespace BooksProject.Models.ViewModel
{
    public class IdentifierViewModel : BaseClassViewModel
    {
        public BookTypeIdentifier IdentifierType { get; set; }
        public string IdentifierNumber { get; set; }
        public BookViewModel Book { get; set; }
    }
}