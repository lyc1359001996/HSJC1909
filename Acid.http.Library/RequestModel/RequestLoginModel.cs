using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.http.Library.RequestModel
{
   public class RequestLoginModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string type { get; set; }

        public RequestLoginModel(string userName, string passWord)
        {
            this.username = userName;
            this.password = passWord;
            this.type = "A";
        }

        public RequestLoginModel()
        {
            this.type = "A";
        }
    }
}
