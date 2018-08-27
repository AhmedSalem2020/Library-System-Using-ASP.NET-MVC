using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ELibrary.Models
{
    [Serializable]
    public class employee
    {
        [Key]
        [ForeignKey("user")]
        public string userId { get; set; }


        //extra information
        //[Range(1000, 20000)]
        public decimal salary { get; set; }

        public virtual ApplicationUser user { get; set; }

    }

}