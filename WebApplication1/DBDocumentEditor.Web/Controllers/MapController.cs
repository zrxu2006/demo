using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBDocumentEditor.Web.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 高德地图 订单
        /// </summary>
        /// <returns></returns>
        [Route("map/amap/order")]
        public ActionResult OrderAmap(string orderId)
        {
            return View();
        }
    }
}