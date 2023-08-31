using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportManagement.Models;
using TransportManagement.ViewModel;

namespace TransportManagement.Controllers
{
    public class HomeController : Controller
    {
        private TransportManagementEntities1 db = new TransportManagementEntities1();
        // GET: Home
        public ActionResult Index()
        {

            return View();
        }

        //GET: Home/Report
        public ActionResult Report()
        {
            var reportData = (from transportDeteail in db.TransportDetail
                             join driverDetail in db.DriverDetails on transportDeteail.DriverId equals driverDetail.DriverId
                             join typeDetail in db.TypeDetails on transportDeteail.TypeId equals typeDetail.TypeId
                             join routeDetail in db.RouteDetails on transportDeteail.RouteId equals routeDetail.RouteId
                             select new TransportRouteModel
                             {
                                 TransportId = transportDeteail.TransportId,

                             }).ToList();
            return View();
        }
    }
}