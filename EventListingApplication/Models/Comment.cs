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

        //[DataType(DataType.MultilineText)]
        //public string UserComments { get; set; }

        //[DataType(DataType.DateTime)]
        //[Display(Name = "Created Date")]
        //[DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        //public DateTime CreatedDate { get; set; }

        //[Display(Name = "Picture")]
        //public byte[] PhotoFile { get; set; }
        //public string ImageMimeType { get; set; }

        //public string Initial { get; set; }

        public virtual Listing Listing { get; set; }
    }
}