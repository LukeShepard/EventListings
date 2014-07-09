using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.IO;



namespace EventListingApplication.Models
{
    public class EventListingInitializer : DropCreateDatabaseAlways<EventListingContext>
    {
        private byte[] getFileBytes(string path)
        {
            FileStream fileonDisk = new FileStream(HttpRuntime.AppDomainAppPath + path, FileMode.Open);
            byte[] filebytes ; 
            using (BinaryReader br = new BinaryReader(fileonDisk))
            {
                filebytes = br.ReadBytes((int)fileonDisk.Length);
            }
            return filebytes;
        }
        protected override void Seed(EventListingContext context)
        {
            base.Seed(context);
            var listings = new List<Listing>
            {
                new Listing {
                    Title = "Test Event",
                    Description = "A Test event",
                    UserName = "NaokiSato",
                    PhotoFile = getFileBytes("\\Images\\flower.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Now 
                    
                }
            };



            listings.ForEach(s => context.Listings.Add(s));
            context.SaveChanges();
            var comments = new List<Comment>
            {
                new Comment {
                    ListingID = 1,
                    Username = "NaokiSato",
                    Subject = "Test Comment",
                    Body = "This comment should appear for listing 1"
                }
            };

            comments.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();

        }

        
    } 
}