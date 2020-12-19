using Acid.common.Library.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var guid = Guid.NewGuid().ToString("N");
            //string v = string.Format("{0:D3}", guid);
            //Console.WriteLine(guid);
            List<InfoListModel> lists = SettingJsonConfig.readData() ?? new List<InfoListModel>();
            List<InfoListModel> listsWhere = lists.Where(u => u.cardNo == "330411199811190011").ToList();
            if (listsWhere.Count() > 0)
            {
                listsWhere.Reverse();
                Console.WriteLine(listsWhere[0].homeAddress);
                Console.WriteLine(listsWhere[0].company);
            }
            else
            {
                return;
            }
            Console.ReadKey();
        }

        
    }
}
                                
