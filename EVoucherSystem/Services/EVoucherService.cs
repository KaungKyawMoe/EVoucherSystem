using EVoucherSystem.Dtos;
using EVoucherSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace EVoucherSystem.Services
{
    public class EVoucherService : IEvoucherService
    {
        private readonly EVoucherDbContext _context;

        public EVoucherService(EVoucherDbContext context)
        {
            _context= context;
        }

        public async Task<List<EVoucher>> GetAllEVouchers(bool? showActive = null)
        {
            if (showActive != null && showActive.HasValue)
            {
                return await _context.EVouchers.AsNoTracking().Where(x => x.IsActive == showActive).ToListAsync();
            }
            else
            {
                return await _context.EVouchers.AsNoTracking().ToListAsync();
            }
        }

        public async Task<EVoucher> GetEVoucherById(int id)
        {
            EVoucher result = await _context.EVouchers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return result;
        }

        public async Task<EVoucher> SaveEvoucher(EVoucher eVoucher)
        {
            eVoucher.EVoucherCode = Guid.NewGuid();
            var data = await _context.EVouchers.FirstOrDefaultAsync();
            if(data == null)
            {
                eVoucher.Id = 1;
            }
            else
            {
                eVoucher.Id = _context.EVouchers.Max(x => x.Id) + 1;
            }
            
            await _context.EVouchers.AddAsync(eVoucher);
            await _context.SaveChangesAsync();

            return eVoucher;
        }

        public async Task<EVoucher> UpdateEvoucher(int id, EVoucher eVoucher)
        {
            _context.EVouchers.Update(eVoucher);
            await _context.SaveChangesAsync();

            return eVoucher;
        }
    }
}
