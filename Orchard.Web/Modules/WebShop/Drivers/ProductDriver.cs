namespace SimpleShop.Drivers
{
    using System;
    using Models;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Drivers;
    using Orchard.ContentManagement.Handlers;

    public class ProductDriver : ContentPartDriver<ProductPart>
    {
        protected override DriverResult Display(ProductPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape(
                    "Parts_Product",
                    () => shapeHelper.Parts_Product(
                        Sku: part.Sku,
                        Price: part.Price,
                        // ImageUrl: part.ImageUrl,
                        Description: part.Description,
                        ItemId: part.ContentItem.Id));                        
        }

        protected override DriverResult Editor(ProductPart part, dynamic shapeHelper)
        {
            return ContentShape(
                    "Parts_Product_Edit",
                    () => shapeHelper.EditorTemplate(TemplateName: "Parts/Product", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(ProductPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return this.Editor(part, shapeHelper);
        }

    }
}