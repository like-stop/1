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
namespace Apps.Models.Sys
{

	public partial class SysAreasModel:Virtual_SysAreasModel
	{
		
	}
	public class Virtual_SysAreasModel
	{
		[Display(Name = "未设置")]
		public virtual string Id { get; set; }
		[Display(Name = "未设置")]
		public virtual string Name { get; set; }
		[Display(Name = "未设置")]
		public virtual string ParentId { get; set; }
		[Display(Name = "未设置")]
		public virtual int Sort { get; set; }
		[Display(Name = "未设置")]
		public virtual bool Enable { get; set; }
		[Display(Name = "未设置")]
		public virtual System.DateTime CreateTime { get; set; }
		[Display(Name = "直辖市")]
		public virtual bool IsMunicipality { get; set; }
		[Display(Name = "港澳台")]
		public virtual bool IsHKMT { get; set; }
		[Display(Name = "其他")]
		public virtual bool IsOther { get; set; }
		}
}
