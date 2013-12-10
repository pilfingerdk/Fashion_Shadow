namespace SimpleShop.Models
{
    using System.ComponentModel.DataAnnotations;
    using Orchard.ContentManagement;
    using System.Collections.Generic;
    using Orchard.Core.Title.Models;

    public class OrderPart : ContentPart<OrderPartRecord>
    {
        public OrderPart()
        {
            CartLines = new List<CartLineRecord>();
        }

        public IList<CartLineRecord> CartLines { get; set; }

        public int CustomerID {
            get { return Record.CustomerID; }
            set { Record.CustomerID = value; }
        }

        // Access to title in the UI for the order
        public string Title
        {
            get { return this.As<TitlePart>().Title; }
            set { this.As<TitlePart>().Title = value; }
        }

        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName
        {
            get { return Record.FirstName; }
            set { Record.FirstName = value; }
        }

        [Required(ErrorMessage = "Please enter a last name.")]
        public string LastName
        {
            get { return Record.LastName; }
            set { Record.LastName = value; }
        }

        [Required(ErrorMessage = "Please enter the first address line.")]
        public string Line1
        {
            get { return Record.Line1; }
            set { Record.Line1 = value; }
        }
        public string Line2
        {
            get { return Record.Line2; }
            set { Record.Line2 = value; }
        }
        

        [Required(ErrorMessage = "Please enter a city name.")]
        public string City
        {
            get { return Record.City; }
            set { Record.City = value; }
        }

        [Required(ErrorMessage = "Please enter a country name.")]
        public string Country
        {
            get { return Record.Country; }
            set { Record.Country = value; }
        }

        [Required(ErrorMessage = "Please enter a postal code.")]
        public string PostCode
        {
            get { return Record.PostCode; }
            set { Record.PostCode = value; }
        }

        public bool GiftWrap {
            get { return Record.GiftWrap; }
            set { Record.GiftWrap = value; }
         }
    }
}