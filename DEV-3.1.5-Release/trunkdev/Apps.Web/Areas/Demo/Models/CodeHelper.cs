using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apps.Web.Areas.Demo.Models
{
    public class CodeHelper
    {
        //获取前缀
        public static string GetLeftStr(string tableName)
        {
            //生成代码
            string nameSpace = "";
            if (tableName.IndexOf("_") > 0)
            {
                nameSpace = tableName.Substring(0, tableName.IndexOf("_"));
            }
            else
            {
                nameSpace = "Sys";
            }
            return nameSpace;
        }
    }
}