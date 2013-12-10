using System.Web.Mvc;
using EventManager.Models;
using Orchard.ContentManagement;
using Orchard.Localization;
using Orchard;
using Orchard.Mvc;
using Orchard.DisplayManagement;
using Orchard.Themes;

namespace EventManager.Controllers
{
    [Themed]
    public class EventController : Controller, IUpdateModel
    {
        private readonly IContentManager _contentManager;
        dynamic Shape { get; set; }

        public EventController(IContentManager contentManager, IShapeFactory shapeFactory)
        {
            _contentManager = contentManager;
            Shape = shapeFactory;
        }

        //Register current user for participation in event
        public ActionResult RegisterForEvent(string userName, int contentItemId)
        {
            //get part record from id passed with actionlink
            var part = _contentManager.Get<MemberEventPart>(contentItemId);

            //Challenge: if two users with the same name exist as site members,
            //we get a problem here. Can you solve this?
            if (part.Attendees != null && part.Attendees.Contains(userName))
            {
                TempData["message"] = "Sorry, a user with that name is already registered for this event.";
            }
            else
            {
                //Add logged in username to part.Attendees
                part.Attendees = part.Attendees + userName + ",";

                if (ModelState.IsValid)
                {
                    this.TryUpdateModel(part);
                    TempData["message"] = "Thank you for registering " + userName + "!";
                }
            }

            //Return a shape with part
            dynamic model = _contentManager.BuildDisplay(part);
            return new ShapeResult(this, model);
        }

        //Required by IUpdateModel
        bool IUpdateModel.TryUpdateModel<TModel>(TModel model,
                                                 string prefix,
                                                 string[] includeProperties,
                                                 string[] excludeProperties)
        {
            return TryUpdateModel(model, prefix, includeProperties, excludeProperties);
        }

        void IUpdateModel.AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}