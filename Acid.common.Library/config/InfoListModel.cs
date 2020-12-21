using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.common.Library.config
{
    public class InfoListModel
    {
        public int index { get; set; }
        public string id { get; set; }
        public string createTime { get; set; }
        public string updateTime { get; set; }
        public string updateName { get; set; }
        public string detectionName { get; set; }
        public string deleted { get; set; }
        public string homeAddress { get; set; }
        public string company { get; set; }
        public string createBy { get; set; }
        public string updateBy { get; set; }
        public string cardNo { get; set; }
        public string userName { get; set; }
        public string serialNumber { get; set; }
        public string sex { get; set; }
        public string address { get; set; }
        public int? testingValue { get; set; }
        public string acidNo { get; set; }
        public string jcdName { get; set; }
        public string updateText { get; set; }

        public bool iscancel { get; set; }
        public bool Editor_company { get; set; }
        public bool Editor_homeAddress { get; set; }
        /// <summary>
        /// 0未同步 1已同步
        /// </summary>
        public int versions { get; set; }


    }
}
