using System;
using System.Linq;
using BooksProject.Models;
using BooksProject.Models.Context;
using BooksProject.Repository.Interface;
using BooksProject.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BooksProject.Repository
{
    public class EditorRepository : IRepository<Editor>
    {
        private readonly ApplicationDbContext _context;

        public EditorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public PagedList<Editor> Get(PaginationParameters paginationParameters)
            => new PagedList<Editor>(_context.Editoras
                                    .Include(e => e.Books)
                                    .OrderBy(e => e.Id),
                                    paginationParameters.PageNumber,
                                    paginationParameters.PageSize);

        public Editor GetById(int id)
            => _context.Editoras
                        .Include(e => e.Books)
                        .Where(e => e.Id == id)
                        .SingleOrDefault();

        public Editor Create(Editor item)
        {
            try
            {
                _context.Editoras.Add(item);
                _context.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }

            return item;
        }

        public Editor Update(Editor item)
        {
            var getItem = _context.Editoras.Where(e => e.Id == item.Id).SingleOrDefault();

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
            var getItem = _context.Editoras.Where(e => e.Id == id).SingleOrDefault();

            if (getItem != null)
            {
                try
                {
                    _context.Editoras.Remove(getItem);
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