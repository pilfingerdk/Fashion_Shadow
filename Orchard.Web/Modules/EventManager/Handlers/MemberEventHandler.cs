using EventManager.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace EventManager.Handlers
{
    public class MemberEventHandler : ContentHandler
    {
        public MemberEventHandler(IRepository<MemberEventPartRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}
