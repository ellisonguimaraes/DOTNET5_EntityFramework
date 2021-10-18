using System.Collections.Generic;

namespace BooksProject.Models.ViewModel
{
    public class GenderViewModel : BaseClassViewModel
    {
        public string Name { get; set; }
        public List<BookViewModel> Books { get; set; }
    }
}