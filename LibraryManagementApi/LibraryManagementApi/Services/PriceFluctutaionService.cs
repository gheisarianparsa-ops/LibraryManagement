using LibraryManagementApi.Data;
using LibraryManagementApi.Models.PriceFluctuationsModel;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Services
{
    public class PriceFluctutaionService
    {
        private readonly LibraryManagementDbContext _context;
        public PriceFluctutaionService(LibraryManagementDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<PriceFluctModel> PriceUpdator(PriceFluctCreateDto PriceFluct)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == PriceFluct.ProductId);
            if (product is null)
            {
                throw new Exception("Product is Not Found");
            }
            var fluct = new PriceFluctModel
            {
                ProductId = PriceFluct.ProductId,
                NewPrice = PriceFluct.NewPrice,
                DateToApply = PriceFluct.DateToApply,
                CreatedDate = DateTimeOffset.UtcNow,
                DeltaPrice = PriceFluct.NewPrice - PriceFluct.OldPrice
            };
            if (fluct.DateToApply < DateTimeOffset.Now)
            {
                throw new Exception("Wrong Date");
            }
            _context.PriceFlucts.Add(fluct);
            await _context.SaveChangesAsync();
            return fluct;
        }
    }
}
