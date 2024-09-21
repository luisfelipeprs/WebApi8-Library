using WebApi8_Library.Dto.Author;
namespace WebApi8_Library.Dto.Book
{
    public class BookCreateDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string TitleUrl { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public AuthorCreateDto Author { get; set; }
    }
}