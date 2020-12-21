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
    }
}
