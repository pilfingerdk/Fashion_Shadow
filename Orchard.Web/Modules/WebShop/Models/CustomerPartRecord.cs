namespace SimpleShop.Models
{
    using Orchard.ContentManagement.Records;
    using System.Collections;
    using System.Collections.Generic;

    public class CustomerPartRecord : ContentPartRecord
    {
        public virtual int UserID { get; set; }
        public virtual string Email { get; set; }
        public virtual string PhoneNumber { get; set; }
    }
}