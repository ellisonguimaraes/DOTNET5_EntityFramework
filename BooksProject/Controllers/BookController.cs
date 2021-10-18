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
    public class BookController : ControllerBase
    {
        private readonly IBusiness<BookViewModel, BookInputModel> _bookBusiness;

        public BookController(IBusiness<BookViewModel, BookInputModel> bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [Route("{PageNumber}/{PageSize}")]
        public IActionResult Get([FromRoute] PaginationParameters paginationParameters)
        {
            var books = _bookBusiness.GetPaginate(paginationParameters);

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
            var book = _bookBusiness.GetById(id);
            
            if (book == null) return BadRequest();

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BookInputModel bookInputModel)
        {
            var result = _bookBusiness.Create(bookInputModel);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update([FromBody] BookInputModel bookInputModel)
        {
            var result = _bookBusiness.Update(bookInputModel);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(int id)
        {
            if (!_bookBusiness.Delete(id)) return BadRequest("Id não existe!"); 
            return NoContent();
        }
    }
}