namespace SimpleShop.Models
{
    using Orchard.ContentManagement.Records;
    using System.Collections;
    using System.Collections.Generic;

    public class OrderPartRecord : ContentPartRecord
    {
        public virtual int CustomerID { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }

        public virtual string Line1 { get; set; }
        public virtual string Line2 { get; set; }       

        public virtual string City { get; set; }
        public virtual string Country { get; set; }
        public virtual string PostCode { get; set; }
        public virtual bool GiftWrap { get; set; }
    }
}