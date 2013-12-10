namespace SimpleShop.Helpers
{    
    using Models;
    using System.Web;    

    public class CartHelper
    {        
        // Connect to session object, get cart        
        public static CartPart GetCart()
        {            
            CartPart cart = (CartPart)HttpContext.Current.Session["Cart"];
            if (cart == null)
            {
                cart = new CartPart();                
                HttpContext.Current.Session["Cart"] = cart;
            }
            return cart;
        }
    }
}
