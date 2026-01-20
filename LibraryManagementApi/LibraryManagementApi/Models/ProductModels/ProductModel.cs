using LibraryManagementApi.Models.CategoryModels;
using LibraryManagementApi.Models.OrderItemsModels;

namespace LibraryManagementApi.Models.ProductModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public ICollection<OrderItemModel> OrderItems { get; set; }
        public ICollection<CategoryModel> Categories { get; set; }
    }
}
