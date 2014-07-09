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

        public ActionResult Create()
        {
            var uComments = new Comment()
            {
                CreatedDate = DateTime.Now
            };
            return View("AddComments", uComments);
        }

        // This is a comment
        [HttpPost]
        public ActionResult Create(Comment uComments, HttpPostedFileBase image)
        {
            uComments.CreatedDate = DateTime.Now;

            if (!ModelState.IsValid) return View("Create", uComments);

            if (image != null)
            {
                uComments.ImageMimeType = image.ContentType;
                uComments.PhotoFile = new byte[image.ContentLength];
                image.InputStream.Read(uComments.PhotoFile, 0, image.ContentLength);
            }
            context.Comments.Add(uComments);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var uComments= context.Comments.Find(id);
            if (uComments == null) return HttpNotFound();
            return View("Delete", uComments);
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
                            orderby p.CreatedDate descending
                            select p).Take(number).ToList();
            }
            return PartialView("_CommentGallery", uComments);
        }
    }
}
