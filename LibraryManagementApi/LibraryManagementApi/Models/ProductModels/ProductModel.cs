using LibraryManagementApi.Models.CategoryModels;
using LibraryManagementApi.Models.OrderItemsModels;
using LibraryManagementApi.Models.PriceFluctuationsModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementApi.Models.ProductModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        [NotMapped]
        public decimal ApplicablePrice
        {
            get
            {
                var priceFlucts = PriceFluctuations ?? new List<PriceFluctModel>();
                return priceFlucts
                    .Where(x => x.DateToApply <= DateTimeOffset.UtcNow)
                    .OrderByDescending(pf => pf.DateToApply)
                    .Select(pf => (decimal?)pf.NewPrice)
                    .FirstOrDefault() ?? 0; // fallback به 0
            }
        }
        public ICollection<PriceFluctModel> PriceFluctuations { get; set; } = new List<PriceFluctModel>();

        public ICollection<OrderItemModel> OrderItems { get; set; }
        public ICollection<CategoryModel> Categories { get; set; }
    }
}
