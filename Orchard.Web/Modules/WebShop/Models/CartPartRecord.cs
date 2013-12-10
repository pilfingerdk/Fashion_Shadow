namespace SimpleShop.Models
{
    using Orchard.ContentManagement.Records;
    using System.Collections.Generic;

    public class CartPartRecord : ContentPartRecord
    {        
        public CartPartRecord()
        {
            CartLines = new List<CartLineRecord>();
        }
        public virtual IList<CartLineRecord> CartLines { get; set; }
    }
}
