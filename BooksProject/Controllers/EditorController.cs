using System.Text.Json;
using BooksProject.Models;
using BooksProject.Repository.Interface;
using BooksProject.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace BooksProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EditorController : ControllerBase
    {
        private readonly IRepository<Editor> _repository;

        public EditorController(IRepository<Editor> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var editors = _repository.Get(paginationParameters);

            var metadata = new {
                editors.TotalCount,
                editors.PageSize,
                editors.CurrentPage,
                editors.HasPrevious,
                editors.HasNext,
                editors.TotalPages
            };

            Response.Headers.Add("x-pagination", JsonSerializer.Serialize(metadata));

            return Ok(editors);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var editor = _repository.GetById(id);

            if (editor == null) return BadRequest();

            return Ok(editor);
        }
    }
}