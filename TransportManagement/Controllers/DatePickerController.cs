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
    public class DatePickerController : Controller
    {
        // GET: DatePicker
        public ActionResult Index()
        {
            return View();
        }
    }
}