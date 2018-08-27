using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ELibrary.Models
{
    [Serializable]

    public class book 
    {
        public int id { get; set; }

        public int copiesCount { get; set; }
        public int availableCopies { get; set; }

        [Required]
        //[Required(ErrorMessage = "Not Valid")]
        ////[StringLength(10, MinimumLength = 2, ErrorMessage = "length must between 2 to 10 ")]
        //[Display(Name = "Book Title")]
        public string title { get; set; }

        [ForeignKey("author")]
        public int autherId { get; set; }
        public virtual author author { get; set; }


        [ForeignKey("publisher")]
        public int publisherId { get; set; }
        public virtual publisher publisher { get; set; }

        [ForeignKey("category")]
        public string categoryName { get; set; }
        public virtual category category { get; set; }

        //[Display(Name = "Book Image")]
        public string cover { get; set; }

        [Required]
        public string name { get; set; }

        public string source { get; set; }
        public bool isDeleted { get; set; }

        public DateTime joinDate { get; set; }
        public DateTime publishDate { get; set; }



        //public virtual List<userBook> userBooks { get; set; }

        public virtual List<userBook> userBooks { get; set; }

    }

}