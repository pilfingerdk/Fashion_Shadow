using System;
using System.Configuration;
using System.ComponentModel;
using System.Globalization;

namespace SimpleShop.Helpers
{  
    
    /// <summary>
    /// TODO: This class would normally get its config info for the specific account out of a root web config for the MVC 3 app, as AppSettings
    /// TODO: Implement Piotr Szmyd's solution for settings in Orchard instead of reying on hardcoding the values here (see notes)
    /// 
    /// <appSettings>
    /// <add key="PayPal:Sandbox" value="True" />
    /// <add key="PayPal:Username" value="*" />
    /// <add key="PayPal:Password" value="*" />
    /// <add key="PayPal:Signature" value="*" />
    /// <add key="PayPal:ReturnUrl" value="http://www.mysite.com" />
    ///<add key="PayPal:CancelUrl" value="http://www.mysite.com" />
    /// </appSettings>
    /// </summary>
    
    
    
    
    public static class PayPalAPISettings
    {
        public static string ApiDomain
        {
            get
            {
                // return Setting<bool>("PayPal:Sandbox") ? "api-3t.sandbox.paypal.com" : "api-3t.paypal.com";
                return "api-3t.sandbox.paypal.com";
            }
        }

        public static string CgiDomain
        {
            get
            {
                // return Setting<bool>("PayPal:Sandbox") ? "www.sandbox.paypal.com" : "www.paypal.com";
                return "www.sandbox.paypal.com";
            }
        }

        
        // TODO: remove hardcoded Sandbox Acc details for sig, uid, pwd       
        public static string Signature
        {
            get
            {
                // return Setting<string>("PayPal:Signature");
                return "YOUR SIG STRING HERE";
            }
        }

        public static string Username
        {
            get
            {
                // return Setting<string>("PayPal:Username");
                return "YOUR UID HERE";
            }
        }

        public static string Password
        {
            get
            {
                // return Setting<string>("PayPal:Password");
                return "YOUR PWD HERE";
            }
        }

        public static string ReturnUrl
        {
            get
            {
                // return Setting<string>("PayPal:ReturnUrl");
                // TODO: check your local port number here - is your Orchard instance always running on 30320?
                // If not, you can experiment with forcing a port number by changing the sln web project's settings under
                // Project >> Orchard.Web Properties >> Web >> 'Servers' section.
                return "http://localhost:30320/OrchardLocal/SimpleShop/Order/OrderCompleted";
            }
        }

        public static string CancelUrl
        {
            get
            {
                // return Setting<string>("PayPal:CancelUrl");
                // TODO: Handle the case where an order fails to complete - look at the hints mentioned in the 
                // unfinished "OrderCompleted" method, in the OrderController.
                return "http://localhost:30320/OrchardLocal/OrderFailed.cshtml";
            }
        }

        private static T Setting<T>(string name)
        {
            string value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                throw new Exception(String.Format("Could not find setting '{0}',", name));
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}
