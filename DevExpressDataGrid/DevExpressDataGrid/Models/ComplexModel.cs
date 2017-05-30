using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DevExpressDataGrid.Models
{
    public class ComplexModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime Date { get; set; }
        public string Title { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> ExtraDataId { get; set; }
        public string Company { get; set; }
    }
}