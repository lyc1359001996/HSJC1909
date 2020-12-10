using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
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
        public static string PostBody(string url, object obj)
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.Timeout = 10000;
            if (UrlModel.Token != "")
            {
                request.AddHeader("authorization", UrlModel.Token);
            }
            request.AddJsonBody(obj);
            var response = client.Execute<dynamic>(request);
            return response.Content;
            //ResultJson<data> retStu = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultJson<data>>(response.Content);
            //Console.WriteLine(retStu.data.token);
        }
        private static void call(IRestResponse<dynamic> res, RestRequestAsyncHandle e)
        {
            Console.WriteLine(res.Content);
            Console.WriteLine(e);
        }
        /// <summary>
        /// 表单
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static string PostForm(string url, object obj, string Token = "")
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddObject(obj);
            var response = client.Execute<dynamic>(request);
            return response.Content;
        }
        /// <summary>
        /// Query String
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static string PostQuery(string url, object obj, string Token = "")
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            List<Parameter> lists = ObjToParameter(obj);
            foreach (var item in lists)
            {
                request.AddParameter(item);
            }
            var response = client.Execute<dynamic>(request);
            return response.Content;
        }

        /// <summary>
        /// Query String
        /// </summary>
        /// <param name="url"></param>
        /// <param name="obj"></param>
        /// <param name="Token"></param>
        /// <returns></returns>
        public static string GetQuery(string url, object obj, string Token = "")
        {
            List<Parameter> lists = ObjToParameter(obj);
            url = url + "?";
            foreach (var item in lists)
            {
                url = url + item.Name + "=" + item.Value + "&";
            }
            url = url.Substring(0, url.Length - 1);
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("authorization", UrlModel.Token);
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
        public static string GetForm(string url, object obj, string Token = "")
        {
            var client = new RestClient(url);
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", Token);
            var response = client.Execute<dynamic>(request);
            return response.Content;
        }
        public static List<Parameter> ObjToParameter(object obj)
        {
            List<Parameter> parameter = new List<Parameter>();
            System.Reflection.PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
            foreach (PropertyInfo item in propertyInfos)
            {
                object v = item.GetValue(obj);
                if (v != null)
                {
                    Parameter parameter1 = new Parameter();
                    parameter1.Name = item.Name;
                    parameter1.Value = v;
                    parameter.Add(parameter1);
                }
            }
            return parameter;
        }
    }
}
