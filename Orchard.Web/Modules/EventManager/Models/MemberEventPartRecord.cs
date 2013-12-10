using Orchard.ContentManagement.Records;

namespace EventManager.Models
{
    public class MemberEventPartRecord : ContentPartRecord
    {
        public virtual string Date { get; set; }
        public virtual string Location { get; set; }
        public virtual string Attendees { get; set; }
        public virtual string Description { get; set; }
    }
}