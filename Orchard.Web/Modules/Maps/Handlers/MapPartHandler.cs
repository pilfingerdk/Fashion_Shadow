using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Maps.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Maps.Handlers
{
    public class MapPartHandler : ContentHandler
    {
        public MapPartHandler(IRepository<MapRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}