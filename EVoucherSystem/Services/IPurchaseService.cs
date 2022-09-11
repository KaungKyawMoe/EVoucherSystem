using EVoucherSystem.Models;

namespace EVoucherSystem.Services
{
    public interface IPurchaseService
    {
        public Task<List<Purchase>> GetAllPurchases();

        public Task<Purchase> GetPurchaseById(int id);

        public Task<Purchase> GetPurchaseByPromoCode(string promoCode);

        public Task<Purchase> SavePurchase(Purchase eVoucher);

        public Task<Purchase> UpdatePurchase(int id, Purchase eVoucher);
    }
}
