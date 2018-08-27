using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Linq;
using System.Web;
using System.Collections.Generic;


namespace ELibrary.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Required]
        public string fName { get; set; }

        [Required]
        public string lName { get; set; }

        public string image { get; set; }

        public DateTime birthDate { get; set; }

        public string address { get; set; }

        public DateTime JoinDate { get; set; }

        public bool isDeleted { get; set; }

        public bool firstLogin { get; set; }

        public virtual employee Employee { get; set; }

        //public virtual member Member { get; set; }

        //public virtual List<userBook> UserBooks { get; set; }

    }

    //add table 

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public virtual DbSet<employee> employees { get; set; }
        public virtual DbSet<userBook> userBook { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<author> authors { get; set; }
        public virtual DbSet<publisher> publishers { get; set; }
        public virtual DbSet<book> books { get; set; }
        public virtual DbSet<member> members { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }


        //public System.Data.Entity.DbSet<ELibrary.Models.ApplicationUser> ApplicationUsers { get; set; }



        // public System.Data.Entity.DbSet<ELibrary.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}