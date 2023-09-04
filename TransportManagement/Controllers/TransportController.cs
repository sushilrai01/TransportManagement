using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using TransportManagement.Models;
using TransportManagement.ViewModel;
using System.Net;
using System.Data.Entity;

namespace TransportManagement.Controllers
{
    public class TransportController : Controller
    {
        private TransportManagementEntities1 db = new TransportManagementEntities1();
        // GET: Transport
        public ActionResult Index()
        {

            return View();
        }

        //GET: Transport/Create
        public ActionResult Create()
        {
            TransportRouteModel model = new TransportRouteModel();

            model.TypeList = db.TypeDetails.Select(x => new DropDownModel
            {
                ID = x.TypeId,
                Text = x.Name
            }).ToList();

            model.RouteList = db.RouteDetails.Select(x => new DropDownModel
            {
                ID = x.RouteId,
                Text = x.Origin + " To " + x.Destination
            }).ToList();

            model.DriverList = db.DriverDetails.Select(x => new DropDownModel { ID = x.DriverId, Text = x.Name }).ToList();

            return View(model);
        }

        //POST: Transport/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TransportRouteModel model)
        {
            TransportDetail transportDetail = new TransportDetail();

            transportDetail.TransportId = model.TransportId;
            transportDetail.TypeId = model.TypeID;
            transportDetail.DriverId = model.DriverID;
            transportDetail.RouteId = model.RouteID;
            transportDetail.Date = (DateTime)model.Date;
            transportDetail.Passengers = model.Passengers;

            if (ModelState.IsValid)
            {
                db.TransportDetail.Add(transportDetail);
                db.SaveChanges();
                return RedirectToAction("Report","Home");
            }
            return View(model);
        }

        //GET: Transport/Edit/id
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TransportRouteModel model = new TransportRouteModel();

            var TransportInfo = db.TransportDetail.Where(x => x.TransportId == id).Select(x => new TransportRouteModel
            {
                TransportId = x.TransportId,
                TypeID = (int)x.TypeId,
                DriverID = (int)x.DriverId,
                RouteID = (int)x.RouteId,
                Date = x.Date,
                Passengers = x.Passengers,
            });

            model = TransportInfo.FirstOrDefault();
            model.TypeList = db.TypeDetails.Select(x => new DropDownModel { ID = x.TypeId, Text = x.Name }).ToList();
            model.RouteList = db.RouteDetails.Select(x => new DropDownModel { ID = x.RouteId, Text = x.Origin + " To " + x.Destination }).ToList();
            model.DriverList = db.DriverDetails.Select(x => new DropDownModel { ID = x.DriverId, Text = x.Name }).ToList(); 
            return View(model);
        }

        //POST: Transport/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TransportRouteModel model)
        {
            TransportDetail transportDetail = new TransportDetail();

            transportDetail.TransportId = model.TransportId;
            transportDetail.TypeId = model.TypeID;
            transportDetail.DriverId = model.DriverID;
            transportDetail.RouteId = model.RouteID;
            transportDetail.Date = (DateTime)model.Date;
            transportDetail.Passengers = model.Passengers;


            if (ModelState.IsValid)
            {
                db.Entry(transportDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Report","Home");
            }
            return View(model); 
        }

        //GET: Transport/Delete/id
        public ActionResult Delete(int? id)
        {
            if(id== null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); 
            }
            var InfoTransport = from transportDetail in db.TransportDetail
                                join driverDetail in db.DriverDetails on transportDetail.DriverId equals driverDetail.DriverId
                                join typeDetail in db.TypeDetails on transportDetail.TypeId equals typeDetail.TypeId
                                join routeDetail in db.RouteDetails on transportDetail.RouteId equals routeDetail.RouteId
                                where transportDetail.TransportId == id 
                                select new TransportRouteModel
                                {
                                    TransportId = transportDetail.TransportId,
                                    Passengers = transportDetail.Passengers,
                                    Date = transportDetail.Date,
                                    VehicleName = typeDetail.Name,
                                    DriverName = driverDetail.Name,
                                    Origin = routeDetail.Origin,
                                    Destination = routeDetail.Destination,
                                    Cost = routeDetail.Cost,
                                };
            return View(InfoTransport.FirstOrDefault()) ;
        }
        //POST: Transport/Delete/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            TransportDetail transportDetail = db.TransportDetail.Find(id);
            db.TransportDetail.Remove(transportDetail); 
            db.SaveChanges();
            return RedirectToAction("Report", "Home");
        }

    }
}