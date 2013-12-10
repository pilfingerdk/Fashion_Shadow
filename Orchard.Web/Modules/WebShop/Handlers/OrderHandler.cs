namespace SimpleShop.Handlers
{
    using Models;

    using Orchard.ContentManagement.Handlers;
    using Orchard.Data;

    public class OrderHandler : ContentHandler
    {
        public OrderHandler(IRepository<OrderPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}