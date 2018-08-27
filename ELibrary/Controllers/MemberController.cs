using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ELibrary.Models;
using Microsoft.AspNet.Identity;

namespace ELibrary.Controllers
{
    public class MemberController : Controller
    {
        ApplicationDbContext lm = new ApplicationDbContext();

        // GET: Member
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Member")]
        public ActionResult NewArrivedBooks()
        {
            string usId = User.Identity.GetUserId();
            ApplicationUser us = lm.Users.FirstOrDefault(a => a.Id ==usId);
            if (us == null)
                return HttpNotFound("user Doesn't Exist");
            int Date = DateTime.Now.Year;
            List<book> b = lm.books.Where(x => x.publishDate.Year == Date).ToList<book>();
            return View(b);
        }

        [Authorize(Roles = "Member")]
        [HttpPost]
        public ActionResult BorrowingBooks(string ddl , string Dt)
        {
            string usId = User.Identity.GetUserId();
            ApplicationUser us = lm.Users.FirstOrDefault(a => a.Id == usId);
            if (us == null)
                return HttpNotFound("user Doesn't Exist");
            DateTime de = DateTime.Parse(Dt);
            if(ddl=="1")
            {
                var qq = lm.userBook.Where(x => x.status == BookStatus.isborrowking && x.startDate.Month == de.Month);

                return View(qq.ToList());
            }

            else if(ddl=="2")
            {
                var qq = lm.userBook.Where(x => x.status == BookStatus.isborrowking && x.startDate.Year == de.Year);
                return View(qq.ToList());

            }

            else if(ddl=="3")
            {
                var qq = lm.userBook.Where(x => x.status == BookStatus.isborrowking && x.startDate.Month == de.Month && x.startDate.Year == de.Year);

                return View(qq.ToList());

            }


            return View();
        }

        [Authorize(Roles = "Member")]
        [HttpGet]
        public ActionResult BorrowingBooks()
        {
            string usId = User.Identity.GetUserId();
            ApplicationUser us = lm.Users.FirstOrDefault(a => a.Id == usId);

            if (us == null)
                return HttpNotFound("user Doesn't Exist");
            DateTime de = DateTime.Now;
            var qq = lm.userBook.Where(x => x.status == BookStatus.isborrowking && x.startDate.Month == de.Month);

            return View(qq.ToList());
        }

        [Authorize(Roles = "Member")]
        [HttpPost]
        public ActionResult ReadingBooks(string Dt, string ddl)
        {
            string usId = User.Identity.GetUserId();
            ApplicationUser us = lm.Users.FirstOrDefault(a => a.Id == usId);
            if (us == null)
                return HttpNotFound("user Doesn't Exist");
            DateTime de = DateTime.Parse(Dt);
            if (ddl == "1")
            {
                var qq = lm.userBook.Where(x => x.status == BookStatus.isReading && x.startDate.Month == de.Month);

                return View(qq.ToList());
            }

            else if (ddl == "2")
            {
                var qq = lm.userBook.Where(x => x.status == BookStatus.isReading && x.startDate.Year == de.Year);

                return View(qq.ToList());

            }

            else if (ddl == "3")
            {
                var qq = lm.userBook.Where(x => x.status == BookStatus.isReading && x.startDate.Month == de.Month && x.startDate.Year == de.Year);

                return View(qq.ToList());

            }
            return View();
        }

        [Authorize(Roles = "Member")]
        [HttpGet]
        public ActionResult ReadingBooks()
        {
            string usId = User.Identity.GetUserId();

            ApplicationUser us = lm.Users.FirstOrDefault(a => a.Id == usId);

            if (us == null)
                return HttpNotFound("user Doesn't Exist");
            DateTime de = DateTime.Now;

            var qq = lm.userBook.Where(x => x.status == BookStatus.isReading && x.startDate.Month == de.Month);

            return View(qq.ToList());
        }

