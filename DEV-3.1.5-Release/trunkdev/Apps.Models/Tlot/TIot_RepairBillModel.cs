using System;
using System.ComponentModel.DataAnnotations;
using Apps.Models;
namespace Apps.Models.TIot
{
    public partial class TIot_RepairBillModel
    {
        public string CreatorName { get; set; }
        public SysUser SysUserM { get; set; }
     }
}

