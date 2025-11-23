namespace LibraryManagementApi.Models.BookModels
{
    public class BookCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
    }
}
