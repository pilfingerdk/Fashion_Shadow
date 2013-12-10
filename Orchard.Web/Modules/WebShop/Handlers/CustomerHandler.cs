namespace SimpleShop.Handlers
{
    using Models;

    using Orchard.ContentManagement.Handlers;
    using Orchard.Data;

    public class CustomerHandler : ContentHandler
    {
        public CustomerHandler(IRepository<CustomerPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}
