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
    public class GenderController : ControllerBase
    {
        private readonly IBusiness<GenderViewModel, GenderInputModel> _genderBusiness;

        public GenderController(IBusiness<GenderViewModel, GenderInputModel> genderBusiness)
        {
            _genderBusiness = genderBusiness;
        }

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var genders = _genderBusiness.GetPaginate(paginationParameters);

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
            var gender = _genderBusiness.GetById(id);

            if (gender == null) return BadRequest();

            return Ok(gender);
        }

        [HttpPost]
        public IActionResult Post([FromBody] GenderInputModel genderInputModel)
        {
            var result = _genderBusiness.Create(genderInputModel);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] GenderInputModel genderInputModel)
        {
            var result = _genderBusiness.Update(genderInputModel);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_genderBusiness.Delete(id)) return BadRequest("Id não existe!");
            return NoContent();
        }
    }
}