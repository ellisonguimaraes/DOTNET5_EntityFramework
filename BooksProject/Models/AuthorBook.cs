namespace BooksProject.Models
{
    public class AuthorBook : BaseClass
    {
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}