namespace SimpleShop.Models
{
    using System.ComponentModel.DataAnnotations;
    using Orchard.ContentManagement;
    using Orchard.Core.Title.Models;

    public class ProductPart : ContentPart<ProductPartRecord>
    {
        ///<Summary>
        /// SKU: A short term for "Stock Keeping Unit". The SKU is a unique numerical identifying number 
        /// that refers to a specific stock item in a retailer's inventory or product catalog. 
        /// The SKU is often used to identify the product, product size or type, and the manufacturer. 
        /// In the retail industry, the SKU is a part of the backend inventory control system and enables 
        /// a retailer to track a product in their inventory that may be in warehouses or in retail outlets.
        /// </Summary>
        public string Sku
        {
            get { return Record.Sku; }
            set { Record.Sku = value; }
        }

        [Required]
        // [Range(0.0f, float.MaxValue, ErrorMessage = "The field Price must be a positive number.")]
        public decimal Price
        {
            get { return Record.Price; }
            set { Record.Price = value; }
        }

        [Required]
        public string Description
        {
            get { return Record.Description; }
            set { Record.Description = value; }
        }

    }
}