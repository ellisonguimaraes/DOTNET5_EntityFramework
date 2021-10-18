using BooksProject.Models;
using BooksProject.Models.Pagination;

namespace BooksProject.Repositories.Interface
{
    public interface IRepository<TEntity> where TEntity : BaseClass
    {
        PagedList<TEntity> Get(PaginationParameters paginationParameters);
        TEntity GetById(int id);
        TEntity Create(TEntity item);
        TEntity Update(TEntity item);
        bool Delete(int id);
    }
}