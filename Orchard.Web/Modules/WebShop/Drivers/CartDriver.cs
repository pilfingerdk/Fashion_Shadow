namespace SimpleShop.Drivers
{
    using System;
    using Models;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Drivers;
    using Orchard.ContentManagement.Handlers;

    public class CartDriver : ContentPartDriver<CartPart>
    {
        protected override DriverResult Display(CartPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape(
                    "Parts_Cart",
                    () => shapeHelper.Parts_Cart(Lines: part.Lines, 
                        TotalPrice: part.ComputeTotalValue(),
                        TotalItems: part.TotalItemsInCart(),
                        CartId: part.ContentItem.Id
                        ));
        }        
    }
}