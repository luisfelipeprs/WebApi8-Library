using Microsoft.EntityFrameworkCore;
using WebApi8_Library.Data;
using WebApi8_Library.Dto.Author;
using WebApi8_Library.Models;

namespace WebApi8_Library.Service.Author
{
    public class AuthorService : IAuthorInterface
    {
        private readonly AppDbContext _context;

        public AuthorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseModel<AuthorModel>> AuthorById(int idAuthor)
        {
            ResponseModel<AuthorModel> response = new ResponseModel<AuthorModel>();
            try
            {
                var author = await _context.Author.FirstOrDefaultAsync(authorsDataBase => authorsDataBase.Id == idAuthor);
                if (author == null)
                {
                    response.Message = "Nenhum registro encontrado!";
                    return response;
                }
                response.Data = author;
                response.Message = "Autor Encontrado!";
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

        public async Task<ResponseModel<AuthorModel>> AuthorByIdBook(int idBook)
        {
            ResponseModel<AuthorModel> response = new ResponseModel<AuthorModel>();

            try
            {
                var book = await _context.Book.Include(a => a.Author).FirstOrDefaultAsync(bookDataBase => bookDataBase.Id == idBook);
                if (book == null)
                {
                    response.Message = "Nenhum registro encontrado!";
                    return response;
                }
                response.Data = book.Author;
                response.Message = "Autor encontrado!";
                return response;

            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                return response;
            }
        }

        public async Task<ResponseModel<List<AuthorModel>>> ListAuthors()
        {
            ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();
            try
            {
                var authors = await _context.Author.ToListAsync();
                response.Data = authors;
                response.Message = "Todos os Autores foram coletados!";
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

        public async Task<ResponseModel<AuthorModel>> CreateAuthor(AuthorCreateDto authorCreateDto)
        {
            var response = new ResponseModel<AuthorModel>();

            try
            {
                // Mapear AuthorCreateDto para AuthorModel
                var author = new AuthorModel
                {
                    Name = authorCreateDto.Nome,
                    LastName = authorCreateDto.Sobrenome
                };

                // Adicionar o autor ao contexto e salvar
                _context.Author.Add(author);
                await _context.SaveChangesAsync();

                response.Data = author;
                response.Message = "Autor criado com sucesso!";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }

            return response;
        }

        public async Task<ResponseModel<AuthorModel>> UpdateAuthor(int idAuthor, AuthorUpdateDto authorUpdateDto)
        {
            var response = new ResponseModel<AuthorModel>();

            try
            {
                var author = await _context.Author.FirstOrDefaultAsync(a => a.Id == idAuthor);
                if (author == null)
                {
                    response.Message = "Nenhum registro encontrado!";
                    return response;
                }

                // Acessar as propriedades da instância 'authorUpdateDto'
                author.Name = authorUpdateDto.Nome;
                author.LastName = authorUpdateDto.Sobrenome;

                // Salvar as alterações
                await _context.SaveChangesAsync();

                response.Data = author;
                response.Message = "Autor atualizado com sucesso!";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
            }

            return response;
        }

        public async Task<ResponseModel<bool>> DeleteAuthor(int idAuthor)
        {
            var response = new ResponseModel<bool>();

            try
            {
                var author = await _context.Author.FirstOrDefaultAsync(a => a.Id == idAuthor);
                if (author == null)
                {
                    response.Message = "Nenhum registro encontrado!";
                    return response;
                }

                _context.Author.Remove(author);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Autor removido com sucesso!";
                response.Status = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.Status = false;
                response.Data = false;
            }

            return response;
        }
    }
}