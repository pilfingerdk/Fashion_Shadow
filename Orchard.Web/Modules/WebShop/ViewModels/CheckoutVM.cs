namespace SimpleShop.ViewModels
{    
    using SimpleShop.Models;

    public class CheckoutVM
    {
        public CartPart CheckoutCart { get; set; }
        public OrderPart CheckoutOrder { get; set; }
        public decimal TotalValue { get; set; }
    }
}
