using System;
using System.Linq;
using BooksProject.Models;
using BooksProject.Models.Context;
using BooksProject.Repository.Interface;
using BooksProject.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BooksProject.Repository
{
    public class GenderRepository : IRepository<Gender>
    {
        private readonly ApplicationDbContext _context;

        public GenderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public PagedList<Gender> Get(PaginationParameters paginationParameters)
            => new PagedList<Gender>(_context.Generos
                                        .Include(g => g.Books)
                                        .OrderBy(g => g.Id),
                                        paginationParameters.PageNumber,
                                        paginationParameters.PageSize);

        public Gender GetById(int id)
            => _context.Generos
                .Include(g => g.Books)
                .Where(g => g.Id == id)
                .SingleOrDefault();

        public Gender Create(Gender item)
        {
            try
            {
                _context.Generos.Add(item);
                _context.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }

            return item;
        }

        public Gender Update(Gender item)
        {
            var getItem = _context.Generos.Where(g => g.Id == item.Id).SingleOrDefault();

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
            var getItem = _context.Generos.Where(g => g.Id == id).SingleOrDefault();

            if (getItem != null)
            {
                try
                {
                    _context.Generos.Remove(getItem);
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