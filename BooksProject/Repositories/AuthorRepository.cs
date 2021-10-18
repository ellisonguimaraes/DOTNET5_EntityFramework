using System;
using System.Linq;
using BooksProject.Models;
using BooksProject.Models.Context;
using BooksProject.Models.Pagination;
using BooksProject.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksProject.Repositories
{
    public class AuthorRepository : IRepository<Author>
    {
        private readonly ApplicationDbContext _context;
        
        public AuthorRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public PagedList<Author> Get(PaginationParameters paginationParameters)
            => new PagedList<Author>(_context.Autores
                                    .Include(a => a.AuthorBooks)
                                    .OrderBy(a => a.Id)
                                    .Select(a => new Author(){
                                        Id = a.Id,
                                        Name = a.Name,
                                        LastName = a.LastName,
                                        BirthDate = a.BirthDate,
                                        AuthorBooks = a.AuthorBooks.Select(ab => new AuthorBook(){
                                            BookId = ab.BookId,
                                            Book = new Book{
                                                Id = ab.Book.Id,
                                                Name = ab.Book.Name,
                                                Price = ab.Book.Price,
                                                PubDate = ab.Book.PubDate,
                                                EditorId = ab.Book.EditorId,
                                                Editor = ab.Book.Editor,
                                                GenderId = ab.Book.GenderId,
                                                Gender = ab.Book.Gender,
                                                IdentifierId = ab.Book.IdentifierId,
                                                Identifier = ab.Book.Identifier,
                                            }
                                        }).ToList()
                                    }),
                                    paginationParameters.PageNumber,
                                    paginationParameters.PageSize);

        public Author GetById(int id)
            => _context.Autores
                .Include(a => a.AuthorBooks).ThenInclude(ab => ab.Book)
                .Include(a => a.AuthorBooks).ThenInclude(ab => ab.Book.Editor)
                .Include(a => a.AuthorBooks).ThenInclude(ab => ab.Book.Gender)
                .Include(a => a.AuthorBooks).ThenInclude(ab => ab.Book.Identifier)
                .Where(a => a.Id == id)
                .SingleOrDefault();

        public Author Create(Author item)
        {
            try
            {
                _context.Autores.Add(item);
                _context.SaveChanges();
            }
            catch(Exception)
            {
                throw;
            }

            return item;
        }

        public Author Update(Author item)
        {
            var getItem = _context.Autores.Where(a => a.Id == item.Id).SingleOrDefault();

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
            var getItem = _context.Autores.Where(a => a.Id == id).SingleOrDefault();

            if (getItem != null)
            {
                try
                {
                    _context.Autores.Remove(getItem);
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