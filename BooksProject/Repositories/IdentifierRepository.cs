using System;
using System.Linq;
using BooksProject.Models;
using BooksProject.Models.Context;
using BooksProject.Models.Pagination;
using BooksProject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksProject.Repositories
{
    public class IdentifierRepository : IRepository<Identifier>
    {
        private readonly ApplicationDbContext _context;

        public IdentifierRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public PagedList<Identifier> Get(PaginationParameters paginationParameters)
            => new PagedList<Identifier>(_context.Identificadores
                                        .Include(i => i.Book)
                                        .Include(i => i.Book).ThenInclude(b => b.Editor)
                                        .Include(i => i.Book).ThenInclude(b => b.Gender)
                                        .Include(i => i.Book).ThenInclude(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                                        .OrderBy(i => i.Id),
                                        paginationParameters.PageNumber,
                                        paginationParameters.PageSize);

        public Identifier GetById(int id)
            => _context.Identificadores
                .Include(i => i.Book)
                .Include(i => i.Book).ThenInclude(b => b.Editor)
                .Include(i => i.Book).ThenInclude(b => b.Gender)
                .Include(i => i.Book).ThenInclude(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Where(i => i.Id == id)
                .SingleOrDefault();

        public Identifier Create(Identifier item)
        {
            try
            {
                _context.Identificadores.Add(item);
                _context.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }

            return item;
        }      

        public Identifier Update(Identifier item)
        {
            var getItem = _context.Identificadores.Where(i => i.Id == item.Id).SingleOrDefault();

            if (getItem != null)
            {
                try
                {
                    _context.Entry(getItem).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                }
                catch(Exception)
                {
                    throw;
                }
            }

            return item;
        }

        public bool Delete(int id)
        {
            var getItem = _context.Identificadores.Where(i => i.Id == id).SingleOrDefault();

            if (getItem != null)
            {
                try
                {
                    _context.Identificadores.Remove(getItem);
                    _context.SaveChanges();
                    return true;
                }
                catch(Exception)
                {
                    throw;
                }
            }

            return false;
        }
    }
}