using BooksProject.Models;

namespace BooksProject.Repositories.Interface
{
    public interface IAuthorBookRepository
    {
        AuthorBook Create(AuthorBook item);
        bool Delete(AuthorBook item);
    }
}