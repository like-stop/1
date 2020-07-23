using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apps.Models.Sys
{
    public partial class AccountModel
    {
        public string Id { get; set; }
        public string TrueName { get; set; }
        public string Photo { get; set; }
        public string UserName { get; set; }
        public string DepId { get; set; }
        public string ZBDepId { get; set; }
        public string DepName { get; set; }

        public string PosName { get; set; }
    }
}
