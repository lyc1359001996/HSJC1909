using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.SDK.Library
{
    public class DataModel
    {
        public int gIndex { get; set; }
        public string SName { get; set; }
        public string Sex { get; set; }
        public string SNationality { get; set; }
        public string Sbirthdate { get; set; }
        public string home { get; set; }
        public string temp { get; set; }
        public long acidNo { get; set; }

        public DataModel(int gIndex, string sName, string sex, string sNationality, string sbirthdate, string home, string temp)
        {
            this.gIndex = gIndex;
            SName = sName;
            Sex = sex;
            SNationality = sNationality;
            Sbirthdate = sbirthdate;
            this.home = home;
            this.temp = temp;
        }
        public DataModel() { }
    }
}