        [Authorize(Roles = "Member")]
        [HttpGet]
        public ActionResult CurrentBorrwedBooks()
        {
            string usId = User.Identity.GetUserId();
            ApplicationUser us = lm.Users.FirstOrDefault(a => a.Id == usId);
            if (us == null)
                return HttpNotFound("user Doesn't Exist");

            var currentBooks = lm.userBook.Where(x => x.status == BookStatus.isborrowking && x.isDelivered == false).ToList();

            //var num= lm.userBook.Where(w => w.status == BookStatus.isborrowking && w.isDelivered == true && w.bookId==currentBooks.Select(a=>a.bookId)).ToList();
            //ViewBag.num = num;

            //string[] separators = { "/", " " };
            //string value = GridView1.Rows[i].Cells[4].Text;
            //string[] date = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            //int a = int.Parse(date[0]);
            //int b = int.Parse(date[1]);
            //int c = int.Parse(date[2]);

            //if (a < DateTime.Now.Month || a == DateTime.Now.Month && b < DateTime.Now.Day)

            //{
            //    GridView1.Rows[i].BackColor = Color.Red;

            //}

            //else
            //{
            //    Response.Redirect("~/User/Login.aspx");

            //}
            return View(currentBooks);
        }

        [Authorize(Roles = "Member")]
        [HttpPost]
        public ActionResult SearchingForBooks(string ddl, string Dt)
        {
            string usId = User.Identity.GetUserId();
            ApplicationUser us = lm.Users.FirstOrDefault(a => a.Id == usId);
            if (us == null)
                return HttpNotFound("user Doesn't Exist");
           
            if (ddl == "1")
            {
                var qq = lm.userBook.Where(au=>au.book.author.fName.Contains(Dt)).ToList();
                return View(qq);
            }

            else if (ddl == "2")
            {
                int de = int.Parse(Dt);
                var qq = lm.userBook.Where(x => x.status == BookStatus.isborrowking && x.startDate.Year == de).ToList();
                return View(qq);

            }

            else if (ddl == "3")
            {
                var qq = lm.userBook.Where(x => x.book.category.name == Dt).ToList();
                //var qq = lm.categories.Select(cat => cat.name).ToList();

                return View(qq);

            }

            else if (ddl == "4")
            {
                var qq = lm.userBook.Where(x => x.book.publisher.name == Dt).ToList();
                //var qq = lm.publishers.Select(pub => pub.name).ToList();

                return View(qq);

            }

            else if (ddl == "5")
            {
                var qq = lm.userBook.Where(x => x.book.title == Dt);
                //var qq = lm.books.Select(tit => tit.title).ToList();

                return View(qq);

            }

            else if (ddl == "6")
            {
                var qq = lm.userBook.Where(x => x.book.name == Dt).ToList();
                //var qq = lm.books.Select(nam =>nam.name).ToList();

                return View(qq);

            }

            return View();
        }

        [Authorize(Roles = "Member")]
        [HttpGet]
        public ActionResult SearchingForBooks(string Dt)
        {
            string usId = User.Identity.GetUserId();
            ApplicationUser us = lm.Users.FirstOrDefault(a => a.Id == usId);
            DateTime de = DateTime.Now;
            var qq = lm.userBook.Where(x => x.status == BookStatus.isReading && x.startDate.Month == de.Month);
            return View(qq.ToList());
        }

        [Authorize(Roles = "Member")]
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

        [Authorize(Roles = "Member")]
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

        [Authorize(Roles = "Member")]
        [HttpGet]
        public ActionResult ViewProfile(ApplicationUser newUser, HttpPostedFileBase img)
        {
            string usId = User.Identity.GetUserId();

            ApplicationUser OUser = lm.Users.FirstOrDefault(a => a.Id == usId);

            return View(OUser);
        }

        [Authorize(Roles = "Member")]
        public ActionResult logout()
        {
            Session["loginsuccess"] = null;
            return RedirectToAction("SmartLogin", "Account");
        }


    }

}