using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace EventListingApplication.Models
{


    public class Listing
    {

        public int ListingID { get; set; }

        [Required(ErrorMessage = "Title is mandatory.")]
        public string Title { get; set; }
 
        [Display(Name="Picture")]
        public byte[] PhotoFile { get; set; }
        public string ImageMimeType { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Must Have A discription.")]
        public string Description { get; set; }

       


        [DataType(DataType.DateTime)]
        [Display(Name = "Created Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yy}")]
        public DateTime CreatedDate { get; set; }

        [DataType(DataType.Text)]
        public string UserName { get; set; }



        public virtual List<Comment> Comments { get; set; }

        
    


    }
}