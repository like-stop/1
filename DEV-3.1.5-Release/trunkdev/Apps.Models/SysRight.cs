//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Apps.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SysRight
    {
        public SysRight()
        {
            this.SysRightOperate = new HashSet<SysRightOperate>();
            this.SysRightDataFilter = new HashSet<SysRightDataFilter>();
        }
    
        public string Id { get; set; }
        public string ModuleId { get; set; }
        public string RoleId { get; set; }
        public bool Rightflag { get; set; }
    
        public virtual SysModule SysModule { get; set; }
        public virtual SysRole SysRole { get; set; }
        public virtual ICollection<SysRightOperate> SysRightOperate { get; set; }
        public virtual ICollection<SysRightDataFilter> SysRightDataFilter { get; set; }
    }
}
