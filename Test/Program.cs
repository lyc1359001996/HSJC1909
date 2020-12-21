using Acid.common.Library.config;
using Acid.http.Library.ResponseModel;
using Acid.http.Library.Service;
using Newtonsoft.Json;
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
            mock();
            Console.ReadKey();
        }

        private static void mock()
        {
            List<InfoListModel> lists = new List<InfoListModel>();
            for (int i = 0; i < 1; i++)
            {
                InfoListModel infoListModel = new InfoListModel()
                {
                    versions = 0,
                    acidNo = UniqueData.Gener(""),
                    address = "松花江",
                    cardNo = new Random().Next(1000000, 99999999).ToString(),
                    company = "松花江江山",
                    createTime = new DateTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    detectionName = "阿坝",
                    homeAddress = "松花江",
                    serialNumber = "",
                    sex = "0",
                    updateName = "阿坝",
                    userName = "阿坝"
                };
                lists.Add(infoListModel);
            }
            synchronousAdd(lists);
        }
        private static void synchronousAdd(List<InfoListModel> lists)
        {
            List<InfoListModel> newlist = lists.Where(u => u.versions == 0).ToList();
            string str = JsonConvert.SerializeObject(newlist);
            List<InfoListModel> lists1 = JsonConvert.DeserializeObject<List<InfoListModel>>(str);
            int count = lists1.Count();
            int page = count / 1000 + 1;
            ResultJson<string> resultJson = new ResultJson<string>() { code = "1" };
            for (int i = 1; i <= page; i++)
            {
                List<InfoListModel> data = lists1.Skip((i - 1) * 1000).Take(1000).ToList();
                resultJson = InfoListService.addNucleic(data);
            }
            if (resultJson.code == "20000")
            {
                foreach (var item in lists.Where(u => u.versions == 0).ToList())
                {
                    item.versions = 1;
                }
                SettingJsonConfig.saveData(lists);
                Console.WriteLine("新增:" + newlist.Count + "条数据");
            }
        }
    }
}

