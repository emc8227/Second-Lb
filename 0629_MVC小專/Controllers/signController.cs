using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using _0629_MVC小專.Models;
using System.Data.SqlClient;

namespace _0629_MVC小專.Controllers
{
    public class signController : Controller
    {
        public ActionResult Index2() {
            return View();
        }
        
       
        // GET: sign
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(Member  newData) {

            ViewBag.useremail = newData.useremail;
            ViewBag.userpassword = newData.userpassword;
            SqlConnection cn = new SqlConnection(@"server=.\SQLEXPRESS;database=Tim001;Integrated Security=true");
            SqlCommand cmd = new SqlCommand("select * from Member where useremail=@email AND userpassword=@password", cn);
            cn.Open();
            cmd.Parameters.AddWithValue("@email", newData.useremail);
            cmd.Parameters.AddWithValue("@password", newData.userpassword);
            int num = Convert.ToInt32(cmd.ExecuteScalar());
            if (num == 0) {
                return View();
            }

            Session["userId"] = newData.useremail;
            return RedirectToAction("billList","sign");

        }

        public ActionResult singOut() {
            Session["userId"] = "guest";
            return RedirectToAction("login", "sign");
        }


        public ActionResult billList(string s) {
            Tim001Entities db = new Tim001Entities();
           
            s = Session["userId"].ToString();
            if (s == "guest") {
                return RedirectToAction("login", "sign");
            }
            var query = from o in db.SignBills
                        where o.bill_userID == s
                        select o;
            List<SignBill> bList = query.ToList<SignBill>();

            return View("billList", bList);
            //return View(db.SignBills.ToList());

        }
        
        public ActionResult create() {

            if (Session["userId"].ToString() == "guest") {

                return RedirectToAction("login", "sign");
            }
            return View();
        }

       
        [HttpPost]
        public ActionResult create(SignBill newBill) {
            Tim001Entities db = new Tim001Entities();
            //ViewBag.billID = newBill.billID;
            ViewBag.adate = newBill.adate;
            ViewBag.company = newBill.company;
            ViewBag.customer = newBill.customer;
            ViewBag.driver = newBill.driver;
            ViewBag.expenses = newBill.expenses;
            ViewBag.freight = newBill.freight;
            ViewBag.number = newBill.number;
            ViewBag.overtime = newBill.overtime;
            ViewBag.product = newBill.product;
            ViewBag.standards = newBill.standards;
            ViewBag.timehour = newBill.timehour;
            Session["userId"] = newBill.bill_userID;

            if (
                //newBill.billID==0 ||
                //string.IsNullOrEmpty(newBill.billID.ToString()) ||
                string.IsNullOrEmpty(newBill.adate) ||
                string.IsNullOrEmpty(newBill.company) ||
                string.IsNullOrEmpty(newBill.customer) ||
                string.IsNullOrEmpty(newBill.driver) ||
                string.IsNullOrEmpty(newBill.expenses.ToString()) ||
                string.IsNullOrEmpty(newBill.freight.ToString()) ||
                string.IsNullOrEmpty(newBill.number.ToString()) ||
                string.IsNullOrEmpty(newBill.overtime.ToString()) ||
                string.IsNullOrEmpty(newBill.product) ||
                string.IsNullOrEmpty(newBill.standards) ||
                string.IsNullOrEmpty(newBill.timehour.ToString())
                ) {
                return View();
            }
            db.SignBills.Add(newBill);
            db.SaveChanges();
            return RedirectToAction("billList", "sign");

        }

        public ActionResult update(int id) {
            Tim001Entities db = new Tim001Entities();
            SignBill sb = db.SignBills.Find(id);

            return View(sb);
        }
        [HttpPost]
        public ActionResult update(SignBill sbForm) {
            Tim001Entities db = new Tim001Entities();
            SignBill sb = db.SignBills.Find(sbForm.billID);
            sb.billID = sbForm.billID;
            sb.company = sbForm.company;
            sb.adate = sbForm.adate;
            sb.customer = sbForm.customer;
            sb.driver = sbForm.driver;
            sb.expenses = sbForm.expenses;
            sb.freight = sbForm.freight;
            sb.number = sbForm.number;
            sb.overtime = sbForm.overtime;
            sb.product = sbForm.product;
            sb.standards = sbForm.standards;
            sb.timehour = sbForm.timehour;
            

            db.SaveChanges();
            return RedirectToAction("billList", "sign");
        }

        public ActionResult delete(int id) {
            Tim001Entities db = new Tim001Entities();
            SignBill sb = db.SignBills.Find(id);
            db.SignBills.Remove(sb);
            db.SaveChanges();
            return RedirectToAction("billList","sign");
        }


        public ActionResult mbcreate() {
           
            return View();
        }


        [HttpPost]
        public ActionResult mbcreate(Member newMb) {

            SqlConnection cn = new SqlConnection(@"server=.\SQLEXPRESS;database=Tim001;Integrated Security=true");
            SqlCommand cmd = new SqlCommand("select * from Member where useremail=@email ", cn);
            cn.Open();
            cmd.Parameters.AddWithValue("@email", newMb.useremail);
            
            int num = Convert.ToInt32(cmd.ExecuteScalar());
            if (num == 0) {
                Tim001Entities db = new Tim001Entities();
                ViewBag.newuseremail = newMb.useremail;
                ViewBag.newuserpassword = newMb.userpassword;
                db.Members.Add(newMb);
                db.SaveChanges();
                return RedirectToAction("login", "sign");
            } else {
                ViewBag.erro = "信箱已經註冊過了";
                return View();

            }
        }       
            
       
    }
}