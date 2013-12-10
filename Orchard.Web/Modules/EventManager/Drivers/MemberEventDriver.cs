
namespace EventManager.Drivers
{
    using System;
    using EventManager.Models;
    using Orchard.ContentManagement;
    using Orchard.ContentManagement.Drivers;
    using Orchard.ContentManagement.Handlers;

    public class MemberEventDriver : ContentPartDriver<MemberEventPart>
    {
        protected override DriverResult Display(MemberEventPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape(
                "Parts_MemberEvent",
                    () => shapeHelper.Parts_MemberEvent(
                        Date: part.Date,
                        Location: part.Location,
                        Attendees: part.Attendees,
                        Description: part.Description,
                        ItemId: part.ContentItem.Id));
        }

        //GET
        protected override DriverResult Editor(MemberEventPart part, dynamic shapeHelper)
        {
            return ContentShape(
                    "Parts_MemberEvent_Edit",
                    () => shapeHelper.EditorTemplate(TemplateName: "Parts/MemberEvent", Model: part, Prefix: Prefix));
        }

        //POST
        protected override DriverResult Editor(MemberEventPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return this.Editor(part, shapeHelper);
        }
    }
}