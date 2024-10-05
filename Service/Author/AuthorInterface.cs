using WebApi8_Library.Dto.Author;
using WebApi8_Library.Models;
namespace WebApi8_Library.Service.Author
{
    public interface IAuthorInterface
    {
        Task<ResponseModel<List<AuthorModel>>> ListAuthors();
        Task<ResponseModel<AuthorModel>> AuthorById(int idAuthor);
        Task<ResponseModel<AuthorModel>> AuthorByIdBook(int idBook);
        Task<ResponseModel<AuthorModel>> CreateAuthor(AuthorCreateDto authorCreateDto);
        Task<ResponseModel<AuthorModel>> UpdateAuthor(int idAuthor, AuthorUpdateDto authorUpdateDto);
        Task<ResponseModel<bool>> DeleteAuthor(int idAuthor);
    }
}