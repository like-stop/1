using Apps.Models;
using Apps.Models.Spl;
using Apps.IDAL.Spl;
using System;
using System.Linq;

namespace Apps.DAL.Spl
{
	public partial class Spl_WareDetailsRepository
	{
        public int GetWareCountByCategoryId(string categoryId)
        {
            return Context.P_Spl_GetWareCountByCategoryId(categoryId).Cast<int>().First();
        }
    }
}
