using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ussmain.Helpers;

namespace ussmain.Controllers
{
    //[Authorize(Roles = "Admin")]
    [MyCustomFilter("Admin")]
    public class BlogController : Controller
    {
        iBlogModuleService _mainobj;
        iTagsModuleService _tagobj;
        public BlogController(iBlogModuleService imainobj, iTagsModuleService tagobj)
        {
            this._mainobj = imainobj;
            this._tagobj = tagobj;

           
        }

        
        public ActionResult Index()
        {
            return View();
        }

       
        public ActionResult GetAll()
        {
            var results = _mainobj.GetAll();
            if (results != null)
            {
                //results = results.OrderByDescending(o => o.blogid).ToList();
                var newresults = new List<BlogModuleModel>();
                foreach (var r in results)
                {
                    if (r.blogtags != null)
                    {
                        var tagarray = r.blogtags.Split(',');
                        var tags = _tagobj.GetAllByOther(tagarray);
                        var tagname = string.Empty;
                        foreach (var item in tags)
                        {
                            tagname += item.tagname + ", ";
                        }
                        r.blogtagsdisplay = tagname;

                    }
                    newresults.Add(r);
                }
                return Json(new { data = newresults }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = new string[] { } }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Save(int id)
        {
            if (id < 0)
                return HttpNotFound();

            var result = _mainobj.GetById(id);
            
            return PartialView("_save", result);
           
        }
        //[HttpPost]
        //public ActionResult Save(BlogModuleModel input, string[] forwebsite, string[] blogtags)
        //{
        //    return new JsonResult { Data = new { status = false } };
        //}

        [HttpPost]
        public ActionResult Save(BlogModuleModel input, string[] forwebsite, string[] blogtags)
        {
            bool status = false;


            if (ModelState.IsValid)
            {
                input.blogwebsite = forwebsite != null ? StringArrayToString(forwebsite) : "Brick Kiln";
                input.blogtags = blogtags != null ? StringArrayToString(blogtags) : null;
                var blogkeywordssanitized = SanitizeInput(input.blogkeywords);
                var blogfromdb = _mainobj.GetByBlogNameKeyword(blogkeywordssanitized);
                if (input.blogid > 0)
                {

                    if (blogfromdb != null && blogfromdb.blogid != input.blogid)
                    {
                        return new JsonResult { Data = new { status = status, message = "Error! Url already existed." } };
                    }

                    var olddata = _mainobj.GetById(input.blogid);
                    olddata.blogdescription = input.blogdescription;
                    olddata.blogdate = input.blogdate;
                    olddata.blogkeywords = blogkeywordssanitized;
                    olddata.blogtags = input.blogtags;
                    olddata.blogtitle = input.blogtitle;
                    olddata.blogupdate = DateTime.Now;
                    olddata.blogwebsite = input.blogwebsite;
                    olddata.blogpublished = input.blogpublished;

                    //foreach (var item in input.blogtags.Split(',')) 
                    //{
                    //    olddata.tags
                    //}

                    var result = _mainobj.Update(input.blogid, olddata);
                }
                else
                {

                    input.blogkeywords = blogkeywordssanitized;
                    if (blogfromdb == null)
                    {
                        input.blogupdate = DateTime.Now;
                        input.blogpublished = true;

                        var result = _mainobj.Create(input);
                    }
                    else
                    {
                        return new JsonResult { Data = new { status = status, message = "Error! Url already existed." } };
                    }
                }
                status = true;
            }


            return new JsonResult { Data = new { status = status } };
        }

       

        [HttpGet]
        public ActionResult SaveImage(int id)
        {
            
            if (id < 0) return HttpNotFound();
            var result = _mainobj.GetById(id);

            //if (result != null)
            //{
            //    var singlequestion = new QuizEntity();
            //    singlequestion = JsonConvert.DeserializeObject<QuizEntity>(result.quesionairestr);
            //    singlequestion.qid = result.quesid;
            //    return PartialView("_SaveImage", singlequestion);
            //}

            return PartialView("_SaveImage", result);

        }
        [HttpPost]
        public ActionResult SaveImage(BlogModuleModel input)
        {
            
            bool status = false;
            try
            {
                if (ModelState.IsValid)
                {
                    if (input.blogid > 0)
                    {
                        var model = _mainobj.GetById(input.blogid);
                        if (Request.Files.Count > 0)
                        {
                            try
                            {
                                //  Get all files from Request object  
                                HttpFileCollectionBase files = Request.Files;
                                for (int i = 0; i < files.Count; i++)
                                {
                                    HttpPostedFileBase file = files[i];
                                    string fname;

                                    var checkresult = StaticData.CheckFile(file);
                                    if (checkresult != StaticData.ImageFileStatus.Success)
                                    {
                                        if (checkresult == StaticData.ImageFileStatus.NotFormat)
                                            return new JsonResult { Data = new { status = status, message = "Please add .jpg file only." } };
                                        else if (checkresult == StaticData.ImageFileStatus.NotSize)
                                            return new JsonResult { Data = new { status = status, message = "Select file less than 2mb" } };

                                        return new JsonResult { Data = new { status = status, message = "An error occured." } }; ;
                                    }
                                    fname = StaticData.RandomData();
                                    // Get the complete folder path and store the file inside it.  
                                    string path = Path.Combine(Server.MapPath("~/ReadWrite/"), Path.GetFileName(fname));
                                    path += ".jpg";
                                    file.SaveAs(path);
                                    ResizeImage(path, 550, 350);

                                    model.blogimage = fname;
                                    var result = _mainobj.Update(input.blogid, model);
                                }
                                status = true;

                            }
                            catch { }
                        }

                    }


                }
            }
            catch { }
            return new JsonResult
            {
                Data = new { status = status, message = "File upload failed." }
            };
            //return RedirectToAction("Index");
            
        }


        [HttpPost]
        public ActionResult DeleteImageConfirm(int id)
        {
            try
            {
                bool status = false;
                if (id > 0)
                {

                    var model = _mainobj.GetById(id);
                    FnDeleteImage(model.blogimage);
                    model.blogimage = null;

                    var result = _mainobj.Update(id, model);

                    status = true;
                    //return RedirectToActionPermanent("Index");

                    return new JsonResult { Data = new { status = status } };
                }
                return new JsonResult { Data = new { status = status, message = "File not deleted." } };
            }
            catch
            {
                return new JsonResult { Data = new { status = false, message = "File not deleted." } };

            }

        }

        private void FnDeleteImage(string imagen)
        {
            if (imagen != null)
            {
                string path = Path.Combine(Server.MapPath("~/ReadWrite"),
                                                     Path.GetFileName(imagen));
                path += ".jpg";
                System.IO.File.Delete(path);
            }
        }



        private void ResizeImage(string loc, int width, int height)
        {
            Image image = System.Drawing.Image.FromFile(loc);
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            image.Dispose();
            Image im = (Image)destImage;

            im.Save(loc, ImageFormat.Jpeg);

        }
        private string StringArrayToString(string[] strArray)
        {
            var result = string.Empty;
            for (var i = 0; i < strArray.Length; i++)
            {
                result += strArray[i];
                if (i < strArray.Length - 1)
                    result += ",";
            }
            //foreach (var item in strArray)
            //{
            //    result += item + ", ";
            //}
            return result;
        }

        private string SanitizeInput(string keywords)
        {
            keywords = keywords.ToLower().Trim();
            Regex rgx = new Regex("[^a-zA-Z0-9-]");
            keywords = rgx.Replace(keywords, "-");
            keywords = Regex.Replace(keywords, @"(\W)+", "$1");
            var firstchar = keywords[0];
            if (firstchar == '-')
                keywords = keywords.Remove(0, 1);
            var lastchar = keywords[keywords.Length - 1];
            if (lastchar == '-')
                keywords = keywords.Remove(keywords.Length - 1);
            return keywords;
        }

    }
}
