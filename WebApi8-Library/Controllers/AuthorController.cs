using Microsoft.AspNetCore.Mvc;
using WebApi8_Library.Dto.Author;
using WebApi8_Library.Models;
using WebApi8_Library.Service.Author;

namespace WebApi8_Library.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorInterface _authorInterface;
        public AuthorController(IAuthorInterface authorInterface)
        {
            _authorInterface = authorInterface;
        }

        [HttpGet("ListAuthors")]
        public async Task<ActionResult<ResponseModel<List<AuthorModel>>>> ListAuthors()
        {
            var authors = await _authorInterface.ListAuthors();
            return Ok(authors);
        }
        [HttpGet("AuthorById/{idAuthor}")]
        public async Task<ActionResult<ResponseModel<AuthorModel>>> AuthorById(int idAuthor)
        {
            var author = await _authorInterface.AuthorById(idAuthor);
            return Ok(author);
        }

        [HttpGet("AuthorByIdBook/{idBook}")]
        public async Task<ActionResult<ResponseModel<AuthorModel>>> AuthorByIdBook(int idBook)
        {
            var author = await _authorInterface.AuthorByIdBook(idBook);
            return Ok(author);
        }

        [HttpPost("CreateAuthor")]
        public async Task<ActionResult<ResponseModel<AuthorModel>>> CreateAuthor([FromBody] AuthorCreateDto authorCreateDto)
        {
            var result = await _authorInterface.CreateAuthor(authorCreateDto);
            if (result.Status)
            {
                return CreatedAtAction(nameof(AuthorById), new { idAuthor = result.Data.Id }, result);
            }
            return BadRequest(result);
        }

        [HttpPut("{idAuthor}")]
        public async Task<IActionResult> UpdateAuthor(int idAuthor, [FromBody] AuthorUpdateDto authorUpdateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authorInterface.UpdateAuthor(idAuthor, authorUpdateDto);
            if (!response.Status)
                return BadRequest(response.Message);

            return Ok(response);
        }

        [HttpDelete("{idAuthor}")]
        public async Task<IActionResult> DeleteAuthor(int idAuthor)
        {
            var response = await _authorInterface.DeleteAuthor(idAuthor);
            if (!response.Status)
                return BadRequest(response.Message);

            return Ok(response);
        }
    }
}