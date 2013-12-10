namespace SimpleShop.Controllers
{
    using System;
    using System.Linq;
    using Models;
    using ViewModels;
    using Orchard.Data;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Drivers;
    using Orchard.ContentManagement.Handlers;
    using System.Web.Mvc;
    
    using Orchard.Localization;    
    using Orchard.Mvc;
    using Orchard.Mvc.DataAnnotations;
    using System.ComponentModel.DataAnnotations;
    using Orchard.DisplayManagement;
    using Orchard.Themes;
    using Orchard;
    using Helpers;
    using Drivers;    

    /// <summary>
    /// Cart Controller. We use the ASP.NET session state feature to store and retrieve Cart objects.
    /// We want each user to have his own cart, and we want the cart to be persistent between requests. 
    /// Data associated with a session is deleted when a session expires (typically because a user hasn’t 
    /// made a request for a while), which means that we don’t need to manage the storage or life cycle 
    /// of the Cart objects.
    /// </summary>

    [Themed]
    public class CartController : Controller, IUpdateModel {
        private readonly IRepository<ProductPartRecord> _productRepository;        
        private readonly IOrchardServices _orchardServices;
        private readonly IContentManager _contentManager;        
        dynamic Shape { get; set; }        
  
        public CartController(IRepository<ProductPartRecord> productRepo,            
            IOrchardServices orchardServices, 
            IShapeFactory shapeFactory) 
            {
                _productRepository = productRepo;            
                _orchardServices = orchardServices;
                _contentManager = _orchardServices.ContentManager;            
                Shape = shapeFactory;             
            }

        // Connect to session using helper class, store cart in session.        
        private CartPart GetCart()
        {
            CartPart cart = CartHelper.GetCart();
            return cart;
        }

        
        public ActionResult Index(string returnUrl) {     
            CartPart cart = GetCart();            
            dynamic cartDisplay = Shape.Parts_Cart(Lines: cart.Lines, TotalValue: cart.ComputeTotalValue(), ReturnUrl: returnUrl);
            return new ShapeResult(this, cartDisplay);    
        }
              
        public ActionResult AddToCart(int productId, string productTitle, string returnUrl) {
            ProductPartRecord product = _productRepository.Fetch(p => p.Id == productId).FirstOrDefault();
            CartPart cart = GetCart();            
            if (product != null)
            {
                GetCart().AddItem(product, productTitle, 1);
            }                     
            
            dynamic cartDisplay = Shape.Parts_Cart(Lines: cart.Lines, TotalValue: cart.ComputeTotalValue(), ReturnUrl: returnUrl);
            return new ShapeResult(this, cartDisplay);          
        }

        public RedirectToRouteResult RemoveFromCart(int productId, string returnUrl) {
            ProductPartRecord product = _productRepository.Fetch(p => p.Id == productId).FirstOrDefault();
            CartPart cart = GetCart();
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
                

        /// <summary>
        /// Render out the cart summary
        /// - number of items and total value
        /// </summary>
        public ActionResult Summary()
        {
            CartPart cart = GetCart();
            dynamic summaryDisplay = _orchardServices.New.CartSummary(
                TotalItems: cart.TotalItemsInCart(),
                TotalValue: cart.ComputeTotalValue()
            );
            return new ShapeResult(this, summaryDisplay);
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
