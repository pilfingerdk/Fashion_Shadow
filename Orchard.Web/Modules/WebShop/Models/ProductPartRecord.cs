namespace SimpleShop.Models
{
    using Orchard.ContentManagement.Records;

    public class ProductPartRecord : ContentPartRecord
    {
        public virtual string Sku { get; set; }

        public virtual decimal Price { get; set; }

        public virtual string Description { get; set; }
        
    }
}