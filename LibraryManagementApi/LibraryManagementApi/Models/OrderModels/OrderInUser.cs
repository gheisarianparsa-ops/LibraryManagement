namespace LibraryManagementApi.Models.OrderModels
{
    public class OrderInUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public int TotalPrice { get; set; }
        public string Status { get; set; }
    }
}
