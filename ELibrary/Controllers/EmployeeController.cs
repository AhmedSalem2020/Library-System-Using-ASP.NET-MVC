using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELibrary.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ELibrary.Controllers
{
    [Authorize(Roles ="Employee")]
    public class EmployeeController : Controller
    {

        ApplicationDbContext lm = new ApplicationDbContext();

        public ActionResult Index()
        {
            string UserId = User.Identity.GetUserId();

            var applicationUsers = lm.Users.FirstOrDefault(a => a.Id == UserId);

            return View(applicationUsers);
        }

        [HttpGet]
        public ActionResult UpdateEmp()
        {
            string usId = User.Identity.GetUserId();
            ApplicationUser us = lm.Users.FirstOrDefault(a => a.Id == usId);
            if (us == null)
                return HttpNotFound("User Doesn't Exist");
            else
                return View(us);
        }

        [HttpPost]
        
        public ActionResult UpdateEmp(ApplicationUser newUser, HttpPostedFileBase img)
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
        public ActionResult AddMember()
        {
            string usId = User.Identity.GetUserId();

            List<member> emp = lm.members.ToList<member>();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee(ApplicationUser newUser, HttpPostedFileBase img, DateTime Dob)
        {
            UserManager<ApplicationUser> us = new UserManager<ApplicationUser>((new UserStore<ApplicationUser>(new ApplicationDbContext())));
            ApplicationUser appus = new ApplicationUser()
            {
                UserName = newUser.UserName,
                fName = newUser.fName,
                lName = newUser.lName,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                address = newUser.address,
                birthDate = Dob,
                JoinDate = DateTime.Now,

                //newUser.birthDate = Dob,
                //newUser.JoinDate = DateTime.Now,
            };
            var result = await us.CreateAsync(appus, newUser.PasswordHash);
            if (result.Succeeded)
            {
                await us.AddToRoleAsync(appus.Id, "Member");
                member Mem = new member() { id = appus.Id };
                lm.members.Add(Mem);
                lm.SaveChanges();

            }

            if (img != null)
            {
                string ext = img.FileName.Substring(img.FileName.LastIndexOf(".")); // to get extension of the file

                string filename = appus.Id.ToString() + ext; // to name the file (id + ext)

                img.SaveAs(Server.MapPath("~/images/") + filename); //save file on server

                lm.Users.SingleOrDefault(x => x.Id == appus.Id).image = filename;
                lm.SaveChanges();
                //newUser.image = filename;

            }
            if (ModelState.IsValid)
            {
                // string usId = User.Identity.GetUserId();

                //lm.Users.Add(newUser);

                //lm.SaveChanges();


                return RedirectToAction("ViewMembers");
            }
            return View();
        }



        [HttpGet]
        public ActionResult UpdateMember(string id)
        {
            string usId = User.Identity.GetUserId();

            List<employee> emp = lm.employees.ToList<employee>();

            return View(lm.Users.Find(id));
        }



        [HttpPost]
        public ActionResult UpdateMember(ApplicationUser newUser, HttpPostedFileBase img, DateTime Dob)
        {
            string usId = User.Identity.GetUserId();
            if (img != null)
            {
                string ext = img.FileName.Substring(img.FileName.LastIndexOf(".")); // to get extension of the file

                string filename = newUser.Id.ToString() + ext; // to name the file (id + ext)

                img.SaveAs(Server.MapPath("~/images/") + filename); //save file on server

                newUser.image = filename;

                ViewBag.img = filename;
            }

            if (ModelState.IsValid)
            {
                ApplicationUser OldUser = lm.Users.FirstOrDefault(a => a.Id == newUser.Id);
                OldUser.UserName = newUser.UserName;
                OldUser.fName = newUser.fName;
                OldUser.lName = newUser.lName;
                OldUser.Email = newUser.Email;
                OldUser.birthDate = newUser.birthDate;
                OldUser.address = newUser.address;
                OldUser.PhoneNumber = newUser.PhoneNumber;
                OldUser.image = newUser.image;
                OldUser.birthDate = Dob;
                OldUser.JoinDate = DateTime.Now;
                lm.SaveChanges();
                return RedirectToAction("ViewMembers");

            }

            return View();
        }



        public ActionResult ViewMembers()
        {
            //string usId = User.Identity.GetUserId();

            var q = lm.members.Select(c => c.user).ToList();
            return View(lm.members.ToList<member>());
        }


        [HttpGet]
        public ActionResult DeleteMember(string id)
        {
            // book bk = lm.employees.FirstOrDefault(x => x.userId == id);
            ApplicationUser Emp = lm.Users.FirstOrDefault(a => a.Id == id);

            return View(Emp);
        }


        [HttpPost]
        public ActionResult DeleteMember([Bind(Include = "Id")] ApplicationUser em)
        {

            List<userBook> data = lm.userBook.Where(c => c.employeeId == em.Id).ToList();
            if (data != null)
                lm.userBook.RemoveRange(data);

            //if (data != null)
            //{
            //    foreach (var item in data)
            //    {
            //        lm.userBook.Remove(item);
            //    }
            //}


            employee emp = lm.employees.FirstOrDefault(a => a.userId == em.Id);
            lm.employees.Remove(emp);

            ApplicationUser DelEmp = lm.Users.FirstOrDefault(a => a.Id == em.Id);
            lm.Users.Remove(DelEmp);


            lm.SaveChanges();
            return RedirectToAction("ViewMembers");
        }

        public ActionResult _AjaxMemSearch(string txt)
        {
            return PartialView("_AjaxMemSearch", lm.employees.Where(c => c.user.fName.Contains(txt)).ToList());
        }

        public ActionResult MemberSearch()
        {
            return View();
        }

        public ActionResult BooksData()
        {
            var qq = lm.books.Select(x => x.id).ToList();
            
            return View(lm.books.ToList<book>());
        }

        //[HttpGet]
        //public ActionResult BorrowingBooks(int id)
        //{
            //var MemSl = lm.members.Where(x => x.isBlock == false).ToList();

            //List<member> members = lm.members.ToList<member>();

            //SelectList MemberList = new SelectList("members","UserId","CategoryName");
            //return View();
        //}




        public ActionResult logout()
        {
            Session["loginsuccess"] = null;
            return RedirectToAction("SmartLogin", "Account");
        }


    }
}