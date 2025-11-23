namespace LibraryManagementApi.Models
{
    public class BookModel
    {
        int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public AuthorModel Author { get; set; }
    }
}
