namespace SimpleShop.Models
{
    using System.ComponentModel.DataAnnotations;
    using Orchard.ContentManagement;
    using System.Collections.Generic;        

    public class CustomerPart : ContentPart<CustomerPartRecord>
    {
        public int UserID
        {
            get { return Record.UserID; }
            set { Record.UserID = value; }
        }
        
        [Required(ErrorMessage = "Please enter a valid email address.")]
        // TODO: if on older browsers, need to replace this constraint with a regex
        [DataType(DataType.EmailAddress)] 
        public string Email
        {
            get { return Record.Email; }
            set { Record.Email = value; }
        }

        [Required(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber
        {
            get { return Record.PhoneNumber; }
            set { Record.PhoneNumber = value; }
        }

        
    }
}