using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using TransportManagement.Models;
using TransportManagement.ViewModel;

namespace TransportManagement.Controllers
{
    public class DriverController : Controller
    {
        private TransportManagementEntities1 db = new TransportManagementEntities1();
        // GET: Driver
        public ActionResult Index()
        {
            //RouteDetail routeDetail = new RouteDetail();
            var DriverInfo = db.DriverDetails.Select(x => new DriverModel
            {
                Name = x.Name,
                DriverId = x.DriverId,
                ContactNo = x.ContactNo,
                DateAvailable = x.DateAvailable,
                RouteId = (int)x.RouteId,
            });
            return View(DriverInfo.ToList());
        }

        //GET: Driver/History/Id
        public ActionResult History(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var DriverInfo = from driverDetail in db.DriverDetails
                             join transportDetail in db.TransportDetail on driverDetail.DriverId equals transportDetail.DriverId
                             join routeDetail in db.RouteDetails on transportDetail.RouteId equals routeDetail.RouteId
                             join typeDetail in db.TypeDetails on transportDetail.TypeId equals typeDetail.TypeId
                             where driverDetail.DriverId == id
                             select new TransportRouteModel
                             {
                                 DriverID = driverDetail.DriverId,
                                 DriverName = driverDetail.Name,
                                 VehicleName = typeDetail.Name,
                                 Date = transportDetail.Date,
                                 Passengers = transportDetail.Passengers,
                                 Origin = routeDetail.Origin,
                                 Destination = routeDetail.Destination,
                             };

            return View(DriverInfo.ToList());  
        }

        //GET: Driver/Create
        public ActionResult Create()
        {
            DriverInfoModel model = new DriverInfoModel();

           var list= db.RouteDetails.Select(x => new DropDownModel
            {
                ID = x.RouteId,
                Text = x.Origin + " To " + x.Destination

            });

            model.DropList = list.ToList();
            return View(model);
        }

        //POST:  Driver/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DriverInfoModel model)
        {
            DriverDetail driverDetail = new DriverDetail ();

            driverDetail.DriverId = model.driverModel.DriverId;
            driverDetail.Name = model.driverModel.Name;
            driverDetail.ContactNo = model.driverModel.ContactNo;
            driverDetail.DateAvailable = model.driverModel.DateAvailable;
            driverDetail.RouteId = model.driverModel.RouteId;

            if (ModelState.IsValid)
            {
                db.DriverDetails.Add(driverDetail);
                db.SaveChanges();
                return RedirectToAction("Index", "Home"); // Home/Index
            }
            return View(model);
        }
    }
}