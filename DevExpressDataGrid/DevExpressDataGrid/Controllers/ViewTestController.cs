using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpressDataGrid.Repository;

namespace DevExpressDataGrid.Controllers
{
    public class ViewTestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        DevExpressDataGrid.Models.Entities db = new DevExpressDataGrid.Models.Entities();

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db.ComplexView;
            return PartialView("_GridViewPartial");
        }
    }
}