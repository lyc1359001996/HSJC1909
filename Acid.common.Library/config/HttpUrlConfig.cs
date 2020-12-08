using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Acid.common.Library.config
{
    public class HttpUrlConfig
    {
        /// <summary>
        /// json
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static string PostBody(string url, object obj, string Token="")
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(obj);
            var response = client.Execute<dynamic>(request);
            return response.Content;
            //ResultJson<data> retStu = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultJson<data>>(response.Content);
            //Console.WriteLine(retStu.data.token);
        }
        /// <summary>
        /// 表单
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static string PostForm(string url, object obj, string Token="")
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddXmlBody(obj);
            var response = client.Execute<dynamic>(request);
            return response.Content;
        }
        /// <summary>
        /// get
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static string GetForm(string url, object obj, string Token="")
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization",Token);
            var response = client.Execute<dynamic>(request);
            return response.Content;
        }
    }
}
