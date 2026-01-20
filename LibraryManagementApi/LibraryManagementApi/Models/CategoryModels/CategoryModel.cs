using LibraryManagementApi.Models.ProductModels;

namespace LibraryManagementApi.Models.CategoryModels
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductModel> Products { get; set; }
    }
}
