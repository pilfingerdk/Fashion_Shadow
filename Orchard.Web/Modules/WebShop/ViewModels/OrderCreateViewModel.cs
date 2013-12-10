namespace SimpleShop.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using Orchard.ContentManagement;
    using System.Collections.Generic;

    public class OrderCreateViewModel
    {

        [Required(ErrorMessage = "Please enter a first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter a last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the first address line.")]
        public string Line1 { get; set; }
        public string Line2 { get; set; }       


        [Required(ErrorMessage = "Please enter a city name.")]
        public string City { get; set; }


        [Required(ErrorMessage = "Please enter a country name.")]
        public string Country { get; set; }


        [Required(ErrorMessage = "Please enter a postal code.")]
        public string PostCode { get; set; }

        public bool GiftWrap { get; set; }
    }
}
