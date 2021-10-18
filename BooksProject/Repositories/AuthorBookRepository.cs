using System;
using System.Linq;
using BooksProject.Models;
using BooksProject.Models.Context;
using BooksProject.Repositories.Interface;

namespace BooksProject.Repositories
{
    public class AuthorBookRepository : IAuthorBookRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorBookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public AuthorBook Create(AuthorBook item)
        {
            try 
            {
                _context.AutorLivro.Add(item);
                _context.SaveChanges();
            } 
            catch(Exception) 
            {
                throw;
            }

            return item;
        }

        public bool Delete(AuthorBook item)
        {
            var getAuthorBook = _context.AutorLivro
                                            .Where(ab => ab.AuthorId == item.AuthorId && ab.BookId == item.BookId)
                                            .SingleOrDefault();

            if (getAuthorBook != null)
            {
                try {   
                    _context.AutorLivro.Remove(getAuthorBook);
                    _context.SaveChanges();
                    return true;
                    
                } catch (Exception) {
                    throw;
                }
            }

            return false;
        }
    }
}