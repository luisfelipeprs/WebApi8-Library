namespace WebApi8_Library.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public AuthorModel Author { get; set; }
        public string TitleUrl { get; set; } = string.Empty;
    }
}