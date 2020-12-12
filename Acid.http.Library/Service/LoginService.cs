using Acid.common.Library.config;
using Acid.http.Library.RequestModel;
using Acid.http.Library.ResponseModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.http.Library.Service
{
   public class LoginService
    {

        public static ResultJson<LoginModel> Login(string username, string password)
        {
            try
            {
                string url = UrlModel.ip + UrlModel.login;
                RequestLoginModel requestLoginModel = new RequestLoginModel(username, password);
                string result = HttpUrlConfig.PostBody(url, requestLoginModel);
                ResultJson<LoginModel> retStu = JsonConvert.DeserializeObject<ResultJson<LoginModel>>(result);
                Logger.Default.Info(result);
                return retStu;
            }
            catch (Exception ex)
            {
                Logger.Default.Error(ex.Message);
                return null;
            }
        }
        public static ResultJson<LoginModel> Login_Ex(string username, string password, bool? rem = false, bool? auto = false)
        {
            ResultJson<LoginModel> resultJson = Login(username, password);
            if (resultJson == null)
            {
                return new ResultJson<LoginModel>() { code = "1", message = "无法连接远程服务器" };
            }
            else if (resultJson.code == "20000")
            {
                //存Token
                UrlModel.Token ="Bearer "+ resultJson.data.token;
                SettingModel json = SettingJsonConfig.readJson()??new SettingModel();
                if ((bool)rem)//保存
                {
                    json.userName = username;
                    json.passWord = password;
                    json.isRember = (bool)rem;
                    json.isAuto = (bool)auto;
                    SettingJsonConfig.saveJson(json);
                }
                else
                {
                    json.userName = "";
                    json.passWord = "";
                    json.isRember = false;
                    json.isAuto = false;
                    SettingJsonConfig.saveJson(json);
                }
                return resultJson;
            }
            else
            {
                return resultJson;
            }
        }
    }
}
