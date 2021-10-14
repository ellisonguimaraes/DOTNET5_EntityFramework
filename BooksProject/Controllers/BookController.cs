using System.Text.Json;
using BooksProject.Models;
using BooksProject.Repository.Interface;
using BooksProject.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace BooksProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> _repository;

        public BookController(IRepository<Book> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var books = _repository.Get(paginationParameters);

            var metadata = new {
                books.TotalCount,
                books.PageSize,
                books.CurrentPage,
                books.HasPrevious,
                books.HasNext,
                books.TotalPages
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));

            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var book = _repository.GetById(id);
            
            if (book == null) return BadRequest();

            return Ok(book);
        }
    }
}