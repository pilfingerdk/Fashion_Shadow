using System.ComponentModel.DataAnnotations;
using EventManager.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace EventManager.Models
{
    public class MemberEventPart : ContentPart<MemberEventPartRecord>
    {
        [Required]
        public string Date
        {
            get { return Record.Date; }
            set { Record.Date = value; }
        }

        [Required]
        public string Location
        {
            get { return Record.Location; }
            set { Record.Location = value; }
        }

        [Required]
        public string Attendees
        {
            get { return Record.Attendees; }
            set { Record.Attendees = value; }
        }

        [Required]
        public string Description
        {
            get { return Record.Description; }
            set { Record.Description = value; }
        }
    }
}