using LibraryManagementApi.Models.OrderItemsModels;
using LibraryManagementApi.Models.UserModels;

namespace LibraryManagementApi.Models.OrderModels
{
    public class OrderModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset OrderDate { get; set; }=DateTimeOffset.Now;
        public int UserId { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }
        public UserModel User { get; set; }
        public ICollection<OrderItemModel> OrderItems { get; set; }
    }

}
