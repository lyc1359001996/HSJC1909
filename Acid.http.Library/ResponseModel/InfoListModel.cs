﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.http.Library.ResponseModel
{
    public class InfoListModel
    {
        public int index { get; set; }
        public string id { get; set; }
        public string createTime { get; set; }
        public string updateTime { get; set; }
        public string updateName { get; set; }
        public string detectionName { get; set; }
        public object deleted { get; set; }
        public string homeAddress { get; set; }
        public long createBy { get; set; }
        public long? updateBy { get; set; }
        public string cardNo { get; set; }
        public string userName { get; set; }
        public string serialNumber { get; set; }
        public string sex { get; set; }
        public string address { get; set; }
        public int testingValue { get; set; }
        public string acidNo { get; set; }

        public string updateText { get; set; }
        public bool Editor_workUnit { get; set; }
        public bool Editor_homeAddress { get; set; }
        /// <summary>
        /// 0未同步 1已同步
        /// </summary>
        public int versions { get; set; }


    }
}
