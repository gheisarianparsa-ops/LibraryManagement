namespace LibraryManagementApi.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<BookModel> Books { get; set; }
    }
}
