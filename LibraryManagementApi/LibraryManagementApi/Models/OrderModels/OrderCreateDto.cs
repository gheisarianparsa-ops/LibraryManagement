using LibraryManagementApi.Models.OrderItemsModels;

namespace LibraryManagementApi.Models.OrderModels
{
    public class OrderCreateDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }

}
