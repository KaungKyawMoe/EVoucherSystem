using EVoucherSystem.Models;

namespace EVoucherSystem.Services
{
    public interface IEvoucherService
    {
        public Task<List<EVoucher>> GetAllEVouchers(bool? showActive = null);

        public Task<EVoucher> GetEVoucherById(int id);

        public Task<EVoucher> SaveEvoucher(EVoucher eVoucher);

        public Task<EVoucher> UpdateEvoucher(int id, EVoucher eVoucher);
    }
}
