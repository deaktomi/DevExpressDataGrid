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
    public class ComplexController : Controller
    {
        // GET: Complex
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = ComplexRepository.GetData();
            ViewBag.GridSettings = GetGridSettings();
            return PartialView("_GridViewPartial", model);
        }

        private GridViewSettings GetGridSettings()
        {
            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "Home", Action = "GridViewPartial" };
            
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
            settings.Columns.Add("Company", "Company");

            //Export
            settings.SettingsExport.FileName = "Report.xlsx";
            settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;
            settings.SettingsExport.ExportSelectedRowsOnly = false;

            return settings;
        }
    }
}