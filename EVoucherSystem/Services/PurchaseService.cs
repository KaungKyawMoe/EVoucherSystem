using EVoucherSystem.Dtos;
using EVoucherSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EVoucherSystem.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly EVoucherDbContext _context;

        public PurchaseService(EVoucherDbContext context)
        {
            _context= context;
        }

        public async Task<List<Purchase>> GetAllPurchases()
        {
            return await _context.Purchases.AsNoTracking().ToListAsync();
        }

        public async Task<Purchase> GetPurchaseById(int id)
        {
            Purchase result = await _context.Purchases.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<Purchase> GetPurchaseByPromoCode(string promoCode)
        {
            Purchase result = await _context.Purchases.AsNoTracking().FirstOrDefaultAsync(x => x.PromoCode == promoCode);
            return result;
        }

        public async Task<Purchase> SavePurchase(Purchase purchase)
        {

            var data = await _context.Purchases.FirstOrDefaultAsync();
            if(data == null)
            {
                purchase.Id = 1;
            }
            else
            {
                purchase.Id = _context.Purchases.Max(x => x.Id) + 1;
            }
            
            await _context.Purchases.AddAsync(purchase);
            await _context.SaveChangesAsync();

            return purchase;
        }

        public async Task<Purchase> UpdatePurchase(int id, Purchase purchase)
        {
            _context.Purchases.Update(purchase);
            await _context.SaveChangesAsync();

            return purchase;
        }
    }
}
