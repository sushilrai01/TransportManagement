using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
            return View();
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