using System;
using System.Linq;
using BooksProject.Models;
using BooksProject.Models.Context;
using BooksProject.Models.Pagination;
using BooksProject.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BooksProject.Repository
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
                                    .Select(a => new Author(){
                                        Id = a.Id,
                                        Name = a.Name,
                                        LastName = a.LastName,
                                        BirthDate = a.BirthDate,
                                        AuthorBooks = a.AuthorBooks.Select(ab => new AuthorBook(){
                                            Author = ab.Author,
                                            AuthorId = ab.AuthorId,
                                            Book = ab.Book,
                                            BookId = ab.BookId
                                        }).ToList()
                                    })
                                    .OrderBy(a => a.Id),
                                    paginationParameters.PageNumber,
                                    paginationParameters.PageSize);

        public Author GetById(int id)
            => _context.Autores
                .Include(a => a.AuthorBooks)
                .Select(a => new Author(){
                    Id = a.Id,
                    Name = a.Name,
                    LastName = a.LastName,
                    BirthDate = a.BirthDate,
                    AuthorBooks = a.AuthorBooks.Select(ab => new AuthorBook(){
                        Author = ab.Author,
                        AuthorId = ab.AuthorId,
                        Book = ab.Book,
                        BookId = ab.BookId
                    }).ToList()
                })
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