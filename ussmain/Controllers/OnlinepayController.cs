using R.BAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ussmain.Models;

namespace ussmain.Controllers
{
    public class OnlinepayController : Controller
    {
        public String salt = "491e09c07329c3df7c364c3384bfef14750ea4df";

        iCouponService _mainobj;

        public OnlinepayController(iCouponService imainobj)
        {
            this._mainobj = imainobj;
        }
        //public ActionResult PaymentDetails() {
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult PaymentDetails(PaymentContact request)
        //{
        //    //ViewBag.paymentdetails = request;
        //    return RedirectToAction("SubmitRequest", request);
        //}

        public ActionResult SubmitRequest()
        {
           
            return View();
        }

        [HttpPost]
        public void SubmitRequest(FormCollection request)
        {
            if (request == null)
            {
                RedirectToAction("SubmitRequest");
            }
            string[] hash_columns = {
            "address_line_1",
            "address_line_2",
            "amount",
            "api_key",
            "city",
            "country",
            "currency",
            "description",
            "email",
            "mode",
            "name",
            "order_id",
            "phone",
            "return_url",
            "state",
            "udf1",
            "udf2",
            "udf3",
            "udf4",
            "udf5",
            "zip_code"
            };




            //var amount = "20000";
            //var amount = ApplyCoupon(request["pcCoupon"]);
            //var amount = GetCouponAmount(request["pcCoupon"]);
            var amount = request["pcAmount"];



            //var amount = "1";
            var mode = "TEST";

            //var mode = "LIVE"; 

            var rndmdata = RandomData();
            RemotePost remotepost = new RemotePost();
            request["api_key"] = "46e6faab-409a-4b3c-99b3-e7a944d61af7";
            //request["salt"] = salt;
            request["return_url"] = Request.Url.ToString().Substring(0, Request.Url.ToString().Length - Request.Url.PathAndQuery.Length + Url.Content("~").Length) + "Onlinepay/Return";
            request["currency"] = "INR";
            request["country"] = "IND";
            request["name"] = request["pcName"];
            request["description"] = "Online payment of ebrickiln.";
            request["address_line_1"]= request["pcAddress"];
            request["phone"] = request["pcMobile"];
            request["city"] = request["pcCity"];
            request["zip_code"] = request["pcZip"];
            request["email"] = request["pcEmail"];
            request["amount"] = amount;
            request["order_id"] = rndmdata;
            request["mode"] = mode;
            request["udf1"] = request["pcCoupon"];




            remotepost.Url = "https://biz.traknpay.in/v1/paymentrequest";
            remotepost.Add("api_key", request["api_key"]);
            remotepost.Add("return_url", request["return_url"]);
            //remotepost.Add("mode", request["mode"]);
            remotepost.Add("mode", mode);



            //remotepost.Add("order_id", request["order_id"]);
            remotepost.Add("order_id", rndmdata);
            //remotepost.Add("amount", request["amount"]);
            remotepost.Add("amount", amount);
            remotepost.Add("name", request["pcName"]);
            remotepost.Add("currency", request["currency"]);
            //remotepost.Add("description", request["description"]);
            remotepost.Add("description", "Online payment of ebrickiln.");

            remotepost.Add("address_line_1", request["pcAddress"]);
            remotepost.Add("address_line_2", request["address_line_2"]);
            remotepost.Add("phone", request["pcMobile"]);
            remotepost.Add("email", request["pcEmail"]);
            remotepost.Add("city", request["pcCity"]);
            remotepost.Add("state", request["state"]);
            remotepost.Add("country", request["country"]);
            remotepost.Add("zip_code", request["pcZip"]);
            remotepost.Add("udf1", request["udf1"]);
            remotepost.Add("udf2", request["udf2"]);
            remotepost.Add("udf3", request["udf3"]);
            remotepost.Add("udf4", request["udf4"]);
            remotepost.Add("udf5", request["udf5"]);
            remotepost.Add("hash", gethash(hash_columns, request));
            remotepost.Post();
        }



        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Return(FormCollection form)
        {
            try
            {
                string transaction_id = string.Empty;
                string order_id = string.Empty;

                string[] keys = Request.Form.AllKeys;
                Array.Sort(keys);


                string hash = gethash(keys, form);
                if (form["hash"] == hash)
                {
                    if (form["response_code"] == "0")
                    {
                        transaction_id = Request.Form["transaction_id"];
                        order_id = Request.Form["order_id"];

                        ViewData["Message"] = "Transaction is successful. Your tranaction ID is: " + transaction_id + " and order id is: " + order_id + ". Please save it for future reference.";
                        HelpingMethods hm = new HelpingMethods();

                        var resulttovisitor = hm.SendMailSales(Request.Form["email"], "eBrickKiln- Payment Successful", mailcontent(transaction_id, order_id), "eBrickKiln- Sales");
                        
                        //  Response.Write("<br/>Transaction is successful. Hash value is matched");

                    }
                    else if (form["response_message"] == "Transaction Failed")
                    {
                        ViewData["Message"] = "Transaction is unsuccessful. Please try again.";
                    }
                    else
                    {
                        string response_message = Request.Form["response_message"];
                        int startIndex = response_message.IndexOf(" - ") + 2;
                        int length = response_message.Length - startIndex;
                        response_message = response_message.Substring(startIndex, length);
                        //Response.Write("Correct the below error <br />");
                        //Response.Write(response_message);
                        ViewData["Message"] = "To continue to transaction, please correct the error: "+ response_message;
                    }

                }
                else
                {
                    ViewData["Message"] = "Transaction is unsuccessful. Please try again.";

                    //Response.Write("<br/>Hash value Not matched");

                }

            }
            catch (Exception ex)
            {
                Response.Write("<span style='color:red'>" + ex.Message + "</span>");

            }
            return View("../Onlinepay/Return");
        }

       

