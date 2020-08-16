using MissionManager.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using System.Web.Mvc;

namespace MissionManager.Controllers
{
    public class MainController : Controller
    {
        ObjectCache cache = MemoryCache.Default;
        
        // GET: Main
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {

            List<Mission> mission = new List<Mission>() ;
            if (cache.Contains("Missions"))
            {

                mission = (List<Mission>)cache.Get("Missions");
            }
            else
            {
                CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(24);
                cache.Add("Missions", mission, cacheItemPolicy);
            }

            return Json(mission, JsonRequestBehavior.AllowGet);
        }

        public string InsertMission(Mission m)
        {

            if (m != null)
            {
                
                List<Mission> mission;
                List<Mission> fromCache;
              
                if (Session["mission"] != null)
                    mission = (List<Mission>)Session["mission"];
                else
                {
                    mission = new List<Mission>();
                }
                mission.Add(m);
                if (cache.Contains("Missions"))
                {
                    fromCache = (List<Mission>)cache.Get("Missions");
                    fromCache.Add(m);
                    cache["Missions"] = fromCache;
                }

                else
                {
                    // Store data in the cache    
                    CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
                    cacheItemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(24);
                    cache.Add("Missions", mission, cacheItemPolicy);

                }

                Session["mission"] = mission;
                //HttpContext.Cache["MissionList"] = mission;
                return "Mission Added Successfully";

            }
            else
            {
                return "Mission Not Inserted! Try Again";
            }
        }


    }
}