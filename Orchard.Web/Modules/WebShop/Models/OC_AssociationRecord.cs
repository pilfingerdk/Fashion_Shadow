using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleShop.Models
{
    public class OC_AssociationRecord
    {
        public virtual int Id { get; set; }
        public virtual int OrderPartRecord_id { get; set; }
        public virtual int CustomerPartRecord_id { get; set; }
    }
}

