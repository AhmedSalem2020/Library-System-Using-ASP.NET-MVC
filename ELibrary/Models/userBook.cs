using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ELibrary.Models
{
    [Serializable]
    public class userBook
    {
        //[ForeignKey("ApplicationUser")]
        //public string Id { get; set; }
        //public virtual ApplicationUser ApplicationUser { get; set; }


        [Key, Column(Order = 0)]
       [ForeignKey("member")]
        public string userId { get; set; }
    
        [Key, Column(Order = 1)]
        [ForeignKey("book")]
        public int bookId { get; set; }
        public virtual book  book { get; set; }

        public BookStatus status { get; set; }

        public DateTime startDate { get; set; }
        
        public DateTime? returnDate { get; set; }

        public DateTime deliveredDate { get; set; }

        public bool isDelivered { get; set; }

        //public int Count { get; set; }

        [ForeignKey("employee")]
        public string employeeId { get; set; }
        public virtual employee employee { get; set; }
        public virtual member   member { get; set; }


    }

}