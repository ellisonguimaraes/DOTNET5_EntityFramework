using System.Text.Json;
using BooksProject.Models;
using BooksProject.Repository.Interface;
using BooksProject.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace BooksProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IRepository<Author> _repository;

        public AuthorController(IRepository<Author> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var authors = _repository.Get(paginationParameters);

            var metadata = new {
                authors.TotalCount,
                authors.PageSize,
                authors.CurrentPage,
                authors.HasPrevious,
                authors.HasNext,
                authors.TotalPages
            };

            Response.Headers.Add("x-pagination", JsonSerializer.Serialize(metadata));

            return Ok(authors);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var author = _repository.GetById(id);

            if (author == null) return BadRequest();

            return Ok(author);
        }
    }
}