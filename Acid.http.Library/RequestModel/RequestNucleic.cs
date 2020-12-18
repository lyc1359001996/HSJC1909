using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.http.Library.RequestModel
{
   public class RequestNucleic
    {
        public long id;
        public string cardNo { get; set; }  //身份证号
        public string userName { get; set; }   //身份证姓名
        public long serialNumber { get; set; }    //编号
        public int sex { get; set; }  //性别
        public string address { get; set; }  //省份证地址
        public string createTime { get; set; }
    }
}
