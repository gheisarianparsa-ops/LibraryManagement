namespace LibraryManagementApi.Models.PriceFluctuationsModel
{
    public class PriceFluctCreateDto
    {
        public int ProductId { get; set; }
        public decimal NewPrice { get; set; }
        public decimal OldPrice { get; set; }
        public DateTimeOffset DateToApply { get; set; }
    }
}
