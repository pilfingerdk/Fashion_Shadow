namespace SimpleShop.Handlers
{
    using Models;
    using Orchard.ContentManagement.Handlers;
    using Orchard.Data;
    using Orchard;
    using System.Web;
    using System.Web.Mvc;
    using SimpleShop.Controllers;
    using Helpers;

    public class SummaryWidgetHandler : ContentHandler
    {
        protected override void BuildDisplayShape(BuildDisplayContext context)
           {                       
            base.BuildDisplayShape(context);
                if (context.ContentItem.ContentType == "SummaryWidget")
                {
                    // TODO: Bind cart model w custom binder in module (difficult), so we can get current cart more elegantly than through the helper
                    CartPart cart = CartHelper.GetCart();                    
                    dynamic summaryDisplay = context.New.CartSummary(
                        TotalItems: cart.TotalItemsInCart(),
                        TotalValue: cart.ComputeTotalValue()
                    );
                    // Check if we are showing the cart - if not, display the widget
                    // Matching for string "Cart" will also remove widget from checkout view
                    string url =  HttpContext.Current.Request.Url.ToString();
                    if (!url.Contains("Cart"))
                    {
                        context.Shape.Zones["Content"].Add(summaryDisplay);
                    }
                    
                }
           }
        }
    }
