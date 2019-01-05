using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ussmain.Models;

namespace ussmain
{
    public static class StaticData
    {

       public static List<SelectListItem> BusinessType()
        {
            var nos = new List<SelectListItem>();
            nos.Add(new SelectListItem { Text = "BTK(Bulls Trench Kiln)", Value = "BTK(Bulls Trench Kiln)" });
            nos.Add(new SelectListItem { Text = "Fly Ash Brick", Value = "Fly Ash Brick" });
            nos.Add(new SelectListItem { Text = "Block and Tiles", Value = "Block and Tiles" });
            nos.Add(new SelectListItem { Text = "Natural High Draft Brick Kiln", Value = "Natural High Draft Brick Kiln" });
            nos.Add(new SelectListItem { Text = "High Draft Brick Kiln", Value = "High Draft Brick Kiln" });
            nos.Add(new SelectListItem { Text = "Vertical Shaft Brick Kiln", Value = "Vertical Shaft Brick Kiln" });
            nos.Add(new SelectListItem { Text = "Hoffmans Kiln", Value = "Hoffmans Kiln" });


            return nos;
        }


       public static List<SelectListItem> DesignationList()
       {
           var nos = new List<SelectListItem>();
           nos.Add(new SelectListItem { Text = "Owner", Value = "Owner" });
           nos.Add(new SelectListItem { Text = "Accountant", Value = "Accountant" });
           nos.Add(new SelectListItem { Text = "CA", Value = "CA" });
           nos.Add(new SelectListItem { Text = "Computer Operator", Value = "Computer Operator" });
           nos.Add(new SelectListItem { Text = "Other", Value = "Other" });
           return nos;
       }
       public static List<SelectListItem> OurWebsiteList()
       {
           var nos = new List<SelectListItem>();
           nos.Add(new SelectListItem { Text = "Brick Kiln", Value = "Brick Kiln" });
           nos.Add(new SelectListItem { Text = "uSofts", Value = "uSofts" });
           
           return nos;
       }

       public static List<SelectListItem> CommentTypeList()
       {
           var items = new List<SelectListItem>();
           string path = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
           using (System.Xml.XmlReader reader = System.Xml.XmlReader.Create(path + @"\ReadWrite\commenttype.xml"))
           {
               while (reader.Read())
               {
                   if (reader.IsStartElement())
                   {
                       //return only when you have START tag
                       switch (reader.Name.ToString())
                       {
                           case "typename":
                               var itemtext = reader.ReadString();
                               items.Add(new SelectListItem { Text = itemtext, Value = itemtext });
                               break;
                       }
                   }
               }
           }
           return items;
       }

       public static List<SelectListItem> PackageType()
       {
           var nos = new List<SelectListItem>();
           nos.Add(new SelectListItem { Text = "Basic", Value = "Basic" });
           nos.Add(new SelectListItem { Text = "Advanced", Value = "Advanced" });

           return nos;
       }
       public static List<SelectListItem> ProjectNameList()
       {
           var list = new string[] { 
            "eBrickkiln", "eCampus-Plus", "eCampus-Enterprise", "eSchool-Lite", "eSchool-Plus","eSchool-Enterprise","eLibsys","Academic","Inventory"
            ,"eBill", "eAutomobile","Web Application","Bulk SMS","eAcademic","Unilead"
            };


           var nos = new List<SelectListItem>();

           foreach (var item in list)
           {
               var newlistitem = new SelectListItem { Text = item, Value = item };
               nos.Add(newlistitem);
           }
           return nos;
       }
       public static List<SelectListItem> TaskStatusList()
       {
           IEnumerable<TaskStatus> actionTypes = Enum.GetValues(typeof(TaskStatus))
                                                        .Cast<TaskStatus>();
           var nos = new List<SelectListItem>();
           foreach (var item in actionTypes)
           {
               var newlistitem = new SelectListItem { Text = item.ToString(), Value = item.ToString() };
               nos.Add(newlistitem);
           }
           return nos;
       }
      

       public static string GetSHA512(string text)
       {
           //UnicodeEncoding UE = new UnicodeEncoding();
           byte[] hashValue;
           //byte[] message = UE.GetBytes(text);
           byte[] message = Encoding.ASCII.GetBytes(text);

           SHA512Managed hashString = new SHA512Managed();
           string hex = "";

           hashValue = hashString.ComputeHash(message);
           foreach (byte x in hashValue)
           {
               hex += String.Format("{0:x2}", x);
           }
           return hex;
       }

