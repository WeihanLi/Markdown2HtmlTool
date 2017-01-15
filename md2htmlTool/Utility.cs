using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace md2htmlTool
{
    /// <summary>
    /// 工具类 
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// UrlEncode方法 
        /// </summary>
        /// <param name="str"> 要encode的参数 </param>
        /// <returns></returns>
        public static string UrlEncode(this String str)
        {
            return System.Web.HttpUtility.UrlEncode(str, Encoding.UTF8);
        }

        #region 判断网络连接状态

        private const int INTERNET_CONNECTION_MODEM = 1;
        private const int INTERNET_CONNECTION_LAN = 2;

        /// <summary>
        /// 判断网络连接状态 
        /// </summary>
        /// <param name="dwFlag">当前网络状态</param>
        /// <param name="dwReserved">保留参数，可设置为0</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("winInet.dll ")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);

        /// <summary>
        /// 获取网络连接状态
        /// </summary>
        /// <returns></returns>
        public static bool GetInternetConnectedStatus()
        {
            int status = 0;
            return InternetGetConnectedState(ref status, 0);
        }

        #endregion 判断网络连接状态

        /// <summary>
        /// 字符串重复扩展方法 
        /// </summary>
        /// <param name="str"> 要重复的字符串 </param>
        /// <param name="repeatCount"> 重复次数 </param>
        /// <returns></returns>
        public static string Multiple(this String str, int repeatCount)
        {
            if (String.IsNullOrEmpty(str) || repeatCount <= 0)
            {
                return "";
            }
            StringBuilder sbText = new StringBuilder();
            for (int i = 0; i < repeatCount; i++)
            {
                sbText.Append(str);
            }
            return sbText.ToString();
        }

        /// <summary>
        /// 生成目录菜单项 
        /// </summary>
        /// <param name="title"> 目录标题 </param>
        /// <param name="titleIndex"> 标题索引 </param>
        /// <returns></returns>
        public static string GenerateMenuItem(this String title, int[] titleIndex)
        {
            StringBuilder sbText = new StringBuilder();
            int i = 0;
            while (titleIndex[i] != 0)
            {
                sbText.Append(titleIndex[i]);
                sbText.Append('.');
                i++;
            }
            string itemIndex = sbText.ToString().Substring(0, sbText.Length - 1);
            sbText.Clear();
            sbText.AppendFormat("{0} {1}", itemIndex, title);
            return sbText.ToString();
        }

        /// <summary>
        /// POST请求 url
        /// 【自定义 UserAgent 复杂，需要特殊设置UA的慎用】
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求数据字节数组</param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<string> HttpPost(string url, byte[] postData)
        {
            using (HttpClient client = new HttpClient())
            {
                Uri uri = new Uri(url, UriKind.Absolute);
                HttpContent content = new ByteArrayContent(postData);
                var res = await client.PostAsync(uri, content);
                if (res.IsSuccessStatusCode)
                {
                    return await res.Content.ReadAsStringAsync();
                }
                else
                {
                    return res.ReasonPhrase;
                }
            }
        }

        /// <summary>
        /// POST请求 url
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">请求数据字节数组</param>
        /// <returns></returns>
        public static string DoHttpPost(string url, byte[] postData)
        {
            Uri uri = new Uri(url, UriKind.Absolute);
            HttpWebRequest request = WebRequest.CreateHttp(uri);
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";
            request.Method = "POST";
            var postStream = request.GetRequestStream();
            postStream.Write(postData, 0, postData.Length);
            postStream.Close();
            using (var response = request.GetResponse())
            {
                var responseSream = response.GetResponseStream();
                if (responseSream != null)
                {
                    //
                    using (StreamReader reader = new StreamReader(responseSream))
                    {
                        return reader.ReadToEnd();
                    }
                }
                else
                {
                    return "http error";
                }
            }
            
        }


        /// <summary>
        /// 对象json序列化
        /// </summary>
        /// <param name="obj">要序列化的对象</param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}