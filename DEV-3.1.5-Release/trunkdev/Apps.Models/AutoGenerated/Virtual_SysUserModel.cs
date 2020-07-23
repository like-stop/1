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

	public partial class SysUserModel:Virtual_SysUserModel
	{
		
	}
	public class Virtual_SysUserModel
	{
		[Display(Name = "GUID主键")]
		public virtual string Id { get; set; }
		[Display(Name = "用户名")]
		public virtual string UserName { get; set; }
		[Display(Name = "密码")]
		public virtual string Password { get; set; }
		[Display(Name = "真是名称")]
		public virtual string TrueName { get; set; }
		[Display(Name = "身份证")]
		public virtual string Card { get; set; }
		[Display(Name = "手机号码")]
		public virtual string MobileNumber { get; set; }
		[Display(Name = "固话")]
		public virtual string PhoneNumber { get; set; }
		[Display(Name = "QQ")]
		public virtual string QQ { get; set; }
		[Display(Name = "Email")]
		public virtual string EmailAddress { get; set; }
		[Display(Name = "其他联系方式")]
		public virtual string OtherContact { get; set; }
		[Display(Name = "省份")]
		public virtual string Province { get; set; }
		[Display(Name = "城市")]
		public virtual string City { get; set; }
		[Display(Name = "城镇街道")]
		public virtual string Village { get; set; }
		[Display(Name = "地址")]
		public virtual string Address { get; set; }
		[Display(Name = "账户状态")]
		public virtual bool State { get; set; }
		[Display(Name = "创建时间")]
		public virtual Nullable<System.DateTime> CreateTime { get; set; }
		[Display(Name = "创建人")]
		public virtual string CreatePerson { get; set; }
		[Display(Name = "性别")]
		public virtual string Sex { get; set; }
		[Display(Name = "生日")]
		public virtual Nullable<System.DateTime> Birthday { get; set; }
		[Display(Name = "加入时间")]
		public virtual Nullable<System.DateTime> JoinDate { get; set; }
		[Display(Name = "婚姻")]
		public virtual string Marital { get; set; }
		[Display(Name = "党派")]
		public virtual string Political { get; set; }
		[Display(Name = "民族")]
		public virtual string Nationality { get; set; }
		[Display(Name = "籍贯")]
		public virtual string Native { get; set; }
		[Display(Name = "毕业学校")]
		public virtual string School { get; set; }
		[Display(Name = "就读专业")]
		public virtual string Professional { get; set; }
		[Display(Name = "学历")]
		public virtual string Degree { get; set; }
		[Display(Name = "部门")]
		public virtual string DepId { get; set; }
		[Display(Name = "职位")]
		public virtual string PosId { get; set; }
		[Display(Name = "个人简介")]
		public virtual string Expertise { get; set; }
		[Display(Name = "在职状况1在职，2离职")]
		public virtual bool JobState { get; set; }
		[Display(Name = "照片")]
		public virtual string Photo { get; set; }
		[Display(Name = "附件")]
		public virtual string Attach { get; set; }
		[Display(Name = "上级领导ID")]
		public virtual string Lead { get; set; }
		[Display(Name = "上级领导")]
		public virtual string LeadName { get; set; }
		[Display(Name = "自选领导")]
		public virtual bool IsSelLead { get; set; }
		[Display(Name = "日程汇报")]
		public virtual bool IsReportCalendar { get; set; }
		[Display(Name = "开启 小秘书")]
		public virtual bool IsSecretary { get; set; }
		}
}
