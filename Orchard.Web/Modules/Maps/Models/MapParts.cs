using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace Maps.Models
{
    public class MapRecord : ContentPartRecord
    {
        public virtual float Latitude { get; set; }
        public virtual float Longitude { get; set; }
    }

    public class MapParts : ContentPart<MapRecord>
    {
        [Required]
        public float Latitude
        {
            get { return Record.Latitude; }
            set { Record.Latitude = value; }
        }

        [Required]
        public float Longitude
        {
            get { return Record.Longitude; }
            set { Record.Longitude = value; }
        }
    }
}