using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.http.Library.ResponseModel
{
    public class LoginModel
    {
        public string role { get; set; }
        public string name { get; set; }
        public string token { get; set; }
        public string jcdName { get; set; }
        public string xzjdName { get; set; }
        public string cydName { get; set; }
        public string principal { get; set; }
        public string districtName { get; set; }
    }
}
