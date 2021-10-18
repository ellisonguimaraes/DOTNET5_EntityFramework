using System;

namespace BooksProject.Models.InputModel
{
    public class AuthorInputModel : BaseClassInputModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}