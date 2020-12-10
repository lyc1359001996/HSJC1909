using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.http.Library.RequestModel
{
   public class RequestInfoListModel
    {
        public int pageNo { get; set; }
        public int pageSize { get; set; }
        public string name { get; set; }
        public string cardNo { get; set; }
        public string testValue { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        

        public RequestInfoListModel(int pageNo, int pageSize, string name, string cardNo, string testValue, string startTime, string endTime)
        {
            this.pageNo = pageNo;
            this.pageSize = pageSize;
            this.name = name;
            this.cardNo = cardNo;
            this.testValue = testValue;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public RequestInfoListModel()
        {
        }

        public RequestInfoListModel(int pageNo, int pageSize)
        {
            this.pageNo = pageNo;
            this.pageSize = pageSize;
        }
    }
}
