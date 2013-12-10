namespace SimpleShop.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Linq;
    using System.Web;
    using System.Collections.Specialized;
    using System.Net;
    using System.IO;
    using System.Globalization;
    using Helpers;
    using Models;

    public class PayPalUtils
    {
        public static PayPalRedirect ExpressCheckout(PayPalOrder order)
        {
            NameValueCollection values = new NameValueCollection();

            values["METHOD"] = "SetExpressCheckout";
            values["RETURNURL"] = PayPalAPISettings.ReturnUrl;
            values["CANCELURL"] = PayPalAPISettings.CancelUrl;            
            values["PAYMENTACTION"] = "Sale";
            values["CURRENCYCODE"] = "DKK"; // "USD";
            values["BUTTONSOURCE"] = "PP-ECWizard";
            values["USER"] = PayPalAPISettings.Username;
            values["PWD"] = PayPalAPISettings.Password;
            values["SIGNATURE"] = PayPalAPISettings.Signature;
            values["SUBJECT"] = "";
            values["VERSION"] = "72.0";
            values["AMT"] = order.Amount.ToString("f").Replace(",", "."); // Takes care of commas coming in with a Danish OS / browser. Alternative: (CultureInfo.InvariantCulture);
            
            // Handling to allow for transmitting multiple items in shopping cart
            if (order.UserCart == null) { order.UserCart = CartHelper.GetCart(); }
            if (order.UserCart.Lines.Count() > 0)
            {
                values["ITEMAMT"] = order.UserCart.ComputeTotalValue().ToString("f").Replace(",", ".");
                int x = 0;
                foreach (var item in order.UserCart.Lines)
                {
                    values["L_QTY" + x.ToString()] = item.Quantity.ToString();
                    values["L_NAME" + x.ToString()] = item.ProductTitle;
                    values["L_AMT" + x.ToString()] = item.Price.ToString("f").Replace(",", ".");
                    x++;
                }
            }

            values = Submit(values);

            string ack = values["ACK"].ToLower();

            // debug ack here to check  - should return a success with the f used above
            if (ack == "success" || ack == "successwithwarning")
            {
                return new PayPalRedirect
                {
                    Token = values["TOKEN"],
                    Url = String.Format("https://{0}/cgi-bin/webscr?cmd=_express-checkout&token={1}",
                       PayPalAPISettings.CgiDomain, values["TOKEN"])
                };
            }
            else
            {
                // can result in an ysod with the relevant error msg returned from PayPal, ie. 'currency not supported' or similar
                throw new Exception(values["L_LONGMESSAGE0"]);
            }
        }

        private static NameValueCollection Submit(NameValueCollection values)
        {
            string data = String.Join("&", values.Cast<string>()
              .Select(key => String.Format("{0}={1}", key, HttpUtility.UrlEncode(values[key]))));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(
               String.Format("https://{0}/nvp", PayPalAPISettings.ApiDomain));

            request.Method = "POST";
            request.ContentLength = data.Length;

            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(data);
            }

            using (StreamReader reader = new StreamReader(request.GetResponse().GetResponseStream()))
            {
                return HttpUtility.ParseQueryString(reader.ReadToEnd());
            }
        }
 
        // TODO: Move these two methods out into separate classes?        
        public class PayPalOrder
        {
            public decimal Amount { get; set; }
            public CartPart UserCart  { get; set; }
        }

        public class PayPalRedirect
        {
            public string Url { get; set; }
            public string Token { get; set; }
        }

    }
}
    
    
    
    
    
    
        