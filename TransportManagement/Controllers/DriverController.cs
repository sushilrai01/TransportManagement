using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using System.Data.Entity;
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
            var DriverInfo = db.DriverDetails.Select(x => new DriverInfoModel
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

            model.DropList = db.RouteDetails.Select(x => new DropDownModel
            {
                ID = x.RouteId,
                Text = x.Origin + " To " + x.Destination

            }).ToList();
            return View(model);
        }

        //POST:  Driver/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DriverInfoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            DriverDetail driverDetail = new DriverDetail ();

            driverDetail.DriverId = model.DriverId;
            driverDetail.Name = model.Name;
            driverDetail.ContactNo = model.ContactNo;
            driverDetail.DateAvailable = model.DateAvailable;
            driverDetail.RouteId = model.RouteId;
            
            db.DriverDetails.Add(driverDetail);
            db.SaveChanges();
            return RedirectToAction("Index", "Home"); // Home/Index
        }

        //GET: Driver/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DriverInfoModel model = new DriverInfoModel();

            var DriverInfo = db.DriverDetails.Where(x => x.DriverId == id).Select(x => new DriverInfoModel
            {
                DriverId = x.DriverId,
                Name = x.Name,  
                DateAvailable = x.DateAvailable,
                ContactNo = x.ContactNo,
                RouteId = x.RouteId==null?0:x.RouteId.Value,
            });

            model = DriverInfo.FirstOrDefault();
            model.DropList = db.RouteDetails.Select(x => new DropDownModel { ID = x.RouteId, Text = x.Origin + " To " + x.Destination}).ToList();

            return View(model);
        }

        //POST: Driver/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DriverInfoModel model)
        {
            DriverDetail driverDetail = new DriverDetail();

            driverDetail.DriverId = model.DriverId;
            driverDetail.Name = model.Name;
            driverDetail.ContactNo = model.ContactNo;
            driverDetail.DateAvailable = model.DateAvailable;
            driverDetail.RouteId = model.RouteId;

            if (ModelState.IsValid)
            {
                db.Entry(driverDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

    }
}