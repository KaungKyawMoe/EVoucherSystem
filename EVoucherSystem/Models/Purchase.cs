namespace EVoucherSystem.Models
{
    public class Purchase
    {
        public int Id { get; set; }

        public int EVoucherId { get; set; }

        public string PromoCode { get; set; }

        public int Qty { get; set; }

        public decimal Amount { get; set; }

        public PurchaseType BuyType { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public string Name { get; set; }

        public string Phno { get; set; }

        public bool IsDeleted { get; set; }
    }
}
