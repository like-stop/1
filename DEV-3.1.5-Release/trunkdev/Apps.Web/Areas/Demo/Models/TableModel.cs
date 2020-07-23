using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps.Web.Areas.Demo.Models
{
    public class TableModel
    {
        public string name { get; set; }
    }

    public class TypeModel
    {
        public string controller { get; set; }
        public string index { get; set; }
        public string create { get; set; }
        public string edit { get; set; }

        public string bll { get; set; }

        public string model { get; set; }
    }
}