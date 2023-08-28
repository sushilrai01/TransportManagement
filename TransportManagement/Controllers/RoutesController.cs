using System.Web.Mvc;
using TransportManagement.Models;
using TransportManagement.ViewModel;

namespace TransportManagement.Controllers
{
    public class RoutesController : Controller
    {
        private TransportManagementEntities1 db = new TransportManagementEntities1();
        // GET: Routes
        public ActionResult Index()
        {
            return View();
        }

        //GET: Routes/Create
        public ActionResult Create()
        {
            return View();
        }

        //POST:  Routes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( RouteModel B)
        {
            RouteDetail A = new RouteDetail();

            A.RouteId = B.RouteId;
            A.Origin = B.Origin;    
            A.Destination = B.Destination;  
            A.Cost = B.Cost;
            //B.RouteId = A.RouteId;
            //B.Origin = A.Origin;
            //B.Destination = A.Destination;
            //B.Cost = A.Cost;
            if (ModelState.IsValid)
            {
                db.RouteDetails.Add(A);
                db.SaveChanges();
                return RedirectToAction("Index", "Home"); // Home/Index
            }
            return View(B);
        }

    }
}