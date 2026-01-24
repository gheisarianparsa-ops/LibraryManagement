namespace LibraryManagementApi.Models.ProductModels
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTimeOffset? NewPriceDateToApply { get; set; }
        public ICollection<int> CategoryIds { get; set; }
    }
}
