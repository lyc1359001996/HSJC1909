using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.http.Library.ResponseModel
{
   public class ResultJson<T>
    {
        public string code { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
