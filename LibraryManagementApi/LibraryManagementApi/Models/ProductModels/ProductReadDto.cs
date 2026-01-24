namespace LibraryManagementApi.Models.ProductModels
{
    public class ProductReadDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<string> CategoryNames { get; set; }
        public ICollection<int> CategoryIds { get; set; }
    }
}
