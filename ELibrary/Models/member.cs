using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ELibrary.Models
{
   [Serializable]
    public class member
    {
        [Key]
        [ForeignKey("user")]
        public string id { get; set; }


        //extra information

        public bool isBlock { get; set; }

        public DateTime? endDate { get; set; }

        public virtual ApplicationUser user { get; set; }

    }

}