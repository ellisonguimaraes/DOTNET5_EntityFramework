using System.Text.Json;
using BooksProject.Business.Interface;
using BooksProject.Models.InputModel;
using BooksProject.Models.Pagination;
using BooksProject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BooksProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IBusiness<AuthorViewModel, AuthorInputModel> _authorBusiness;

        public AuthorController(IBusiness<AuthorViewModel, AuthorInputModel> authorBusiness)
        {
            _authorBusiness = authorBusiness;
        }

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var authors = _authorBusiness.GetPaginate(paginationParameters);

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
            var author = _authorBusiness.GetById(id);

            if (author == null) return BadRequest();

            return Ok(author);
        }

        [HttpPost]
        public IActionResult Post([FromBody] AuthorInputModel authorInputModel)
        {
            var result = _authorBusiness.Create(authorInputModel);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] AuthorInputModel authorInputModel)
        {
            var result = _authorBusiness.Update(authorInputModel);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if(!_authorBusiness.Delete(id)) return BadRequest("Id não existe!");
            return NoContent();
        }
    }
}