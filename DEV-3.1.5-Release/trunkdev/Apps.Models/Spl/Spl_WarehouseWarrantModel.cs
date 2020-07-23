using System;
using System.ComponentModel.DataAnnotations;
using Apps.Models;
namespace Apps.Models.Spl
{
    public partial class Spl_WarehouseWarrantModel
    {
        [Display(Name = "单号")]
        public override string Id { get; set; }

        [Display(Name = "入库时间")]
        public override DateTime InTime { get; set; }

        [Display(Name = "经手人")]
        public override string Handler { get; set; }
         [Display(Name = "经手人")]
        public string HandlerName { get; set; }
        [Display(Name = "描述")]
        public override string Remark { get; set; }

        [Display(Name = "总价")]
        public override decimal PriceTotal { get; set; }
        //1 正常 2 审核中 3 审核完成 0作废
        [Display(Name = "状态")]
        public override int State { get; set; }

        [Display(Name = "审核人")]
        [NotNullExpression]
        public override string Checker { get; set; }
        public string CheckerName { get; set; }

        [Display(Name = "审核时间")]
        public override DateTime? CheckTime { get; set; }

        [Display(Name = "创建时间")]
        public override DateTime CreateTime { get; set; }

        [Display(Name = "制单人")]
        public override string CreatePerson { get; set; }

        [Display(Name = "修改时间")]
        public override DateTime? ModifyTime { get; set; }

        [Display(Name = "修改人")]
        public override string ModifyPerson { get; set; }

        [Display(Name = "单据确认")]
        public override bool Confirmation { get; set; }

        [Display(Name = "入库类别")]
        public override string InOutCategoryId { get; set; }

        [Display(Name = "所属仓库")]
        public override string WarehouseId { get; set; }

        [Display(Name = "所属仓库")]
        public string WarehouseName { get; set; }
        [Display(Name = "入库类别")]
        public string InOutCategoryName { get; set; }
    }
}
