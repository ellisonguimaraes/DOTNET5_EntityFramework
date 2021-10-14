using System.Text.Json;
using BooksProject.Models;
using BooksProject.Repository.Interface;
using BooksProject.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace BooksProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenderController : ControllerBase
    {
        private readonly IRepository<Gender> _repository;

        public GenderController(IRepository<Gender> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var genders = _repository.Get(paginationParameters);

            var metadata = new {
                genders.TotalCount,
                genders.PageSize,
                genders.CurrentPage,
                genders.HasPrevious,
                genders.HasNext,
                genders.TotalPages
            };

            Response.Headers.Add("x-pagination", JsonSerializer.Serialize(metadata));

            return Ok(genders); 
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var gender = _repository.GetById(id);

            if (gender == null) return BadRequest();

            return Ok(gender);
        }
    }
}