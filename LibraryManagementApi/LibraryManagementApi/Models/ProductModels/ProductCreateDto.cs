namespace LibraryManagementApi.Models.ProductModels
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public ICollection<int> CategoryIds { get; set; }
    }
}
