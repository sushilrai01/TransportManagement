using System.Web.Mvc;
using TransportManagement.Models;
using TransportManagement.ViewModel;

namespace TransportManagement.Controllers
{
    public class TransportTypeController : Controller
    {
        private TransportManagementEntities1 db = new TransportManagementEntities1();
        // GET: Routes
        public ActionResult Index()
        {
            return View();
        }

        //GET: Type/Create
        public ActionResult Create()
        {
            TypeModel typeModel = new TypeModel();
            typeModel.Name = "HEROHONDA";
            return View(typeModel);
        }

        //POST:  Type/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TypeModel typeModel)
        {
            TypeDetail typeDetail = new TypeDetail();

            typeDetail.TypeId = typeModel.TypeId;
            typeDetail.Name = typeModel.Name;

            if (ModelState.IsValid)
            {
                db.TypeDetails.Add(typeDetail);
                db.SaveChanges();
                return RedirectToAction("Create"); // Home/Index
            }
            return View(typeModel);
        }
    }
}