       public static ImageFileStatus CheckFile(HttpPostedFileBase fup)
       {
           if (fup.ContentLength == 0)
           {
               return ImageFileStatus.NoFile;
           }

           if (System.IO.Path.GetExtension(fup.FileName) != ".jpg")
           {
               return ImageFileStatus.NotFormat;
           }
           int size_gp_pdf = fup.ContentLength;
           if (size_gp_pdf > 2100000 && size_gp_pdf < 0)
           {
               return ImageFileStatus.NotSize;
           }
           return ImageFileStatus.Success;

       }

       public static string RandomData()
       {
           string name = "";
           System.Text.StringBuilder sb = new System.Text.StringBuilder();

           int len = 6;
           string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

           Random r = new Random();
           while ((len--) > 0)
           {
               sb.Append(str[(int)(r.NextDouble() * str.Length)]);
           }

           name = sb.ToString() + DateTime.Now.ToShortDateString().Replace("/", "");
           return name;
       }

       public enum ImageFileStatus
       {
           Success,
           NotSize,
           NotFormat,
           NoFile
       }

      public enum ManageType 
       {
           Project,
           Task,
           User,
          Comment
       }

      public enum TaskStatus
      {
          New,
          Open,
          Closed,
          Waiting,
      }
       
    }

    public class HelpingMethods
    {
        public bool SendMail(string mailto, string mail_subject, string mail_body, string AdminText = "uSoftS, Admin")
        {

            bool success = false;
            try
            {
                var smtphost = ConfigurationManager.AppSettings["smtphost"];
                var smtpport = ConfigurationManager.AppSettings["smtpport"];
                var smtpuser = ConfigurationManager.AppSettings["smtpuser"];
                var smtppswd = ConfigurationManager.AppSettings["smtppswd"];

                
                string AdminUser = smtpuser;
                string AdminP = smtppswd;

                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();

                MailAddress fromAddress = new MailAddress(AdminUser, AdminText);
                message.From = fromAddress;
                message.To.Add(mailto);

                message.Subject = mail_subject;
                message.IsBodyHtml = true;
                message.Body = mail_body;
                //smtpClient.Host = "mail.brickkilnsoftware.net";
                //smtpClient.Host = "mail.usofts.net";
                smtpClient.Host = smtphost;
                if(smtpport!="")
                    smtpClient.Port =Convert.ToInt32(smtpport);


                smtpClient.Credentials = new System.Net.NetworkCredential(AdminUser, AdminP);

                smtpClient.Send(message);
                success = true;

            }
            catch { }
            return success;
        }

        public bool SendMailSales(string mailto, string mail_subject, string mail_body, string AdminText = "uSoftS, Admin")
        {

            bool success = false;
            try
            {
                var smtphost = ConfigurationManager.AppSettings["smtphost"];
                var smtpport = ConfigurationManager.AppSettings["smtpport"];
                var smtpuser = ConfigurationManager.AppSettings["smtpuser"];
                var smtppswd = ConfigurationManager.AppSettings["smtppswd"];

                string AdminUser = smtpuser;
                string AdminP = smtppswd;

                

                MailMessage message = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();

                MailAddress fromAddress = new MailAddress(AdminUser, AdminText);
                message.From = fromAddress;
                message.To.Add(mailto);

                message.Subject = mail_subject;
                message.IsBodyHtml = true;
                message.Body = mail_body;
                //smtpClient.Host = "mail.brickkilnsoftware.net";
                //smtpClient.Host = "mail.usofts.net";
                smtpClient.Host = smtphost;
                if (smtpport != "")
                    smtpClient.Port = Convert.ToInt32(smtpport);

                smtpClient.Credentials = new System.Net.NetworkCredential(AdminUser, AdminP);

                smtpClient.Send(message);
                success = true;

            }
            catch { }
            return success;
        }

        public bool CheckReCaptcha(string response)
        {
            //var response = Request["g-recaptcha-response"];
            if (response == null)
            {
                //ViewBag.resultmail = "Captcha is not valid. Please try again.";
                return false;
            }
            const string secret = "6LdqWhYUAAAAAG40Ieg9uDPFRKOWSynrL3GPBYyd";
            var client = new WebClient();
            var reply =
                client.DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                secret, response));
            var captchaResponse = JsonConvert.DeserializeObject<CaptchaResponse>(reply);

            //when response is false check for the error message
            if (captchaResponse.Success)
            {
                return true;
            }

            return false;

        }

        

        public string RandomNumber()
        {
            string number = "";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            int len = 5;
            string str = "1234567890";

            Random r = new Random();
            while ((len--) > 0)
            {
                sb.Append(str[(int)(r.NextDouble() * str.Length)]);
            }

            number = sb.ToString();
            return number;
        }

       


    }
}