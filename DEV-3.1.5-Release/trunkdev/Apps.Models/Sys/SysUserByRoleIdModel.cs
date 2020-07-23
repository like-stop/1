using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Models.Sys
{
    public class SysUserByRoleIdModel
    {
        public string Id { get; set; }
        public string TrueName { get; set; }
        public string UserName { get; set; }

        public int flag { get; set; }
    }
}
