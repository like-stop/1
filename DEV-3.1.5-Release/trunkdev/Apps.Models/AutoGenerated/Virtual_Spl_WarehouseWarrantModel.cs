//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

using Apps.Models;
using System;
using System.ComponentModel.DataAnnotations;
namespace Apps.Models.Spl
{

	public partial class Spl_WarehouseWarrantModel:Virtual_Spl_WarehouseWarrantModel
	{
		
	}
	public class Virtual_Spl_WarehouseWarrantModel
	{
		[Display(Name = "单号")]
		public virtual string Id { get; set; }
		[Display(Name = "入库时间")]
		public virtual System.DateTime InTime { get; set; }
		[Display(Name = "经手人")]
		public virtual string Handler { get; set; }
		[Display(Name = "未设置")]
		public virtual string Remark { get; set; }
		[Display(Name = "未设置")]
		public virtual decimal PriceTotal { get; set; }
		[Display(Name = "1 正常 2 审核中 3 审核完成 0作废")]
		public virtual int State { get; set; }
		[Display(Name = "未设置")]
		public virtual string Checker { get; set; }
		[Display(Name = "未设置")]
		public virtual Nullable<System.DateTime> CheckTime { get; set; }
		[Display(Name = "创建时间")]
		public virtual System.DateTime CreateTime { get; set; }
		[Display(Name = "制单人")]
		public virtual string CreatePerson { get; set; }
		[Display(Name = "未设置")]
		public virtual Nullable<System.DateTime> ModifyTime { get; set; }
		[Display(Name = "未设置")]
		public virtual string ModifyPerson { get; set; }
		[Display(Name = "单据确认")]
		public virtual bool Confirmation { get; set; }
		[Display(Name = "未设置")]
		public virtual string InOutCategoryId { get; set; }
		[Display(Name = "未设置")]
		public virtual string WarehouseId { get; set; }
		}
}
