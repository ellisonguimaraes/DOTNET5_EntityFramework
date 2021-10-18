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
    public class EditorController : ControllerBase
    {
        private readonly IBusiness<EditorViewModel, EditorInputModel> _editorBusiness;

        public EditorController(IBusiness<EditorViewModel, EditorInputModel> editorBusiness)
        {
            _editorBusiness = editorBusiness;
        }

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var editors = _editorBusiness.GetPaginate(paginationParameters);

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
            var editor = _editorBusiness.GetById(id);

            if (editor == null) return BadRequest();

            return Ok(editor);
        }

        [HttpPost]
        public IActionResult Post([FromBody] EditorInputModel editorInputModel)
        {
            var result = _editorBusiness.Create(editorInputModel);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] EditorInputModel editorInputModel)
        {
            var result = _editorBusiness.Update(editorInputModel);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_editorBusiness.Delete(id)) return BadRequest("Id não existe!"); 
            return NoContent();
        }
    }
}