using System;
using System.Collections.Generic;
using System.Linq;

namespace BooksProject.Models.Pagination
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;        
              
        public PagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            TotalCount = source.Count();
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(TotalCount / (double)PageSize);

            var items = source.Skip((this.CurrentPage - 1) * this.PageSize)
                                .Take(this.PageSize)
                                .ToList();

            this.AddRange(items);
        }
    }
}