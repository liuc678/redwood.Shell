using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace redwood.Shell
{
    public class FileDownloader
    {
        public static async Task DownloadFileAsync(string url, string localFilePath,string token)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", token);
                
                try
                {
                    // 发送GET请求
                    HttpResponseMessage response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode(); // 确保响应成功
                    
                    var contentType = response.Headers.GetValues("content-type").First();
                    if (contentType == "application/json")
                    {
                        string errmsg = await response.Content.ReadAsStringAsync();
                        throw new Exception("获取打印模板失败\n" + errmsg);
                    }

                    // 创建文件流
                    using (FileStream fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        // 将网络流复制到文件流
                        await response.Content.CopyToAsync(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"下载失败: {ex.Message}");
                    throw;
                }
            }
        }

        public static void DownloadFile(string url, string localFilePath, string token)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", token);
            using (WebResponse response = httpWebRequest.GetResponse())
            {
                var contentType = response.ContentType;
                if (contentType == "application/json"
                    || contentType.IndexOf("html")>0)
                {
                    Stream stream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);
                    string errmsg = streamReader.ReadToEnd();
                    throw new Exception("获取打印模板失败\n" + errmsg);
                }
                else
                {
                    Stream stream = response.GetResponseStream();
                    using (FileStream fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        stream.CopyTo(fileStream);
                        fileStream.Close();                    
                    }
                    //File.WriteAllText("D:\\123456.html", streamReader.ReadToEnd());                    
                }
            }
        }
    }
}
