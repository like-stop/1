using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Apps.Common
{
    /// <summary>
    /// 分页实体
    /// </summary>
    [DataContract]
    public class BasePager
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        [DataMember]
        public int rows { get; set; }
        /// <summary>
        /// 当前页是第几页
        /// </summary>
        [DataMember]
        public int page { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        [DataMember]
        public string order { get; set; }
        /// <summary>
        /// 排序列
        /// </summary>
        [DataMember]
        public string sort { get; set; }

        /// <summary>
        /// 查询条件
        /// </summary>
        [DataMember]
        public string queryStr { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [DataMember]
        public DateTime beginDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [DataMember]
        public string endDate { get; set; }
    }
}

