using Microsoft.AspNetCore.Mvc;
using WebApi8_Library.Dto.Book;
using WebApi8_Library.Models;
using WebApi8_Library.Service.Book;

namespace WebApi8_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookInterface _bookInterface;

        public BookController(IBookInterface bookInterface)
        {
            _bookInterface = bookInterface;
        }

        [HttpGet("ListBooks")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> ListBooks()
        {
            var books = await _bookInterface.ListBooks();
            return Ok(books);
        }

        [HttpGet("BookById/{idBook}")]
        public async Task<ActionResult<ResponseModel<BookModel>>> BookById(int idBook)
        {
            var book = await _bookInterface.BookById(idBook);
            return Ok(book);
        }

        [HttpGet("BooksByAuthor/{idAuthor}")]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> BooksByAuthor(int idAuthor)
        {
            var books = await _bookInterface.BookByIdAuthor(idAuthor);
            return Ok(books);
        }

        [HttpPost("CreateBook")]
        public async Task<ActionResult<ResponseModel<BookModel>>> CreateBook([FromBody] BookCreateDto bookCreateDto)
        {
            var result = await _bookInterface.CreateBook(bookCreateDto);
            if (result.Status)
            {
                return CreatedAtAction(nameof(BookById), new { idBook = result.Data.Id }, result);
            }
            return BadRequest(result);
        }

        // Método para atualizar um livro
        [HttpPut("UpdateBook/{idBook}")]
        public async Task<ActionResult<ResponseModel<BookModel>>> UpdateBook(int idBook, [FromBody] BookUpdateDto bookUpdateDto)
        {
            var result = await _bookInterface.UpdateBook(idBook, bookUpdateDto);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Método para deletar um livro
        [HttpDelete("DeleteBook/{idBook}")]
        public async Task<ActionResult<ResponseModel<bool>>> DeleteBook(int idBook)
        {
            var result = await _bookInterface.DeleteBook(idBook);
            if (result.Status)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
