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
            transportDetail.Date = model.Date;
            transportDetail.Passengers = model.Passengers;

            if (ModelState.IsValid)
            {
                db.TransportDetail.Add(transportDetail);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
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

            //var typeList = db.TypeDetails.Select(x => new DropDownModel { ID = x.TypeId, Text = x.Name });
            //var routeList = db.RouteDetails.Select(x => new DropDownModel { ID = x.RouteId, Text = x.Origin + " To " + x.Destination });
            //var driverList = db.DriverDetails.Select(x => new DropDownModel { ID = x.DriverId, Text = x.Name });

            var TransportInfo = db.TransportDetail.Where(x => x.TransportId == id).Select(x => new TransportRouteModel
            {
                TransportId = x.TransportId,
                TypeID = (int)x.TypeId,
                DriverID = (int)x.DriverId,
                RouteID = (int)x.RouteId,
                Date = x.Date,
                Passengers = x.Passengers,
                //TypeList = typeList.ToList(),
                //RouteList = routeList.ToList(),
                //DriverList = driverList.ToList(),
            });

            model = TransportInfo.FirstOrDefault();

            //TxModel model = new TxModel();
            //var transportInfo = db.TransportDetail.Where(x => x.TransportId == id).Select(x => new TransportModel{
            //    TransportId = x.TransportId,
            //    TypeID = (int)x.TypeId,
            //    DriverID = (int)x.DriverId,
            //    RouteID = (int)x.RouteId,
            //    Date = x.Date,  
            //    Passengers = x.Passengers,
            //});
            //model.MODEL = transportInfo.FirstOrDefault();

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

            //transportDetail.TransportId = model.MODEL.TransportId;
            //transportDetail.TypeId = model.MODEL.TypeID;
            //transportDetail.DriverId = model.MODEL.DriverID;
            //transportDetail.RouteId = model.MODEL.RouteID;
            //transportDetail.Date = model.MODEL.Date;
            //transportDetail.Passengers = model.MODEL.Passengers;

            transportDetail.TransportId = model.TransportId;
            transportDetail.TypeId = model.TypeID;
            transportDetail.DriverId = model.DriverID;
            transportDetail.RouteId = model.RouteID;
            transportDetail.Date = model.Date;
            transportDetail.Passengers = model.Passengers;


            if (ModelState.IsValid)
            {
                db.Entry(transportDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Report","Home");
            }
            return View(model); 
        }
    }
}