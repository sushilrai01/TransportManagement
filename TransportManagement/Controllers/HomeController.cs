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
            var reportData = (from transportDetail in db.TransportDetail
                             join driverDetail in db.DriverDetails on transportDetail.DriverId equals driverDetail.DriverId
                             join typeDetail in db.TypeDetails on transportDetail.TypeId equals typeDetail.TypeId
                             join routeDetail in db.RouteDetails on transportDetail.RouteId equals routeDetail.RouteId
                             select new TransportRouteModel
                             {
                                 TransportId =  transportDetail.TransportId,
                                 VehicleName = typeDetail.Name,
                                 DriverName = driverDetail.Name,
                                 Origin = routeDetail.Origin,
                                 Destination = routeDetail.Destination, 
                                 Cost = routeDetail.Cost,   
                                 Date = transportDetail.Date,
                                 Passengers = transportDetail.Passengers, 
                             }).ToList();
            return View(reportData);
        }
    }
}