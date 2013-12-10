namespace SimpleShop.Drivers
{
    using System;
    using Models;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Drivers;
    using Orchard.ContentManagement.Handlers;

    public class OrderDriver : ContentPartDriver<OrderPart>
    {
        protected override DriverResult Display(OrderPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape(
                    "Parts_Order",
                    () => shapeHelper.Parts_Order(FirstName: part.FirstName, LastName: part.LastName, Line1: part.Line1, Line2: part.Line2,
                       City: part.City, Country: part.Country, PostCode: part.PostCode, GiftWrap: part.GiftWrap));  // CartLines: part.CartLines,
        }      

        
        // This editor override is needed to be able to save an order part with the values entered by the user for address etc.
        // According to Bertrand le Roy, "The modifications applied to parts are handled by the Drivers. They can be seen as controllers 
        // for specific portions of the UI." Source: http://orchard.codeplex.com/discussions/230525
        protected override DriverResult Editor(OrderPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return this.Editor(part, shapeHelper);
        }        
    }
}