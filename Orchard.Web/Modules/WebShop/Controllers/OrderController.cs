namespace SimpleShop.Controllers
{    
    using System.Linq;
    using System.Web.Mvc;
    using Models;
    using ViewModels;
    using Helpers;    
    using Orchard.ContentManagement;
    using Orchard.Localization;

    using Orchard;
    using Orchard.Data;
    using Orchard.Mvc;
    using Orchard.DisplayManagement;
    using Orchard.Themes;
    using Orchard.Core.Title.Models;
    using Orchard.Security;
    // using PayPal; // TODO: set ref to lib folder's paypal dll if necessary 
    

    [Themed]
    public class OrderController : Controller, IUpdateModel
    {
        private readonly IRepository<CartLineRecord> _cartLineRepository;
        private readonly IRepository<OC_AssociationRecord> _associationRepository;
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;
        dynamic Shape { get; set; }

        public OrderController(IOrchardServices orchardServices, IShapeFactory shapeFactory,
            IRepository<CartLineRecord> cartLineRepo, IRepository<OC_AssociationRecord> associationRepo)
        {            
            _orchardServices = orchardServices;
            _contentManager = orchardServices.ContentManager;
            _cartLineRepository = cartLineRepo;
            _associationRepository = associationRepo;
            Shape = shapeFactory;
        }      
                
        
        // HTTP Get: show checkout form for collection of user's shipping details        
        public ActionResult Checkout(string returnUrl)
        {
            CartPart cart = CartHelper.GetCart();      

            var part = _contentManager.New<OrderPart>("Order");
            _contentManager.Create(part);
            part.Title = "Black Hat Magic Inc Order No. " + part.ContentItem.Id;        
            part.FirstName = "First name here please";          
      
            dynamic editor = Shape.EditorTemplate(TemplateName: "Checkout",
                    Model: part.As<OrderPart>(),
                    Prefix: null);            

            if (!ModelState.IsValid)
            {
                _orchardServices.TransactionManager.Cancel();               
                return View((object)part);
            }
            
            return new ShapeResult(this, editor);                                
        }
                

        // HTTP Post
        [HttpPost]
        // public ActionResult Checkout(OrderPart orderData) // this will crash on model binding, so no go.
        public ActionResult Checkout(
            int orderId, 
            string firstName, 
            string lastName,
            string line1,
            string line2,
            string postCode,
            string city,
            string country,
            bool giftwrap   
            )        
        {
            CartPart cart = CartHelper.GetCart();
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            var order = _contentManager.Get<OrderPart>(orderId);
            
            if (order == null)
                return HttpNotFound();
            
            order.FirstName = firstName;
            order.LastName = lastName;
            order.Line1 = line1;
            order.Line2 = line2;
            order.PostCode = postCode;
            order.City = city;
            order.Country = country;
            order.GiftWrap = giftwrap;            

            var model = _contentManager.UpdateEditor(order, this);

            if (!ModelState.IsValid)
            {
                _orchardServices.TransactionManager.Cancel();                
                return View("EditorTemplates/Checkout", order);
            }          

            if (ModelState.IsValid)
            {                
                foreach (CartLineRecord line in cart.Lines)
                {
                    line.OrderId = order.Record.Id;
                    _cartLineRepository.Create(line);                    
                }
                
                // build a viewmodel containing the cart and the order details, 
                // and send this to the paypal processing page
                var viewmodel = new CheckoutVM {
                    CheckoutCart = cart,
                    CheckoutOrder = order,
                    TotalValue = cart.ComputeTotalValue()
                };               

                // return View("PayPalCheckout1", viewmodel);    // Uses BuyNow button from API Wrapper in helper DLL               
                return View("PayPalCheckout3", viewmodel);       // PP Express button for custom API wrapper and calls
            }
            else
            {
                return View("EditorTemplates/Checkout", order);
            }
        }


        public ActionResult Pay()
        {
            CartPart cart = CartHelper.GetCart();
            if (cart.Lines.Count() == 0)
            {
                TempData["message"] = "Sorry, your cart is empty!";
            }

            SimpleShop.Helpers.PayPalUtils.PayPalRedirect redirect = PayPalUtils.ExpressCheckout(new PayPalUtils.PayPalOrder {
                Amount = cart.ComputeTotalValue(),
                UserCart = cart 
            });
            
            // TODO: Explain about token issues here
            Session["token"] = redirect.Token;
            return new RedirectResult(redirect.Url);
        }

        public ViewResult OrderCompleted(string token, string payerId)
        {
            // TODO: Use the token and payerId here, to contact paypal again.
            // From here, You could pass this information on to a new method you make yourself in the public static class
            // PayPalUtils.cs; this can then process the token and payerId to construct a call to the "DoExpressCheckout" 
            // method on the Paypal website / the Paypal API, in order to complete the transaction. 
            // You would need to construct the call as dictated by the Paypal API; see the existing method:

            // "public static PayPalRedirect ExpressCheckout(PayPalOrder order)"

            // - in PayPalUtils.cs for an example of setting up such a call.

            return View();
        }


        // required by IUpdateModel
        bool IUpdateModel.TryUpdateModel<TModel>(TModel model, string prefix, string[] includeProperties, string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }

    }
}

