namespace SimpleShop.Handlers
{
    using Models;

    using Orchard.ContentManagement.Handlers;
    using Orchard.Data;

    public class CartHandler : ContentHandler
    {
        public CartHandler(IRepository<CartPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}