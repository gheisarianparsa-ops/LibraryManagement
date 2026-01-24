using LibraryManagementApi.Models.ProductModels;

namespace LibraryManagementApi.Models.PriceFluctuationsModel
{
    public class PriceFluctModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public decimal NewPrice { get; set; }
        public decimal DeltaPrice { get; set; }
        public DateTimeOffset DateToApply { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;
    }
}
