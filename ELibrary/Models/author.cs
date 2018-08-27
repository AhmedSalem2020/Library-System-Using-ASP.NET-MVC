using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ELibrary.Models
{
    [Serializable]
    public class author
    {
        public int id { get; set; }

        [EmailAddress]
        public string email { get; set; }

        [Required]
        //[Required(ErrorMessage = "*")]
        //[StringLength(25, MinimumLength = 3, ErrorMessage = "length must between 3 to 25 ")]
        //[Display(Name = "First Name")]
        public string fName { get; set; }

        [Required]
        public string lName { get; set; }

        public string image { get; set; }


        public DateTime birthDate { get; set; }
        public bool isDeleted { get; set; }



    }

}