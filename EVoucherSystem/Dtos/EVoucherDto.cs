using EVoucherSystem.Models;

namespace EVoucherSystem.Dtos
{
    public class EVoucherDto
    {
        public int Id { get; set; }

        public Guid EVoucherCode { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime ExpiredDate { get; set; }

        public String Image { get; set; }

        public decimal Amount { get; set; }

        public int MaxBuyLimit { get; set; }

        public int GiftPerUserLimit { get; set; }

        public bool IsActive { get; set; }

        public bool IsDeleted { get; set; }
    }
}
