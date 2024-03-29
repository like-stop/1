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
    
    public partial class Spl_WarehouseWarrant
    {
        public Spl_WarehouseWarrant()
        {
            this.Spl_WarehouseWarrantDetails = new HashSet<Spl_WarehouseWarrantDetails>();
        }
    
        public string Id { get; set; }
        public System.DateTime InTime { get; set; }
        public string Handler { get; set; }
        public string Remark { get; set; }
        public decimal PriceTotal { get; set; }
        public int State { get; set; }
        public string Checker { get; set; }
        public Nullable<System.DateTime> CheckTime { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string CreatePerson { get; set; }
        public Nullable<System.DateTime> ModifyTime { get; set; }
        public string ModifyPerson { get; set; }
        public bool Confirmation { get; set; }
        public string InOutCategoryId { get; set; }
        public string WarehouseId { get; set; }
    
        public virtual Spl_InOutCategory Spl_InOutCategory { get; set; }
        public virtual Spl_Warehouse Spl_Warehouse { get; set; }
        public virtual ICollection<Spl_WarehouseWarrantDetails> Spl_WarehouseWarrantDetails { get; set; }
    }
}
