using Microsoft.EntityFrameworkCore;
using WebApi8_Library.Data;
using WebApi8_Library.Dto.Book;
using WebApi8_Library.Models;

namespace WebApi8_Library.Service.Book
{
    public class BookService : IBookInterface
    {
        private readonly AppDbContext _context;

        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<BookModel>> BookById(int idBook)
        {
            ResponseModel<BookModel> response = new ResponseModel<BookModel>();
            try
            {
                var book = await _context.Book.FirstOrDefaultAsync(booksDataBase => booksDataBase.Id == idBook);
                if (book == null)
                {
                    response.Message = "Nenhum registro encontrado!";
                    return response;
                }
                response.Data = book;
                response.Message = "Livro Encontrado!";
                response.Status = true;

                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<BookModel>>> BookByIdAuthor(int idAuthor)
        {
            ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();

            try
            {
                // Inclui os livros associados ao autor na consulta
                var author = await _context.Author
                                           .Include(a => a.Books)
                                           .FirstOrDefaultAsync(authorDataBase => authorDataBase.Id == idAuthor);

                // Verifica se o autor foi encontrado
                if (author == null)
                {
                    response.Message = "Nenhum autor encontrado!";
                    return response;
                }

                // Verifica se o autor tem livros associados
                if (author.Books == null || !author.Books.Any())
                {
                    response.Message = "Nenhum livro encontrado para este autor!";
                    return response;
                }

                // Retorna a lista de livros
                response.Data = author.Books.ToList();
                response.Message = "Livros encontrados!";
                response.Status = true;
                return response;

            }
            catch (Exception ex)
            {
                // Tratar exceção e retornar a mensagem de erro
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<BookModel>>> ListBooks()
        {
            ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();
            try
            {
                var books = await _context.Book.ToListAsync();
                response.Data = books;
                response.Message = "Todos os livros foram coletados!";
                response.Status = true;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<BookModel>> CreateBook(BookCreateDto bookCreateDto)
        {
            var response = new ResponseModel<BookModel>();

            try
            {
                // Verifique se o AuthorId existe
                var authorExists = await _context.Author.AnyAsync(a => a.Id == bookCreateDto.AuthorId);
                if (!authorExists)
                {
                    response.Message = "O autor especificado não existe.";
                    response.Status = false;
                    return response;
                }

                // Mapear BookCreateDto para BookModel
                var book = new BookModel
                {
                    Title = bookCreateDto.Title,
                    Description = bookCreateDto.Description,
                    TitleUrl = bookCreateDto.TitleUrl,
                    AuthorId = bookCreateDto.AuthorId // Certifique-se de que está mapeando o AuthorId corretamente
                };

                // Adicionar o livro ao contexto e salvar
                _context.Book.Add(book);
                await _context.SaveChangesAsync();

                response.Data = book;
                response.Message = "Livro criado com sucesso!";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = $"Erro: {ex.Message}. Detalhes: {ex.InnerException?.Message}";
                response.Status = false;
            }

            return response;
        }

        public async Task<ResponseModel<BookModel>> UpdateBook(int idBook, BookUpdateDto bookUpdateDto)
        {
            var response = new ResponseModel<BookModel>();

            try
            {
                var book = await _context.Book.FindAsync(idBook);
                if (book == null)
                {
                    response.Message = "Livro não encontrado!";
                    response.Status = false;
                    return response;
                }

                book.Title = bookUpdateDto.Title;
                book.Description = bookUpdateDto.Description;
                book.TitleUrl = bookUpdateDto.TitleUrl;
                book.AuthorId = bookUpdateDto.AuthorId; // Atualiza a relação com o autor

                _context.Book.Update(book);
                await _context.SaveChangesAsync();

                response.Data = book;
                response.Message = "Livro atualizado com sucesso!";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = $"Erro: {ex.Message}. Detalhes: {ex.InnerException?.Message}";
                response.Status = false;
            }

            return response;
        }

        public async Task<ResponseModel<bool>> DeleteBook(int idBook)
        {
            var response = new ResponseModel<bool>();

            try
            {
                var book = await _context.Book.FindAsync(idBook);
                if (book == null)
                {
                    response.Message = "Livro não encontrado!";
                    response.Status = false;
                    response.Data = false;
                    return response;
                }
                _context.Book.Remove(book);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Livro deletado com sucesso!";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = $"Erro: {ex.Message}. Detalhes: {ex.InnerException?.Message}";
                response.Status = false;
                response.Data = false;
            }

            return response;
        }

    }
}