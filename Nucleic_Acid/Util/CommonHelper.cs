using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleic_Acid.Util
{
   public class CommonHelper
    {
        public static string ToSex(int sex)
        {
            switch (sex)
            {
                case 0:return "女"; 
                case 1: return "男"; 
                default:return "男";
            }
        }

        public static string SexToInt(string sex)
        {
            switch (sex)
            {
                case "女": return "0";
                case "男": return "1";
                default: return "1";
            }
        }
    }
}
