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
namespace Apps.Models.MIS
{

	public partial class MIS_Article_AlbumsModel:Virtual_MIS_Article_AlbumsModel
	{
		
	}
	public class Virtual_MIS_Article_AlbumsModel
	{
		[Display(Name = "未设置")]
		public virtual string Id { get; set; }
		[Display(Name = "未设置")]
		public virtual string ArticleId { get; set; }
		[Display(Name = "未设置")]
		public virtual string BigImg { get; set; }
		[Display(Name = "未设置")]
		public virtual string SmallImg { get; set; }
		[Display(Name = "未设置")]
		public virtual string Remark { get; set; }
		}
}
