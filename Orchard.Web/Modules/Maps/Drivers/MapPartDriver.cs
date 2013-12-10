using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maps.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;

namespace Maps.Drivers
{
    public class MapPartDriver : ContentPartDriver<MapParts>
    {
        protected override DriverResult Display(MapParts part, string displayType, dynamic shapeHelper)
        {
            if (displayType == "Summary")
                return ContentShape("Parts_Map_Summary",
                    () => shapeHelper.Parts_Map_Summary(Longitude: part.Longitude, Latitude: part.Latitude));

            return ContentShape("Parts_Map",
                () => shapeHelper.Parts_Map(Longitude: part.Longitude, Latitude: part.Latitude));
        }

        //GET: Added "Prefix: Prefix" to EditorTemplate params
        protected override DriverResult Editor(MapParts part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Map_Edit",
                () => shapeHelper.EditorTemplate(TemplateName: "Parts/Map", Model: part, Prefix: Prefix));
        }
        //POST
        protected override DriverResult Editor(MapParts part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}