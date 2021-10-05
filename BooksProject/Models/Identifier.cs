using BooksProject.Enums;

namespace BooksProject.Models
{
    public class Identifier : BaseClass
    {
        public BookTypeIdentifier IdentifierType { get; set; }
        public string IdentifierNumber { get; set; }

        // Relationship
        public virtual Book Book { get; set; }
    }
}