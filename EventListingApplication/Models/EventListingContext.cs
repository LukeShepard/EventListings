using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EventListingApplication.Models
{
    public class EventListingContext :DbContext 
    {
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}