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
    public class InfoListService
    {
        /// <summary>
        /// 初始化没有条件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static ResultJson<ResponseInfoListModel> getAll(int page,int size)
        {
            try
            {
                string url = UrlModel.ip + UrlModel.nucleic;
                RequestInfoListModel requestLoginModel = new RequestInfoListModel(page, size);
                string result = HttpUrlConfig.GetQuery(url, requestLoginModel);
                ResultJson<ResponseInfoListModel> retStu = JsonConvert.DeserializeObject<ResultJson<ResponseInfoListModel>>(result);
                Console.WriteLine(result);
                return retStu;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static ResultJson<ResponseInfoListModel> getQuery(RequestInfoListModel re)
        {
            try
            {
                string url = UrlModel.ip + UrlModel.nucleic;
                string result = HttpUrlConfig.GetQuery(url, re);
                ResultJson<ResponseInfoListModel> retStu = JsonConvert.DeserializeObject<ResultJson<ResponseInfoListModel>>(result);
                Console.WriteLine(result);
                return retStu;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 更新检测
        /// </summary>
        /// <param name="requestNucleic"></param>
        /// <returns></returns>
        public static ResultJson<string> updateNucleic(List<ResponseModel.InfoListModel> requestNucleic) 
        {
            try
            {
                string url = UrlModel.ip + UrlModel.nucleic_update;
                string result = HttpUrlConfig.PostBody(url, requestNucleic);
                ResultJson<string> retStu = JsonConvert.DeserializeObject<ResultJson<string>>(result);
                Console.WriteLine(result);
                return retStu;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResultJson<string>() {  code="1",message="修改失败，请稍后重试"};
            }
        }

        /// <summary>
        /// 新增检测
        /// </summary>
        /// <param name="requestNucleic"></param>
        /// <returns></returns>
        public static ResultJson<string> addNucleic(List<ResponseModel.InfoListModel> requestNucleic)
        {
            try
            {
                string url = UrlModel.ip + UrlModel.nucleic_add;
                string result = HttpUrlConfig.PostBody(url, requestNucleic);
                ResultJson<string> retStu = JsonConvert.DeserializeObject<ResultJson<string>>(result);
                Console.WriteLine(result);
                return retStu;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResultJson<string>() { code = "1", message = "新增失败，请稍后重试" };
            }
        }

        /// <summary>
        /// 删除检测
        /// </summary>
        /// <param name="requestNucleic"></param>
        /// <returns></returns>
        public static ResultJson<string> deleteNucleic(ResponseModel.InfoListModel requestNucleic)
        {
            try
            {
                string url = UrlModel.ip + UrlModel.nucleic_delete;
                string result = HttpUrlConfig.GetQuery(url, requestNucleic);
                ResultJson<string> retStu = JsonConvert.DeserializeObject<ResultJson<string>>(result);
                Console.WriteLine(result);
                return retStu;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new ResultJson<string>() { code = "1", message = "删除失败，请稍后重试" };
            }
        }

    }
}
