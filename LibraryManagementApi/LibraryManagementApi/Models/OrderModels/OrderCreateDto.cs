using LibraryManagementApi.Models.OrderItemsModels;

namespace LibraryManagementApi.Models.OrderModels
{
    public class OrderCreateDto
    {
        public string Name { get; set; }
        public ICollection<OrderItemModel> OrderItems { get; set; }
    }

}
