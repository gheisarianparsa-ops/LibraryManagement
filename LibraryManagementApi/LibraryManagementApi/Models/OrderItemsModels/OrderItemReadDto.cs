namespace LibraryManagementApi.Models.OrderItemsModels
{
    public class OrderItemReadDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal FeePrice { get; set; }
        public decimal TotalPrice => Quantity * FeePrice;
    }
}
