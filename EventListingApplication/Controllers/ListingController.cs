using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using EventListingApplication.Models;


namespace EventListingApplication.Controllers
{
  

    public class ListingController : Controller
    {

        private EventListingContext context = new EventListingContext();


        //
        // GET: /Listing/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Display(int id = 0)
        {
            var listing = context.Listings.Find(id);
            if (listing == null)
                return HttpNotFound();
            //listing.Comments = context.Comments.Find(1).ToList();
            return View("Display", listing);
        }

        public ActionResult Create()
        {
            var listings = new Listing()
            {
                CreatedDate = DateTime.Now
            };
            return View("Create", listings);
        }

        // This is a comment
        [HttpPost]
        public ActionResult Create(Listing listing, HttpPostedFileBase image)
        {
            listing.CreatedDate = DateTime.Now;

            if (!ModelState.IsValid) return View("Create", listing);

            if (image != null)
            {
                listing.ImageMimeType = image.ContentType;
                listing.PhotoFile = new byte[image.ContentLength];
                image.InputStream.Read(listing.PhotoFile, 0, image.ContentLength);
            }
            context.Listings.Add(listing);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var listing = context.Listings.Find(id);
            if (listing == null) return HttpNotFound();
            return View("Delete", listing);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var listing = context.Listings.Find(id);
            context.Listings.Remove(listing);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public FileContentResult GetImage(int id)
        {
            var listing = context.Listings.Find(id);
            if (listing != null)
                return File(listing.PhotoFile, listing.ImageMimeType);
            return null;
        }
        [ChildActionOnly]
        public ActionResult _ListingGallery(int number = 0)
        {
            List<Listing> listings;
            if (number == 0)
            {
                listings = context.Listings.ToList();
            }
            else
            {
                listings = (from p in context.Listings
                            orderby p.CreatedDate descending
                            select p).Take(number).ToList();
            }
            return PartialView("_ListingGallery", listings);
        }

     
    }
}
