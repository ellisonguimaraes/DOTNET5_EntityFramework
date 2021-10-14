using System.Text.Json;
using BooksProject.Models;
using BooksProject.Repository.Interface;
using BooksProject.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace BooksProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentifierController : ControllerBase
    {
        private readonly IRepository<Identifier> _repository;

        public IdentifierController(IRepository<Identifier> repository)
        {
            _repository = repository;            
        }

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var identifiers = _repository.Get(paginationParameters);

            var metadata = new {
                identifiers.TotalCount,
                identifiers.PageSize,
                identifiers.CurrentPage,
                identifiers.HasPrevious,
                identifiers.HasNext,
                identifiers.TotalPages
            };

            Response.Headers.Add("x-pagination", JsonSerializer.Serialize(metadata));

            return Ok(identifiers);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var identifier = _repository.GetById(id);

            if (identifier == null) return BadRequest();

            return Ok(identifier);
        }
    }
}