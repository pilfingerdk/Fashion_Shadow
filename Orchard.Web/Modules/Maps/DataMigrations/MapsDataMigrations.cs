using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.MetaData;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using System.Data;
using Maps.Models;

namespace Maps.DataMigrations
{
    public class MapsDataMigrations : DataMigrationImpl
    {
        public int Create()
        {
            //Creating table MapRecord
            SchemaBuilder.CreateTable("MapRecord", table => table
                .ContentPartRecord()
                .Column("Latitude", DbType.Single)
                .Column("Longitude", DbType.Single)
            );

            ContentDefinitionManager.AlterPartDefinition(typeof(MapParts).Name, cfg => cfg.Attachable());

            return 1;
        }

        public int UpdateFrom1()
        {
            //Create a new widget content type with our map
            ContentDefinitionManager.AlterTypeDefinition("MapWidget", cfg => cfg
               .WithPart("MapPart")
               .WithPart("WidgetPart")
               .WithPart("CommonPart")
               .WithSetting("Stereotype", "widget"));

            return 2;
        }
    }
}