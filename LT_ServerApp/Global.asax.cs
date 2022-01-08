using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System;
using System.Web.Caching;
using System.Diagnostics;

namespace LT_ServerApp
{
    public class MvcApplication : HttpApplication
    {
        //private const string DummyCacheItemKey = "GagaGuguGigi";
        protected void Application_Start()
        {
            //RegisterCacheEntry();
            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            
        }

        //private bool RegisterCacheEntry()
        //{
        //    try
        //    {
        //        if (null != HttpContext.Current.Cache[DummyCacheItemKey]) return false;

        //        HttpContext.Current.Cache.Add(DummyCacheItemKey, "Test", null,
        //            DateTime.MaxValue, TimeSpan.FromMinutes(1),
        //            CacheItemPriority.Normal,
        //            new CacheItemRemovedCallback(this.CacheItemRemovedCallback));

        //        return true;
        //    }
        //    catch (Exception s)
        //    {
        //        Debug.WriteLine("Exception IN Main =" + s.Message.ToString());
        //        return false;
        //    }
        //}

        //public void CacheItemRemovedCallback(string key,
        //    object value, CacheItemRemovedReason reason)
        //{
        //    try
        //    {
        //        Debug.WriteLine("Cache item callback: " + DateTime.Now.ToString());

        //        // Do the service works

        //        DoWork();
        //    }
        //    catch(Exception e)
        //    {
        //        Debug.WriteLine("Exception=" + e.Message.ToString());
        //    }
        //}
        //private const string DummyPageUrl = "http://localhost:17685/";
        //private void DoWork()
        //{
        //    System.Net.WebClient client = new System.Net.WebClient();
        //    client.DownloadData(DummyPageUrl);
        //}
    }
}