        private string ApplyCoupon(string couponcode) 
        {
            couponcode = couponcode.Trim();
            var resultamount = "20000";
            //if (couponcode == "4067A12E89df")
            //    resultamount = "1";
            //else if (couponcode == "ebrickspecial18K")
            //    resultamount = "18000";
            if (couponcode == "ebrickspecial18K")
                resultamount = "18000";
            return resultamount;
        }

        private string mailcontent(string tid, string oid) {
            StringBuilder sbEmailBodyl = new StringBuilder();
            sbEmailBodyl.Append("<p><span >Respected Sir/Madam,</span></p>");
            sbEmailBodyl.Append("<p><b><span style='font-size:14.0pt'>Greeting From</span><span style='font-size:14.0pt;color:#254180'> </span><span style='font-size:14.0pt;color:#f1ab1f'>uSofts</span></b></p>");
            sbEmailBodyl.Append("<p>Transaction is successful.<br> Your tranaction ID is: " + tid + "<br>Your order id is: " + oid + "</p>");
            sbEmailBodyl.Append("<p>Thanks &  Regards, </p>");
            sbEmailBodyl.Append("<p><b><span style='font-family:'Segoe UI',sans-serif;color:#984806'>uSofts Sale </span></b></p>");
            sbEmailBodyl.Append("<p>sales@usofts.net <span style='color:#d9d9d9;'> | </span>+91-87250-52452</p>");
            sbEmailBodyl.Append("<p><b><span style='color:#254180;'>Unique Software Solution</span></b><span style='color:#d9d9d9;'>&nbsp;|&nbsp;</span>www.usofts.net</p>");
            sbEmailBodyl.Append("<p style='color:#a6a6a6;'>This email is only for individual or entity to whom they are addressed. If you have received the email in error then please inform the sender immediately and delete"
                + " the email from your system permanently. Computer viruses can be transmitted via email. The recipient should check this email and any attachments for the presence of viruses. uSofts accepts no liability for any damage caused by any"
                + " virus transmitted by this email. Any or all information provided by uSofts in this email is sourced from internet or other mediums, concerned person must verify or confirm it from respective person, body or department about which/whom"
                + " the information is provided</span></p>");
            return sbEmailBodyl.ToString();
        }

        private string RandomData()
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



        private string gethash(string[] hash_columns, FormCollection requests)
        {

            string checksumString;
            checksumString = salt;
            foreach (string column in hash_columns)
            {
                if (requests.Get(column) != null && column != "hash")
                {
                    if (!string.IsNullOrEmpty(requests[column]))
                    {
                        checksumString += "|" + requests[column];
                    }
                }

            }
            string result = Generatehash512(checksumString);
            return result;
        }


        public string Generatehash512(string text)
        {

            byte[] message = System.Text.Encoding.UTF8.GetBytes(text);

            System.Text.UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex.ToUpper();

        }

        public class RemotePost
        {
            private System.Collections.Specialized.NameValueCollection Inputs = new System.Collections.Specialized.NameValueCollection();


            public string Url = "";
            public string Method = "post";
            public string FormName = "form1";

            public void Add(string name, string value)
            {
                Inputs.Add(name, value);
            }

            public void Post()
            {
                System.Web.HttpContext.Current.Response.Clear();

                System.Web.HttpContext.Current.Response.Write("<html><head>");

                System.Web.HttpContext.Current.Response.Write(string.Format("</head><body onload=\"document.{0}.submit()\">", FormName));
                System.Web.HttpContext.Current.Response.Write(string.Format("<form name=\"{0}\" method=\"{1}\" action=\"{2}\" >", FormName, Method, Url));
                for (int i = 0; i < Inputs.Keys.Count; i++)
                {
                    System.Web.HttpContext.Current.Response.Write(string.Format("<input name=\"{0}\" type=\"hidden\" value=\"{1}\">", Inputs.Keys[i], Inputs[Inputs.Keys[i]]));
                }
                System.Web.HttpContext.Current.Response.Write("</form>");
                System.Web.HttpContext.Current.Response.Write("</body></html>");

                System.Web.HttpContext.Current.Response.End();
            }
        }

        [HttpPost]
        public ActionResult ApplyCouponCode(string cvalue)
        {
            var status = false;
            try
            {
                if (cvalue.Trim() != "")
                {
                    cvalue = cvalue.ToLower();
                    var coupondetail = _mainobj.GetByCouponCode(cvalue);
                    if (coupondetail != null)
                    {
                        var cmsg = coupondetail.cmsgforuser;
                        var camount = coupondetail.camount;
                        status = true;
                        return new JsonResult { Data = new { status = status, message = cmsg, newamount = camount, couponmessage="Coupon code applied." } };
                    }

                }
            }
            catch { }
            return new JsonResult { Data = new { status = status, couponmessage = "Coupon code is expired." } };
        }

        private string GetCouponAmount(string cvalue) 
        {
            string result = "20000";
            try
            {
                if (cvalue != null)
                {
                    if (cvalue.Trim() != "")
                    {
                        cvalue = cvalue.ToLower();
                        var coupondetail = _mainobj.GetByCouponCode(cvalue);
                        if (coupondetail != null)
                        {
                            result = coupondetail.camount.ToString();

                        }

                    }
                }
            }
            catch { }
            return result;
        }
    }
}
