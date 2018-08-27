using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELibrary.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace ELibrary.Controllers
{

    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {


        ApplicationDbContext lm = new ApplicationDbContext();



        //[Authorize(Roles ="Admin")]
        [HttpGet]
        public ActionResult Edit()
        {
            ViewBag.btnTitle = "Edit";
            string usId = User.Identity.GetUserId();
            ApplicationUser us = lm.Users.FirstOrDefault(a => a.Id == usId);
            if (us == null)
                return HttpNotFound("user Doesn't Exist");
            return View(us);
        }




        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(ApplicationUser newUser, HttpPostedFileBase img)
        {
            string usId = User.Identity.GetUserId();
            ApplicationUser OldUser = lm.Users.FirstOrDefault(a => a.Id == usId);

            OldUser.fName = newUser.fName;
            OldUser.lName = newUser.lName;
            OldUser.Email = newUser.Email;
            OldUser.address = newUser.address;
            OldUser.PhoneNumber = newUser.PhoneNumber;
            lm.SaveChanges();
            string ext = img.FileName.Substring(img.FileName.LastIndexOf(".")); // to get extension of the file
            string filename = newUser.Id.ToString() + ext; // to name the file (id + ext)
            img.SaveAs(Server.MapPath("~/images/") + filename); //save file on server

            ViewBag.img = filename;
            OldUser.image = filename;
            lm.SaveChanges();
            return RedirectToAction("ViewProfile");
        }




       // [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult ViewProfile(ApplicationUser newUser, HttpPostedFileBase img)
        {
            string usId = User.Identity.GetUserId();

            ApplicationUser OUser = lm.Users.FirstOrDefault(a => a.Id == usId);

            return View(OUser);
        }



        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddEmployee()
        {
            string usId = User.Identity.GetUserId();

            List<employee> emp = lm.employees.ToList<employee>();

            return View();
        }



        //[Authorize(Roles = "Admin")]
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
                JoinDate=DateTime.Now,

                //newUser.birthDate = Dob,
                //newUser.JoinDate = DateTime.Now,
            };
            var result = await us.CreateAsync(appus, newUser.PasswordHash);
            if(result.Succeeded)
            {
                await us.AddToRoleAsync(appus.Id, "Employee");
                employee emp = new employee() { userId = appus.Id, salary = 2000 };
                lm.employees.Add(emp);
                lm.SaveChanges();

            }

            if (img != null)
            {
                string ext = img.FileName.Substring(img.FileName.LastIndexOf(".")); // to get extension of the file

                string filename = appus.Id.ToString() + ext; // to name the file (id + ext)

                img.SaveAs(Server.MapPath("~/images/") + filename); //save file on server

                lm.Users.SingleOrDefault(x => x.Id == appus.Id).image = filename ;
                lm.SaveChanges();
                //newUser.image = filename;

            }
            if (ModelState.IsValid)
            {
                // string usId = User.Identity.GetUserId();
               
                //lm.Users.Add(newUser);

                //lm.SaveChanges();


                return RedirectToAction("ViewEmployess");
            }
            return View();
        }



       // [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult UpdateEmployee(string id)
        {
            string usId = User.Identity.GetUserId();

            List<employee> emp = lm.employees.ToList<employee>();

            return View(lm.Users.Find(id));
        }



        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UpdateEmployee( ApplicationUser newUser, HttpPostedFileBase img,DateTime Dob)
        {
            string usId = User.Identity.GetUserId();
            if(img !=null)
            {
                string ext = img.FileName.Substring(img.FileName.LastIndexOf(".")); // to get extension of the file

                string filename = newUser.Id.ToString() + ext; // to name the file (id + ext)

                img.SaveAs(Server.MapPath("~/images/") + filename); //save file on server

                newUser.image = filename;

                ViewBag.img = filename;
            }

            if(ModelState.IsValid)
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
             return RedirectToAction("ViewEmployess");

            }

            return View();
        }



        //[Authorize(Roles = "Admin")]
        public ActionResult ViewEmployess()
        {
            //string usId = User.Identity.GetUserId();

            var q = lm.employees.Select(c => c.user).ToList();
            return View(lm.employees.ToList<employee>());
        }




       // [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddBook()
        {
            string usId = User.Identity.GetUserId();

            List<author> auth = lm.authors.ToList<author>();
            SelectList authsl = new SelectList(auth, "Id ", "fName");
            ViewBag.aut = authsl;

            List<publisher> pub = lm.publishers.ToList<publisher>();
            SelectList pubsl = new SelectList(pub, "Id ", "Name");
            ViewBag.pub = pubsl;
            SelectList cat = new SelectList(lm.categories.ToList(), "name ", "name");
            ViewBag.cat = cat;

            ViewBag.btnTitle = "AddBook";
            return View();
        }




        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddBook(book bk, HttpPostedFileBase img)
        {
            string usId = User.Identity.GetUserId();
            bk.joinDate = DateTime.Now;
            lm.books.Add(bk);

            lm.SaveChanges();

            string ext = img.FileName.Substring(img.FileName.LastIndexOf(".")); // to get extension of the file

            string filename = bk.id.ToString() + ext; // to name the file (id + ext)

            img.SaveAs(Server.MapPath("~/images/") + filename); //save file on server

            ViewBag.img = filename;
            bk.cover = filename;
            lm.SaveChanges();
            return RedirectToAction("ViewBooks");
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult UpdateBook(int id)
        {
            string usId = User.Identity.GetUserId();

            List<author> auth = lm.authors.ToList<author>();
            SelectList authsl = new SelectList(auth, "Id ", "fName");
            ViewBag.aut = authsl;

            List<publisher> pub = lm.publishers.ToList<publisher>();
            SelectList pubsl = new SelectList(pub, "Id ", "Name");
            ViewBag.pub = pubsl;

            SelectList cat = new SelectList(lm.categories.ToList(), "name ", "name");
            ViewBag.cat = cat;

            ViewBag.btnTitle = "AddBook";

            return View(lm.books.Find(id));
        }



        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult UpdateBook(book bk, HttpPostedFileBase img)
        {
            string usId = User.Identity.GetUserId();
            bk.joinDate = DateTime.Now;

            book OldBk = lm.books.FirstOrDefault(a => a.id == bk.id);
            if (img != null)
            {

                string ext = img.FileName.Substring(img.FileName.LastIndexOf(".")); // to get extension of the file

                string filename = bk.id.ToString() + ext; // to name the file (id + ext)

                img.SaveAs(Server.MapPath("~/images/") + filename); //save file on server


                OldBk.cover = filename;
            }
            OldBk.title= bk.title;
            OldBk.name= bk.name;
            OldBk.autherId= bk.autherId;
            OldBk.publisherId= bk.publisherId;
            OldBk.publishDate= bk.publishDate;
            OldBk.categoryName = bk.categoryName;
            OldBk.copiesCount = bk.copiesCount;
            OldBk.availableCopies = bk.availableCopies;

            lm.SaveChanges();

            return RedirectToAction("ViewBooks");
        }




        //[Authorize(Roles = "Admin")]
        public ActionResult ViewBooks()
        {
            string usId = User.Identity.GetUserId();

            List<book> books = lm.books.ToList<book>();

            return View(books);
        }

        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteBook(int id)
        {
            book bk = lm.books.FirstOrDefault(x => x.id == id);

            return View(bk);
        }


       // [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteBook([Bind(Include = "Id")] book bk)
        {
            book Delbk = lm.books.FirstOrDefault(x => x.id == bk.id);

            lm.books.Remove(Delbk);
            lm.SaveChanges();
            return RedirectToAction("ViewBooks");
        }


        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult DeleteEmployee(string id)
        {
           // book bk = lm.employees.FirstOrDefault(x => x.userId == id);
            ApplicationUser Emp = lm.Users.FirstOrDefault(a => a.Id == id);

            return View(Emp);
        }


       // [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteEmployee([Bind(Include = "Id")] ApplicationUser em)
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

            ApplicationUser DelEmp = lm.Users.FirstOrDefault(a => a.Id ==em.Id );
            lm.Users.Remove(DelEmp);

           
            lm.SaveChanges();
            return RedirectToAction("ViewEmployess");
        }


        //[Authorize(Roles = "Admin")]
        public ActionResult logout()
        {
            Session["loginsuccess"] = null;
            return RedirectToAction("SmartLogin", "Account");
        }



        //[Authorize(Roles = "Admin")]
        public ActionResult _AjaxEmpSearch(string txt)
        {
            return PartialView("_AjaxEmpSearch",lm.employees.Where(c => c.user.fName.Contains(txt)).ToList());
        }

        public ActionResult EmpSearch()
        {
            return View();
        }

    }
}