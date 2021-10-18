using System.Text.Json;
using BooksProject.Business.Interface;
using BooksProject.Models;
using BooksProject.Models.InputModel;
using BooksProject.Models.Pagination;
using BooksProject.Models.ViewModel;
using BooksProject.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BooksProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentifierController : ControllerBase
    {
        private readonly IBusiness<IdentifierViewModel, IdentifierInputModel> _identifierBusiness;

        public IdentifierController(IBusiness<IdentifierViewModel, IdentifierInputModel> identifierBusiness)
        {
            _identifierBusiness = identifierBusiness;            
        }

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var identifiers = _identifierBusiness.GetPaginate(paginationParameters);

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
            var identifier = _identifierBusiness.GetById(id);

            if (identifier == null) return BadRequest();

            return Ok(identifier);
        }

        [HttpPost]
        public IActionResult Post([FromBody] IdentifierInputModel identifierInputModel)
        {
            var result = _identifierBusiness.Create(identifierInputModel);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] IdentifierInputModel identifierInputModel)
        {
            var result = _identifierBusiness.Update(identifierInputModel);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if(!_identifierBusiness.Delete(id)) return BadRequest("Id não existe!");
            return NoContent();
        }
    }
}