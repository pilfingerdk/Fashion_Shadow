using System;
using System.Collections.Generic;
using System.Data;
using EventManager.Models;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Indexing;

namespace EventManager.DataMigrations
{
    public class EventManagerDataMigrations : DataMigrationImpl
    {
        public int Create()
        {
            //Create table MemberEventRecord
            SchemaBuilder.CreateTable("MemberEventPartRecord", table => table
                .ContentPartRecord()
                .Column("Date", DbType.String)
                .Column("Location", DbType.String)
                .Column("Attendees", DbType.String)
                .Column("Description", DbType.String)
            );

            ContentDefinitionManager.AlterPartDefinition(
                typeof(MemberEventPart).Name,
                builder => builder.Attachable()
                                  .WithLocation(new Dictionary<string, ContentLocation>
                                                {
                                                    { "Default", new ContentLocation { Zone = "primary", Position = "3" } },
                                                    { "Editor", new ContentLocation {Zone = "primary", Position = "3" } }
                                                }));

            ContentDefinitionManager.AlterTypeDefinition(
                "MemberEvent",
                cfg => cfg.WithPart("CommonPart")
                          .WithPart("TitlePart")
                          .WithPart("MemberEventPart")
                          .WithPart("MenuPart")
                          .WithPart("ContainablePart")
                          .Creatable()
                          .Indexed());
            return 1;
        }
    }
}