namespace SimpleShop.Drivers
{
    using System;
    using Models;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Drivers;
    using Orchard.ContentManagement.Handlers;

    public class CustomerDriver : ContentPartDriver<CustomerPart>
    {
        protected override DriverResult Display(CustomerPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape(
                    "Parts_Customer",
                    () => shapeHelper.Parts_Customer(userID: part.UserID, Email: part.Email, PhoneNumber: part.PhoneNumber));  
        }

        protected override DriverResult Editor(CustomerPart part, dynamic shapeHelper)
        {
            return ContentShape(
                    "Parts_Customer_Edit",
                    () => shapeHelper.EditorTemplate(TemplateName: "Parts/Customer", Model: part, Prefix: Prefix));
        }

        protected override DriverResult Editor(CustomerPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return this.Editor(part, shapeHelper);
        }
    }
}