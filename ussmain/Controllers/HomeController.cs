using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ussmain.Models;
using ussmain;
using System.Text;
using R.BusinessEntities;
using R.BAL;
using System.Web.Security;
using System.Security.Principal;


namespace ussmain.Controllers
{
    public class HomeController : Controller
    {

        iClientRecordService _mainobj;
        iBlogModuleService _blogsobj;
        iTagsModuleService _tagobj;

        static int opttrycount = 0;


        public HomeController(iClientRecordService imainobj, iBlogModuleService blogsobj, iTagsModuleService tagobj)
        {
            this._mainobj = imainobj;
            this._blogsobj = blogsobj;
            this._tagobj = tagobj;

        }

        public ActionResult Index()
        {
            //ViewBag.Message = "This is home page";
            ViewBag.resultmail = null;

            return View();
        }

        //public ActionResult About()
        //{
        //    ViewBag.Message = "About the Unique Software Solution";

        //    return View();
        //}

        public ActionResult features()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult Blogs(string id)
        {
            Session["tagname"] = id;
            return View();
        }

        public ActionResult BlogsAll() 
        {
            var publishedblogs = GetAllPublishedBlogs();
            return PartialView("_blogsall",publishedblogs);
        }

        private List<BlogModuleModel> GetAllPublishedBlogs()
        {
            var result = _blogsobj.GetAll();

            var tagid = GetTagByname();

            var publishedblogs = new List<BlogModuleModel>();
            if (result != null)
            {
                foreach (var blog in result)
                {
                    if (blog.blogwebsite.Contains("Brick Kiln"))
                    {
                        if (blog.blogpublished)
                        {
                            if (tagid != 0)
                            {
                                if (blog.blogtags != null)
                                {
                                    if (blog.blogtags.Split(',').Contains(tagid.ToString()))
                                    {
                                        publishedblogs.Add(blog);
                                    }
                                }
                                continue;
                            }
                            publishedblogs.Add(blog);
                        }
                    }
                }
                publishedblogs = publishedblogs.AsEnumerable().OrderByDescending(a => a.blogdate).ToList();
            }
            return publishedblogs;
        }

        private int GetTagByname() 
        {
             var tagid = 0;
             if (Session["tagname"] != null)
             {
                 var tagmodel = _tagobj.GetByName(Session["tagname"].ToString());
                 Session.Remove("tagname");

                 if (tagmodel != null)
                 {
                     tagid = tagmodel.tagid;
                 }
             }
            return tagid;
        }

        public ActionResult BlogDetail(string id)
        {
            try
            {
                var result = _blogsobj.GetByBlogNameKeyword(id);
                if (result != null)
                {
                    if (result.blogtags != null)
                    {
                        var tagarray = result.blogtags.Split(',');
                        var tags = _tagobj.GetAllByOther(tagarray);
                        var tagname = string.Empty;
                        foreach (var item in tags)
                        {
                            tagname += item.tagname + ", ";
                        } 
                        result.blogtagsdisplay = tagname;

                    }
                    Session["blogid"] = result;
                    ViewBag.Title = result.blogtitle;
                    ViewBag.Metatagscustom = "<meta property=\"og:image\" content=\"http://brickkilnsoftware.net/ReadWrite/" + result.blogimage + ".jpg\"/>";
                    return View();
                }
            }
            catch { }
           return RedirectToAction("Blogs");
        }

        public ActionResult BlogDescription() 
        {
            var bid =(BlogModuleModel)Session["blogid"];
            Session.Remove("blogid");
            return PartialView("_blogdetail", bid);
        }

        //public ActionResult BlogRelated(string btags, long currentid)
        //{
        //    var publishedblogs = GetAllPublishedBlogs();


        //    var tids = btags.Split(',');
        //    var oblogs = _blogsobj.GetAllByOther(tids);
        //    oblogs = oblogs.AsEnumerable().Where(a => a.blogpublished).ToList();
        //    return PartialView("_BlogRelated", oblogs);
        //}

