namespace LibraryManagementApi.Models.OrderItemsModels
{
    public class OrderItemReadDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int FeePrice { get; set; }
        public int TotalPrice => Quantity * FeePrice;
    }
}
