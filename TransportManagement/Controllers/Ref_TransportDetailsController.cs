using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TransportManagement.Models;

namespace TransportManagement.Controllers
{
    public class Ref_TransportDetailsController : Controller
    {
        private TransportManagementEntities1 db = new TransportManagementEntities1();

        // GET: XxTransportDetails
        public ActionResult Index()
        {
            var transportDetails = db.TransportDetail.Include(t => t.RouteDetail).Include(t => t.TypeDetail).Include(t => t.DriverDetail);
            return View(transportDetails.ToList());
        }

        // GET: XxTransportDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportDetail transportDetail = db.TransportDetail.Find(id);
            if (transportDetail == null)
            {
                return HttpNotFound(); 
            }
            return View(transportDetail);
        }

        // GET: XxTransportDetails/Create
        public ActionResult Create()
        {
            ViewBag.TransportId = new SelectList(db.RouteDetails, "RouteId", "Origin");
            ViewBag.TransportId = new SelectList(db.TypeDetails, "TypeId", "Name");
            ViewBag.DriverId = new SelectList(db.DriverDetails, "DriverId", "Name");
            return View();
        }

        // POST: XxTransportDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransportId,TypeId,DriverId,RouteId,Date,Passengers")] TransportDetail transportDetail)
        {
            if (ModelState.IsValid)
            {
                db.TransportDetail.Add(transportDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TransportId = new SelectList(db.RouteDetails, "RouteId", "Origin", transportDetail.TransportId);
            ViewBag.TransportId = new SelectList(db.TypeDetails, "TypeId", "Name", transportDetail.TransportId);
            ViewBag.DriverId = new SelectList(db.DriverDetails, "DriverId", "Name", transportDetail.DriverId);
            return View(transportDetail);
        }

        // GET: XxTransportDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportDetail transportDetail = db.TransportDetail.Find(id);
            if (transportDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.TransportId = new SelectList(db.RouteDetails, "RouteId", "Origin", transportDetail.TransportId);
            ViewBag.TransportId = new SelectList(db.TypeDetails, "TypeId", "Name", transportDetail.TransportId);
            ViewBag.DriverId = new SelectList(db.DriverDetails, "DriverId", "Name", transportDetail.DriverId);
            return View(transportDetail);
        }

        // POST: XxTransportDetails/Edit/5
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransportId,TypeId,DriverId,RouteId,Date,Passengers")] TransportDetail transportDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transportDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TransportId = new SelectList(db.RouteDetails, "RouteId", "Origin", transportDetail.TransportId);
            ViewBag.TransportId = new SelectList(db.TypeDetails, "TypeId", "Name", transportDetail.TransportId);
            ViewBag.DriverId = new SelectList(db.DriverDetails, "DriverId", "Name", transportDetail.DriverId);
            return View(transportDetail);
        }

        // GET: XxTransportDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransportDetail transportDetail = db.TransportDetail.Find(id);
            if (transportDetail == null)
            {
                return HttpNotFound();
            }
            return View(transportDetail);
        }

        // POST: XxTransportDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var transportDetail = db.TransportDetail.Find(id);
            db.TransportDetail.Remove(transportDetail);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