        //[HttpPost]
        //public ActionResult message(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("message");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}


        //[HttpGet]
        //public ActionResult Contact()
        //{
        //    //ViewBag.Message = "Contact Unique Software Solution";
        //    ViewBag.resultmail = null;
        //    return View();
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Contact model)
        {
            ViewBag.resultmail = null;

            try
            {
                if (ModelState.IsValid)
                {
                    HelpingMethods hm = new HelpingMethods();
                    //if (!hm.CheckReCaptcha(Request["g-recaptcha-response"]))
                    //{
                    //    ViewBag.resultmailhome = "Captcha is not valid. Please try again.";
                    //    return View();
                    //}

                    ViewBag.resultmailhome = "Message not sent";

                    StringBuilder sbEmailBodyl = new StringBuilder();

                    sbEmailBodyl.Append("Name: " + model.Name + "<br/>");
                    sbEmailBodyl.Append("Email: " + model.Email + "<br/>");
                    sbEmailBodyl.Append("Mobile: " + model.Mobile + "<br/>");
                    sbEmailBodyl.Append("Message: " + model.Message + "<br/>");
                    var result = hm.SendMail("sales@usofts.net", "Message", sbEmailBodyl.ToString(), "eBrickKiln, Message by visitor");
                    //var result = hm.SendMail("gssaini7@gmail.com", "Message", sbEmailBodyl.ToString(), "Usofts, Message by visitor");

                    if (result)
                        ViewBag.resultmailhome = "Message succesfully sent.";
                    //return View("SuccessMessage");
                    //return View();

                }
            }
            catch { }
            
            return View();
        }

        public ActionResult LoginAdmin()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

