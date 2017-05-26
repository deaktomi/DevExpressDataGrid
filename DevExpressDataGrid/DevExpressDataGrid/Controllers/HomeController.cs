using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using DevExpress.Export;
using DevExpress.Web;
using DevExpress.XtraPrinting;

namespace DevExpressDataGrid.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Model"] == null)
            {
                Session["Model"] = db.SimpleData.ToList();
            }
            return View(Session["Model"]);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        DevExpressDataGrid.Models.Entities db = new DevExpressDataGrid.Models.Entities();

        // Handles GridView callbacks. 
        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db.SimpleData;
            ViewBag.GridSettings = GetGridSettings();
            return PartialView("_GridViewPartial", Session["Model"]);
        }

        // This action method sends a PDF document with the exported Grid to response. 
        public ActionResult ExportTo()
        {
            var model = Session["Model"];
            return GridViewExtension.ExportToXlsx(GetGridSettings(), model);
        }

        private GridViewSettings GetGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };

            settings.SettingsEditing.AddNewRowRouteValues = new { Controller = "Home", Action = "GridViewPartialAddNew" };
            settings.SettingsEditing.UpdateRowRouteValues = new { Controller = "Home", Action = "GridViewPartialUpdate" };
            settings.SettingsEditing.DeleteRowRouteValues = new { Controller = "Home", Action = "GridViewPartialDelete" };
            settings.SettingsEditing.Mode = GridViewEditingMode.EditFormAndDisplayRow;
            settings.SettingsBehavior.ConfirmDelete = true;

            settings.CommandColumn.Visible = true;
            settings.CommandColumn.ShowNewButton = true;
            settings.CommandColumn.ShowDeleteButton = true;
            settings.CommandColumn.ShowEditButton = true;
            
            settings.KeyFieldName = "Id";

            settings.SettingsPager.Visible = true;
            settings.Settings.ShowGroupPanel = true;
            settings.Settings.ShowFilterRow = true;
            settings.SettingsBehavior.AllowSelectByRowClick = true;

            settings.Settings.ShowHeaderFilterButton = true;
            settings.TotalSummary.Add(DevExpress.Data.SummaryItemType.Count, "Name");
            settings.Settings.ShowFooter = true;


            settings.SettingsAdaptivity.AdaptivityMode = GridViewAdaptivityMode.Off;
            settings.SettingsAdaptivity.AdaptiveColumnPosition = GridViewAdaptiveColumnPosition.Right;
            settings.SettingsAdaptivity.AdaptiveDetailColumnCount = 1;
            settings.SettingsAdaptivity.AllowOnlyOneAdaptiveDetailExpanded = false;
            settings.SettingsAdaptivity.HideDataCellsAtWindowInnerWidth = 0;

            settings.Columns.Add("Name", "Name");
            settings.Columns.Add("Title", "Title");
            settings.Columns.Add("Date", "Date of birth", MVCxGridViewColumnType.DateEdit);
            settings.Columns.Add("Active", "Active", MVCxGridViewColumnType.CheckBox);

            //Export
            settings.SettingsExport.FileName = "Report.xlsx";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.ExportSelectedRowsOnly = false;

            return settings;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] DevExpressDataGrid.Models.SimpleData item)
        {
            var model = db.SimpleData;
            ViewBag.GridSettings = GetGridSettings();
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] DevExpressDataGrid.Models.SimpleData item)
        {
            ViewBag.GridSettings = GetGridSettings();
            var model = db.SimpleData;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.Id == item.Id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int32 Id)
        {
            ViewBag.GridSettings = GetGridSettings();
            var model = db.SimpleData;
            if (Id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.Id == Id);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", model.ToList());
        }
    }
}