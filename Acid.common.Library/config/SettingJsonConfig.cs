using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.common.Library.config
{
    public class SettingJsonConfig
    {
        public static void saveJson(object obj) 
        {
            string fp = "C:\\info.json";
            if (!File.Exists(fp))  // 判断是否已有相同文件 
            {
                FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
                fs1.Close();
            }
            File.WriteAllText(fp, JsonConvert.SerializeObject(obj));
        }
        public static void saveJson(object obj,string flie)
        {
            if (!File.Exists(flie))  // 判断是否已有相同文件 
            {
                FileStream fs1 = new FileStream(flie, FileMode.Create, FileAccess.ReadWrite);
                fs1.Close();
            }
            File.WriteAllText(flie, JsonConvert.SerializeObject(obj));
        }

        public static SettingModel readJson()
        {
            string fp = "C:\\info.json";
            if (!File.Exists(fp))  // 判断是否存在文件 
            {
                FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
                fs1.Close();
            }
            return JsonConvert.DeserializeObject<SettingModel>(File.ReadAllText(fp));  // 尖括号<>中填入对象的类名 
        }
        public static List<InfoListModel> readData(string flie)
        {
            if (!File.Exists(flie))  // 判断是否存在文件 
            {
                FileStream fs1 = new FileStream(flie, FileMode.Create, FileAccess.ReadWrite);
                fs1.Close();
            }
            return JsonConvert.DeserializeObject<List<InfoListModel>>(File.ReadAllText(flie));  // 尖括号<>中填入对象的类名 
        }
        public static void saveData(object obj)
        {
            string fp = "C:\\data.json";
            if (!File.Exists(fp))  // 判断是否已有相同文件 
            {
                FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
                fs1.Close();
            }
            File.WriteAllText(fp, JsonConvert.SerializeObject(obj));
        }

        public static List<InfoListModel> readData()
        {
            string fp = "C:\\data.json";
            if (!File.Exists(fp))  // 判断是否存在文件 
            {
                FileStream fs1 = new FileStream(fp, FileMode.Create, FileAccess.ReadWrite);
                fs1.Close();
            }
            return JsonConvert.DeserializeObject<List<InfoListModel>>(File.ReadAllText(fp));  // 尖括号<>中填入对象的类名 
        }
    }
}
