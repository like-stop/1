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

	public partial class SysRoleModel:Virtual_SysRoleModel
	{
		
	}
	public class Virtual_SysRoleModel
	{
		[Display(Name = "GUID主键")]
		public virtual string Id { get; set; }
		[Display(Name = "角色名称")]
		public virtual string Name { get; set; }
		[Display(Name = "说明")]
		public virtual string Description { get; set; }
		[Display(Name = "创建时间")]
		public virtual System.DateTime CreateTime { get; set; }
		[Display(Name = "创建人")]
		public virtual string CreatePerson { get; set; }
		}
}
