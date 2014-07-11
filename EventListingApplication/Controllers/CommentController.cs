using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using EventListingApplication.Models;

namespace EventListingApplication.Controllers
{
    public class CommentController : Controller
    {

        private EventListingContext context = new EventListingContext();

        //
        // GET: /Comment/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Display(int id = 0)
        {
            var uComments = context.Comments.Find(id);
            if (uComments == null)
                return HttpNotFound();
            return View("Display", uComments);
        }

        public ActionResult Create(int parent)
        {
            var uComments = new Comment()
            {
                ListingID = parent//,
                //CreatedDate = DateTime.Now
            };
            return View("AddComments", uComments);
        }

        // This is a comment
        [HttpPost]
        public ActionResult Create(Comment uComments)
        {
            //uComments.CreatedDate = DateTime.Now;

            if (!ModelState.IsValid)
                return View("AddComments", uComments);
            
            context.Comments.Add(uComments);
            context.SaveChanges();

            return RedirectToAction("Display", "Listing", new { id = uComments.ListingID });
        }

        public ActionResult Delete(int id)
        {
            var Comments= context.Comments.Find(id);
            if (Comments == null) return HttpNotFound();
            return View("Delete", Comments);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var uComments = context.Comments.Find(id);
            context.Comments.Remove(uComments);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        [ChildActionOnly]
        public ActionResult _CommentGallery(int number = 0)
        {
            List<Comment> uComments;
            if (number == 0)
            {
                uComments = context.Comments.ToList();
            }
            else
            {
                uComments = (from p in context.Comments
                            //orderby p.CreatedDate descending
                            select p).Take(number).ToList();
            }
            return PartialView("_CommentGallery", uComments);
        }
    }
}
