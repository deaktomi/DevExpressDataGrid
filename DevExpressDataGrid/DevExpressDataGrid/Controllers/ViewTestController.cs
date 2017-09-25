using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;
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
            //var model = db.ComplexView;
            return PartialView("_GridViewPartial");
        }

        public ActionResult ExportTo()
        {
            var model = db.ComplexView;
            return GridViewExtension.ExportToXlsx(GetGridSettings(), model.ToList());
        }

        private GridViewSettings GetGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "ViewTest", Action = "GridViewPartial" };

            settings.SettingsEditing.Mode = GridViewEditingMode.EditFormAndDisplayRow;
            settings.SettingsBehavior.ConfirmDelete = true;

            settings.CommandColumn.Visible = true;

            settings.KeyFieldName = "Id";

            settings.SettingsPager.Visible = true;
            settings.Settings.ShowGroupPanel = true;
            settings.Settings.ShowFilterRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;

            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.Off;
            settings.SettingsAdaptivity.AdaptiveColumnPosition = GridViewAdaptiveColumnPosition.Right;
            settings.SettingsAdaptivity.AdaptiveDetailColumnCount = 1;
            settings.SettingsAdaptivity.AllowOnlyOneAdaptiveDetailExpanded = false;
            settings.SettingsAdaptivity.HideDataCellsAtWindowInnerWidth = 0;
            settings.Settings.ShowHeaderFilterButton = true;

            settings.Columns.Add("Id");
            settings.Columns.Add("Name");
            settings.Columns.Add("Date");
            settings.Columns.Add("Company");

            return settings;
        }
    }
}