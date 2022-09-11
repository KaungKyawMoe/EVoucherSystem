using EVoucherSystem.Dtos;

namespace EVoucherSystem.Models
{
    public class PurchaseEVoucherViewModel
    {
        public Guid Id { get; set; }
        public PurchaseDto purchaseDto { get; set; }

        public List<EVoucherDto> eVoucherDtos { get; set; }
    }
}
