using R.BAL;
using R.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ussmain.Controllers
{
    //[Authorize]
    public class AlldataController : ApiController
    {
        iBlogModuleService _mainobj;
        iTagsModuleService _tagobj;
        public AlldataController(iBlogModuleService imainobj, iTagsModuleService tagobj)
        {
            this._mainobj = imainobj;
            this._tagobj = tagobj;
        }
        [HttpGet]
        public HttpResponseMessage Blogs()
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

                return Request.CreateResponse(HttpStatusCode.OK, new { data = newresults });
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }
        [HttpGet]

        public HttpResponseMessage tags()
        {
            var results = _tagobj.GetAll();
            if (results != null)
            {
                results = results.OrderBy(o => o.tagname).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { data = results });
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found");
        }

    }
}
