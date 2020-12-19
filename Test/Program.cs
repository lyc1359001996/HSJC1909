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
            List<string> list = new List<string>();
            Task.Run(() =>
            {
                int len = 0;
                for (int i = 0; i < 10000; i++)
                {
                    string v = UniqueData.Gener("");
                    if (list.Contains(v))
                    {
                        len += 1;
                    }
                    list.Add(v);
                    Console.WriteLine("一号："+v);
                }
                Console.WriteLine(len);
            });
            Console.ReadKey();
        }

        
    }
}
                                
