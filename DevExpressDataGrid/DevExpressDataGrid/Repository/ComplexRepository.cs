using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DevExpressDataGrid.Models;

namespace DevExpressDataGrid.Repository
{
    public class ComplexRepository
    {
        public static List<ComplexModel> GetData()
        {
            using (var db = new Entities())
            {
                var list = new List<ComplexModel>();
                var data = db.SimpleData.Include(nameof(ExtraData));
                foreach (var row in data)
                {
                    var elem = new ComplexModel();
                    elem.Id = row.Id;
                    elem.Name = row.Name;
                    elem.Title = row.Title;
                    elem.Active = row.Active;
                    elem.Date = row.Date;
                    elem.ExtraDataId = row.ExtraDataId;
                    elem.Company = row.ExtraData?.Company;
                    list.Add(elem);
                }
                return list;
            }
        }

        public static void Delete(int id)
        {
            using (var db = new Entities())
            {
                var elem = db.SimpleData.FirstOrDefault(sd => sd.Id == id);
                if (elem != null)
                {
                    db.SimpleData.Remove(elem);
                    db.SaveChanges();
                }
            }
        }
    }
}