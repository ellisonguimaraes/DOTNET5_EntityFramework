using System;
using System.Linq;
using BooksProject.Models;
using BooksProject.Models.Context;
using BooksProject.Repository.Interface;
using BooksProject.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BooksProject.Repository
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseClass
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public PagedList<TEntity> Get(PaginationParameters paginationParameters) =>
            new PagedList<TEntity>(_dbSet.OrderBy(i => i.Id), 
                                    paginationParameters.PageNumber,
                                    paginationParameters.PageSize);

        public TEntity GetById(int id)
            => _dbSet
                .Where(i => i.Id == id)
                .SingleOrDefault();
        
        public TEntity Create(TEntity item)
        {
            try 
            {
                _dbSet.Add(item);
                _context.SaveChanges();
            } 
            catch(Exception) 
            {
                throw;
            }

            return item;
        }

        public TEntity Update(TEntity item)
        {
            TEntity getItem = _dbSet.Where(i => i.Id == item.Id).SingleOrDefault();

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
            TEntity getItem = _dbSet.Where(i => i.Id == id).SingleOrDefault();

            if (getItem != null)
            {
                try
                {
                    _dbSet.Remove(getItem);
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