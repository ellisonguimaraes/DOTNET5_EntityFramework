using System.Linq;
using BooksProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LibraryController : ControllerBase
    {
        public readonly ApplicationDbContext _context;

        public LibraryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("identifiers")]
        public IActionResult GetIdentifiers()
        {
            return Ok(_context.Identificadores.Include(x => x.Book).ToList());
        }

        [HttpGet("editors")]
        public IActionResult GetEditors()
        {
            return Ok(_context.Editoras.Include(x => x.Books).ToList());
        }

        [HttpGet("genders")]
        public IActionResult GetGenders()
        {
            return Ok(_context.Generos.Include(x => x.Books).ToList());
        }

        [HttpGet("authors")]
        public IActionResult GetAuthors()
        {
            return Ok(_context.Autores.Include(x => x.AuthorBooks).ToList());
        }

        [HttpGet("books")]
        public IActionResult GetBooks()
        {
            return Ok(_context.Livros
                            .Include(x => x.AuthorBooks)
                            .Include(x => x.Editor)
                            .Include(x => x.Gender)
                            .Include(x => x.Identifier).ToList());
        }
    }
}