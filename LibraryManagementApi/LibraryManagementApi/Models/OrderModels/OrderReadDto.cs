using LibraryManagementApi.Models.OrderItemsModels;

namespace LibraryManagementApi.Models.OrderModels
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }
        public ICollection<OrderItemReadDto> OrderItems { get; set; } = new List<OrderItemReadDto>();
    }

}
