namespace LibraryManagementApi.Models.PriceFluctuationsModel
{
    public class PriceFluctReadDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public decimal NewPrice { get; set; }
        public DateTimeOffset DateToApply { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
   
    
}
