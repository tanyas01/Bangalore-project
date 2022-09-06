using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XtraCoverBBGA.Models;

namespace XtraCoverBBGA.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveData(string DeviceType, string IMEINo, string Brand, string Name, string Model, string EmailId, string DealerCode, string Phone, string PurchasePrice, string AlternatePhone, string PurchaseDate, string AddLine1, string AddLine2)
        {
            if (db.Registrations.Where(e => e.IMEINo == IMEINo).Count() > 0)
            {
                return Json(0, JsonRequestBehavior.AllowGet);
            }
            Registration reg = new Registration();
            reg.DeviceType = DeviceType;
            reg.IMEINo = IMEINo;
            reg.Brand = Brand;
            reg.Name = Name;
            reg.ModelName = Model;
            reg.EmailId = EmailId;
            reg.DealerCode = DealerCode;
            reg.Phone = Phone;
            reg.PurchasePrice =Convert.ToDecimal(PurchasePrice);
            reg.AlternatePhone = AlternatePhone;
            reg.PurchaseDate = Convert.ToDateTime(PurchaseDate);
            reg.AddLine1 = AddLine1;
            reg.AddLine2 = AddLine2;
            reg.InsertDate = DateTime.UtcNow.AddHours(5.5);
            db.Registrations.Add(reg);
            db.SaveChanges();

            return Json(reg.ID, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadFiles()
        {
            string SaveURL = "";
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        string id = Request.Form["username"].ToString();
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname = "";

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        string[] exsp = fname.Split('.');
                        string ex = exsp[exsp.Length - 1];
                        //if (!ex.ToLower().Equals("pdf"))
                        //{
                        //    return Json("Error Invalid File");
                        //}

                        fname = "Invoice-" + id + "." + ex;
                        SaveURL = "/Uploads/" + fname;
                        //Get the complete folder path and store the file inside it.
                        fname = Path.Combine(Server.MapPath("~/Uploads/"), fname);
                        file.SaveAs(fname);
                        int userid = Convert.ToInt32(id);

                        var register = db.Registrations.Find(userid);
                        register.Invoicefile = SaveURL;
                        db.Entry(register).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    // Returns message that successfully uploaded  
                    return Json(SaveURL, 0);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }

        }

        private static Random rnd = new Random();
        private string GenerateSerialNo(int n)
        {
            string randomStr = string.Format("{0:00000}", n);
            randomStr = "XC" + randomStr;
            return randomStr;
        }
        private string GenerateRandomString()
        {
            string alphanumericCharacters = "ABCDEFGHIJKLMNPQRSTUVWXYZ123456789";
            var randomChars = new char[7];

            for (int i = 0; i < randomChars.Length; i++)
            {
                randomChars[i] = alphanumericCharacters[rnd.Next(alphanumericCharacters.Length)];
            }

            string randomStr = new string(randomChars);
            return randomStr;
        }
        public ActionResult GenerateCode()
        {
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public ActionResult GenerateCode(string device, int noofcode)
        {
            int maxsrno = 0;
            try
            {
                maxsrno = db.Vouchers.Max(e => e.ID);
            }
            catch { }
            Random rm = new Random();
            for (int i = 0, j = maxsrno; i < noofcode; i++)
            {
                Voucher vcode = new Voucher();
                vcode.DeviceName = device;
                vcode.Status = false;
            srno:
                j++;
                vcode.SerialNo = GenerateSerialNo(j);
                if (db.Vouchers.Where(e => e.SerialNo == vcode.SerialNo).Count() > 0)
                    goto srno;
                seccode:
                vcode.SecretCode = GenerateRandomString();
                if (db.Vouchers.Where(e => e.SecretCode == vcode.SecretCode).Count() > 0)
                    goto seccode;
                db.Vouchers.Add(vcode);
            }
            db.SaveChanges();
            ViewBag.Message = "";
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}