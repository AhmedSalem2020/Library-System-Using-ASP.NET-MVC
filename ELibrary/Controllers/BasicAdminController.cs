using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELibrary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace ELibrary.Controllers
{
    //[Authorize(Roles = "BasicAdmin")]
    public class BasicAdminController : Controller
    {
        private ApplicationUserManager userManager;

        ApplicationDbContext lm = new ApplicationDbContext();


        public ApplicationUserManager UserManager
        {
            get
            {
                return userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                userManager = value;
            }
        }

        // GET: Profile
       // [Authorize(Roles = "BasicAdmin")]
        public ActionResult Index()
        {
            string UserId = User.Identity.GetUserId();

            var applicationUsers = lm.Users.FirstOrDefault(a => a.Id == UserId);

            return View(applicationUsers);
        }

        [HttpGet]
       // [Authorize(Roles="BasicAdmin")]
        public ActionResult UpdateBA()
        {
            string usId = User.Identity.GetUserId();
            ApplicationUser us = lm.Users.FirstOrDefault(a => a.Id == usId);
            if (us == null)
                return HttpNotFound("User Doesn't Exist");
            else
            return View(us);
        }

        [HttpPost]
        //[Authorize(Roles ="BasicAdmin")]
        public ActionResult UpdateBA(ApplicationUser newUser , HttpPostedFileBase img)
        {
            string usId = User.Identity.GetUserId();
            ApplicationUser OldUser = lm.Users.FirstOrDefault(x => x.Id == usId);

            OldUser.fName = newUser.fName;
            OldUser.lName = newUser.lName;
            OldUser.address = newUser.address;
            OldUser.Email = newUser.Email;
            OldUser.PhoneNumber = newUser.PhoneNumber;
            lm.SaveChanges();

            string ext = img.FileName.Substring(img.FileName.LastIndexOf(".")); // to get extension of the file
            string filename = newUser.Id.ToString() + ext; // to name the file (id + ext)
            img.SaveAs(Server.MapPath("~/images/") + filename); //save file on server

            ViewBag.img = filename;
            OldUser.image = filename;
            lm.SaveChanges();


           return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(string UserName, string Email, string fName, string lName, DateTime BirthDate, string Address, string PhoneNumber, string PasswordHash, DateTime JoinDate, decimal salary)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = UserName, Email = Email, fName = fName, lName = lName, birthDate = BirthDate, address = Address, PhoneNumber = PhoneNumber, JoinDate = JoinDate };
                var result = await UserManager.CreateAsync(user, PasswordHash);

                if (result.Succeeded)
                {
                    lm.employees.Add(new employee() { userId = user.Id, salary = salary });
                    lm.SaveChanges();
                    //Employee employee = new Employee() { Id = user.Id, Salary = salary };
                    //db.Employees.Add(employee);

                    await UserManager.AddToRoleAsync(user.Id, "Admin");
                }
            }
            return RedirectToAction("Add");
        }

        public ActionResult Admins()
        {
            return View();
        }

        public ActionResult GetAdminsData()
        {
            using (ApplicationDbContext AppDbContext = new ApplicationDbContext())
            {
                List<ApplicationUser> users = lm.Users.Where(a => a.Roles.FirstOrDefault().RoleId == "2" && a.isDeleted == false).Include("Employee").ToList<ApplicationUser>();
                var subCategoryToReturn = users.Select(us => new
                {
                    id = us.Id,
                    UserName = us.UserName,
                    Email = us.Email,
                    fName = us.fName,
                    lName = us.lName,
                    Address = us.address,
                    PhoneNumber = us.PhoneNumber,
                    BirthDate = us.birthDate.ToString(),
                    JoinDate = us.JoinDate.ToString(),
                    //Salary = us.Employee.salary
                });
                return this.Json(new { data = subCategoryToReturn }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult PopUpEdit(string id)
        {
            using (ApplicationDbContext AppDbContext = new ApplicationDbContext())
            {
                return View(AppDbContext.Users.Where(a => a.Id == id).FirstOrDefault<ApplicationUser>());
            }
        }

        [HttpPost]
        public ActionResult PopUpEdit(ApplicationUser user)
        {
            using (ApplicationDbContext AppDbContext = new ApplicationDbContext())
            {
                AppDbContext.Entry(user).State = EntityState.Modified;
                AppDbContext.SaveChanges();
                return Json(new { success = true, message = "Admin Updated Successfuly" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult DeleteAdmin(string id)
        {
            using (ApplicationDbContext AppDbContext = new ApplicationDbContext())
            {
                ApplicationUser AppUser = AppDbContext.Users.Where(a => a.Id == id).FirstOrDefault<ApplicationUser>();
                AppDbContext.Users.Remove(AppUser);
                AppDbContext.SaveChanges();
                return Json(new { success = true, message = "Admin Deleted Successfuly" }, JsonRequestBehavior.AllowGet);
            }
        }


        //[Authorize(Roles = "BasicAdmin")]
        public ActionResult logout()
        {
            Session["loginsuccess"] = null;
            return RedirectToAction("SmartLogin", "Account");
        }


        [HttpGet]
        public ActionResult Ask()
        {
            List<book> books = lm.books.ToList();
            return View(books);
        }
        [HttpPost]
        public ActionResult Ask(string name, string Dt)
        {
           
            string[] keyword_1 = { "how", "many", "books", "categories", "copies", "number" };
            string[] keyword_2 = { "available", "category", "name", "each", "borrowed","which" };
            string[] keyword_3 = { "joined", "total", "2018", "users","yet","return", "salaries", "avarage" };

            var sentence = name.ToLower();
            List<book> books = lm.books.ToList();

            foreach (var item in books)
            {
                // Question 1 : How many  books in the library?
                if (name.Contains(keyword_1[0]) && name.Contains(keyword_1[1]) && name.Contains(keyword_1[2]))
                {
                    ViewBag.Count = lm.books.Count();
                    return View(books);
                }
                ////Question 2 :how many number of the available copies for each book? 
                else if (name.Contains(keyword_1[4]) && name.Contains(keyword_1[5]) && name.Contains(keyword_2[0]))
                {
                    return View("AvialbleCopies", books);

                }
                // Question 3 : which books in a category of   (The name of category)?
                else if (name.Contains(keyword_2[1]) && name.Contains(keyword_2[5]))
                {
                    var book_name = lm.books.Where(c => c.categoryName == "science" || c.categoryName == "technology and applied science");
                    ViewBag.book_count = book_name.Count();
                    return View("Booksincategory", book_name);
                }
                //Question 4  : What is then number books in each category ?
                else if (name.Contains(keyword_2[1]) && name.Contains(keyword_1[2]) && name.Contains(keyword_1[5]))
                {
                    List<category> categories = lm.categories.ToList();
                    ViewBag.booksPerCategory = categories.Count();
                    return View("CategoryBooks", categories);
                }
                //Question5 : What is the Number of borrowed books?
                else if (name.Contains(keyword_1[5]) && name.Contains(keyword_2[4]) && name.Contains(keyword_1[2]))
                {
                    var qq = lm.userBook.Where(x => x.status == BookStatus.isborrowking).ToList();
                    ViewBag.borrowed = qq.Count();

                    return View("Borrowing", qq);
                }
                //Question6 : what are the total books number joined in 2018?
                else if (name.Contains(keyword_1[5]) && name.Contains(keyword_3[0]) && name.Contains(keyword_3[2]))
                {

                    // DateTime de = DateTime.Parse(keyword_3[2]);
                    string dt = DateTime.Now.Year.ToString();
                    var qq = lm.books.Where(x =>x.joinDate.Year.ToString()==dt).ToList();
                    return View("BooksPerJoinDate",qq);
                }
                //Question7 : what are the users who didn't return the books yet?
                else if (name.Contains(keyword_3[3]) && name.Contains(keyword_3[4]) && name.Contains(keyword_3[5]))
                {
                    var qq = lm.userBook.Where(x => x.status == BookStatus.isborrowking && x.isDelivered == false).ToList();
                    return View("BooksReturn", qq);
                }

            }

            return View("ErrorPage");

        }






    }


}