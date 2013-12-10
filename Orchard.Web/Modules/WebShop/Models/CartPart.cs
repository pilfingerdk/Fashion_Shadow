namespace SimpleShop.Models
{
    using System.ComponentModel.DataAnnotations;
    using Orchard.ContentManagement;
    using System.Collections.Generic;
    using System.Linq;

    public class CartPart : ContentPart<CartPartRecord>
    {

        private List<CartLineRecord> lineCollection = new List<CartLineRecord>();        

        public void AddItem(ProductPartRecord product, string productTitle, int quantity)
        {
            CartLineRecord line = lineCollection
                .Where(p => p.ProductId == product.Id)
                .FirstOrDefault();            
            
            if (line == null)
            {
                lineCollection.Add(new CartLineRecord { ProductId = product.Id, Quantity = quantity, ProductTitle = productTitle, Price = product.Price });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(ProductPartRecord product)
        {
            lineCollection.RemoveAll(l => l.ProductId == product.Id);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Price * e.Quantity);
        }

        public int TotalItemsInCart()
        {
            return lineCollection.Sum(x => x.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLineRecord> Lines
        {
            get { return lineCollection; }
        }
    }
}