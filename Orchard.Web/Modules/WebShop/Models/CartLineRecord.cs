namespace SimpleShop.Models
{
    using Orchard.ContentManagement.Records;

    public class CartLineRecord
    {        
        public virtual int Id { get; set; }
        
        public virtual int ProductId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual string ProductTitle { get; set; }
        public virtual int Quantity { get; set; }
        public virtual decimal Price { get; set; }        
    }
}
