using System;
using System.Linq;
using BooksProject.Models;
using BooksProject.Models.Context;
using BooksProject.Models.Pagination;
using BooksProject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksProject.Repositories
{
    public class BookRepository : IRepository<Book> 
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public PagedList<Book> Get(PaginationParameters paginationParameters)
            => new PagedList<Book>(_context.Livros
                                    .Include(b => b.Editor)
                                    .Include(b => b.Gender)
                                    .Include(b => b.Identifier)
                                    .Include(b => b.AuthorBooks)
                                    .OrderBy(b => b.Id)
                                    .Select(b => new Book(){
                                        Id = b.Id,
                                        Name = b.Name,
                                        Price = b.Price,
                                        PubDate = b.PubDate,
                                        EditorId = b.EditorId,
                                        GenderId = b.GenderId,
                                        IdentifierId = b.IdentifierId,
                                        Editor = b.Editor,
                                        Gender = b.Gender,
                                        Identifier = b.Identifier,
                                        AuthorBooks = b.AuthorBooks.Select(ab => new AuthorBook{
                                            Author = ab.Author,
                                            AuthorId = ab.AuthorId
                                        }).ToList()
                                    }),
                                    paginationParameters.PageNumber,
                                    paginationParameters.PageSize);

        public Book GetById(int id)
            => _context.Livros
                .Include(b => b.Editor)
                .Include(b => b.Gender)
                .Include(b => b.Identifier)
                .Include(b => b.AuthorBooks).ThenInclude(ab => ab.Author)
                .Where(b => b.Id == id)
                .SingleOrDefault();

        public Book Create(Book item)
        {
            try
            {
                _context.Livros.Add(item);
                _context.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }

            return item;
        }

        public Book Update(Book item)
        {
            var getItem = _context.Livros.Where(b => b.Id == item.Id).SingleOrDefault();

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
            var getItem = _context.Livros.Where(b => b.Id == id).SingleOrDefault();

            if (getItem != null)
            {
                try
                {
                    _context.Livros.Remove(getItem);
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