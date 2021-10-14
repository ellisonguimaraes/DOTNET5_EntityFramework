using System;
using System.Linq;
using BooksProject.Models;
using BooksProject.Models.Context;
using BooksProject.Repository.Interface;
using BooksProject.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BooksProject.Repository
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
                                        .OrderBy(i => i.Id),
                                        paginationParameters.PageNumber,
                                        paginationParameters.PageSize);

        public Identifier GetById(int id)
            => _context.Identificadores
                .Include(i => i.Book)
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