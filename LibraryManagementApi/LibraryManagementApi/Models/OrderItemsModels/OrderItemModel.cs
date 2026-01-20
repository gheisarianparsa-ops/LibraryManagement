using LibraryManagementApi.Models.OrderModels;
using LibraryManagementApi.Models.ProductModels;

namespace LibraryManagementApi.Models.OrderItemsModels
{
    public class OrderItemModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderModel Order { get; set; }
        public int Quantity { get; set; }
        public int FeePrice { get; set; }
        public int TotalPrice => Quantity * FeePrice;
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
    }
}
