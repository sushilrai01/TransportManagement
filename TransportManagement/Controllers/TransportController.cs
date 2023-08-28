using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransportManagement.Models;
using TransportManagement.ViewModel;

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
    }
}