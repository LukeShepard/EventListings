using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace EventListingApplication.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public int ListingID { get; set; }
        public string Username { get; set; }
        
        [Required(ErrorMessage="Subject is mandatory.")]
        [MaxLength(250)]
        public string Subject { get; set; }

        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public virtual Listing Listing { get; set; }
    }
}