         [HttpPost]
         //[ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string byotp, string bypassword)
         {
             
             if (ModelState.IsValid)
             {
                 try
                 {
                     var localusermobile = model.MobileLogin.Trim();

                     if (localusermobile.All(char.IsDigit))
                     {
                         if (localusermobile.Length == 10) 
                         {
                             var userdetail = _mainobj.GetByMobile(localusermobile);
                             if (userdetail == null)
                             {
                                 ModelState.AddModelError("MobileLogin", "Mobile is not registered with us, please contact us to get registerd.");
                                 return View();
                             }
                             else 
                             {
                                 if (userdetail.userblocked) 
                                 {
                                     ModelState.AddModelError("MobileLogin", "Your account is blocked by admin.");
                                     return View();
                                 }
                                 var currentwebsite = "Brick Kiln,";
                                 var userforcuurentwebsite = userdetail.formodule.Contains(currentwebsite);
                                 if (!userforcuurentwebsite) 
                                 {
                                     ModelState.AddModelError("MobileLogin", "Mobile is not registered with us for current website, please contact us to get registerd.");
                                     return View();
                                 }
                             }

                             if (!string.IsNullOrEmpty(byotp))
                             {
                                 HelpingMethods hm = new HelpingMethods();

                                 Session["otp"] = hm.RandomNumber();
                                 //Session["otp"] = 1111;/////////////////////////////////////// temp
                                 Session["mobilenumber"] = model.MobileLogin;
                                 ViewBag.mobilenumber = model.MobileLogin;

                                 string message = "OTP is " + Session["otp"].ToString() + " for login to brick kiln support.";
                                 var result = hm.sendsmsany(message, ViewBag.mobilenumber);

                                 Session["modelvalues"] = model;
                                 opttrycount = 0;
                                 return View("ConfirmOTP");
                             }
                            else if (!string.IsNullOrEmpty(bypassword))
                             {
                                 var upassword = StaticData.GetSHA512(model.Password);
                                 var loginresult = _mainobj.GetByMobilePassword(localusermobile, upassword);
                                 if (loginresult != null)
                                 {
                                     FormsAuthentication.Initialize();
                                     HttpContext currentContext = System.Web.HttpContext.Current;
                                     FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                             1, loginresult.clientname, DateTime.Now, DateTime.Now.AddMinutes(30), true,
                                            loginresult.userrole, FormsAuthentication.FormsCookiePath);
                                     string hash = FormsAuthentication.Encrypt(ticket);
                                     HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                                     currentContext.Response.Cookies.Add(cookie);

                                     AuthenticateThisRequest();

                                     if (User.IsInRole("Admin"))
                                         return RedirectToAction("Index", "Description");
                                     else if (User.IsInRole("Support"))
                                         return RedirectToAction("ClientDetail", "Support");
                                     else if (User.IsInRole("Client"))
                                         return RedirectToAction("Index", "Support");
                                 }
                                 else {
                                     ModelState.AddModelError("MobileLogin", "Mobile or Password is not valid");
                                     return View("LoginAdmin");
                                 }
                             }

                             
                         }
                     }
                 }
                 catch { }
             }
             ModelState.AddModelError("MobileLogin", "Mobile is not valid");
             return View();
         }

        [HttpPost]
         public ActionResult ResendOTP() 
         {
             var status = false;
             try
             {
                 HelpingMethods hm = new HelpingMethods();
                 Session["otp"] = hm.RandomNumber();
                 string message = "OTP is " + Session["otp"].ToString() + " for login to brick kiln support.";
                 var r = hm.sendsmsany(message, Session["mobilenumber"].ToString());
                 status = true;
             }
             catch { }
             return new JsonResult { Data = new { status = status } };
         }


         private void AuthenticateThisRequest()
         {
             //NOTE:  if the user is already logged in (e.g. under a different user account)
             //       then this will NOT reset the identity information.  Be aware of this if
             //       you allow already-logged in users to "re-login" as different accounts 
             //       without first logging out.
             if (HttpContext.User.Identity.IsAuthenticated) return;

             var name = FormsAuthentication.FormsCookieName;
             var cookie = Response.Cookies[name];
             if (cookie != null)
             {
                 var ticket = FormsAuthentication.Decrypt(cookie.Value);
                 if (ticket != null && !ticket.Expired)
                 {
                     string[] roles = (ticket.UserData as string ?? "").Split(',');
                     HttpContext.User = new GenericPrincipal(new FormsIdentity(ticket), roles);
                 }
             }
         }

         //public ActionResult ConfirmOTP()
         //{
         //    return View();
         //}
         
         [ValidateAntiForgeryToken]
         public ActionResult ConfirmOTPFinal(OTPModel otpdata)
         {
             try
             {
                 if (ModelState.IsValid)
                 {

                     var otpapp = Session["otp"];
                     var mobilenumber = Session["mobilenumber"];
                     if (otpdata.Otpvalue == otpapp.ToString())
                     {
                         
                             Session.Remove("otp");
                             Session.Remove("mobilenumber");

                             var userdetail = _mainobj.GetByMobile(mobilenumber.ToString());
                             if (userdetail != null)
                             {
                                 FormsAuthentication.Initialize();
                                 HttpContext currentContext = System.Web.HttpContext.Current;
                                 FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                         1, userdetail.clientname, DateTime.Now, DateTime.Now.AddMinutes(30), true,
                                        userdetail.userrole, FormsAuthentication.FormsCookiePath);
                                 string hash = FormsAuthentication.Encrypt(ticket);
                                 HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                                 currentContext.Response.Cookies.Add(cookie);

                                 AuthenticateThisRequest();
                             }
                             //ModelState.AddModelError("MobileLogin", "Your mobile number is not registerd with us.");
                             return RedirectToAction("Index", "Support");
                        
                     }
                     else
                     {
                         opttrycount++;
                         if (opttrycount > 2)
                         {
                             Session.Remove("otp");
                             Session.Remove("mobilenumber");

                             ModelState.AddModelError("MobileLogin", "OTP is expired. To get new OTP, enter mobile number again.");
                             return View("Login");
                         }

                         ViewBag.mobilenumber = mobilenumber;
                         ModelState.AddModelError("Otpvalue", "OTP is not valid");
                         return View("ConfirmOTP");
                     }
                 }
             }
             catch { }
             return View("Login");

         }

         [ValidateAntiForgeryToken]
         public ActionResult ConfirmPassword(OTPModel otpdata)
         {
             try
             {
                 if (ModelState.IsValid)
                 {

                     var otpapp = Session["otp"];
                     var mobilenumber = Session["mobilenumber"];
                     if (otpdata.Otpvalue == otpapp.ToString())
                     {

                         Session.Remove("otp");
                         Session.Remove("mobilenumber");

                         var userdetail = _mainobj.GetByMobile(mobilenumber.ToString());
                         if (userdetail != null)
                         {
                             FormsAuthentication.Initialize();
                             HttpContext currentContext = System.Web.HttpContext.Current;
                             FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                     1, userdetail.clientname, DateTime.Now, DateTime.Now.AddMinutes(30), true,
                                    userdetail.userrole, FormsAuthentication.FormsCookiePath);
                             string hash = FormsAuthentication.Encrypt(ticket);
                             HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);
                             currentContext.Response.Cookies.Add(cookie);

                             AuthenticateThisRequest();
                         }
                         //ModelState.AddModelError("MobileLogin", "Your mobile number is not registerd with us.");
                         return RedirectToAction("Index", "Support");

                     }
                     else
                     {
                         opttrycount++;
                         if (opttrycount > 2)
                         {
                             Session.Remove("otp");
                             Session.Remove("mobilenumber");

                             ModelState.AddModelError("MobileLogin", "OTP is expired. To get new OTP, enter mobile number again.");
                             return View("Login");
                         }

                         ViewBag.mobilenumber = mobilenumber;
                         ModelState.AddModelError("Otpvalue", "OTP is not valid");
                         return View("ConfirmOTP");
                     }
                 }
             }
             catch { }
             return View("Login");

         }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public ActionResult LogOff()
         {

             Session.Clear();
             Session.Abandon();
             Session.RemoveAll();
             Session.Remove("userlogedin");
             FormsAuthentication.SignOut();
             //User = new GenericPrincipal(new GenericIdentity(string.Empty), null);
             Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

             return RedirectToAction("Index", "Home");
         }

         
        public ActionResult RequestQuote()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestQuote(QuoteModel model)
        {
            try
            {
                if (ModelState.IsValid) 
                {
                    HelpingMethods hm = new HelpingMethods();
                    //if (!hm.CheckReCaptcha(Request["g-recaptcha-response"]))
                    //{
                    //    ViewBag.resultmail = "Captcha is not valid. Please try again.";
                    //    return View();
                    //}


                    StringBuilder sbEmailBodyl = new StringBuilder();
                    sbEmailBodyl.Append("<table border='1' style='width:100%;'>");
                    sbEmailBodyl.Append("<tr><th colspan='2'>Owner Details</th></tr>");
                    sbEmailBodyl.Append("<tr><td>Name </td><td>" + model.Name + "</td></tr>");
                    sbEmailBodyl.Append("<tr><td>Designation </td><td>" + model.Designation + "</td></tr>");
                    sbEmailBodyl.Append("<tr><td>Contact Number </td><td>" + model.Mobile + "</td></tr>");
                    sbEmailBodyl.Append("<tr><td>Email </td><td>" + model.Email + "</td></tr>");
                    sbEmailBodyl.Append("<tr><th colspan='2'>Business Details</th></tr>");
                    sbEmailBodyl.Append("<tr><td>BKO or Business Name </td><td>" + model.Org_Name + "</td></tr>");
                    sbEmailBodyl.Append("<tr><td>Brick Kiln or Business Type </td><td>" + model.busiess_type + "</td></tr>");
                    sbEmailBodyl.Append("<tr><td>Location </td><td>" + model.City_village + "</td></tr>");
                    sbEmailBodyl.Append("<tr><td>City or Village </td><td>" + model.District + "</td></tr>");
                    sbEmailBodyl.Append("<tr><td>State </td><td>" + model.State + "</td></tr>");
                    sbEmailBodyl.Append("<tr><td>Country</td><td>" + model.Country + "</td></tr>");
                    sbEmailBodyl.Append("<tr><td>See the Vedio Demo First</td><td>" + model.seen_video + "</td></tr>");
                    sbEmailBodyl.Append("<tr><td>Interested For</td><td>" + model.Interested_for + "</td></tr>");
                    sbEmailBodyl.Append("</table>");
                    //var result = hm.SendMail("gssaini7@gmail.com", "Request Quote", sbEmailBodyl.ToString(), "eBrickKiln- " + model.Interested_for + ", Request Quote by vistor");

                    var result = hm.SendMailSales("sales@usofts.net", "eBrickKiln- " + model.Interested_for, sbEmailBodyl.ToString(), "eBrickKiln- Sales");
                    var resulttovisitor = hm.SendMailSales(model.Email, "eBrickKiln- Greetings", MailToClient(), "eBrickKiln- Sales");

                    var resultsms=hm.sendsmsany("Thanks from 'uSofts' to show your interest in our eBrickkiln product for your business. Detail sent on your mail Id Soon you will get call from sale executive.", model.Mobile);

                    if (result)
                    {
                        //ViewBag.resultmail = "Your message sent.";
                        return View("ContactSuccess");
                    }
                }
            }
            catch { }
            return View();
        }


        private string MailToClient() 
        {
            StringBuilder sbEmailBodyl = new StringBuilder();
            sbEmailBodyl.Append("<p><span >Respected Sir/ Madam,</span></p>");
            sbEmailBodyl.Append("<p><b><span style='font-size:14.0pt'>Greeting From</span><span style='font-size:14.0pt;color:#254180'> </span><span style='font-size:14.0pt;color:#f1ab1f'>uSofts</span></b></p>");
            sbEmailBodyl.Append("<p>As per your request regarding the <b>eBrickkiln </b>software for <b>brick kiln Business</b>. uSofts will provide you platform "
+"where you can get all information related to business on your finger tips. We don’t mind from wherever you buy software for your brick kiln, but being a responsible company, who is serving brick kiln industry for last 14 years,"
          +" we consider it our primary responsibility to guide and advise (on how to buy a good software for brick kiln) all brick kiln owners who still are not using computer as their primary record keeping tool or are using computer to maintain record but are not satisfied the way it is answering their queries. We suggest Twenty Tips for Brick Kiln Owners how to buy the best software"
+" for <b>brick kiln Business</b>?</p>");
            sbEmailBodyl.Append("<p><b>Other key Point Which make the software Unique </b> </p>");
            sbEmailBodyl.Append("<ul>");
            sbEmailBodyl.Append("<li>uSofts Team member has same business so can easily understand the client requirement and provide them solution</li>");
            sbEmailBodyl.Append("<li>This is a Business Oriented Software for brick kiln to manage the account up to balance sheet as well as other process.</li>");
            sbEmailBodyl.Append("<li>Due to User friendly and easy flow layman can understand in short time after basic training.</li>");
             sbEmailBodyl.Append("<li>This software developed under guidance of brick kiln owners, CA  accountant and other concern so nothing is missing in eBrckkiln.</li>");
             sbEmailBodyl.Append("<li>Easy to trace the leakage point in business during audit process due to tremendous report.</li>");
            sbEmailBodyl.Append("</ul>");
            sbEmailBodyl.Append("<table style='width:100%;border-collapse:collapse;border:medium none' height='52' cellspacing='0' cellpadding='0' border='1'>"
  +"<tbody>"
  +"<tr style='height:16.5pt'>"
    +"<td style='width:15.0%;height:1;border:1.0pt inset gray;padding:0in;background:silver' width='15%'><p style='text-align:center' align='center'><b>Descriptions</b></p></td>"
    +"<td colspan='2' style='width:26%;height:1;border-left:medium none;border-right:1.0pt inset gray;border-top:1.0pt inset gray;border-bottom:1.0pt inset gray;padding:0in;background:silver'><p style='text-align:center' align='center'><b>"
    +"Download Link</b></p></td>"
  +"</tr>"
  +"<tr style='height:14.5pt'>"
+"    <td style='width:15.0%;height:28;border-left:1.0pt inset gray;border-right:1.0pt inset gray;border-top:medium none;border-bottom:1.0pt inset gray;padding:0in' width='15%'>"
  +"  <p>Software Video demo  </p></td>"
    +"<td colspan='2' style='width:26%;height:28;border-left:medium none;border-right:1.0pt inset gray;border-top:medium none;border-bottom:1.0pt inset gray;padding:0in'>"
    + "<p><b><a href='http://brickkilnsoftware.net/' style='text-decoration:none' target='_blank'>Video Demo Link</a>  </b></p></td>"
  +"</tr>"
  +"<tr style='height:1.0pt'>"
+"    <td style='width:15.0%;height:28;border-left:1.0pt inset gray;border-right:1.0pt inset gray;border-top:medium none;border-bottom:1.0pt inset gray;padding:0in' width='15%'>"
    +"<p>Download for online demo </p></td>"
    +"<td colspan='2' style='width:26%;height:28;border-left:medium none;border-right:1.0pt inset gray;border-top:medium none;border-bottom:1.0pt inset gray;padding:0in'>"
    +"<p><b><a href='https://www.teamviewer.com/en/latest-version/?pid=google.tv_12.s.in&amp;gclid=Cj0KEQiA9P7FBRCtoO33_LGUtPQBEiQAU_tBgGHhYEWLfTV6265niQ4HA8BW1eetOuqWxxpH1ictloIaArZT8P8HAQ' style='text-decoration:none' target='_blank'> Team Viewer</a> Or  <a href='https://anydesk.com/download' style='text-decoration:none' target='_blank'>Any Desk</a> "
    +"</b></p></td>"
  +"</tr>"
  +"<tr style='height:7.5pt'>"
+"    <td style='width:15.0%;height:1;border-left:1.0pt inset gray;border-right:1.0pt inset gray;border-top:medium none;border-bottom:1.0pt inset gray;padding:0in' width='15%'>"
  +"  <p>eBrickkiln Price</p></td>"
+"    <td style='width:13.0%;height:1;border-left:medium none;border-right:1.0pt inset gray;border-top:medium none;border-bottom:1.0pt inset gray;padding:0in' width='13%'>"
    +"<p><b>Single Company or Kiln</b></p></td>"
    +"<td style='width:13%;height:1;border-left:medium none;border-right:1.0pt inset gray;border-top:medium none;border-bottom:1.0pt inset gray;padding:0in'>"
    +"<p><b>20000 Rs.(Tax Inclusive)</b></p></td>"
  +"</tr>"
  +"<tr style='height:17.0pt'>"
    +"<td colspan='3' style='width:41%;height:8;border-left:1.0pt inset gray;border-right:1.0pt inset gray;border-top:medium none;border-bottom:1.0pt inset gray;padding:0in' bgcolor='#C0C0C0'>"
    +"<p><span style='font-weight:700'>For Extra company </span>"
    +"<span style='font-weight:700'> 5000/-&nbsp;"
    +"</font> </span>"
    
+"    </p></td>"
  +"</tr>"
  +"<tr style='height:17.0pt'>"
    +"<td colspan='3' style='width:41%;height:77;border-left:1.0pt inset gray;border-right:1.0pt inset gray;border-top:medium none;border-bottom:1.0pt inset gray;padding:0in'>"
    +"<b>"
    +"<span style='font-family:Calibri,sans-serif;color:#254180'>"
    +"<font size='4'>"
    +"Process to Get the eBrickkiln"
    +"</font>"
    +"<font size='5'>"
    +"<br>"
    +"</font>"
    +"</span>"
    +"</b><i>"
    +"<b>"
    +"<span style='font-family:Calibri,sans-serif;color:red'>"
    +"Quotation</span></b><span style='font-family:Calibri,sans-serif;color:red'><b> &gt;"
    +"</b> </span>"
    +"<b>"
    +"<span style='font-family:Calibri,sans-serif;color:red'>"
    +"Order Confirmation </span></b></i><b><i>"
    +"<span style='font-family:Calibri,sans-serif;color:red'>"
    +"&gt;"
    + " 100%&nbsp;Advance&nbsp;Payment&nbsp;"
    +"&gt;"
    + "&nbsp;Software installation &amp;&nbsp;"
    + "&nbsp;Training </span></i></b></td>"
  +"</tr>"
+"</tbody></table>");
            sbEmailBodyl.Append("<p><b>Annual Subscription</b> after one year 3000/- (Will Cover support, New version updation, Online training if required after implementation)</p>");
            sbEmailBodyl.Append("<p>We are also dealing with other products and services given below:-</p>");
            sbEmailBodyl.Append("<ul>");
            sbEmailBodyl.Append("<li><i>eSchool (A complete School Management ERP Solution)</i></li>");
             sbEmailBodyl.Append("<li><i>eCampus (A complete College Management ERP Solution)</i></li>");
             sbEmailBodyl.Append("<li><i>eLibsys(A Complete Library Management Software)</i></li>");
             sbEmailBodyl.Append("<li><i>Unileads (Marketing and Sales related CRM )</i></li>");
             sbEmailBodyl.Append("<li><i>Web Designing and development</i></li>");
             sbEmailBodyl.Append("<li><i>Domain Registration and Hosting Services</i></li>");
             sbEmailBodyl.Append("<li><i>Search Engine Optimization (SEO)</i></li>");
             sbEmailBodyl.Append("<li><i>Bulk SMS Solution</i></li>");
            sbEmailBodyl.Append("</ul>");

            sbEmailBodyl.Append("<p><b>If you have any question regarding software or about our services always fell free to ask.</b></p>");
            sbEmailBodyl.Append("<p><i><b>We believe in a satisfied customer is business earned.</b></i></p>");
            sbEmailBodyl.Append("<p>Thanks &  Regards, </p>");
            sbEmailBodyl.Append("<p><b><span style='font-family:'Segoe UI',sans-serif;color:#984806'>uSofts Sale </span></b></p>");
            sbEmailBodyl.Append("<p>sales@usofts.net <span style='color:#d9d9d9;'> | </span>+91-87250-52452</p>");
            sbEmailBodyl.Append("<p><b><span style='color:#254180;'>Unique Software Solution</span></b><span style='color:#d9d9d9;'>&nbsp;|&nbsp;</span>www.usofts.net</p>");
            sbEmailBodyl.Append("<p style='color:#a6a6a6;'>This email is only for individual or entity to whom they are addressed. If you have received the email in error then please inform the sender immediately and delete"
                +" the email from your system permanently. Computer viruses can be transmitted via email. The recipient should check this email and any attachments for the presence of viruses. uSofts accepts no liability for any damage caused by any"
                +" virus transmitted by this email. Any or all information provided by uSofts in this email is sourced from internet or other mediums, concerned person must verify or confirm it from respective person, body or department about which/whom"
                +" the information is provided</span></p>");


            return sbEmailBodyl.ToString();

        }

        
        
    }
}
