using System.Linq;
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
            var RouteInfo = db.RouteDetails.Select(x => new RouteModel
            {
                RouteId = x.RouteId,
                Origin = x.Origin,
                Destination = x.Destination,
                Cost = x.Cost,
            });
            return View(RouteInfo.ToList());
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
           
            if (!ModelState.IsValid)
            {
                return View(B);
            }
            RouteDetail A = new RouteDetail();

            A.RouteId = B.RouteId;
            A.Origin = B.Origin;    
            A.Destination = B.Destination;  
            A.Cost = B.Cost;

                db.RouteDetails.Add(A);
                db.SaveChanges();
                return RedirectToAction("Index"); // Home/Index
            
          
        }

    }
}