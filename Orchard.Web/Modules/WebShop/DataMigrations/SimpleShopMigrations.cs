using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;
using Orchard.Indexing;
using SimpleShop.Models;

namespace SimpleShop
{
    public class SimpleShopMigrations : DataMigrationImpl
    {

        public int Create()
        {
            // Creating table CartPartRecord
            SchemaBuilder.CreateTable("CartPartRecord", table => table
                .ContentPartRecord()
            );

            // Creating table OrderPartRecord
            SchemaBuilder.CreateTable("OrderPartRecord", table => table
                .ContentPartRecord()
                .Column("CustomerID", DbType.Int32)
                .Column("FirstName", DbType.String)
                .Column("LastName", DbType.String)
                .Column("Line1", DbType.String)
                .Column("Line2", DbType.String)                
                .Column("City", DbType.String)
                .Column("Country", DbType.String)
                .Column("PostCode", DbType.String)
                .Column("GiftWrap", DbType.Boolean, column => column.WithDefault(false))                
            );

            // Creating table CustomerPartRecord
            SchemaBuilder.CreateTable("CustomerPartRecord", table => table
                .ContentPartRecord()
                .Column("UserID", DbType.Int32)                
                .Column("Email", DbType.String)
                .Column("Phone", DbType.String)                
            );

            // Creating table ProductPartRecord
            SchemaBuilder.CreateTable("ProductPartRecord", table => table
                .ContentPartRecord()
                .Column("Sku", DbType.String)
                .Column("Price", DbType.Decimal)                
                .Column("Description", DbType.String)
            );

            // Creating table CartLineRecord
            SchemaBuilder.CreateTable("CartLineRecord", table => table
                .Column("Id", DbType.Int32, column => column.PrimaryKey().Identity())
                .Column("ProductId", DbType.Int32)
                .Column("OrderId", DbType.Int32)
                .Column("ProductTitle", DbType.String)
                .Column("Quantity", DbType.Int32)
                .Column("Price", DbType.Decimal)
            );            

            return 1;
        }

        public int UpdateFrom1()
        {
            ContentDefinitionManager.AlterPartDefinition(
                typeof(ProductPart).Name,
                builder => builder.Attachable()
                                  .WithLocation(
                                                new Dictionary<string, ContentLocation> 
                                                {
                                                    { "Default", new ContentLocation { Zone = "primary", Position = "3" } },
                                                    { "Editor", new ContentLocation { Zone = "primary", Position = "3" } } 
                                                }));

            ContentDefinitionManager.AlterTypeDefinition(
                "Product",
                cfg => cfg.WithPart("CommonPart")
                          .WithPart("TitlePart")
                          .WithPart("ProductPart")                          
                          .WithPart("ContainablePart")
                          .Creatable()
                          .Indexed());

            ContentDefinitionManager.AlterPartDefinition(
                typeof(OrderPart).Name,
                builder => builder.Attachable()
                                  .WithLocation(
                                                new Dictionary<string, ContentLocation> 
                                                {
                                                    { "Default", new ContentLocation { Zone = "primary", Position = "4" } },
                                                    { "Editor", new ContentLocation { Zone = "primary", Position = "4" } } 
                                                }));

            ContentDefinitionManager.AlterTypeDefinition(
                "Order",
                cfg => cfg.WithPart("CommonPart")
                          .WithPart("TitlePart")
                          .WithPart("OrderPart")                          
                          .WithPart("ContainablePart")                          
                          .Indexed());

            return 2;
        }

        public int UpdateFrom2()
        {
            // Create a new widget content type for displaying cart summary
            ContentDefinitionManager.AlterTypeDefinition("SummaryWidget", cfg => cfg
            .WithPart("WidgetPart")
            .WithPart("CommonPart")
            .WithSetting("Stereotype", "Widget"));    

            return 3;
        }

        public int UpdateFrom3()
        {
            // Set up Customer and later, the Order 1-N relation
            ContentDefinitionManager.AlterPartDefinition(
                typeof(CustomerPart).Name,
                builder => builder.Attachable()
                                  .WithLocation(
                                                new Dictionary<string, ContentLocation> 
                                                {
                                                    { "Default", new ContentLocation { Zone = "primary", Position = "5" } },
                                                    { "Editor", new ContentLocation { Zone = "primary", Position = "5" } } 
                                                }));

            ContentDefinitionManager.AlterTypeDefinition(
                "Customer",
                cfg => cfg.WithPart("CommonPart")
                          .WithPart("TitlePart")
                          .WithPart("CustomerPart")                          
                          .WithPart("ContainablePart")                          
                          .Indexed());

            return 4;
        }

        // Not currently used, but needs to be here if we declare the type under models
        public int UpdateFrom4()
        {
            // Creating bridge table - Order ids referring to Customer ids
            SchemaBuilder.CreateTable("OC_AssociationRecord", table => table
                .Column<int>("Id", column => column.PrimaryKey().Identity())
                .Column<int>("OrderPartRecord_id")
                .Column<int>("CustomerPartRecord_id")
            );
            return 5;
        }

        public int UpdateFrom5()
        {
            // Use MediaLibrary for product image, updated for v 1.7.2
            ContentDefinitionManager.AlterPartDefinition(typeof(ProductPart).Name,
                part => part
                    .WithField("Image", f => f.OfType("MediaLibraryPickerField"))                    
                    .Attachable()
            );            
            return 6;
        } 
    }
}