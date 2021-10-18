using BooksProject.Enums;

namespace BooksProject.Models.InputModel
{
    public class IdentifierInputModel : BaseClassInputModel
    {
        public BookTypeIdentifier IdentifierType { get; set; }
        public string IdentifierNumber { get; set; }
    }
}