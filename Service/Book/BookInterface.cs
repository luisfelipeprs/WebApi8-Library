using WebApi8_Library.Dto.Book;
using WebApi8_Library.Models;

namespace WebApi8_Library.Service.Book
{
    public interface IBookInterface
    {
        Task<ResponseModel<List<BookModel>>> ListBooks();
        Task<ResponseModel<BookModel>> BookById(int idBook);
        Task<ResponseModel<List<BookModel>>> BookByIdAuthor(int idAuthor);
        Task<ResponseModel<BookModel>> CreateBook(BookCreateDto bookCreateDto);
        Task<ResponseModel<BookModel>> UpdateBook(int idBook, BookUpdateDto bookUpdateDto);
        Task<ResponseModel<bool>> DeleteBook(int idBook);
    }
}